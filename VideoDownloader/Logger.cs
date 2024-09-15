using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoDownloader
{
    internal class Logger
    {
        IProgress<int> progress;
        Label label;
        public Logger(IProgress<int> progress, Label label)
        {
            this.progress = progress;
            this.label = label;
        }
        private void atualizarProgresso(int value)
        {
            progress.Report(value);
        }

        private void atualizarLabel(string text)
        {
            label.Text = text;
        }

        public void Log(int value, string text)
        {
            atualizarProgresso(value);
            atualizarLabel(text);
        }

    }
}
