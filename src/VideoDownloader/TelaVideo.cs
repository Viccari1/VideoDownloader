using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoDownloader
{
    public partial class TelaVideo : Form
    {
        private bool emUso = false;
        private VideoDetails _detalhes = null;
        private Video _videoDownloader = null;
        private string _lastLoadedUrl = null;

        public TelaVideo()
        {
            InitializeComponent();
            CreateDirEssential();
            this.Shown += TelaVideo_Shown;
        }

        private void TelaVideo_Shown(object sender, EventArgs e)
        {
            pic_thumb.Image?.Dispose();
            pic_thumb.Image = ThumbnailHelper.CriarPlaceholder(pic_thumb.Width, pic_thumb.Height, "Aguardando URL do Vídeo");
        }

        private void CreateDirEssential()
        {
            string videoDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Baixadas");
            if (!Directory.Exists(videoDir))
            {
                Directory.CreateDirectory(videoDir);
            }

            string musicDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Baixadas");
            if (!Directory.Exists(musicDir))
            {
                Directory.CreateDirectory(musicDir);
            }
        }

        private void text_link_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btn_buscar.PerformClick();
            }
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await BuscarDetalhesAsync(silencioso: false);
        }

        private async Task<bool> BuscarDetalhesAsync(bool silencioso)
        {
            string url = text_link.Text?.Trim();
            if (string.IsNullOrWhiteSpace(url))
            {
                if (!silencioso)
                {
                    MessageBox.Show("Por favor, insira a URL do vídeo do YouTube para buscar as informações.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            btn_buscar.Enabled = false;
            label_status.Text = "Status: Analisando link e buscando qualidades disponíveis...";
            bar_progresso.Value = 25;

            try
            {
                _videoDownloader = new Video(url);
                _detalhes = await _videoDownloader.ObterDetalhesAsync();

                if (_detalhes == null)
                {
                    label_status.Text = "Status: Não foi possível obter os detalhes do vídeo.";
                    bar_progresso.Value = 0;
                    return false;
                }

                _lastLoadedUrl = url;

                // Atualiza dados na tela
                lbl_titulo.Text = _detalhes.Title ?? "Vídeo do YouTube";
                lbl_canal.Text = $"Canal: {_detalhes.Author ?? "Desconhecido"}";
                lbl_duracao.Text = $"Duração: {_detalhes.GetFormattedDuration()}";

                // Atualiza ComboBox de qualidades
                cb_qualidades.Items.Clear();
                foreach (var q in _detalhes.Qualities)
                {
                    cb_qualidades.Items.Add(q);
                }

                if (cb_qualidades.Items.Count > 0)
                {
                    cb_qualidades.SelectedIndex = 0;
                }

                bar_progresso.Value = 65;
                label_status.Text = "Status: Carregando capa em alta definição...";

                // Carrega thumbnail segura
                var thumb = await ThumbnailHelper.CarregarThumbnailAsync(_detalhes.VideoId, _detalhes.ThumbnailUrl);
                if (thumb != null)
                {
                    pic_thumb.Image?.Dispose();
                    pic_thumb.Image = thumb;
                }

                bar_progresso.Value = 100;
                label_status.Text = "Status: Vídeo carregado com sucesso! Escolha o formato e clique em Iniciar Download.";
                return true;
            }
            catch (ArgumentException)
            {
                label_status.Text = "Status: Link inválido.";
                bar_progresso.Value = 0;
                if (!silencioso)
                {
                    MessageBox.Show("A URL fornecida não é válida para um vídeo do YouTube.", "Link Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }
            catch (Exception ex)
            {
                label_status.Text = "Status: Erro ao carregar informações.";
                bar_progresso.Value = 0;
                if (!silencioso)
                {
                    MessageBox.Show("Não foi possível carregar as informações do vídeo:\n\n" + ex.Message, "Erro na Busca", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            finally
            {
                btn_buscar.Enabled = true;
            }
        }

        private void rb_formato_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_audio.Checked)
            {
                cb_qualidades.Enabled = false;
                label_qualidade.Text = "Formato de Áudio: MP3 320kbps (Alta Fidelidade)";
            }
            else
            {
                cb_qualidades.Enabled = true;
                label_qualidade.Text = "Qualidade / Tamanho Estimado:";
            }
        }

        private async void btn_baixar_Click(object sender, EventArgs e)
        {
            if (emUso)
            {
                MessageBox.Show("O processo de download já está em andamento!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string url = text_link.Text?.Trim();
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Por favor, insira a URL do vídeo do YouTube.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!FFmpegHelper.IsDisponivel())
            {
                bool ok = FFmpegHelper.GarantirFFmpeg(this);
                if (!ok)
                {
                    MessageBox.Show("Não é possível realizar downloads sem o FFmpeg configurado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Se ainda não buscou detalhes ou a URL foi alterada no campo
            if (_detalhes == null || _lastLoadedUrl != url)
            {
                bool buscou = await BuscarDetalhesAsync(silencioso: false);
                if (!buscou) return;
            }

            emUso = true;
            btn_baixar.Enabled = false;
            btn_buscar.Enabled = false;

            Logger logger = new Logger(new Progress<int>(value => bar_progresso.Value = Math.Max(0, Math.Min(100, value))), label_status);

            try
            {
                IDownloader downloader;

                if (rb_video.Checked)
                {
                    var opt = cb_qualidades.SelectedItem as QualityOption;
                    string qualidadeEscolhida = opt?.Label ?? (_detalhes?.Qualities.FirstOrDefault()?.Label ?? "1080p");

                    _videoDownloader.qualidade = qualidadeEscolhida;
                    downloader = _videoDownloader;
                }
                else
                {
                    logger.Log(5, "Verificando áudio...");
                    var audioDownloader = new Audio(url);
                    var (valido, msg) = await audioDownloader.VerifyVideoOKAsync();
                    if (!valido)
                    {
                        MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        logger.Log(0, "Status: Inativo");
                        return;
                    }

                    downloader = audioDownloader;
                }

                await downloader.Download(logger);

                var resultado = MessageBox.Show(
                    "Download concluído com sucesso!\n\nDeseja abrir a pasta onde o arquivo foi salvo?",
                    "Sucesso",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (resultado == DialogResult.Yes)
                {
                    string arg = "/select, \"" + downloader.arquivo + "\"";
                    System.Diagnostics.Process.Start("explorer.exe", arg);
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Link inválido, verifique se a URL informada está correta!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException err)
            {
                MessageBox.Show(err.Message, "Erro no Processamento", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception err)
            {
                MessageBox.Show("Ocorreu um erro durante o download:\n\n" + err.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                logger.Log(0, "Status: Pronto para novo download");
                emUso = false;
                btn_baixar.Enabled = true;
                btn_buscar.Enabled = true;
            }
        }

        private void btn_voltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_pasta_video_Click(object sender, EventArgs e)
        {
            var arg = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Baixadas");
            System.Diagnostics.Process.Start("explorer.exe", arg);
        }

        private void btn_pasta_audio_Click(object sender, EventArgs e)
        {
            var arg = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Baixadas");
            System.Diagnostics.Process.Start("explorer.exe", arg);
        }
    }
}
