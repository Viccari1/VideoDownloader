using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

namespace VideoDownloader
{
    internal class Playlist
    {
        string url;
        public string pasta;
        YoutubeClient youtube;
        string ffmpegPath = FFmpegHelper.GetFFmpegPath();
        public bool qualidadeAlta = false;
        public string fullDir;

        public Playlist(string url)
        {
            this.url = url;
            youtube = new YoutubeClient();
        }

        private string ExistFile(string basePath, string nome, string ext)
        {
            string nomeLimpo = RemoveInvalidChars(nome);
            string nomeFinal = nomeLimpo;
            int i = 1;

            while (true)
            {
                string path = Path.Combine(basePath, nomeFinal + ext);
                if (!File.Exists(path))
                {
                    return path;
                }

                nomeFinal = $"{nomeLimpo} ({i})";
                i++;
            }
        }

        private void CreateDirVideo(string dir)
        {
            pasta = RemoveInvalidChars(dir);
            string videoPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Baixadas", pasta);

            if (!Directory.Exists(videoPath))
            {
                Directory.CreateDirectory(videoPath);
            }
        }

        private void CreateDirAudio(string dir)
        {
            pasta = RemoveInvalidChars(dir);
            string musicPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Baixadas", pasta);

            if (!Directory.Exists(musicPath))
            {
                Directory.CreateDirectory(musicPath);
            }
        }

        private string RemoveInvalidChars(string nome)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                nome = nome.Replace(c, '_');
            }
            return string.IsNullOrWhiteSpace(nome) ? "item" : nome.Trim();
        }

        public async Task<(bool, string)> VerifyPlaylistOKAsync()
        {
            try
            {
                var playlist = await youtube.Playlists.GetAsync(url);
                return (true, $"Playlist encontrada: {playlist.Title}");
            }
            catch (ArgumentException)
            {
                return (false, "Link de playlist inválido, verifique o formato da URL.");
            }
            catch (Exception ex)
            {
                return (false, $"Não foi possível carregar a playlist: {ex.Message}");
            }
        }

        public (bool, string) VerifyPlaylistOK()
        {
            return VerifyPlaylistOKAsync().GetAwaiter().GetResult();
        }
        private (IStreamInfo, IStreamInfo) GetStreamsVideoHigh(StreamManifest manifest)
        {
            var audioStreamInfo = manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            var videoStreamInfo = manifest.GetVideoOnlyStreams()
                                      .Where(s => s.VideoQuality.Label != "360p")
                                      .OrderByDescending(s => s.Bitrate)
                                      .FirstOrDefault();
            return (videoStreamInfo, audioStreamInfo);
        }
        private (IStreamInfo, IStreamInfo) GetStreamsVideoLow(StreamManifest manifest)
        {
            var audioStreamInfo = manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            var videoStreamInfo = manifest.GetVideoOnlyStreams()
                                      .Where(s => s.VideoQuality.Label == "360p")
                                      .OrderByDescending(s => s.Bitrate)
                                      .FirstOrDefault();
            return (videoStreamInfo, audioStreamInfo);
        }
        public async Task DownloadVideos(Logger logger)
        {
            logger.Log(5, "Buscando playlist...");
            var playlist = await youtube.Playlists.GetAsync(url);
            var videos = await youtube.Playlists.GetVideosAsync(url);
            string tituloPlaylist = playlist.Title;
            CreateDirVideo(tituloPlaylist);

            var destinoPasta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Baixadas", pasta);
            this.fullDir = destinoPasta;

            int total = videos.Count;
            int i = 0;
            int baixados = 0;
            int falhas = 0;

            foreach (var video in videos)
            {
                i++;
                int percentualBase = (int)(((double)i / total) * 100);

                try
                {
                    logger.Log(percentualBase, $"Processando ({i}/{total}): {video.Title}...");

                    var manifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                    IStreamInfo videoStream, audioStream;

                    if (qualidadeAlta)
                    {
                        (videoStream, audioStream) = GetStreamsVideoHigh(manifest);
                    }
                    else
                    {
                        (videoStream, audioStream) = GetStreamsVideoLow(manifest);
                    }

                    if (videoStream == null || audioStream == null)
                    {
                        throw new InvalidOperationException("Streams de vídeo/áudio não encontrados para este item.");
                    }

                    string arquivoFinal = ExistFile(destinoPasta, video.Title, ".mp4");
                    string videoPath = Path.Combine(Path.GetTempPath(), $"vid_{Guid.NewGuid():N}.mp4");
                    string audioPath = Path.Combine(Path.GetTempPath(), $"aud_{Guid.NewGuid():N}.mp3");

                    try
                    {
                        await youtube.Videos.Streams.DownloadAsync(videoStream, videoPath);
                        await youtube.Videos.Streams.DownloadAsync(audioStream, audioPath);

                        string args = $"-i \"{videoPath}\" -i \"{audioPath}\" -c:v copy -c:a aac \"{arquivoFinal}\"";
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
                                throw new InvalidOperationException("Erro ao mesclar vídeo e áudio via FFmpeg.");
                            }
                        }

                        baixados++;
                    }
                    finally
                    {
                        if (File.Exists(videoPath)) File.Delete(videoPath);
                        if (File.Exists(audioPath)) File.Delete(audioPath);
                    }
                }
                catch (Exception ex)
                {
                    falhas++;
                    logger.Log(percentualBase, $"[Aviso] Falha no item {i} ('{video.Title}'): {ex.Message}. Continuando...");
                    await Task.Delay(1000);
                }
            }

            if (falhas > 0)
            {
                logger.Log(100, $"Concluído! {baixados} de {total} vídeos baixados ({falhas} com erro/indisponíveis).");
            }
            else
            {
                logger.Log(100, $"Concluído! Todos os {baixados} vídeos foram baixados com sucesso.");
            }
        }

        public async Task DownloadAudios(Logger logger)
        {
            logger.Log(5, "Buscando playlist...");
            var playlist = await youtube.Playlists.GetAsync(url);
            var videos = await youtube.Playlists.GetVideosAsync(url);
            string tituloPlaylist = playlist.Title;
            CreateDirAudio(tituloPlaylist);

            var destinoPasta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Baixadas", pasta);
            this.fullDir = destinoPasta;

            int total = videos.Count;
            int i = 0;
            int baixados = 0;
            int falhas = 0;

            foreach (var video in videos)
            {
                i++;
                int percentualBase = (int)(((double)i / total) * 100);

                try
                {
                    logger.Log(percentualBase, $"Baixando áudio ({i}/{total}): {video.Title}...");
                    string arquivoFinal = ExistFile(destinoPasta, video.Title, ".mp3");

                    await youtube.Videos.DownloadAsync(video.Url, arquivoFinal, o => o
                        .SetContainer("mp3")
                        .SetPreset(ConversionPreset.Fast)
                        .SetFFmpegPath(ffmpegPath));

                    baixados++;
                }
                catch (Exception ex)
                {
                    falhas++;
                    logger.Log(percentualBase, $"[Aviso] Falha no áudio {i} ('{video.Title}'): {ex.Message}. Continuando...");
                    await Task.Delay(1000);
                }
            }

            if (falhas > 0)
            {
                logger.Log(100, $"Concluído! {baixados} de {total} áudios baixados ({falhas} com erro/indisponíveis).");
            }
            else
            {
                logger.Log(100, $"Concluído! Todos os {baixados} áudios foram baixados com sucesso.");
            }
        }
    }
}