using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace VideoDownloader
{
    public static class ThumbnailHelper
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(8)
        };

        public static async Task<Image> CarregarThumbnailAsync(string videoId, string fallbackUrl = null)
        {
            var candidatos = new List<string>();

            if (!string.IsNullOrWhiteSpace(videoId))
            {
                string id = videoId.Trim();
                candidatos.Add($"https://i.ytimg.com/vi/{id}/maxresdefault.jpg");
                candidatos.Add($"https://i.ytimg.com/vi/{id}/sddefault.jpg");
                candidatos.Add($"https://i.ytimg.com/vi/{id}/hqdefault.jpg");
                candidatos.Add($"https://i.ytimg.com/vi/{id}/mqdefault.jpg");
            }

            if (!string.IsNullOrWhiteSpace(fallbackUrl) && !fallbackUrl.EndsWith(".webp", StringComparison.OrdinalIgnoreCase))
            {
                candidatos.Add(fallbackUrl);
            }

            foreach (string url in candidatos)
            {
                try
                {
                    using (var response = await _httpClient.GetAsync(url))
                    {
                        if (!response.IsSuccessStatusCode)
                            continue;

                        byte[] data = await response.Content.ReadAsByteArrayAsync();

                        // Garante uma cópia desvinculada do stream nativo do GDI+
                        using (var ms = new MemoryStream(data))
                        using (var temp = Image.FromStream(ms))
                        {
                            return new Bitmap(temp);
                        }
                    }
                }
                catch
                {
                    // Tenta o próximo candidato
                }
            }

            return null;
        }

        public static Image CriarPlaceholder(int width, int height, string texto = "Pré-visualização do Vídeo")
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.FromArgb(32, 32, 36));

                // Borda suave
                using (Pen pen = new Pen(Color.FromArgb(63, 63, 70), 1))
                {
                    g.DrawRectangle(pen, 0, 0, width - 1, height - 1);
                }

                // Ícone e texto centralizado
                string label = "🎬\n" + texto;
                using (Font font = new Font("Segoe UI", 9.5f, FontStyle.Regular))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(161, 161, 170)))
                using (StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    g.DrawString(label, font, brush, new RectangleF(0, 0, width, height), sf);
                }
            }
            return bmp;
        }
    }
}
