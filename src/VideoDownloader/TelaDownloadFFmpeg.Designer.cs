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
            this.label_titulo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_titulo.ForeColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(244)), ((byte)(245)));
            this.label_titulo.Location = new System.Drawing.Point(14, 14);
            this.label_titulo.Name = "label_titulo";
            this.label_titulo.Size = new System.Drawing.Size(426, 20);
            this.label_titulo.TabIndex = 0;
            this.label_titulo.Text = "Configurando componente FFmpeg automaticamente...";
            // 
            // label_status
            // 
            this.label_status.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_status.ForeColor = System.Drawing.Color.FromArgb(((byte)(161)), ((byte)(161)), ((byte)(170)));
            this.label_status.Location = new System.Drawing.Point(14, 38);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(426, 18);
            this.label_status.TabIndex = 1;
            this.label_status.Text = "Iniciando download...";
            // 
            // bar_progresso
            // 
            this.bar_progresso.Location = new System.Drawing.Point(16, 62);
            this.bar_progresso.Name = "bar_progresso";
            this.bar_progresso.Size = new System.Drawing.Size(422, 22);
            this.bar_progresso.TabIndex = 2;
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.BackColor = System.Drawing.Color.FromArgb(((byte)(39)), ((byte)(39)), ((byte)(42)));
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_cancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_cancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.ForeColor = System.Drawing.Color.FromArgb(((byte)(212)), ((byte)(212)), ((byte)(216)));
            this.btn_cancelar.Location = new System.Drawing.Point(343, 96);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(95, 28);
            this.btn_cancelar.TabIndex = 3;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // TelaDownloadFFmpeg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((byte)(24)), ((byte)(24)), ((byte)(27)));
            this.ClientSize = new System.Drawing.Size(454, 136);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.bar_progresso);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.label_titulo);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaDownloadFFmpeg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Instalação do FFmpeg";
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
