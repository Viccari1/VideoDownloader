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
            Playlist downloader = new Playlist(text_link.Text);
            Logger logger = new Logger(new Progress<int>(value => bar_progresso.Value = value), label_status);
            try
            {
                if (downloader.VerifyPlaylistOK().Item1 == false)
                {
                    MessageBox.Show(downloader.VerifyPlaylistOK().Item2, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // downloader.VerifyVideoOK();
                if (rb_video.Checked)
                {
                    await downloader.DownloadVideos(logger);
                }
                else
                {
                    await downloader.DownloadAudios(logger);
                }
                var resultado = MessageBox.Show("Download concluído! Deseja abrir a pasta onde o arquivo foi baixado?", "Sucesso", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    string arg = "/select, \"" + downloader.fullDir + "\"";
                    System.Diagnostics.Process.Start("explorer.exe", arg);
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Link inválido, verifique se o link está correto!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException err)
            {
                MessageBox.Show(err.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            logger.Log(0, "Status: Inativo");
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
