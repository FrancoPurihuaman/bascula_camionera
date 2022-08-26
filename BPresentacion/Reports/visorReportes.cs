using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BSourceReports;
using Microsoft.Reporting.WinForms;

namespace BPresentacion.Reports
{
    public partial class visorReportes : Form
    {
        public visorReportes()
        {
            InitializeComponent();
        }

        private void visorReportes_Load(object sender, EventArgs e)
        {
            
            
        }
        
        
        public void setSourceReporteDiario(DateTime fechaInicio, DateTime fechaFin, String usuario, String placa)
        {
            reportViewer1.Reset();

            ReportDataSource reportDataSource1 = new ReportDataSource();
            reportDataSource1.Name = "sourceReportResumenDiario";
            reportDataSource1.Value = this.SRResumenDiarioBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);

            ReportDataSource reportDataSource2 = new ReportDataSource();
            reportDataSource2.Name = "binSourceResumenDiario";
            reportDataSource2.Value = this.BinResumenDiarioBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BPresentacion.Reports.Report2.rdlc";

            SRResumenDiario oSRResumenDiario = new SRResumenDiario(fechaInicio, fechaFin, usuario, placa);
            if (placa == "")
            {
                oSRResumenDiario.getReportResumenDiario();
            }
            else
            {
                oSRResumenDiario.getReportResumenDiarioxMarca();
            }
            

            SRResumenDiarioBindingSource.DataSource = oSRResumenDiario;
            BinResumenDiarioBindingSource.DataSource = oSRResumenDiario.listBinResumenDiario;
            this.reportViewer1.RefreshReport();
        }

        public void setSourceLiquidacionCaja(DateTime fechaInicio, DateTime fechaFin, int idUsuario, String usuario)
        {
            reportViewer1.Reset();

            ReportDataSource reportDataSource1 = new ReportDataSource();
            reportDataSource1.Name = "dsLiquidacionCaja";
            reportDataSource1.Value = this.SRLiquidacionCajaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);

            ReportDataSource reportDataSource2 = new ReportDataSource();
            reportDataSource2.Name = "binLiquidacionCaja";
            reportDataSource2.Value = this.BinLiquidacionCajaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BPresentacion.Reports.Report3.rdlc";

            SRLiquidacionCaja oSRLiquidacionCaja = new SRLiquidacionCaja(fechaInicio, fechaFin, idUsuario, usuario);
            oSRLiquidacionCaja.getReportLiquidacionCaja();

            SRLiquidacionCajaBindingSource.DataSource = oSRLiquidacionCaja;
            BinLiquidacionCajaBindingSource.DataSource = oSRLiquidacionCaja.listBinLiquidacionCaja;
            this.reportViewer1.RefreshReport();
        }
    }
}
