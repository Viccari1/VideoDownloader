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
    public partial class TelaQualidade : Form
    {
        public TelaQualidade()
        {
            InitializeComponent();
        }

        private void TelaQualidade_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Global.qualidades.Count; i++)
            {
                comboBox1.Items.Add(Global.qualidades[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Global.qualidadeDefinida = comboBox1.SelectedItem.ToString();
            this.Close();
        }
    }
}
