using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoDownloader
{
    public partial class TelaQualidade : Form
    {
        private readonly VideoDetails _detalhes;
        public QualityOption QualidadeSelecionada { get; private set; }

        public TelaQualidade(VideoDetails detalhes)
        {
            InitializeComponent();
            _detalhes = detalhes ?? throw new ArgumentNullException(nameof(detalhes));
            this.Shown += TelaQualidade_Shown;
            ConfigurarInterface();
        }

        public TelaQualidade()
        {
            InitializeComponent();
        }

        private void ConfigurarInterface()
        {
            if (_detalhes == null) return;

            lbl_titulo.Text = _detalhes.Title ?? "Vídeo do YouTube";
            lbl_info.Text = $"Canal: {_detalhes.Author ?? "Desconhecido"}  |  Duração: {_detalhes.GetFormattedDuration()}";

            cb_qualidades.Items.Clear();
            foreach (var q in _detalhes.Qualities)
            {
                cb_qualidades.Items.Add(q);
            }

            if (cb_qualidades.Items.Count > 0)
            {
                cb_qualidades.SelectedIndex = 0;
            }
        }

        private async void TelaQualidade_Shown(object sender, EventArgs e)
        {
            await CarregarThumbnailAsync();
        }

        private async Task CarregarThumbnailAsync()
        {
            if (_detalhes == null) return;

            // Lista ordenada de URLs JPEG garantidas para compatibilidade total com o GDI+ do Windows
            var candidatos = new List<string>();

            if (!string.IsNullOrWhiteSpace(_detalhes.VideoId))
            {
                string id = _detalhes.VideoId.Trim();
                candidatos.Add($"https://i.ytimg.com/vi/{id}/maxresdefault.jpg");
                candidatos.Add($"https://i.ytimg.com/vi/{id}/sddefault.jpg");
                candidatos.Add($"https://i.ytimg.com/vi/{id}/hqdefault.jpg");
                candidatos.Add($"https://i.ytimg.com/vi/{id}/mqdefault.jpg");
            }

            if (!string.IsNullOrWhiteSpace(_detalhes.ThumbnailUrl) && !_detalhes.ThumbnailUrl.EndsWith(".webp", StringComparison.OrdinalIgnoreCase))
            {
                candidatos.Add(_detalhes.ThumbnailUrl);
            }

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(6);

                foreach (string url in candidatos)
                {
                    try
                    {
                        using (var response = await client.GetAsync(url))
                        {
                            if (!response.IsSuccessStatusCode)
                                continue;

                            byte[] data = await response.Content.ReadAsByteArrayAsync();

                            // Cria cópia desvinculada do stream (essencial no GDI+ para evitar imagem corrompida/preta)
                            using (var ms = new MemoryStream(data))
                            using (var temp = Image.FromStream(ms))
                            {
                                pic_thumb.Image?.Dispose();
                                pic_thumb.Image = new Bitmap(temp);
                                return; // Carregada com sucesso
                            }
                        }
                    }
                    catch
                    {
                        // Tenta o próximo candidato de fallback
                    }
                }
            }
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            if (cb_qualidades.SelectedItem is QualityOption selecionada)
            {
                QualidadeSelecionada = selecionada;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma opção de qualidade.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            QualidadeSelecionada = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
