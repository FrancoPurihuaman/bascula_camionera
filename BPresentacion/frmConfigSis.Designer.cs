namespace BPresentacion
{
    partial class frmConfigSis
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnIntegrar = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.cbxPrinterLocal = new System.Windows.Forms.ComboBox();
            this.cbxIntegrar = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnIntegrar);
            this.groupBox1.Controls.Add(this.btnAplicar);
            this.groupBox1.Controls.Add(this.cbxPrinterLocal);
            this.groupBox1.Controls.Add(this.cbxIntegrar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(29, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 259);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configurar";
            // 
            // btnIntegrar
            // 
            this.btnIntegrar.BackColor = System.Drawing.Color.Teal;
            this.btnIntegrar.Enabled = false;
            this.btnIntegrar.FlatAppearance.BorderSize = 0;
            this.btnIntegrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIntegrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIntegrar.ForeColor = System.Drawing.Color.White;
            this.btnIntegrar.Location = new System.Drawing.Point(49, 196);
            this.btnIntegrar.Name = "btnIntegrar";
            this.btnIntegrar.Size = new System.Drawing.Size(169, 27);
            this.btnIntegrar.TabIndex = 9;
            this.btnIntegrar.Text = "Integrar";
            this.btnIntegrar.UseVisualStyleBackColor = false;
            this.btnIntegrar.Click += new System.EventHandler(this.btnIntegrar_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.BackColor = System.Drawing.Color.Teal;
            this.btnAplicar.FlatAppearance.BorderSize = 0;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.ForeColor = System.Drawing.Color.White;
            this.btnAplicar.Location = new System.Drawing.Point(49, 88);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(169, 27);
            this.btnAplicar.TabIndex = 6;
            this.btnAplicar.Text = "Aplicar Cambios";
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // cbxPrinterLocal
            // 
            this.cbxPrinterLocal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPrinterLocal.FormattingEnabled = true;
            this.cbxPrinterLocal.Location = new System.Drawing.Point(19, 58);
            this.cbxPrinterLocal.Name = "cbxPrinterLocal";
            this.cbxPrinterLocal.Size = new System.Drawing.Size(221, 24);
            this.cbxPrinterLocal.TabIndex = 3;
            this.cbxPrinterLocal.SelectedIndexChanged += new System.EventHandler(this.cbxPrinterLocal_SelectedIndexChanged);
            // 
            // cbxIntegrar
            // 
            this.cbxIntegrar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxIntegrar.Enabled = false;
            this.cbxIntegrar.FormattingEnabled = true;
            this.cbxIntegrar.Location = new System.Drawing.Point(19, 166);
            this.cbxIntegrar.Name = "cbxIntegrar";
            this.cbxIntegrar.Size = new System.Drawing.Size(221, 24);
            this.cbxIntegrar.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccionar:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Buscar:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BPresentacion.Properties.Resources.printer_6029;
            this.pictureBox1.Location = new System.Drawing.Point(295, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(212, 158);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // frmConfigSis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Firebrick;
            this.ClientSize = new System.Drawing.Size(626, 294);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfigSis";
            this.Text = "frmConfigSis";
            this.Load += new System.EventHandler(this.frmConfigSis_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxPrinterLocal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnIntegrar;
        private System.Windows.Forms.ComboBox cbxIntegrar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}