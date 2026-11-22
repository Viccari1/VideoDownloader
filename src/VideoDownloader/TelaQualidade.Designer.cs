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
            this.pic_thumb.BackColor = System.Drawing.Color.Black;
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
            this.lbl_titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_titulo.Location = new System.Drawing.Point(16, 228);
            this.lbl_titulo.Name = "lbl_titulo";
            this.lbl_titulo.Size = new System.Drawing.Size(370, 36);
            this.lbl_titulo.TabIndex = 1;
            this.lbl_titulo.Text = "Título do Vídeo";
            // 
            // lbl_info
            // 
            this.lbl_info.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_info.Location = new System.Drawing.Point(16, 268);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(370, 20);
            this.lbl_info.TabIndex = 2;
            this.lbl_info.Text = "Canal: ... | Duração: 00:00";
            // 
            // lbl_instrucao
            // 
            this.lbl_instrucao.AutoSize = true;
            this.lbl_instrucao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_instrucao.Location = new System.Drawing.Point(16, 298);
            this.lbl_instrucao.Name = "lbl_instrucao";
            this.lbl_instrucao.Size = new System.Drawing.Size(263, 13);
            this.lbl_instrucao.TabIndex = 3;
            this.lbl_instrucao.Text = "Selecione a resolução desejada (com peso):";
            // 
            // cb_qualidades
            // 
            this.cb_qualidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_qualidades.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_qualidades.FormattingEnabled = true;
            this.cb_qualidades.Location = new System.Drawing.Point(16, 317);
            this.cb_qualidades.Name = "cb_qualidades";
            this.cb_qualidades.Size = new System.Drawing.Size(370, 23);
            this.cb_qualidades.TabIndex = 4;
            // 
            // btn_confirmar
            // 
            this.btn_confirmar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_confirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_confirmar.Location = new System.Drawing.Point(16, 355);
            this.btn_confirmar.Name = "btn_confirmar";
            this.btn_confirmar.Size = new System.Drawing.Size(250, 36);
            this.btn_confirmar.TabIndex = 5;
            this.btn_confirmar.Text = "Baixar nesta qualidade";
            this.btn_confirmar.UseVisualStyleBackColor = true;
            this.btn_confirmar.Click += new System.EventHandler(this.btn_confirmar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(276, 355);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(110, 36);
            this.btn_cancelar.TabIndex = 6;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // TelaQualidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 405);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_confirmar);
            this.Controls.Add(this.cb_qualidades);
            this.Controls.Add(this.lbl_instrucao);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.lbl_titulo);
            this.Controls.Add(this.pic_thumb);
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
