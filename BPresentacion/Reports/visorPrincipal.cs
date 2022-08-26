using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using BPresentacion.libraries;
using BSourceReports;
using Microsoft.Reporting.WinForms;

namespace BPresentacion.Reports
{
    public partial class visorPrincipal : Form
    {
        private int ticket;
        private string tipo, metodo;
        public visorPrincipal( int codigo,string valor,string valor2)
        {
            InitializeComponent();
            this.ticket = codigo;
            this.tipo = valor;
            this.metodo = valor2;
        }

        private void visorPrincipal_Load(object sender, EventArgs e)
        {
            
        }

        public void imprimirMatricialEntrada()
        {
            
            try
            {
                LocalReport oLocalReport = new LocalReport();
                oLocalReport.ReportPath = @"..\..\Reports\reportPrincipal.rdlc";

                // Si pasa algun error puede ser por no encontrar la direccion exacta del reporte, toma este.
                oLocalReport.ReportEmbeddedResource = "BPresentacion.Reports.reportPrincipal.rdlc";

                SourReportPrincipal oPrueba = new SourReportPrincipal(this.ticket);
                oPrueba.getPesoGeneral();

                // ENVIANDO PARAMETROS PARA LA PRIMERA PESADA
                ReportParameter ticket = new ReportParameter("ticket", oPrueba.ticket.ToString("D9"));
                ReportParameter placa = new ReportParameter("placa", Convert.ToString(oPrueba.placa));
                ReportParameter fechaE = new ReportParameter("fechaE", Convert.ToString(oPrueba.fechaEnt));
                ReportParameter pesoE = new ReportParameter("pesoE", Convert.ToString(oPrueba.pesoE));
                ReportParameter importe = new ReportParameter("importe", Convert.ToString(oPrueba.importe));
                ReportParameter usuario = new ReportParameter("usuario", Convert.ToString(oPrueba.usuario));
                ReportParameter observacion = new ReportParameter("observacion", Convert.ToString(oPrueba.observacion));


                // INDICAREMOS COMO DESEA LA IMPRESION D: DIRECTO O V: VISOR
                if (this.metodo == "D")
                {
                    oLocalReport.SetParameters(new ReportParameter[] { ticket, placa, fechaE, 
                                                                        pesoE,  importe, usuario, observacion });
                    PrinterDirectory oPrinterDirectory = new PrinterDirectory();
                    oPrinterDirectory.Imprime(oLocalReport);
                }
                else
                {
                    visorReportPrincipal.LocalReport.SetParameters(new ReportParameter[] { ticket, placa, fechaE, 
                                                                                            pesoE, importe, usuario, observacion });

                    this.visorReportPrincipal.RefreshReport();
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tickets registrado. \n" + ex.Message);
            }
        }
        public void imprimirMatricialSalida()
        {
            
            try
            {
                LocalReport oLocalReport = new LocalReport();
                oLocalReport.ReportPath = @"..\..\Reports\reportPrincipalSal.rdlc"; //direccion absoluta del reporte.

                // si pasa algun error puede ser por no encontrar la direccion exacta del reporte, toma este.
                oLocalReport.ReportEmbeddedResource = "BPresentacion.Reports.reportPrincipalSal.rdlc";

                SourReportPrincipal oPrueba = new SourReportPrincipal(this.ticket);
                oPrueba.getPesoGeneral();

                // ENVIANDO PARAMETROS PARA LA PRIMERA PESADA
                ReportParameter ticket = new ReportParameter("ticket", oPrueba.ticket.ToString("D9"));
                ReportParameter placa = new ReportParameter("placa", Convert.ToString(oPrueba.placa));
                ReportParameter fechaE = new ReportParameter("fechaE", Convert.ToString(oPrueba.fechaEnt));
                ReportParameter pesoE = new ReportParameter("pesoE", Convert.ToString(oPrueba.pesoE));
                ReportParameter importe = new ReportParameter("importe", Convert.ToString(oPrueba.importe));
                ReportParameter usuario = new ReportParameter("usuario", Convert.ToString(oPrueba.usuario));
                ReportParameter observacion = new ReportParameter("observacion", Convert.ToString(oPrueba.observacion));

                //// ENVIANDO PARAMETROS PARA LA SEGUNDA PESADA
                ReportParameter fechaS = new ReportParameter("fechaS", verificarFechaSalida(oPrueba.fechaSal));
                ReportParameter pesoS = new ReportParameter("pesoS", Convert.ToString(oPrueba.pesoS));
                

                // INDICAREMOS COMO DESEA LA IMPRESION D: DIRECTO O V: VISOR
                if (this.metodo == "D")
                {
                    oLocalReport.SetParameters(new ReportParameter[] { ticket, placa, fechaE, fechaS,
                                                                        pesoE, pesoS, importe, usuario, observacion });
                    PrinterDirectory oPrinterDirectory = new PrinterDirectory();
                    oPrinterDirectory.Imprime(oLocalReport);
                }
                else
                {
                    // Limpio la fuente actual
                    this.visorReportPrincipal.Reset();

                    // Asigno un nombre la fuente del reporte
                    this.visorReportPrincipal.LocalReport.ReportEmbeddedResource = "BPresentacion.Reports.reportPrincipalSal.rdlc";

                    // ENVIO LOS DATOS DE MANERA DE PARAMETROS
                    visorReportPrincipal.LocalReport.SetParameters(new ReportParameter[] { ticket, placa, fechaE, fechaS,
                                                                                            pesoE, pesoS, importe, usuario, observacion });

                    this.visorReportPrincipal.RefreshReport();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tickets registrado - " + ex.Message);
            }

        }
        private String verificarFechaSalida(DateTime oDateTime)
        {
            String fechaSalida;

            DateTime oDataTimeAux = new DateTime(1500, 1, 1, 1, 1, 1);
            if (oDateTime == oDataTimeAux)
            {
                fechaSalida = " ";
            }else{
                fechaSalida = Convert.ToString(oDateTime);
            }

            return fechaSalida;
        }
        

    }
}
