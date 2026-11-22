using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace VideoDownloader
{
    internal class Audio : IDownloader
    {
        public string arquivo { get; private set; }

        private readonly string url;
        private readonly YoutubeClient youtube;

        // Cache local para evitar requisições duplicadas
        private YoutubeExplode.Videos.Video _cachedVideo;

        public Audio(string url)
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

        public async Task Download(Logger logger)
        {
            try
            {
                logger.Log(10, "Buscando dados do áudio...");
                if (_cachedVideo == null)
                {
                    _cachedVideo = await youtube.Videos.GetAsync(url);
                }

                logger.Log(15, "Obtendo streams de áudio de alta resolução...");
                var manifest = await youtube.Videos.Streams.GetManifestAsync(_cachedVideo.Id);
                var audioStream = manifest.GetAudioOnlyStreams().GetWithHighestBitrate();

                if (audioStream == null)
                {
                    throw new InvalidOperationException("Não foi possível encontrar streams de áudio disponíveis para este vídeo.");
                }

                arquivo = FileHelper.ObterCaminhoDisponivel(FileHelper.ObterPastaMusicas(), _cachedVideo.Title, ".mp3");
                logger.Log(20, $"Arquivo de destino: {Path.GetFileName(arquivo)}");

                string audioExt = audioStream.Container.Name;
                string tempAudio = Path.Combine(Path.GetTempPath(), $"aud_{Guid.NewGuid():N}.{audioExt}");

                try
                {
                    // Progresso do download direto do fluxo de áudio original do YouTube (20% a 85%)
                    var progress = new Progress<double>(p =>
                    {
                        int percent = 20 + (int)(p * 65);
                        logger.Log(percent, $"Baixando áudio: {p:P0}...");
                    });

                    await youtube.Videos.Streams.DownloadAsync(audioStream, tempAudio, progress);

                    logger.Log(88, "Convertendo áudio para MP3 (320 kbps Alta Fidelidade)...");
                    await FFmpegHelper.ConverterParaMp3Async(tempAudio, arquivo);

                    logger.Log(100, "Áudio baixado e convertido com sucesso!");
                }
                finally
                {
                    if (File.Exists(tempAudio))
                    {
                        File.Delete(tempAudio);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(YoutubeErrorHelper.TratarExcecao(ex), ex);
            }
        }
    }
}
