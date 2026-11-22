using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoDownloader
{
    public partial class TelaDownloadFFmpeg : Form
    {
        private CancellationTokenSource _cts;
        private bool _concluidoComSucesso = false;

        public TelaDownloadFFmpeg()
        {
            InitializeComponent();
            _cts = new CancellationTokenSource();
        }

        private async void TelaDownloadFFmpeg_Shown(object sender, EventArgs e)
        {
            var progresso = new Progress<int>(v =>
            {
                if (!IsDisposed && bar_progresso != null)
                {
                    bar_progresso.Value = Math.Max(0, Math.Min(100, v));
                }
            });

            var status = new Progress<string>(texto =>
            {
                if (!IsDisposed && label_status != null)
                {
                    label_status.Text = texto;
                }
            });

            try
            {
                await FFmpegHelper.BaixarEInstalarFFmpegAsync(progresso, status, _cts.Token);
                _concluidoComSucesso = true;
                await Task.Delay(600); // Breve pausa para exibir o 100%

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (OperationCanceledException)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocorreu um erro ao baixar o FFmpeg automaticamente:\n\n" + ex.Message,
                    "Falha no Download",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void TelaDownloadFFmpeg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_concluidoComSucesso && _cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }
        }

        private void Cancelar()
        {
            btn_cancelar.Enabled = false;
            label_status.Text = "Cancelando...";
            _cts?.Cancel();
        }
    }
}
