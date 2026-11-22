namespace VideoDownloader
{
    partial class TelaPrincipal
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_videos = new System.Windows.Forms.Button();
            this.btn_playlists = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(244)), ((byte)(245)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(536, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "⚡ Video Downloader";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((byte)(161)), ((byte)(161)), ((byte)(170)));
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(536, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Baixe vídeos individuais ou playlists completas em alta definição";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_videos
            // 
            this.btn_videos.BackColor = System.Drawing.Color.FromArgb(((byte)(39)), ((byte)(39)), ((byte)(42)));
            this.btn_videos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_videos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_videos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((byte)(225)), ((byte)(29)), ((byte)(72)));
            this.btn_videos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_videos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_videos.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_videos.ForeColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(244)), ((byte)(245)));
            this.btn_videos.Location = new System.Drawing.Point(28, 95);
            this.btn_videos.Name = "btn_videos";
            this.btn_videos.Size = new System.Drawing.Size(242, 95);
            this.btn_videos.TabIndex = 2;
            this.btn_videos.Text = "🎬  Baixar Vídeos\r\n\r\n(Download em MP4 / MP3)";
            this.btn_videos.UseVisualStyleBackColor = false;
            this.btn_videos.Click += new System.EventHandler(this.btn_videos_Click);
            // 
            // btn_playlists
            // 
            this.btn_playlists.BackColor = System.Drawing.Color.FromArgb(((byte)(39)), ((byte)(39)), ((byte)(42)));
            this.btn_playlists.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_playlists.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_playlists.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((byte)(225)), ((byte)(29)), ((byte)(72)));
            this.btn_playlists.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_playlists.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_playlists.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_playlists.ForeColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(244)), ((byte)(245)));
            this.btn_playlists.Location = new System.Drawing.Point(290, 95);
            this.btn_playlists.Name = "btn_playlists";
            this.btn_playlists.Size = new System.Drawing.Size(242, 95);
            this.btn_playlists.TabIndex = 3;
            this.btn_playlists.Text = "📑  Baixar Playlists\r\n\r\n(Download em lote)";
            this.btn_playlists.UseVisualStyleBackColor = false;
            this.btn_playlists.Click += new System.EventHandler(this.btn_playlists_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((byte)(113)), ((byte)(113)), ((byte)(122)));
            this.label3.Location = new System.Drawing.Point(12, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(536, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Desenvolvido por Andrey Viccari";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TelaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((byte)(24)), ((byte)(24)), ((byte)(27)));
            this.ClientSize = new System.Drawing.Size(560, 238);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_playlists);
            this.Controls.Add(this.btn_videos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TelaPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Video Downloader";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_videos;
        private System.Windows.Forms.Button btn_playlists;
        private System.Windows.Forms.Label label3;
    }
}
