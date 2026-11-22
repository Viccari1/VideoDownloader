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
    public partial class TelaPlaylist : Form
    {
        private bool emUso = false;
        private PlaylistDetails _detalhes = null;
        private Playlist _downloader = null;
        private string _lastLoadedUrl = null;

        public TelaPlaylist()
        {
            InitializeComponent();
            CreateDirEssential();
            this.Shown += TelaPlaylist_Shown;
        }

        private void TelaPlaylist_Shown(object sender, EventArgs e)
        {
            pic_thumb.Image?.Dispose();
            pic_thumb.Image = ThumbnailHelper.CriarPlaceholder(pic_thumb.Width, pic_thumb.Height, "Aguardando URL da Playlist");
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
            await BuscarDetalhesPlaylistAsync(silencioso: false);
        }

        private async Task<bool> BuscarDetalhesPlaylistAsync(bool silencioso)
        {
            string url = text_link.Text?.Trim();
            if (string.IsNullOrWhiteSpace(url))
            {
                if (!silencioso)
                {
                    MessageBox.Show("Por favor, insira o link da playlist do YouTube para buscar as informações.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            btn_buscar.Enabled = false;
            label_status.Text = "Status: Analisando link e buscando vídeos da playlist...";
            bar_progresso.Value = 25;

            try
            {
                _downloader = new Playlist(url);
                _detalhes = await _downloader.ObterDetalhesPlaylistAsync();

                if (_detalhes == null)
                {
                    label_status.Text = "Status: Não foi possível obter os dados da playlist.";
                    bar_progresso.Value = 0;
                    return false;
                }

                _lastLoadedUrl = url;

                // Atualiza textos
                lbl_titulo.Text = _detalhes.Title ?? "Playlist do YouTube";
                lbl_canal.Text = $"Criador: {_detalhes.Author ?? "Desconhecido"}";
                lbl_total_videos.Text = $"Total de Vídeos: {_detalhes.TotalVideos}";

                bar_progresso.Value = 65;
                label_status.Text = "Status: Carregando capa da playlist...";

                // Carrega capa / thumbnail
                var thumb = await ThumbnailHelper.CarregarThumbnailAsync(_detalhes.FirstVideoId, _detalhes.ThumbnailUrl);
                if (thumb != null)
                {
                    pic_thumb.Image?.Dispose();
                    pic_thumb.Image = thumb;
                }

                bar_progresso.Value = 100;
                label_status.Text = $"Status: Playlist carregada! ({_detalhes.TotalVideos} vídeos disponíveis). Clique em Baixar.";
                return true;
            }
            catch (ArgumentException)
            {
                label_status.Text = "Status: Link de playlist inválido.";
                bar_progresso.Value = 0;
                if (!silencioso)
                {
                    MessageBox.Show("A URL fornecida não é um link válido de playlist do YouTube.", "Link Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }
            catch (Exception ex)
            {
                label_status.Text = "Status: Erro ao carregar playlist.";
                bar_progresso.Value = 0;
                if (!silencioso)
                {
                    MessageBox.Show("Não foi possível carregar as informações da playlist:\n\n" + ex.Message, "Erro na Busca", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                panel_resolucao.Enabled = false;
                label_resolucao.Text = "Formato de Áudio: MP3 320kbps (Alta Fidelidade)";
            }
            else
            {
                panel_resolucao.Enabled = true;
                label_resolucao.Text = "Qualidade dos Vídeos:";
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
                MessageBox.Show("Por favor, insira o link da playlist do YouTube.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            // Se ainda não buscou detalhes ou a URL foi alterada
            if (_detalhes == null || _lastLoadedUrl != url)
            {
                bool buscou = await BuscarDetalhesPlaylistAsync(silencioso: false);
                if (!buscou) return;
            }

            emUso = true;
            btn_baixar.Enabled = false;
            btn_buscar.Enabled = false;

            Logger logger = new Logger(new Progress<int>(value => bar_progresso.Value = Math.Max(0, Math.Min(100, value))), label_status);

            try
            {
                if (rb_video.Checked)
                {
                    _downloader.qualidadeAlta = rb_alta.Checked;
                    await _downloader.DownloadVideos(logger);
                }
                else
                {
                    await _downloader.DownloadAudios(logger);
                }

                var resultado = MessageBox.Show(
                    "Download da playlist concluído com sucesso!\n\nDeseja abrir a pasta onde os arquivos foram salvos?",
                    "Sucesso",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (resultado == DialogResult.Yes && !string.IsNullOrEmpty(_downloader.fullDir))
                {
                    string arg = "/select, \"" + _downloader.fullDir + "\"";
                    System.Diagnostics.Process.Start("explorer.exe", arg);
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Link inválido, verifique se a URL da playlist está correta!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException err)
            {
                MessageBox.Show(err.Message, "Erro no Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
