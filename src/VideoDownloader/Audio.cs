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
        private readonly string url;
        private readonly YoutubeClient youtube;
        private readonly string ffmpegPath = FFmpegHelper.GetFFmpegPath();

        // Cache para evitar requisições duplicadas
        private YoutubeExplode.Videos.Video _cachedVideo;

        public Audio(string url)
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
                       "O YouTube restringiu temporariamente o acesso a este stream de áudio.\n" +
                       "Possíveis causas:\n" +
                       "• Restrição de direitos autorais ou restrição de idade;\n" +
                       "• Bloqueio regional de exibição;\n" +
                       "• Excesso temporário de requisições do seu IP.\n\n" +
                       "Aguarde alguns minutos ou tente outro vídeo.";
            }

            if (ex is ArgumentException)
            {
                return "A URL informada é inválida. Verifique o link e tente novamente.";
            }

            return ex.Message;
        }

        private static string RemoveInvalidChars(string nome)
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

        private static string ExistFile(string tituloAudio)
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

        public async Task Download(Logger logger)
        {
            try
            {
                logger.Log(10, "Buscando dados do áudio...");
                if (_cachedVideo == null)
                {
                    _cachedVideo = await youtube.Videos.GetAsync(url);
                }

                arquivo = ExistFile(_cachedVideo.Title);
                logger.Log(15, $"Preparando arquivo: {Path.GetFileName(arquivo)}");

                var progress = new Progress<double>(p =>
                {
                    int percent = 15 + (int)(p * 80);
                    logger.Log(percent, $"Baixando e convertendo áudio: {p:P0}...");
                });

                await youtube.Videos.DownloadAsync(url, arquivo, o => o
                    .SetContainer("mp3")
                    .SetPreset(ConversionPreset.Fast)
                    .SetFFmpegPath(ffmpegPath),
                    progress);

                logger.Log(100, "Áudio baixado com sucesso!");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(TratarErroYoutube(ex), ex);
            }
        }
    }
}
