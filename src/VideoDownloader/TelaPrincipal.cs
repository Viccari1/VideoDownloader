using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoDownloader
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
            this.Shown += TelaPrincipal_Shown;
        }

        private void TelaPrincipal_Shown(object sender, EventArgs e)
        {
            if (!FFmpegHelper.IsDisponivel())
            {
                var dialog = MessageBox.Show(
                    "O componente essencial FFmpeg não foi encontrado no seu computador.\n\n" +
                    "Ele é obrigatório para converter músicas e mesclar áudio/vídeo.\n\n" +
                    "Deseja fazer o download e configuração automática agora?",
                    "Configuração Inicial - FFmpeg",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (dialog == DialogResult.Yes)
                {
                    FFmpegHelper.GarantirFFmpeg(this);
                }
            }
        }

        private void btn_videos_Click(object sender, EventArgs e)
        {
            bool ok = FFmpegHelper.GarantirFFmpeg(this);
            if (!ok)
            {
                MessageBox.Show("O FFmpeg é obrigatório para realizar os downloads.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TelaVideo telaVideo = new TelaVideo();
            telaVideo.FormClosed += TelaFormClosed;
            this.Hide();
            telaVideo.Show();
        }

        private void btn_playlists_Click(object sender, EventArgs e)
        {
            bool ok = FFmpegHelper.GarantirFFmpeg(this);
            if (!ok)
            {
                MessageBox.Show("O FFmpeg é obrigatório para realizar os downloads.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TelaPlaylist telaPlaylist = new TelaPlaylist();
            telaPlaylist.FormClosed += TelaFormClosed;
            this.Hide();
            telaPlaylist.Show();
        }
        private void TelaFormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
    }
}
