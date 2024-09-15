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
        }

        private void btn_videos_Click(object sender, EventArgs e)
        {
            TelaVideo telaVideo = new TelaVideo();
            telaVideo.FormClosed += TelaFormClosed;
            this.Hide();
            telaVideo.Show();
        }

        private void btn_playlists_Click(object sender, EventArgs e)
        {
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
