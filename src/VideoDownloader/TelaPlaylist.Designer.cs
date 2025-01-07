namespace VideoDownloader
{
    partial class TelaPlaylist
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.text_link = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_status = new System.Windows.Forms.Label();
            this.bar_progresso = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.rb_audio = new System.Windows.Forms.RadioButton();
            this.rb_video = new System.Windows.Forms.RadioButton();
            this.btn_baixar = new System.Windows.Forms.Button();
            this.btn_pasta_audio = new System.Windows.Forms.Button();
            this.btn_pasta_video = new System.Windows.Forms.Button();
            this.btn_voltar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rb_alta = new System.Windows.Forms.RadioButton();
            this.rb_baixa = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(545, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Baixar Playlist";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Link da Playlist:";
            // 
            // text_link
            // 
            this.text_link.Location = new System.Drawing.Point(98, 107);
            this.text_link.Name = "text_link";
            this.text_link.Size = new System.Drawing.Size(456, 20);
            this.text_link.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.label_status);
            this.panel1.Controls.Add(this.bar_progresso);
            this.panel1.Location = new System.Drawing.Point(15, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 58);
            this.panel1.TabIndex = 3;
            // 
            // label_status
            // 
            this.label_status.Location = new System.Drawing.Point(3, 9);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(536, 20);
            this.label_status.TabIndex = 1;
            this.label_status.Text = "Status: Inativo";
            // 
            // bar_progresso
            // 
            this.bar_progresso.Location = new System.Drawing.Point(3, 32);
            this.bar_progresso.Name = "bar_progresso";
            this.bar_progresso.Size = new System.Drawing.Size(536, 23);
            this.bar_progresso.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Baixar como:";
            // 
            // rb_audio
            // 
            this.rb_audio.AutoSize = true;
            this.rb_audio.Location = new System.Drawing.Point(65, 0);
            this.rb_audio.Name = "rb_audio";
            this.rb_audio.Size = new System.Drawing.Size(57, 17);
            this.rb_audio.TabIndex = 5;
            this.rb_audio.Tag = "1";
            this.rb_audio.Text = "Audios";
            this.rb_audio.UseVisualStyleBackColor = true;
            // 
            // rb_video
            // 
            this.rb_video.AutoSize = true;
            this.rb_video.Checked = true;
            this.rb_video.Location = new System.Drawing.Point(3, 0);
            this.rb_video.Name = "rb_video";
            this.rb_video.Size = new System.Drawing.Size(57, 17);
            this.rb_video.TabIndex = 6;
            this.rb_video.TabStop = true;
            this.rb_video.Tag = "1";
            this.rb_video.Text = "Videos";
            this.rb_video.UseVisualStyleBackColor = true;
            this.rb_video.CheckedChanged += new System.EventHandler(this.rb_video_CheckedChanged);
            // 
            // btn_baixar
            // 
            this.btn_baixar.Location = new System.Drawing.Point(12, 196);
            this.btn_baixar.Name = "btn_baixar";
            this.btn_baixar.Size = new System.Drawing.Size(542, 54);
            this.btn_baixar.TabIndex = 7;
            this.btn_baixar.Text = "Baixar playlist";
            this.btn_baixar.UseVisualStyleBackColor = true;
            this.btn_baixar.Click += new System.EventHandler(this.btn_baixar_Click);
            // 
            // btn_pasta_audio
            // 
            this.btn_pasta_audio.Location = new System.Drawing.Point(340, 257);
            this.btn_pasta_audio.Name = "btn_pasta_audio";
            this.btn_pasta_audio.Size = new System.Drawing.Size(214, 51);
            this.btn_pasta_audio.TabIndex = 8;
            this.btn_pasta_audio.Text = "Mostrar pasta de audios";
            this.btn_pasta_audio.UseVisualStyleBackColor = true;
            this.btn_pasta_audio.Click += new System.EventHandler(this.btn_pasta_audio_Click);
            // 
            // btn_pasta_video
            // 
            this.btn_pasta_video.Location = new System.Drawing.Point(120, 256);
            this.btn_pasta_video.Name = "btn_pasta_video";
            this.btn_pasta_video.Size = new System.Drawing.Size(214, 51);
            this.btn_pasta_video.TabIndex = 9;
            this.btn_pasta_video.Text = "Mostrar pasta de videos";
            this.btn_pasta_video.UseVisualStyleBackColor = true;
            this.btn_pasta_video.Click += new System.EventHandler(this.btn_pasta_video_Click);
            // 
            // btn_voltar
            // 
            this.btn_voltar.Location = new System.Drawing.Point(12, 256);
            this.btn_voltar.Name = "btn_voltar";
            this.btn_voltar.Size = new System.Drawing.Size(102, 51);
            this.btn_voltar.TabIndex = 10;
            this.btn_voltar.Text = "Voltar";
            this.btn_voltar.UseVisualStyleBackColor = true;
            this.btn_voltar.Click += new System.EventHandler(this.btn_voltar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Qual a qualidade dos videos:";
            // 
            // rb_alta
            // 
            this.rb_alta.AutoSize = true;
            this.rb_alta.Checked = true;
            this.rb_alta.Location = new System.Drawing.Point(158, 0);
            this.rb_alta.Name = "rb_alta";
            this.rb_alta.Size = new System.Drawing.Size(43, 17);
            this.rb_alta.TabIndex = 12;
            this.rb_alta.TabStop = true;
            this.rb_alta.Text = "Alta";
            this.rb_alta.UseVisualStyleBackColor = true;
            // 
            // rb_baixa
            // 
            this.rb_baixa.AutoSize = true;
            this.rb_baixa.Location = new System.Drawing.Point(207, 0);
            this.rb_baixa.Name = "rb_baixa";
            this.rb_baixa.Size = new System.Drawing.Size(51, 17);
            this.rb_baixa.TabIndex = 13;
            this.rb_baixa.Text = "Baixa";
            this.rb_baixa.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rb_video);
            this.panel2.Controls.Add(this.rb_audio);
            this.panel2.Location = new System.Drawing.Point(86, 133);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(124, 20);
            this.panel2.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.rb_alta);
            this.panel3.Controls.Add(this.rb_baixa);
            this.panel3.Location = new System.Drawing.Point(12, 159);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(261, 18);
            this.panel3.TabIndex = 15;
            // 
            // TelaPlaylist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 319);
            this.Controls.Add(this.btn_voltar);
            this.Controls.Add(this.btn_pasta_video);
            this.Controls.Add(this.btn_pasta_audio);
            this.Controls.Add(this.btn_baixar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.text_link);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Name = "TelaPlaylist";
            this.Text = "Baixador de Playlists";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_link;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ProgressBar bar_progresso;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rb_audio;
        private System.Windows.Forms.Button btn_baixar;
        private System.Windows.Forms.Button btn_pasta_audio;
        private System.Windows.Forms.Button btn_pasta_video;
        private System.Windows.Forms.Button btn_voltar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rb_video;
        private System.Windows.Forms.RadioButton rb_alta;
        private System.Windows.Forms.RadioButton rb_baixa;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}