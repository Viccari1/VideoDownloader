using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using static System.Net.Mime.MediaTypeNames;

namespace VideoDownloader
{
    internal class Video : IDownloader
    {
        public string arquivo { get; private set; }
        string url;
        public string qualidade { get; set; }
        YoutubeClient youtube;
        string ffmpegPath = FFmpegHelper.GetFFmpegPath();

        public Video(string url)
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
            return string.IsNullOrWhiteSpace(nome) ? "video" : nome.Trim();
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

        private string ExistFile(string tituloVideo)
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
            var videoStreamInfo = manifest.GetVideoOnlyStreams()
                                      .Where(s => s.VideoQuality.Label == qualidade)
                                      .OrderByDescending(s => s.Bitrate)
                                      .FirstOrDefault();
            return (videoStreamInfo, audioStreamInfo);
        }
        public async Task<List<String>> GetQuality()
        {
            var manifest = await youtube.Videos.Streams.GetManifestAsync(url);
            var stream = manifest.GetVideoOnlyStreams().ToList();
            var quality = new List<String>();
            for (var i = 0; i < stream.Count; i++)
            {
                var label = stream[i].VideoQuality.Label;
                if (quality.Contains(label))
                    continue;

                quality.Add(label);
            }
            return quality;
        }

        public async Task Download(Logger logger)
        {
            logger.Log(20, "Obtendo dados do vídeo...");
            var video = await youtube.Videos.GetAsync(url);
            var manifest = await youtube.Videos.Streams.GetManifestAsync(url);
            arquivo = ExistFile(video.Title);
            logger.Log(40, $"Preparando arquivo: {Path.GetFileName(arquivo)}...");

            // a verificação de qualidade vai aqui =====================

            var (videoStream, audioStream) = GetStreams(manifest);
            string videoPath = Path.Combine(Path.GetTempPath(), "video.mp4");
            string audioPath = Path.Combine(Path.GetTempPath(), "audio.mp3");
            logger.Log(60, "Baixando stream do video...");
            await youtube.Videos.Streams.DownloadAsync(videoStream, videoPath);
            logger.Log(70, "Baixando stream do audio...");
            await youtube.Videos.Streams.DownloadAsync(audioStream, audioPath);
            logger.Log(80, "Criando processo de união...");

            string args = $"-i \"{videoPath}\" -i \"{audioPath}\" -c:v copy -c:a aac \"{arquivo}\"";

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = args,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            logger.Log(90, "Unindo video e audio...");
            process.Start();
            process.WaitForExit();

            // Limpar arquivos temporários
            logger.Log(95, "Deletando arquivos temporários...");
            File.Delete(videoPath);
            File.Delete(audioPath);

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException("Erro ao mesclar vídeo e áudio.");
            }
            logger.Log(100, "Pronto!");
        }
    }
}
