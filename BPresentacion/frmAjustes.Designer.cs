namespace BPresentacion
{
    partial class frmAjustes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAjustes));
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnConfigAjustes = new System.Windows.Forms.Button();
            this.btnConfigUsuarios = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelBotones.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.DarkRed;
            this.panelBotones.Controls.Add(this.btnConfigAjustes);
            this.panelBotones.Controls.Add(this.btnConfigUsuarios);
            this.panelBotones.Controls.Add(this.label5);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBotones.Location = new System.Drawing.Point(0, 0);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(140, 344);
            this.panelBotones.TabIndex = 0;
            // 
            // btnConfigAjustes
            // 
            this.btnConfigAjustes.BackColor = System.Drawing.Color.Firebrick;
            this.btnConfigAjustes.FlatAppearance.BorderColor = System.Drawing.Color.Firebrick;
            this.btnConfigAjustes.FlatAppearance.BorderSize = 0;
            this.btnConfigAjustes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigAjustes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfigAjustes.ForeColor = System.Drawing.Color.White;
            this.btnConfigAjustes.Image = ((System.Drawing.Image)(resources.GetObject("btnConfigAjustes.Image")));
            this.btnConfigAjustes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfigAjustes.Location = new System.Drawing.Point(1, 115);
            this.btnConfigAjustes.Name = "btnConfigAjustes";
            this.btnConfigAjustes.Size = new System.Drawing.Size(138, 39);
            this.btnConfigAjustes.TabIndex = 2;
            this.btnConfigAjustes.Text = "     Impresora";
            this.btnConfigAjustes.UseVisualStyleBackColor = false;
            this.btnConfigAjustes.Click += new System.EventHandler(this.btnConfigAjustes_Click);
            // 
            // btnConfigUsuarios
            // 
            this.btnConfigUsuarios.BackColor = System.Drawing.Color.Firebrick;
            this.btnConfigUsuarios.FlatAppearance.BorderColor = System.Drawing.Color.Firebrick;
            this.btnConfigUsuarios.FlatAppearance.BorderSize = 0;
            this.btnConfigUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfigUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnConfigUsuarios.Image = ((System.Drawing.Image)(resources.GetObject("btnConfigUsuarios.Image")));
            this.btnConfigUsuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfigUsuarios.Location = new System.Drawing.Point(1, 75);
            this.btnConfigUsuarios.Name = "btnConfigUsuarios";
            this.btnConfigUsuarios.Size = new System.Drawing.Size(138, 39);
            this.btnConfigUsuarios.TabIndex = 1;
            this.btnConfigUsuarios.Text = "   Usuarios";
            this.btnConfigUsuarios.UseVisualStyleBackColor = false;
            this.btnConfigUsuarios.Click += new System.EventHandler(this.btnConfigUsuarios_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "Ajustes";
            // 
            // panelContenedor
            // 
            this.panelContenedor.BackColor = System.Drawing.Color.Firebrick;
            this.panelContenedor.Controls.Add(this.pictureBox1);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelContenedor.Location = new System.Drawing.Point(140, 0);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(642, 344);
            this.panelContenedor.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(115, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(412, 242);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmAjustes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 344);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelBotones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAjustes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " THOMAS - Sistema de Pesaje Computarizado";
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.panelContenedor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnConfigAjustes;
        private System.Windows.Forms.Button btnConfigUsuarios;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}