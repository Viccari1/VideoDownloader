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
        public TelaVideo()
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
                MessageBox.Show("O processo ainda não terminou!");
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

            IDownloader downloader;
            Logger logger = new Logger(new Progress<int>(value => bar_progresso.Value = value), label_status);
            if (rb_video.Checked)
            {
                var videoDownloader = new Video(text_link.Text);
                List<string> qualidades = await videoDownloader.GetQuality();
                Global.qualidades.Clear();

                for (int i = 0; i < qualidades.Count; i++)
                {
                    Global.qualidades.Add(qualidades[i]);
                }
                TelaQualidade tela = new TelaQualidade();
                tela.ShowDialog();
                if (Global.qualidadeDefinida == null)
                {
                    emUso = false;
                    MessageBox.Show("Selecione uma qualidade!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                videoDownloader.qualidade = Global.qualidadeDefinida;
                downloader = videoDownloader;
            }
            else
            {
                downloader = new Audio(text_link.Text);
            }
            try
            {
                var (valido, mensagemValidacao) = await downloader.VerifyVideoOKAsync();
                if (!valido)
                {
                    MessageBox.Show(mensagemValidacao, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    emUso = false;
                    return;
                }

                await downloader.Download(logger);
                var resultado = MessageBox.Show("Download concluído! Deseja abrir a pasta onde o arquivo foi baixado?", "Sucesso", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    string arg = "/select, \"" + downloader.arquivo + "\"";
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
            emUso = false;
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
