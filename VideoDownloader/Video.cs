using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using static System.Net.Mime.MediaTypeNames;

namespace VideoDownloader
{
    internal class Video
    {
        string NOME = "arquivo";
        public string arquivo;
        string url;
        YoutubeClient youtube;
        string ffmpegPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe");

        public Video(string url)
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
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Baixadas\\" + nome + ".mp4");

                if (File.Exists(path) == false)
                    return path;
                else
                {
                    nome = NOME + Convert.ToString(i);
                    i++;
                }
            }
        }

        private (IStreamInfo, IStreamInfo) GetStreamsHigh(StreamManifest manifest)
        {
            var audioStreamInfo = manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            var videoStreamInfo = manifest.GetVideoOnlyStreams()
                                      .Where(s => s.VideoQuality.Label != "360p")
                                      .OrderByDescending(s => s.Bitrate)
                                      .FirstOrDefault();
            return (videoStreamInfo, audioStreamInfo);
        }

        private (IStreamInfo, IStreamInfo) GetStreamsLow(StreamManifest manifest)
        {
            var audioStreamInfo = manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            var videoStreamInfo = manifest.GetVideoOnlyStreams()
                                      .Where(s => s.VideoQuality.Label == "360p")
                                      .OrderByDescending(s => s.Bitrate)
                                      .FirstOrDefault();
            return (videoStreamInfo, audioStreamInfo);
        }

        public async Task Download(IProgress<int> progress)
        {
            progress.Report(20);
            var video = await youtube.Videos.GetAsync(url);
            var manifest = await youtube.Videos.Streams.GetManifestAsync(url);
            arquivo = ExistFile();
            progress.Report(40);

            // a verificação de qualidade vai aqui =====================

            var (videoStream, audioStream) = GetStreamsHigh(manifest);
            string videoPath = Path.Combine(Path.GetTempPath(), "video.mp4");
            string audioPath = Path.Combine(Path.GetTempPath(), "audio.mp3");
            progress.Report(60);
            await youtube.Videos.Streams.DownloadAsync(videoStream, videoPath);
            progress.Report(70);
            await youtube.Videos.Streams.DownloadAsync(audioStream, audioPath);
            progress.Report(80);

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
            progress.Report(90);
            process.Start();
            process.WaitForExit();

            // Limpar arquivos temporários
            File.Delete(videoPath);
            File.Delete(audioPath);
            progress.Report(95);

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException("Erro ao mesclar vídeo e áudio.");
            }
            progress.Report(100);
        }
    }
}
