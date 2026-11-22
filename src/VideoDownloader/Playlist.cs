using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos.Streams;

namespace VideoDownloader
{
    internal class Playlist
    {
        private readonly string url;
        private readonly YoutubeClient youtube;

        public string pasta { get; private set; }
        public string fullDir { get; private set; }
        public bool qualidadeAlta { get; set; } = true;

        // Cache para evitar requisições repetidas
        private YoutubeExplode.Playlists.Playlist _cachedPlaylist;
        private IReadOnlyList<YoutubeExplode.Playlists.PlaylistVideo> _cachedVideos;

        public Playlist(string url)
        {
            this.url = url;
            this.youtube = YoutubeHelper.Client;
        }

        public async Task<PlaylistDetails> ObterDetalhesPlaylistAsync()
        {
            try
            {
                if (_cachedPlaylist == null)
                {
                    _cachedPlaylist = await youtube.Playlists.GetAsync(url);
                }
                if (_cachedVideos == null)
                {
                    _cachedVideos = await youtube.Playlists.GetVideosAsync(url);
                }

                string firstThumb = null;
                string firstId = null;

                if (_cachedVideos != null && _cachedVideos.Count > 0)
                {
                    var primeiro = _cachedVideos[0];
                    firstId = primeiro.Id.Value;
                    firstThumb = primeiro.Thumbnails.GetWithHighestResolution().Url;
                }
                else if (_cachedPlaylist.Thumbnails != null && _cachedPlaylist.Thumbnails.Count > 0)
                {
                    firstThumb = _cachedPlaylist.Thumbnails.GetWithHighestResolution().Url;
                }

                return new PlaylistDetails
                {
                    Title = _cachedPlaylist.Title,
                    Author = _cachedPlaylist.Author?.ChannelTitle ?? "Desconhecido",
                    TotalVideos = _cachedVideos?.Count ?? 0,
                    ThumbnailUrl = firstThumb,
                    FirstVideoId = firstId
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(YoutubeErrorHelper.TratarExcecao(ex), ex);
            }
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
                return (false, $"Não foi possível carregar a playlist: {YoutubeErrorHelper.TratarExcecao(ex)}");
            }
        }

        private (IStreamInfo, IStreamInfo) ObterStreamsVideo(StreamManifest manifest, bool altaQualidade)
        {
            var audioStream = manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            IStreamInfo videoStream;

            if (altaQualidade)
            {
                videoStream = manifest.GetVideoOnlyStreams()
                    .Where(s => s.VideoQuality.Label != "360p")
                    .OrderByDescending(s => s.Bitrate)
                    .FirstOrDefault()
                    ?? manifest.GetVideoOnlyStreams().OrderByDescending(s => s.VideoQuality.MaxHeight).FirstOrDefault();
            }
            else
            {
                videoStream = manifest.GetVideoOnlyStreams()
                    .Where(s => s.VideoQuality.Label == "360p")
                    .OrderByDescending(s => s.Bitrate)
                    .FirstOrDefault()
                    ?? manifest.GetVideoOnlyStreams().OrderBy(s => s.VideoQuality.MaxHeight).FirstOrDefault();
            }

            return (videoStream, audioStream);
        }

        public async Task DownloadVideos(Logger logger)
        {
            logger.Log(5, "Buscando playlist...");
            if (_cachedPlaylist == null)
            {
                _cachedPlaylist = await youtube.Playlists.GetAsync(url);
            }
            if (_cachedVideos == null)
            {
                _cachedVideos = await youtube.Playlists.GetVideosAsync(url);
            }

            pasta = FileHelper.SanitizarNomeArquivo(_cachedPlaylist.Title, "Playlist");
            string destinoPasta = Path.Combine(FileHelper.ObterPastaVideos(), pasta);
            if (!Directory.Exists(destinoPasta))
            {
                Directory.CreateDirectory(destinoPasta);
            }
            this.fullDir = destinoPasta;

            int total = _cachedVideos.Count;
            int i = 0;
            int baixados = 0;
            int falhas = 0;

            foreach (var video in _cachedVideos)
            {
                i++;
                int percentualBase = (int)(((double)i / total) * 100);

                try
                {
                    logger.Log(percentualBase, $"Processando ({i}/{total}): {video.Title}...");

                    var manifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                    var (videoStream, audioStream) = ObterStreamsVideo(manifest, qualidadeAlta);

                    if (videoStream == null || audioStream == null)
                    {
                        throw new InvalidOperationException("Streams de mídia indisponíveis para este vídeo.");
                    }

                    string arquivoFinal = FileHelper.ObterCaminhoDisponivel(destinoPasta, video.Title, ".mp4");
                    string videoExt = videoStream.Container.Name;
                    string audioExt = audioStream.Container.Name;
                    string videoTemp = Path.Combine(Path.GetTempPath(), $"vid_{Guid.NewGuid():N}.{videoExt}");
                    string audioTemp = Path.Combine(Path.GetTempPath(), $"aud_{Guid.NewGuid():N}.{audioExt}");

                    try
                    {
                        await youtube.Videos.Streams.DownloadAsync(videoStream, videoTemp);
                        await youtube.Videos.Streams.DownloadAsync(audioStream, audioTemp);

                        await FFmpegHelper.MesclarAudioVideoAsync(videoTemp, audioTemp, arquivoFinal);
                        baixados++;
                    }
                    finally
                    {
                        if (File.Exists(videoTemp)) File.Delete(videoTemp);
                        if (File.Exists(audioTemp)) File.Delete(audioTemp);
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
            if (_cachedPlaylist == null)
            {
                _cachedPlaylist = await youtube.Playlists.GetAsync(url);
            }
            if (_cachedVideos == null)
            {
                _cachedVideos = await youtube.Playlists.GetVideosAsync(url);
            }

            pasta = FileHelper.SanitizarNomeArquivo(_cachedPlaylist.Title, "Playlist");
            string destinoPasta = Path.Combine(FileHelper.ObterPastaMusicas(), pasta);
            if (!Directory.Exists(destinoPasta))
            {
                Directory.CreateDirectory(destinoPasta);
            }
            this.fullDir = destinoPasta;

            int total = _cachedVideos.Count;
            int i = 0;
            int baixados = 0;
            int falhas = 0;

            foreach (var video in _cachedVideos)
            {
                i++;
                int percentualBase = (int)(((double)i / total) * 100);

                try
                {
                    logger.Log(percentualBase, $"Baixando áudio ({i}/{total}): {video.Title}...");

                    var manifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                    var audioStream = manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                    if (audioStream == null)
                    {
                        throw new InvalidOperationException("Stream de áudio indisponível para este vídeo.");
                    }

                    string arquivoFinal = FileHelper.ObterCaminhoDisponivel(destinoPasta, video.Title, ".mp3");
                    string audioExt = audioStream.Container.Name;
                    string audioTemp = Path.Combine(Path.GetTempPath(), $"aud_{Guid.NewGuid():N}.{audioExt}");

                    try
                    {
                        await youtube.Videos.Streams.DownloadAsync(audioStream, audioTemp);
                        await FFmpegHelper.ConverterParaMp3Async(audioTemp, arquivoFinal);
                        baixados++;
                    }
                    finally
                    {
                        if (File.Exists(audioTemp))
                        {
                            File.Delete(audioTemp);
                        }
                    }
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
