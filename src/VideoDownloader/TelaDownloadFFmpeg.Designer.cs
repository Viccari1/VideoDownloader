namespace VideoDownloader
{
    partial class TelaDownloadFFmpeg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_titulo = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.bar_progresso = new System.Windows.Forms.ProgressBar();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_titulo
            // 
            this.label_titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_titulo.Location = new System.Drawing.Point(12, 13);
            this.label_titulo.Name = "label_titulo";
            this.label_titulo.Size = new System.Drawing.Size(430, 20);
            this.label_titulo.TabIndex = 0;
            this.label_titulo.Text = "Baixando o FFmpeg (necessário para processamento de vídeo e áudio)";
            // 
            // label_status
            // 
            this.label_status.Location = new System.Drawing.Point(12, 38);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(430, 18);
            this.label_status.TabIndex = 1;
            this.label_status.Text = "Iniciando download...";
            // 
            // bar_progresso
            // 
            this.bar_progresso.Location = new System.Drawing.Point(15, 62);
            this.bar_progresso.Name = "bar_progresso";
            this.bar_progresso.Size = new System.Drawing.Size(427, 23);
            this.bar_progresso.TabIndex = 2;
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(347, 98);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(95, 28);
            this.btn_cancelar.TabIndex = 3;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // TelaDownloadFFmpeg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 136);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.bar_progresso);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.label_titulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaDownloadFFmpeg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuração de Dependências - FFmpeg";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TelaDownloadFFmpeg_FormClosing);
            this.Shown += new System.EventHandler(this.TelaDownloadFFmpeg_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_titulo;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ProgressBar bar_progresso;
        private System.Windows.Forms.Button btn_cancelar;
    }
}
