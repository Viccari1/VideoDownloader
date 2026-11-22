using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoDownloader
{
    internal static class FFmpegHelper
    {
        // URL canônica dinâmica do GitHub Releases que sempre aponta e redireciona para a última versão estável compilada
        private const string LatestFFmpegUrl = "https://github.com/BtbN/FFmpeg-Builds/releases/download/latest/ffmpeg-master-latest-win64-gpl.zip";
        private const string FallbackFFmpegUrl = "https://github.com/FFmpeg-Builds/FFmpeg-Builds/releases/download/latest/ffmpeg-master-latest-win64-gpl.zip";

        public static string GetFFmpegPath()
        {
            // 1. Verifica no diretório base da aplicação
            string localPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe");
            if (File.Exists(localPath))
            {
                return localPath;
            }

            // 2. Verifica se o executável está no PATH do sistema operacional
            string pathEnv = Environment.GetEnvironmentVariable("PATH");
            if (!string.IsNullOrEmpty(pathEnv))
            {
                string[] paths = pathEnv.Split(';');
                foreach (string dir in paths)
                {
                    try
                    {
                        string trimmed = dir.Trim();
                        if (string.IsNullOrEmpty(trimmed))
                            continue;

                        string candidate = Path.Combine(trimmed, "ffmpeg.exe");
                        if (File.Exists(candidate))
                        {
                            return candidate;
                        }
                    }
                    catch
                    {
                        // Ignora diretórios inválidos no PATH
                    }
                }
            }

            return localPath;
        }

        public static bool IsDisponivel()
        {
            return File.Exists(GetFFmpegPath());
        }

        public static bool GarantirFFmpeg(IWin32Window parent = null)
        {
            if (IsDisponivel())
            {
                return true;
            }

            using (TelaDownloadFFmpeg tela = new TelaDownloadFFmpeg())
            {
                DialogResult result = (parent != null) ? tela.ShowDialog(parent) : tela.ShowDialog();
                return result == DialogResult.OK && IsDisponivel();
            }
        }

        public static async Task BaixarEInstalarFFmpegAsync(
            IProgress<int> progresso,
            IProgress<string> status,
            CancellationToken cancellationToken)
        {
            // Habilita protocolos TLS modernos no .NET Framework
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | (SecurityProtocolType)3072;

            string targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe");
            string tempZip = Path.Combine(Path.GetTempPath(), "ffmpeg_download_" + Guid.NewGuid().ToString("N") + ".zip");

            Exception ultimaExcecao = null;

            string[] urls = new[] { LatestFFmpegUrl, FallbackFFmpegUrl };

            foreach (string url in urls)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    status.Report("Conectando ao GitHub para obter a versão mais recente do FFmpeg...");
                    progresso.Report(0);

                    using (HttpClientHandler handler = new HttpClientHandler { AllowAutoRedirect = true })
                    using (HttpClient client = new HttpClient(handler))
                    {
                        client.Timeout = TimeSpan.FromMinutes(15);
                        client.DefaultRequestHeaders.Add("User-Agent", "VideoDownloader-App");

                        using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                        {
                            response.EnsureSuccessStatusCode();

                            long? totalBytes = response.Content.Headers.ContentLength;

                            using (Stream contentStream = await response.Content.ReadAsStreamAsync())
                            using (FileStream fileStream = new FileStream(tempZip, FileMode.Create, FileAccess.Write, FileShare.None, 81920, true))
                            {
                                byte[] buffer = new byte[81920];
                                long totalRead = 0;
                                int bytesRead;

                                while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                                {
                                    await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                                    totalRead += bytesRead;

                                    if (totalBytes.HasValue && totalBytes.Value > 0)
                                    {
                                        int percent = (int)((totalRead * 100) / totalBytes.Value);
                                        progresso.Report(percent);

                                        double mbRead = totalRead / (1024.0 * 1024.0);
                                        double mbTotal = totalBytes.Value / (1024.0 * 1024.0);
                                        status.Report($"Baixando FFmpeg: {mbRead:F1} MB / {mbTotal:F1} MB ({percent}%)...");
                                    }
                                    else
                                    {
                                        double mbRead = totalRead / (1024.0 * 1024.0);
                                        status.Report($"Baixando FFmpeg: {mbRead:F1} MB...");
                                    }
                                }
                            }
                        }
                    }

                    // Extração do ffmpeg.exe do zip
                    status.Report("Extraindo executável ffmpeg.exe...");
                    progresso.Report(95);

                    using (ZipArchive archive = ZipFile.OpenRead(tempZip))
                    {
                        ZipArchiveEntry ffmpegEntry = archive.Entries.FirstOrDefault(e =>
                            e.Name.Equals("ffmpeg.exe", StringComparison.OrdinalIgnoreCase));

                        if (ffmpegEntry == null)
                        {
                            throw new FileNotFoundException("O arquivo ffmpeg.exe não foi encontrado dentro do pacote baixado.");
                        }

                        ffmpegEntry.ExtractToFile(targetFile, overwrite: true);
                    }

                    progresso.Report(100);
                    status.Report("FFmpeg instalado com sucesso!");
                    return; // Sucesso, finaliza a execução
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    ultimaExcecao = ex;
                    // Tenta a próxima URL de fallback
                }
                finally
                {
                    try
                    {
                        if (File.Exists(tempZip))
                        {
                            File.Delete(tempZip);
                        }
                    }
                    catch
                    {
                        // Ignora falha de limpeza temporária
                    }
                }
            }

            throw new InvalidOperationException("Não foi possível baixar o FFmpeg de nenhuma das fontes disponíveis. Verifique sua conexão com a internet.", ultimaExcecao);
        }
    }
}
