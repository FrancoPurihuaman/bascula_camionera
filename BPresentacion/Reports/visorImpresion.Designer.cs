namespace BPresentacion.Reports
{
    partial class visorImpresion
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewerImpre = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SRPrimerPesadaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BinPrimerPesadaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BinSegundaPesadaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SRPrimerPesadaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinPrimerPesadaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinSegundaPesadaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewerImpre
            // 
            this.reportViewerImpre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewerImpre.DocumentMapWidth = 1;
            reportDataSource1.Name = "dsPrimerPesada";
            reportDataSource1.Value = this.SRPrimerPesadaBindingSource;
            reportDataSource2.Name = "dsBinPrimerPesada";
            reportDataSource2.Value = this.BinPrimerPesadaBindingSource;
            this.reportViewerImpre.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewerImpre.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewerImpre.LocalReport.ReportEmbeddedResource = "BPresentacion.Reports.impPrimerPesa.rdlc";
            this.reportViewerImpre.Location = new System.Drawing.Point(71, 1);
            this.reportViewerImpre.Name = "reportViewerImpre";
            this.reportViewerImpre.ServerReport.BearerToken = null;
            this.reportViewerImpre.Size = new System.Drawing.Size(809, 500);
            this.reportViewerImpre.TabIndex = 0;
            // 
            // SRPrimerPesadaBindingSource
            // 
            this.SRPrimerPesadaBindingSource.DataSource = typeof(BSourceReports.SRPrimerPesada);
            // 
            // BinPrimerPesadaBindingSource
            // 
            this.BinPrimerPesadaBindingSource.DataSource = typeof(BSourceReports.BinPrimerPesada);
            // 
            // BinSegundaPesadaBindingSource
            // 
            this.BinSegundaPesadaBindingSource.DataSource = typeof(BSourceReports.BinSegundaPesada);
            // 
            // visorImpresion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 462);
            this.Controls.Add(this.reportViewerImpre);
            this.Name = "visorImpresion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visor de Impresion";
            this.Load += new System.EventHandler(this.visorImpresion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SRPrimerPesadaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinPrimerPesadaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinSegundaPesadaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerImpre;
        private System.Windows.Forms.BindingSource SRPrimerPesadaBindingSource;
        private System.Windows.Forms.BindingSource BinPrimerPesadaBindingSource;
        private System.Windows.Forms.BindingSource BinSegundaPesadaBindingSource;
    }
}