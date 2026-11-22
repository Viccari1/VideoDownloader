using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public string qualidade { get; set; }

        private readonly string url;
        private readonly YoutubeClient youtube;

        // Cache local para eliminar requisições redundantes à API do YouTube
        private YoutubeExplode.Videos.Video _cachedVideo;
        private StreamManifest _cachedManifest;

        public Video(string url)
        {
            this.url = url;
            this.youtube = YoutubeHelper.Client;
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
                return (false, YoutubeErrorHelper.TratarExcecao(ex));
            }
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
                throw new InvalidOperationException(YoutubeErrorHelper.TratarExcecao(ex), ex);
            }
        }

        private (IStreamInfo, IStreamInfo) ObterStreamsCorrespondentes(StreamManifest manifest)
        {
            var audioStream = manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            IStreamInfo videoStream = manifest.GetVideoOnlyStreams()
                .Where(s => s.VideoQuality.Label == qualidade)
                .OrderByDescending(s => s.Bitrate)
                .FirstOrDefault();

            // Fallback para maior resolução se a qualidade solicitada não existir no manifesto
            if (videoStream == null)
            {
                videoStream = manifest.GetVideoOnlyStreams()
                    .OrderByDescending(s => s.VideoQuality.MaxHeight)
                    .FirstOrDefault();
            }

            return (videoStream, audioStream);
        }

        public async Task Download(Logger logger)
        {
            try
            {
                logger.Log(5, "Carregando informações do vídeo...");
                if (_cachedVideo == null)
                {
                    _cachedVideo = await youtube.Videos.GetAsync(url);
                }

                if (_cachedManifest == null)
                {
                    _cachedManifest = await youtube.Videos.Streams.GetManifestAsync(url);
                }

                arquivo = FileHelper.ObterCaminhoDisponivel(FileHelper.ObterPastaVideos(), _cachedVideo.Title, ".mp4");
                logger.Log(10, $"Arquivo de destino: {Path.GetFileName(arquivo)}");

                var (videoStream, audioStream) = ObterStreamsCorrespondentes(_cachedManifest);
                if (videoStream == null || audioStream == null)
                {
                    throw new InvalidOperationException("Não foi possível encontrar streams compatíveis para a qualidade selecionada.");
                }

                string videoExt = videoStream.Container.Name;
                string audioExt = audioStream.Container.Name;
                string videoTemp = Path.Combine(Path.GetTempPath(), $"vid_{Guid.NewGuid():N}.{videoExt}");
                string audioTemp = Path.Combine(Path.GetTempPath(), $"aud_{Guid.NewGuid():N}.{audioExt}");

                try
                {
                    // Progresso do download do stream de vídeo (10% a 60%)
                    var videoProgress = new Progress<double>(p =>
                    {
                        int percent = 10 + (int)(p * 50);
                        logger.Log(percent, $"Baixando vídeo: {p:P0}...");
                    });
                    await youtube.Videos.Streams.DownloadAsync(videoStream, videoTemp, videoProgress);

                    // Progresso do download do stream de áudio (60% a 85%)
                    var audioProgress = new Progress<double>(p =>
                    {
                        int percent = 60 + (int)(p * 25);
                        logger.Log(percent, $"Baixando áudio: {p:P0}...");
                    });
                    await youtube.Videos.Streams.DownloadAsync(audioStream, audioTemp, audioProgress);

                    logger.Log(88, "Mesclando vídeo e áudio via FFmpeg...");
                    await FFmpegHelper.MesclarAudioVideoAsync(videoTemp, audioTemp, arquivo);

                    logger.Log(100, "Download concluído com sucesso!");
                }
                finally
                {
                    if (File.Exists(videoTemp)) File.Delete(videoTemp);
                    if (File.Exists(audioTemp)) File.Delete(audioTemp);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(YoutubeErrorHelper.TratarExcecao(ex), ex);
            }
        }
    }
}
