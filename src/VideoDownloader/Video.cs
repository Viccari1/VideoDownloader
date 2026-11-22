using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace VideoDownloader
{
    internal class Video : IDownloader
    {
        public string arquivo { get; private set; }
        private readonly string url;
        public string qualidade { get; set; }
        private readonly YoutubeClient youtube;
        private readonly string ffmpegPath = FFmpegHelper.GetFFmpegPath();

        // Cache para eliminar requisições repetidas
        private YoutubeExplode.Videos.Video _cachedVideo;
        private StreamManifest _cachedManifest;

        public Video(string url)
        {
            this.url = url;
            this.youtube = new YoutubeClient();
        }

        private static string TratarErroYoutube(Exception ex)
        {
            string erroCompleto = ex.ToString();
            if (erroCompleto.Contains("403") || erroCompleto.Contains("Forbidden"))
            {
                return "Erro 403 (Acesso Negado pelo YouTube):\n\n" +
                       "O YouTube restringiu temporariamente o acesso a este stream de mídia.\n" +
                       "Possíveis causas:\n" +
                       "• Restrição de idade ou conteúdo protegido por login/membros;\n" +
                       "• Bloqueio regional de exibição;\n" +
                       "• Excesso temporário de requisições do seu IP.\n\n" +
                       "Aguarde alguns minutos ou tente outro vídeo.";
            }

            if (ex is ArgumentException)
            {
                return "A URL do YouTube informada é inválida. Verifique o link e tente novamente.";
            }

            return ex.Message;
        }

        private static string RemoveInvalidChars(string nome)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                nome = nome.Replace(c, '_');
            }
            return string.IsNullOrWhiteSpace(nome) ? "video" : nome.Trim();
        }

        public async Task<(bool, string)> VerifyVideoOKAsync()
        {
            try
            {
                if (_cachedVideo == null)
                {
                    _cachedVideo = await youtube.Videos.GetAsync(url);
                }
                return (true, $"Vídeo encontrado: {_cachedVideo.Title}");
            }
            catch (Exception ex)
            {
                return (false, TratarErroYoutube(ex));
            }
        }

        public (bool, string) VerifyVideoOK()
        {
            return VerifyVideoOKAsync().GetAwaiter().GetResult();
        }

        public async Task<VideoDetails> ObterDetalhesAsync()
        {
            try
            {
                if (_cachedVideo == null)
                {
                    _cachedVideo = await youtube.Videos.GetAsync(url);
                }

                if (_cachedManifest == null)
                {
                    _cachedManifest = await youtube.Videos.Streams.GetManifestAsync(url);
                }

                var audioStream = _cachedManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                long audioBytes = audioStream?.Size.Bytes ?? 0;

                var detalhes = new VideoDetails
                {
                    VideoId = _cachedVideo.Id.Value,
                    Title = _cachedVideo.Title,
                    Author = _cachedVideo.Author.ChannelTitle,
                    Duration = _cachedVideo.Duration,
                    ThumbnailUrl = _cachedVideo.Thumbnails.GetWithHighestResolution().Url,
                    AudioOnlySizeBytes = audioBytes
                };

                // Agrupa streams de vídeo pela etiqueta de resolução e seleciona o maior bitrate de cada uma
                var streamsAgrupados = _cachedManifest.GetVideoOnlyStreams()
                    .GroupBy(s => s.VideoQuality.Label)
                    .Select(g => g.OrderByDescending(s => s.Bitrate).First())
                    .OrderByDescending(s => s.VideoQuality.MaxHeight)
                    .ToList();

                foreach (var s in streamsAgrupados)
                {
                    long totalBytes = s.Size.Bytes + audioBytes;
                    string peso = VideoDetails.FormatBytes(totalBytes);
                    string fps = s.VideoQuality.Framerate > 30 ? $" {s.VideoQuality.Framerate}fps" : "";

                    detalhes.Qualities.Add(new QualityOption
                    {
                        Label = s.VideoQuality.Label,
                        VideoSizeBytes = s.Size.Bytes,
                        AudioSizeBytes = audioBytes,
                        DisplayText = $"{s.VideoQuality.Label}{fps} (~{peso})"
                    });
                }

                return detalhes;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(TratarErroYoutube(ex), ex);
            }
        }

        public async Task<List<string>> GetQuality()
        {
            var detalhes = await ObterDetalhesAsync();
            return detalhes.Qualities.Select(q => q.Label).ToList();
        }

        private static string ExistFile(string tituloVideo)
        {
            string pasta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Baixadas");
            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            string nomeLimpo = RemoveInvalidChars(tituloVideo);
            string nomeFinal = nomeLimpo;
            int i = 1;

            while (true)
            {
                string path = Path.Combine(pasta, nomeFinal + ".mp4");
                if (!File.Exists(path))
                {
                    return path;
                }

                nomeFinal = $"{nomeLimpo} ({i})";
                i++;
            }
        }

        private (IStreamInfo, IStreamInfo) GetStreams(StreamManifest manifest)
        {
            var audioStreamInfo = manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            IStreamInfo videoStreamInfo = manifest.GetVideoOnlyStreams()
                                      .Where(s => s.VideoQuality.Label == qualidade)
                                      .OrderByDescending(s => s.Bitrate)
                                      .FirstOrDefault();

            if (videoStreamInfo == null)
            {
                // Fallback para maior resolução disponível se a selecionada não for encontrada
                videoStreamInfo = manifest.GetVideoOnlyStreams()
                                          .OrderByDescending(s => s.VideoQuality.MaxHeight)
                                          .FirstOrDefault();
            }

            return (videoStreamInfo, audioStreamInfo);
        }

        public async Task Download(Logger logger)
        {
            try
            {
                logger.Log(5, "Carregando informações...");
                if (_cachedVideo == null)
                {
                    _cachedVideo = await youtube.Videos.GetAsync(url);
                }

                if (_cachedManifest == null)
                {
                    _cachedManifest = await youtube.Videos.Streams.GetManifestAsync(url);
                }

                arquivo = ExistFile(_cachedVideo.Title);
                logger.Log(10, $"Arquivo de destino: {Path.GetFileName(arquivo)}");

                var (videoStream, audioStream) = GetStreams(_cachedManifest);
                if (videoStream == null || audioStream == null)
                {
                    throw new InvalidOperationException("Não foi possível encontrar streams compatíveis para a qualidade selecionada.");
                }

                string videoPath = Path.Combine(Path.GetTempPath(), $"vid_{Guid.NewGuid():N}.mp4");
                string audioPath = Path.Combine(Path.GetTempPath(), $"aud_{Guid.NewGuid():N}.mp3");

                try
                {
                    // Progresso contínuo real do download do vídeo (10% a 60%)
                    var videoProgress = new Progress<double>(p =>
                    {
                        int percent = 10 + (int)(p * 50);
                        logger.Log(percent, $"Baixando vídeo: {p:P0}...");
                    });
                    await youtube.Videos.Streams.DownloadAsync(videoStream, videoPath, videoProgress);

                    // Progresso contínuo real do download do áudio (60% a 85%)
                    var audioProgress = new Progress<double>(p =>
                    {
                        int percent = 60 + (int)(p * 25);
                        logger.Log(percent, $"Baixando áudio: {p:P0}...");
                    });
                    await youtube.Videos.Streams.DownloadAsync(audioStream, audioPath, audioProgress);

                    logger.Log(88, "Mesclando vídeo e áudio via FFmpeg...");
                    string args = $"-i \"{videoPath}\" -i \"{audioPath}\" -c:v copy -c:a aac \"{arquivo}\"";

                    using (var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = ffmpegPath,
                            Arguments = args,
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                    })
                    {
                        process.Start();
                        process.WaitForExit();

                        if (process.ExitCode != 0)
                        {
                            throw new InvalidOperationException("Erro ao mesclar vídeo e áudio através do FFmpeg.");
                        }
                    }

                    logger.Log(100, "Download concluído com sucesso!");
                }
                finally
                {
                    if (File.Exists(videoPath)) File.Delete(videoPath);
                    if (File.Exists(audioPath)) File.Delete(audioPath);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(TratarErroYoutube(ex), ex);
            }
        }
    }
}
