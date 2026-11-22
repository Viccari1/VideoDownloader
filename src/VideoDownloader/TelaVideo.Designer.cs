namespace VideoDownloader
{
    partial class TelaVideo
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
            this.label_titulo_app = new System.Windows.Forms.Label();
            this.label_subtitulo = new System.Windows.Forms.Label();
            this.label_link = new System.Windows.Forms.Label();
            this.text_link = new System.Windows.Forms.TextBox();
            this.btn_buscar = new System.Windows.Forms.Button();
            this.panel_preview = new System.Windows.Forms.Panel();
            this.pic_thumb = new System.Windows.Forms.PictureBox();
            this.lbl_titulo = new System.Windows.Forms.Label();
            this.lbl_canal = new System.Windows.Forms.Label();
            this.lbl_duracao = new System.Windows.Forms.Label();
            this.label_qualidade = new System.Windows.Forms.Label();
            this.cb_qualidades = new System.Windows.Forms.ComboBox();
            this.label_formato = new System.Windows.Forms.Label();
            this.rb_video = new System.Windows.Forms.RadioButton();
            this.rb_audio = new System.Windows.Forms.RadioButton();
            this.panel_progresso = new System.Windows.Forms.Panel();
            this.label_status = new System.Windows.Forms.Label();
            this.bar_progresso = new System.Windows.Forms.ProgressBar();
            this.btn_baixar = new System.Windows.Forms.Button();
            this.btn_voltar = new System.Windows.Forms.Button();
            this.btn_pasta_video = new System.Windows.Forms.Button();
            this.btn_pasta_audio = new System.Windows.Forms.Button();
            this.panel_preview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_thumb)).BeginInit();
            this.panel_progresso.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_titulo_app
            // 
            this.label_titulo_app.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_titulo_app.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_titulo_app.ForeColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(244)), ((byte)(245)));
            this.label_titulo_app.Location = new System.Drawing.Point(16, 12);
            this.label_titulo_app.Name = "label_titulo_app";
            this.label_titulo_app.Size = new System.Drawing.Size(688, 25);
            this.label_titulo_app.TabIndex = 0;
            this.label_titulo_app.Text = "🎬 Download de Vídeo & Áudio Individual";
            this.label_titulo_app.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_subtitulo
            // 
            this.label_subtitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_subtitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_subtitulo.ForeColor = System.Drawing.Color.FromArgb(((byte)(161)), ((byte)(161)), ((byte)(170)));
            this.label_subtitulo.Location = new System.Drawing.Point(18, 38);
            this.label_subtitulo.Name = "label_subtitulo";
            this.label_subtitulo.Size = new System.Drawing.Size(686, 18);
            this.label_subtitulo.TabIndex = 1;
            this.label_subtitulo.Text = "Insira a URL do YouTube para carregar o vídeo, visualizar detalhes e escolher a qualidade.";
            this.label_subtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_link
            // 
            this.label_link.AutoSize = true;
            this.label_link.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_link.ForeColor = System.Drawing.Color.FromArgb(((byte)(212)), ((byte)(212)), ((byte)(216)));
            this.label_link.Location = new System.Drawing.Point(16, 68);
            this.label_link.Name = "label_link";
            this.label_link.Size = new System.Drawing.Size(130, 17);
            this.label_link.TabIndex = 2;
            this.label_link.Text = "🔗 Link do YouTube:";
            // 
            // text_link
            // 
            this.text_link.BackColor = System.Drawing.Color.FromArgb(((byte)(39)), ((byte)(39)), ((byte)(42)));
            this.text_link.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_link.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_link.ForeColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(244)), ((byte)(245)));
            this.text_link.Location = new System.Drawing.Point(16, 90);
            this.text_link.Name = "text_link";
            this.text_link.Size = new System.Drawing.Size(560, 26);
            this.text_link.TabIndex = 3;
            this.text_link.KeyDown += new System.Windows.Forms.KeyEventHandler(this.text_link_KeyDown);
            // 
            // btn_buscar
            // 
            this.btn_buscar.BackColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_buscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_buscar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((byte)(82)), ((byte)(82)), ((byte)(91)));
            this.btn_buscar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((byte)(39)), ((byte)(39)), ((byte)(42)));
            this.btn_buscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((byte)(82)), ((byte)(82)), ((byte)(91)));
            this.btn_buscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_buscar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_buscar.ForeColor = System.Drawing.Color.White;
            this.btn_buscar.Location = new System.Drawing.Point(584, 88);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(120, 29);
            this.btn_buscar.TabIndex = 4;
            this.btn_buscar.Text = "🔍 Buscar";
            this.btn_buscar.UseVisualStyleBackColor = false;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // panel_preview
            // 
            this.panel_preview.BackColor = System.Drawing.Color.FromArgb(((byte)(39)), ((byte)(39)), ((byte)(42)));
            this.panel_preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_preview.Controls.Add(this.pic_thumb);
            this.panel_preview.Controls.Add(this.lbl_titulo);
            this.panel_preview.Controls.Add(this.lbl_canal);
            this.panel_preview.Controls.Add(this.lbl_duracao);
            this.panel_preview.Controls.Add(this.label_qualidade);
            this.panel_preview.Controls.Add(this.cb_qualidades);
            this.panel_preview.Controls.Add(this.label_formato);
            this.panel_preview.Controls.Add(this.rb_video);
            this.panel_preview.Controls.Add(this.rb_audio);
            this.panel_preview.Location = new System.Drawing.Point(16, 128);
            this.panel_preview.Name = "panel_preview";
            this.panel_preview.Size = new System.Drawing.Size(688, 220);
            this.panel_preview.TabIndex = 5;
            // 
            // pic_thumb
            // 
            this.pic_thumb.BackColor = System.Drawing.Color.FromArgb(((byte)(24)), ((byte)(24)), ((byte)(27)));
            this.pic_thumb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_thumb.Location = new System.Drawing.Point(14, 14);
            this.pic_thumb.Name = "pic_thumb";
            this.pic_thumb.Size = new System.Drawing.Size(250, 140);
            this.pic_thumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_thumb.TabIndex = 0;
            this.pic_thumb.TabStop = false;
            // 
            // lbl_titulo
            // 
            this.lbl_titulo.AutoEllipsis = true;
            this.lbl_titulo.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_titulo.ForeColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(244)), ((byte)(245)));
            this.lbl_titulo.Location = new System.Drawing.Point(276, 14);
            this.lbl_titulo.Name = "lbl_titulo";
            this.lbl_titulo.Size = new System.Drawing.Size(396, 44);
            this.lbl_titulo.TabIndex = 1;
            this.lbl_titulo.Text = "Aguardando URL do vídeo...";
            // 
            // lbl_canal
            // 
            this.lbl_canal.AutoEllipsis = true;
            this.lbl_canal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_canal.ForeColor = System.Drawing.Color.FromArgb(((byte)(161)), ((byte)(161)), ((byte)(170)));
            this.lbl_canal.Location = new System.Drawing.Point(276, 62);
            this.lbl_canal.Name = "lbl_canal";
            this.lbl_canal.Size = new System.Drawing.Size(396, 18);
            this.lbl_canal.TabIndex = 2;
            this.lbl_canal.Text = "Canal: -";
            // 
            // lbl_duracao
            // 
            this.lbl_duracao.AutoEllipsis = true;
            this.lbl_duracao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_duracao.ForeColor = System.Drawing.Color.FromArgb(((byte)(161)), ((byte)(161)), ((byte)(170)));
            this.lbl_duracao.Location = new System.Drawing.Point(276, 82);
            this.lbl_duracao.Name = "lbl_duracao";
            this.lbl_duracao.Size = new System.Drawing.Size(396, 18);
            this.lbl_duracao.TabIndex = 3;
            this.lbl_duracao.Text = "Duração: -";
            // 
            // label_qualidade
            // 
            this.label_qualidade.AutoSize = true;
            this.label_qualidade.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_qualidade.ForeColor = System.Drawing.Color.FromArgb(((byte)(212)), ((byte)(212)), ((byte)(216)));
            this.label_qualidade.Location = new System.Drawing.Point(276, 110);
            this.label_qualidade.Name = "label_qualidade";
            this.label_qualidade.Size = new System.Drawing.Size(182, 15);
            this.label_qualidade.TabIndex = 4;
            this.label_qualidade.Text = "Qualidade / Tamanho Estimado:";
            // 
            // cb_qualidades
            // 
            this.cb_qualidades.BackColor = System.Drawing.Color.FromArgb(((byte)(24)), ((byte)(24)), ((byte)(27)));
            this.cb_qualidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_qualidades.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_qualidades.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_qualidades.ForeColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(244)), ((byte)(245)));
            this.cb_qualidades.FormattingEnabled = true;
            this.cb_qualidades.Location = new System.Drawing.Point(276, 130);
            this.cb_qualidades.Name = "cb_qualidades";
            this.cb_qualidades.Size = new System.Drawing.Size(396, 25);
            this.cb_qualidades.TabIndex = 5;
            // 
            // label_formato
            // 
            this.label_formato.AutoSize = true;
            this.label_formato.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_formato.ForeColor = System.Drawing.Color.FromArgb(((byte)(212)), ((byte)(212)), ((byte)(216)));
            this.label_formato.Location = new System.Drawing.Point(14, 175);
            this.label_formato.Name = "label_formato";
            this.label_formato.Size = new System.Drawing.Size(76, 15);
            this.label_formato.TabIndex = 6;
            this.label_formato.Text = "Baixar como:";
            // 
            // rb_video
            // 
            this.rb_video.AutoSize = true;
            this.rb_video.Checked = true;
            this.rb_video.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb_video.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_video.ForeColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(244)), ((byte)(245)));
            this.rb_video.Location = new System.Drawing.Point(100, 172);
            this.rb_video.Name = "rb_video";
            this.rb_video.Size = new System.Drawing.Size(117, 21);
            this.rb_video.TabIndex = 7;
            this.rb_video.TabStop = true;
            this.rb_video.Text = "🎬 Vídeo (MP4)";
            this.rb_video.UseVisualStyleBackColor = true;
            this.rb_video.CheckedChanged += new System.EventHandler(this.rb_formato_CheckedChanged);
            // 
            // rb_audio
            // 
            this.rb_audio.AutoSize = true;
            this.rb_audio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb_audio.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_audio.ForeColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(244)), ((byte)(245)));
            this.rb_audio.Location = new System.Drawing.Point(235, 172);
            this.rb_audio.Name = "rb_audio";
            this.rb_audio.Size = new System.Drawing.Size(117, 21);
            this.rb_audio.TabIndex = 8;
            this.rb_audio.Text = "🎵 Áudio (MP3)";
            this.rb_audio.UseVisualStyleBackColor = true;
            this.rb_audio.CheckedChanged += new System.EventHandler(this.rb_formato_CheckedChanged);
            // 
            // panel_progresso
            // 
            this.panel_progresso.BackColor = System.Drawing.Color.FromArgb(((byte)(39)), ((byte)(39)), ((byte)(42)));
            this.panel_progresso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_progresso.Controls.Add(this.label_status);
            this.panel_progresso.Controls.Add(this.bar_progresso);
            this.panel_progresso.Location = new System.Drawing.Point(16, 360);
            this.panel_progresso.Name = "panel_progresso";
            this.panel_progresso.Size = new System.Drawing.Size(688, 62);
            this.panel_progresso.TabIndex = 6;
            // 
            // label_status
            // 
            this.label_status.AutoEllipsis = true;
            this.label_status.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_status.ForeColor = System.Drawing.Color.FromArgb(((byte)(228)), ((byte)(228)), ((byte)(231)));
            this.label_status.Location = new System.Drawing.Point(8, 7);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(668, 18);
            this.label_status.TabIndex = 0;
            this.label_status.Text = "Status: Aguardando link...";
            // 
            // bar_progresso
            // 
            this.bar_progresso.Location = new System.Drawing.Point(8, 30);
            this.bar_progresso.Name = "bar_progresso";
            this.bar_progresso.Size = new System.Drawing.Size(668, 20);
            this.bar_progresso.TabIndex = 1;
            // 
            // btn_baixar
            // 
            this.btn_baixar.BackColor = System.Drawing.Color.FromArgb(((byte)(225)), ((byte)(29)), ((byte)(72)));
            this.btn_baixar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_baixar.FlatAppearance.BorderSize = 0;
            this.btn_baixar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((byte)(190)), ((byte)(18)), ((byte)(60)));
            this.btn_baixar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(63)), ((byte)(94)));
            this.btn_baixar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_baixar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_baixar.ForeColor = System.Drawing.Color.White;
            this.btn_baixar.Location = new System.Drawing.Point(16, 434);
            this.btn_baixar.Name = "btn_baixar";
            this.btn_baixar.Size = new System.Drawing.Size(688, 48);
            this.btn_baixar.TabIndex = 7;
            this.btn_baixar.Text = "⬇️  INICIAR DOWNLOAD";
            this.btn_baixar.UseVisualStyleBackColor = false;
            this.btn_baixar.Click += new System.EventHandler(this.btn_baixar_Click);
            // 
            // btn_voltar
            // 
            this.btn_voltar.BackColor = System.Drawing.Color.FromArgb(((byte)(39)), ((byte)(39)), ((byte)(42)));
            this.btn_voltar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_voltar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_voltar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_voltar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_voltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_voltar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_voltar.ForeColor = System.Drawing.Color.FromArgb(((byte)(212)), ((byte)(212)), ((byte)(216)));
            this.btn_voltar.Location = new System.Drawing.Point(16, 496);
            this.btn_voltar.Name = "btn_voltar";
            this.btn_voltar.Size = new System.Drawing.Size(100, 38);
            this.btn_voltar.TabIndex = 10;
            this.btn_voltar.Text = "⬅️ Voltar";
            this.btn_voltar.UseVisualStyleBackColor = false;
            this.btn_voltar.Click += new System.EventHandler(this.btn_voltar_Click);
            // 
            // btn_pasta_video
            // 
            this.btn_pasta_video.BackColor = System.Drawing.Color.FromArgb(((byte)(39)), ((byte)(39)), ((byte)(42)));
            this.btn_pasta_video.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_pasta_video.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_pasta_video.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_pasta_video.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_pasta_video.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pasta_video.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_pasta_video.ForeColor = System.Drawing.Color.FromArgb(((byte)(212)), ((byte)(212)), ((byte)(216)));
            this.btn_pasta_video.Location = new System.Drawing.Point(124, 496);
            this.btn_pasta_video.Name = "btn_pasta_video";
            this.btn_pasta_video.Size = new System.Drawing.Size(286, 38);
            this.btn_pasta_video.TabIndex = 8;
            this.btn_pasta_video.Text = "📁 Pasta de Vídeos";
            this.btn_pasta_video.UseVisualStyleBackColor = false;
            this.btn_pasta_video.Click += new System.EventHandler(this.btn_pasta_video_Click);
            // 
            // btn_pasta_audio
            // 
            this.btn_pasta_audio.BackColor = System.Drawing.Color.FromArgb(((byte)(39)), ((byte)(39)), ((byte)(42)));
            this.btn_pasta_audio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_pasta_audio.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_pasta_audio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_pasta_audio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((byte)(63)), ((byte)(63)), ((byte)(70)));
            this.btn_pasta_audio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pasta_audio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_pasta_audio.ForeColor = System.Drawing.Color.FromArgb(((byte)(212)), ((byte)(212)), ((byte)(216)));
            this.btn_pasta_audio.Location = new System.Drawing.Point(418, 496);
            this.btn_pasta_audio.Name = "btn_pasta_audio";
            this.btn_pasta_audio.Size = new System.Drawing.Size(286, 38);
            this.btn_pasta_audio.TabIndex = 9;
            this.btn_pasta_audio.Text = "🎵 Pasta de Áudios";
            this.btn_pasta_audio.UseVisualStyleBackColor = false;
            this.btn_pasta_audio.Click += new System.EventHandler(this.btn_pasta_audio_Click);
            // 
            // TelaVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((byte)(24)), ((byte)(24)), ((byte)(27)));
            this.ClientSize = new System.Drawing.Size(720, 552);
            this.Controls.Add(this.label_titulo_app);
            this.Controls.Add(this.label_subtitulo);
            this.Controls.Add(this.label_link);
            this.Controls.Add(this.text_link);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.panel_preview);
            this.Controls.Add(this.panel_progresso);
            this.Controls.Add(this.btn_baixar);
            this.Controls.Add(this.btn_voltar);
            this.Controls.Add(this.btn_pasta_video);
            this.Controls.Add(this.btn_pasta_audio);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TelaVideo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download de Vídeos e Músicas - VideoDownloader";
            this.panel_preview.ResumeLayout(false);
            this.panel_preview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_thumb)).EndInit();
            this.panel_progresso.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_titulo_app;
        private System.Windows.Forms.Label label_subtitulo;
        private System.Windows.Forms.Label label_link;
        private System.Windows.Forms.TextBox text_link;
        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.Panel panel_preview;
        private System.Windows.Forms.PictureBox pic_thumb;
        private System.Windows.Forms.Label lbl_titulo;
        private System.Windows.Forms.Label lbl_canal;
        private System.Windows.Forms.Label lbl_duracao;
        private System.Windows.Forms.Label label_qualidade;
        private System.Windows.Forms.ComboBox cb_qualidades;
        private System.Windows.Forms.Label label_formato;
        private System.Windows.Forms.RadioButton rb_video;
        private System.Windows.Forms.RadioButton rb_audio;
        private System.Windows.Forms.Panel panel_progresso;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ProgressBar bar_progresso;
        private System.Windows.Forms.Button btn_baixar;
        private System.Windows.Forms.Button btn_voltar;
        private System.Windows.Forms.Button btn_pasta_video;
        private System.Windows.Forms.Button btn_pasta_audio;
    }
}
