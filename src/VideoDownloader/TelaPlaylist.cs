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
        public TelaPlaylist()
        {
            InitializeComponent();
            CreateDirEssential();
        }

        private void CreateDirEssential()
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Baixadas")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Baixadas"));
            }
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Baixadas")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Baixadas"));
            }
        }

        private async void btn_baixar_Click(object sender, EventArgs e)
        {
            if (emUso)
            {
                MessageBox.Show("O processo ainda não terminou!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(text_link.Text))
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

            emUso = true;
            Playlist downloader = new Playlist(text_link.Text);
            Logger logger = new Logger(new Progress<int>(value => bar_progresso.Value = Math.Max(0, Math.Min(100, value))), label_status);

            try
            {
                var (valido, mensagemValidacao) = await downloader.VerifyPlaylistOKAsync();
                if (!valido)
                {
                    MessageBox.Show(mensagemValidacao, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (rb_video.Checked)
                {
                    downloader.qualidadeAlta = rb_alta.Checked;
                    await downloader.DownloadVideos(logger);
                }
                else
                {
                    await downloader.DownloadAudios(logger);
                }

                var resultado = MessageBox.Show(
                    "Download da playlist concluído com sucesso!\n\nDeseja abrir a pasta onde os arquivos foram salvos?",
                    "Sucesso",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (resultado == DialogResult.Yes)
                {
                    string arg = "/select, \"" + downloader.fullDir + "\"";
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
                logger.Log(0, "Status: Inativo");
                emUso = false;
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

        private void rb_video_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_video.Checked)
                panel3.Visible = true;
            else
                panel3.Visible = false;
        }
    }
}
