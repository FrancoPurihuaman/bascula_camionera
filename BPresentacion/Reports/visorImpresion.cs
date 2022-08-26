using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using BPresentacion.libraries;
using BSourceReports;
using Microsoft.Reporting.WinForms;

namespace BPresentacion.Reports
{
    public partial class visorImpresion : Form
    {
        private int ticket;
        private string tipo;
        public visorImpresion(string type, int codigo)
        {
            InitializeComponent();
            this.ticket = codigo;
            this.tipo = type;
        }

        private void visorImpresion_Load(object sender, EventArgs e)
        {
            if (this.tipo == "E")
                setSourceVisorImpreEnt();
            else if (this.tipo == "S")
                setSourceVisorImpreSal();
            
        }

        public void setSourceVisorImpreEnt()
        {

            SRPrimerPesada oSRPrimer = new SRPrimerPesada(this.ticket);
            oSRPrimer.getPrimerPesada();

            SRPrimerPesadaBindingSource.DataSource = oSRPrimer;
            BinPrimerPesadaBindingSource.DataSource = oSRPrimer.listBinPrimerPesada;

            this.reportViewerImpre.RefreshReport();
        }
        private void setSourceVisorImpreSal()
        {
            this.reportViewerImpre.Reset();
            // CREO UNA NUEVA INSTANCIA PARA MOSTRAR EL SEGUNDO REPORTE
            ReportDataSource reportDataSource2 = new ReportDataSource();

            // 
            reportDataSource2.Name = "dsBinSegundaPesada";
            reportDataSource2.Value = this.BinSegundaPesadaBindingSource;
            this.reportViewerImpre.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewerImpre.LocalReport.ReportEmbeddedResource = "BPresentacion.Reports.impSeguPesa.rdlc";

            // ALMACENOS TODOS LOS DATOS A LOS PARAMETROS
            SRSegundaPesada oSRSegunda = new SRSegundaPesada(this.ticket);
            oSRSegunda.getSegundapesada();

            BinSegundaPesadaBindingSource.DataSource = oSRSegunda.listBinSegundaPesada;
            this.reportViewerImpre.RefreshReport();
        }

        public void imprimirDirectoEntradaImpresoraMatricial()
        {
            try
            {
                LocalReport oLocalReport = new LocalReport();
                oLocalReport.ReportPath = @"..\..\Reports\PrimerPesada.rdlc";

                SoursTicketDirectory oPrueba = new SoursTicketDirectory(this.ticket);
                oPrueba.getPrimerPesada();

                
                ReportParameter ticket = new ReportParameter("ticket", oPrueba.ticket.ToString("D8"));
                ReportParameter placa = new ReportParameter("placa", Convert.ToString(oPrueba.placa));
                ReportParameter fechaEnt = new ReportParameter("fechaEnt", Convert.ToString(oPrueba.fechaEnt));
                ReportParameter importe = new ReportParameter("importe", Convert.ToString(oPrueba.importe));
                ReportParameter pesoE = new ReportParameter("pesoE", Convert.ToString(oPrueba.pesoE));
                ReportParameter observacion = new ReportParameter("observacion", Convert.ToString(oPrueba.observacion));

                oLocalReport.SetParameters(new ReportParameter[] { ticket, placa, fechaEnt, importe, pesoE, observacion });
                PrinterDirectory oPrinterDirectory = new PrinterDirectory();
                oPrinterDirectory.Imprime(oLocalReport);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void imprimirDirectoSalidaImpresoraMatricial()
        {
            try
            {
                LocalReport oLocalReport = new LocalReport();
                oLocalReport.ReportPath = @"..\..\Reports\SegundaPesada.rdlc";

                SoursTicketDirectory oPrueba = new SoursTicketDirectory(this.ticket);
                oPrueba.getSegundaPesada();

                ReportParameter ticket = new ReportParameter("ticket", oPrueba.ticket.ToString("D8"));
                ReportParameter placa = new ReportParameter("placa", Convert.ToString(oPrueba.placa));
                ReportParameter fechaSal = new ReportParameter("fechaSal", Convert.ToString(oPrueba.fechaSal));
                ReportParameter importe = new ReportParameter("importe", Convert.ToString(oPrueba.importe));
                ReportParameter pesoE = new ReportParameter("pesoEnt", Convert.ToString(oPrueba.pesoE));
                ReportParameter pesoS = new ReportParameter("pesoSal", Convert.ToString(oPrueba.pesoS));
                oLocalReport.SetParameters(new ReportParameter[] { ticket, placa, fechaSal, importe, pesoE, pesoS });
                PrinterDirectory oPrinterDirectory = new PrinterDirectory();
                oPrinterDirectory.Imprime(oLocalReport);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
 }
