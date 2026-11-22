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
                MessageBox.Show("O processo de download ainda está em andamento!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(text_link.Text))
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

            emUso = true;
            Logger logger = new Logger(new Progress<int>(value => bar_progresso.Value = Math.Max(0, Math.Min(100, value))), label_status);

            try
            {
                IDownloader downloader;

                if (rb_video.Checked)
                {
                    logger.Log(5, "Carregando detalhes do vídeo e qualidades...");
                    var videoDownloader = new Video(text_link.Text);

                    // Obtém detalhes, thumbnail, duração e peso estimado por resolução
                    var detalhes = await videoDownloader.ObterDetalhesAsync();

                    using (var tela = new TelaQualidade(detalhes))
                    {
                        if (tela.ShowDialog(this) != DialogResult.OK || tela.QualidadeSelecionada == null)
                        {
                            logger.Log(0, "Status: Inativo");
                            return;
                        }

                        videoDownloader.qualidade = tela.QualidadeSelecionada.Label;
                    }

                    downloader = videoDownloader;
                }
                else
                {
                    logger.Log(5, "Verificando link de áudio...");
                    var audioDownloader = new Audio(text_link.Text);

                    var (valido, mensagemValidacao) = await audioDownloader.VerifyVideoOKAsync();
                    if (!valido)
                    {
                        MessageBox.Show(mensagemValidacao, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        logger.Log(0, "Status: Inativo");
                        return;
                    }

                    downloader = audioDownloader;
                }

                // Inicia o download com progresso contínuo
                await downloader.Download(logger);

                var resultado = MessageBox.Show(
                    "Download concluído com sucesso!\n\nDeseja abrir a pasta onde o arquivo foi baixado?",
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
    }
}
