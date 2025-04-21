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
        string ffmpegPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe");
        public bool qualidadeAlta = false;
        public string fullDir;

        public Playlist(string url)
        {
            this.url = url;
            youtube = new YoutubeClient();
        }

        private bool TryFindPlaylist() {
            try
            {
                youtube.Playlists.GetAsync(url);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private string ExistFileVideo(string nome, string ext)
        {
            string path = "";
            int i = 1;
            nome = RemoveInvalidChars(nome);
            while (true)
            {
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Baixadas\\" + pasta + "\\" + nome + ext);

                if (File.Exists(path) == false)
                    return path;
                else
                {
                    nome = nome + Convert.ToString(i);
                    i++;
                }
            }
        }
        private string ExistFileAudio(string nome, string ext)
        {
            string path = "";
            int i = 1;
            nome = RemoveInvalidChars(nome);
            while (true)
            {
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Baixadas\\" + pasta + "\\" + nome + ext);

                if (File.Exists(path) == false)
                    return path;
                else
                {
                    nome = nome + Convert.ToString(i);
                    i++;
                }
            }
        }

        private void CreateDirVideo(string dir)
        {
            pasta = RemoveInvalidChars(dir);
            string videoPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Baixadas\\", pasta);

            if (!Directory.Exists(videoPath))
            {
                Directory.CreateDirectory(videoPath);
            }
        }
        private void CreateDirAudio(string dir)
        {
            pasta = RemoveInvalidChars(dir);
            string musicPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Baixadas\\", pasta);

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
            return nome;
        }
        public (bool, string) VerifyPlaylistOK()
        {
            if (!TryFindPlaylist())
            {
                return (false, "Playlist não encontrada");
            }
            else
            {
                return (true, "");
            }
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
            int i = 0, total = Convert.ToInt32(videos.Count());
            foreach (var video in videos)
            {
                i++;
                logger.Log(20, "Iniciando processo ("+i+"/"+total+")...");
                var videoName = video.Title;
                var manifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                logger.Log(40, "Recebendo stream (" + i + "/" + total + ")...");
                IStreamInfo videoStream, audioStream;
                if (qualidadeAlta)
                {
                    (videoStream, audioStream) = GetStreamsVideoHigh(manifest);
                }
                else
                {
                    (videoStream, audioStream) = GetStreamsVideoLow(manifest);
                }
                var fullDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Baixadas\\", RemoveInvalidChars(tituloPlaylist));
                var arquivo = Path.Combine(fullDir, ExistFileVideo(videoName, ".mp4") + ".mp4");

                logger.Log(60, "Criando arquivos temporários (" + i + "/" + total + ")...");
                string videoPath = Path.Combine(Path.GetTempPath(), "video.mp4");
                string audioPath = Path.Combine(Path.GetTempPath(), "audio.mp3");
                logger.Log(80, "Baixando streams de video e audio (" + i + "/" + total + ")...");
                await youtube.Videos.Streams.DownloadAsync(videoStream, videoPath);
                await youtube.Videos.Streams.DownloadAsync(audioStream, audioPath);

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
                logger.Log(90, "Mesclando arquivos (" + i + "/" + total + ")...");

                process.Start();
                process.WaitForExit();
                logger.Log(95, "Apagando temporarios (" + i + "/" + total + ")...");
                File.Delete(videoPath);
                File.Delete(audioPath);

                if (process.ExitCode != 0)
                {
                    throw new InvalidOperationException("Erro ao mesclar vídeo e áudio.");
                }            
            this.fullDir = fullDir;
            }
        }
        public async Task DownloadAudios(Logger logger)
        {
            logger.Log(5, "Buscando playlist...");
            var playlist = await youtube.Playlists.GetAsync(url);
            var videos = await youtube.Playlists.GetVideosAsync(url);
            string tituloPlaylist = playlist.Title;
            var fullDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Baixadas\\", RemoveInvalidChars(tituloPlaylist));
            CreateDirAudio(tituloPlaylist);
            int i = 1, total = Convert.ToInt32(videos.Count());
            foreach (var video in videos)
            {
                logger.Log(30, "Buscando audio (" + i + "/" + total + ")...");
                var manifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                var arquivo = ExistFileAudio(video.Title, ".mp3");
                logger.Log(60, "Buscando stream de audio do video (" + i + "/" + total + ")...");
                var audioStream = manifest.GetAudioOnlyStreams().GetWithHighestBitrate();

                try
                {
                    logger.Log(90, "Baixando e convertendo audio (" + i + "/" + total + ")...");
                    await youtube.Videos.DownloadAsync(video.Url, arquivo, o => o
                                             .SetContainer("mp3")
                                             .SetPreset(ConversionPreset.Fast)
                                             .SetFFmpegPath(ffmpegPath));
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                this.fullDir = fullDir;
            }
        }
    }
}