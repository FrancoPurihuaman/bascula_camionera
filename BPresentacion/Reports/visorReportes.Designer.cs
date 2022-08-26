namespace BPresentacion.Reports
{
    partial class visorReportes
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
            this.SRResumenDiarioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BinResumenDiarioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SRLiquidacionCajaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BinLiquidacionCajaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.SRResumenDiarioBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinResumenDiarioBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SRLiquidacionCajaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinLiquidacionCajaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // SRResumenDiarioBindingSource
            // 
            this.SRResumenDiarioBindingSource.DataSource = typeof(BSourceReports.SRResumenDiario);
            // 
            // BinResumenDiarioBindingSource
            // 
            this.BinResumenDiarioBindingSource.DataSource = typeof(BSourceReports.BinResumenDiario);
            // 
            // SRLiquidacionCajaBindingSource
            // 
            this.SRLiquidacionCajaBindingSource.DataSource = typeof(BSourceReports.SRLiquidacionCaja);
            // 
            // BinLiquidacionCajaBindingSource
            // 
            this.BinLiquidacionCajaBindingSource.DataSource = typeof(BSourceReports.BinLiquidacionCaja);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.reportViewer1.AutoSize = true;
            reportDataSource1.Name = "sourceReportResumenDiario";
            reportDataSource1.Value = this.SRResumenDiarioBindingSource;
            reportDataSource2.Name = "binSourceResumenDiario";
            reportDataSource2.Value = this.BinResumenDiarioBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BPresentacion.Reports.Report2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(49, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(820, 476);
            this.reportViewer1.TabIndex = 0;
            // 
            // visorReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 479);
            this.Controls.Add(this.reportViewer1);
            this.Name = "visorReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "THOMAS - Reports";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.visorReportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SRResumenDiarioBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinResumenDiarioBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SRLiquidacionCajaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinLiquidacionCajaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource SRResumenDiarioBindingSource;
        private System.Windows.Forms.BindingSource BinResumenDiarioBindingSource;
        private System.Windows.Forms.BindingSource SRLiquidacionCajaBindingSource;
        private System.Windows.Forms.BindingSource BinLiquidacionCajaBindingSource;
    }
}