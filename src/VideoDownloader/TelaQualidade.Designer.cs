namespace VideoDownloader
{
    partial class TelaQualidade
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
            this.pic_thumb = new System.Windows.Forms.PictureBox();
            this.lbl_titulo = new System.Windows.Forms.Label();
            this.lbl_info = new System.Windows.Forms.Label();
            this.lbl_instrucao = new System.Windows.Forms.Label();
            this.cb_qualidades = new System.Windows.Forms.ComboBox();
            this.btn_confirmar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pic_thumb)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_thumb
            // 
            this.pic_thumb.BackColor = System.Drawing.Color.FromArgb(((byte)(31)), ((byte)(31)), ((byte)(35)));
            this.pic_thumb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_thumb.Location = new System.Drawing.Point(16, 12);
            this.pic_thumb.Name = "pic_thumb";
            this.pic_thumb.Size = new System.Drawing.Size(370, 208);
            this.pic_thumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_thumb.TabIndex = 0;
            this.pic_thumb.TabStop = false;
            // 
            // lbl_titulo
            // 
            this.lbl_titulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_titulo.ForeColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(244)), ((byte)(245)));
            this.lbl_titulo.Location = new System.Drawing.Point(16, 228);
            this.lbl_titulo.Name = "lbl_titulo";
            this.lbl_titulo.Size = new System.Drawing.Size(370, 40);
            this.lbl_titulo.TabIndex = 1;
            this.lbl_titulo.Text = "Carregando título do vídeo...";
            // 
            // lbl_info
            // 
            this.lbl_info.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_info.ForeColor = System.Drawing.Color.FromArgb(((byte)(161)), ((byte)(161)), ((byte)(170)));
            this.lbl_info.Location = new System.Drawing.Point(16, 270);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(370, 20);
            this.lbl_info.TabIndex = 2;
            this.lbl_info.Text = "Canal: ... | Duração: 00:00";
            // 
            // lbl_instrucao
            // 
            this.lbl_instrucao.AutoSize = true;
            this.lbl_instrucao.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_instrucao.ForeColor = System.Drawing.Color.FromArgb(((byte)(228)), ((byte)(228)), ((byte)(231)));
            this.lbl_instrucao.Location = new System.Drawing.Point(16, 298);
            this.lbl_instrucao.Name = "lbl_instrucao";
            this.lbl_instrucao.Size = new System.Drawing.Size(252, 15);
            this.lbl_instrucao.TabIndex = 3;
            this.lbl_instrucao.Text = "Selecione a resolução desejada (com peso):";
            // 
            // cb_qualidades
            // 
            this.cb_qualidades.BackColor = System.Drawing.Color.FromArgb(((byte)(39)), ((byte)(39)), ((byte)(42)));
            this.cb_qualidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_qualidades.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_qualidades.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_qualidades.ForeColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(244)), ((byte)(245)));
            this.cb_qualidades.FormattingEnabled = true;
            this.cb_qualidades.Location = new System.Drawing.Point(16, 318);
            this.cb_qualidades.Name = "cb_qualidades";
            this.cb_qualidades.Size = new System.Drawing.Size(370, 25);
            this.cb_qualidades.TabIndex = 4;
            // 
            // btn_confirmar
            // 
            this.btn_confirmar.BackColor = System.Drawing.Color.FromArgb(((byte)(225)), ((byte)(29)), ((byte)(72)));
            this.btn_confirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_confirmar.FlatAppearance.BorderSize = 0;
            this.btn_confirmar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((byte)(190)), ((byte)(18)), ((byte)(60)));
            this.btn_confirmar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((byte)(244)), ((byte)(63)), ((byte)(94)));
            this.btn_confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_confirmar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_confirmar.ForeColor = System.Drawing.Color.White;
            this.btn_confirmar.Location = new System.Drawing.Point(16, 356);
            this.btn_confirmar.Name = "btn_confirmar";
            this.btn_confirmar.Size = new System.Drawing.Size(255, 38);
            this.btn_confirmar.TabIndex = 5;
            this.btn_confirmar.Text = "⬇️  BAIXAR NESTA QUALIDADE";
            this.btn_confirmar.UseVisualStyleBackColor = false;
            this.btn_confirmar.Click += new System.EventHandler(this.btn_confirmar_Click);
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
            this.btn_cancelar.Location = new System.Drawing.Point(281, 356);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(105, 38);
            this.btn_cancelar.TabIndex = 6;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // TelaQualidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((byte)(24)), ((byte)(24)), ((byte)(27)));
            this.ClientSize = new System.Drawing.Size(402, 408);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_confirmar);
            this.Controls.Add(this.cb_qualidades);
            this.Controls.Add(this.lbl_instrucao);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.lbl_titulo);
            this.Controls.Add(this.pic_thumb);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaQualidade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detalhes do Vídeo e Resolução";
            ((System.ComponentModel.ISupportInitialize)(this.pic_thumb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_thumb;
        private System.Windows.Forms.Label lbl_titulo;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.Label lbl_instrucao;
        private System.Windows.Forms.ComboBox cb_qualidades;
        private System.Windows.Forms.Button btn_confirmar;
        private System.Windows.Forms.Button btn_cancelar;
    }
}
