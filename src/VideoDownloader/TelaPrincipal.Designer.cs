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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(527, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Baixador de video e playlist do Youtube";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(527, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Escolha o que você deseja baixar";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_videos
            // 
            this.btn_videos.Location = new System.Drawing.Point(12, 66);
            this.btn_videos.Name = "btn_videos";
            this.btn_videos.Size = new System.Drawing.Size(260, 80);
            this.btn_videos.TabIndex = 2;
            this.btn_videos.Text = "Baixar videos";
            this.btn_videos.UseVisualStyleBackColor = true;
            this.btn_videos.Click += new System.EventHandler(this.btn_videos_Click);
            // 
            // btn_playlists
            // 
            this.btn_playlists.Location = new System.Drawing.Point(279, 66);
            this.btn_playlists.Name = "btn_playlists";
            this.btn_playlists.Size = new System.Drawing.Size(260, 80);
            this.btn_playlists.TabIndex = 3;
            this.btn_playlists.Text = "Baixar Playlists";
            this.btn_playlists.UseVisualStyleBackColor = true;
            this.btn_playlists.Click += new System.EventHandler(this.btn_playlists_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(527, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Programa feito por Andrey Viccari";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TelaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 178);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_playlists);
            this.Controls.Add(this.btn_videos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TelaPrincipal";
            this.Text = "Form1";
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

