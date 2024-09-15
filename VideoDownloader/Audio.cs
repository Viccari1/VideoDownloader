using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

namespace VideoDownloader
{
    internal class Audio
    {
        string NOME = "arquivo";
        public string arquivo;
        string url;
        YoutubeClient youtube;
        string ffmpegPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe");
        public Audio(string url)
        {
            this.url = url;
            youtube = new YoutubeClient();
        }
        private bool TryFindVideo()
        {
            try
            {
                youtube.Videos.GetAsync(url);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        public (bool, string) VerifyVideoOK()
        {
            if (TryFindVideo())
            {
                return (true, "Video encontrado");
            }
            else
            {
                return (false, "Video não encontrado, verifique se o link está correto!");
            }
        }
        private string ExistFile()
        {
            string path = "";
            int i = 1;
            string nome = NOME;
            while (true)
            {
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Baixadas\\" + nome + ".mp3");

                if (File.Exists(path) == false)
                    return path;
                else
                {
                    nome = NOME + Convert.ToString(i);
                    i++;
                }
            }
        }

        private IStreamInfo GetStreamAudio(StreamManifest manifest)
        {
            return manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
        }

        public async Task Download(IProgress<int> progress)
        {
            progress.Report(30);
            var video = await youtube.Videos.GetAsync(url);
            var manifest = await youtube.Videos.Streams.GetManifestAsync(url);
            arquivo = ExistFile();
            var audioStream = GetStreamAudio(manifest);
            progress.Report(60);

            try
            {
                await youtube.Videos.DownloadAsync(url, arquivo, o => o
                                         .SetContainer("mp3")
                                         .SetPreset(ConversionPreset.Fast)
                                         .SetFFmpegPath(ffmpegPath));
                progress.Report(100);
            }
            catch (Exception)
            {
                throw new Exception("Erro ao baixar áudio.");
            }
        }
    }
}
