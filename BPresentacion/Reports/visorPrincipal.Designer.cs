namespace BPresentacion.Reports
{
    partial class visorPrincipal
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
            this.visorReportPrincipal = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // visorReportPrincipal
            // 
            this.visorReportPrincipal.LocalReport.ReportEmbeddedResource = "BPresentacion.Reports.reportPrincipal.rdlc";
            this.visorReportPrincipal.Location = new System.Drawing.Point(60, 3);
            this.visorReportPrincipal.Name = "visorReportPrincipal";
            this.visorReportPrincipal.ServerReport.BearerToken = null;
            this.visorReportPrincipal.Size = new System.Drawing.Size(776, 500);
            this.visorReportPrincipal.TabIndex = 0;
            // 
            // visorPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 462);
            this.Controls.Add(this.visorReportPrincipal);
            this.Name = "visorPrincipal";
            this.Text = "Thomas: COMPROBANTE";
            this.Load += new System.EventHandler(this.visorPrincipal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer visorReportPrincipal;
    }
}