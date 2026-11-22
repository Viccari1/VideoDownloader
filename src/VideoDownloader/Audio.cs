using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

namespace VideoDownloader
{
    internal class Audio : IDownloader
    {
        public string arquivo { get; private set; }
        string url;
        YoutubeClient youtube;
        string ffmpegPath = FFmpegHelper.GetFFmpegPath();

        public Audio(string url)
        {
            this.url = url;
            youtube = new YoutubeClient();
        }

        private string RemoveInvalidChars(string nome)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                nome = nome.Replace(c, '_');
            }
            return string.IsNullOrWhiteSpace(nome) ? "audio" : nome.Trim();
        }

        public async Task<(bool, string)> VerifyVideoOKAsync()
        {
            try
            {
                var video = await youtube.Videos.GetAsync(url);
                return (true, $"Vídeo encontrado: {video.Title}");
            }
            catch (ArgumentException)
            {
                return (false, "Link inválido, verifique se a URL está correta!");
            }
            catch (Exception ex)
            {
                return (false, $"Vídeo indisponível ou não encontrado: {ex.Message}");
            }
        }

        public (bool, string) VerifyVideoOK()
        {
            return VerifyVideoOKAsync().GetAwaiter().GetResult();
        }

        private string ExistFile(string tituloAudio)
        {
            string pasta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Baixadas");
            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            string nomeLimpo = RemoveInvalidChars(tituloAudio);
            string nomeFinal = nomeLimpo;
            int i = 1;

            while (true)
            {
                string path = Path.Combine(pasta, nomeFinal + ".mp3");
                if (!File.Exists(path))
                {
                    return path;
                }

                nomeFinal = $"{nomeLimpo} ({i})";
                i++;
            }
        }

        private IStreamInfo GetStreamAudio(StreamManifest manifest)
        {
            return manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
        }

        public async Task Download(Logger logger)
        {
            logger.Log(30, "Buscando dados do áudio...");
            var video = await youtube.Videos.GetAsync(url);
            var manifest = await youtube.Videos.Streams.GetManifestAsync(url);
            arquivo = ExistFile(video.Title);
            var audioStream = GetStreamAudio(manifest);
            logger.Log(60, $"Baixando áudio: {Path.GetFileName(arquivo)}...");

            try
            {
                await youtube.Videos.DownloadAsync(url, arquivo, o => o
                                         .SetContainer("mp3")
                                         .SetPreset(ConversionPreset.Fast)
                                         .SetFFmpegPath(ffmpegPath));
                logger.Log(100, "Audio baixado!");
            }
            catch (Exception)
            {
                throw new Exception("Erro ao baixar áudio.");
            }
        }
    }
}
