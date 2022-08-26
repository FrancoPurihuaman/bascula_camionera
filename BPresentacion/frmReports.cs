using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BModelo;

namespace BPresentacion
{
    public partial class frmReports : Form
    {
        int selectReporte = 1;
        public frmReports()
        {
            InitializeComponent();
        }

        
        private void btnCancelar_Click(object sender, EventArgs e) => this.Close();

        private void Reports_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
          
            DateTime fecInicio = Convert.ToDateTime(dateInicio.Text);
            DateTime fecFin = Convert.ToDateTime(dateFin.Text);
            string tipoReporte = "";
            

            switch (this.selectReporte)
            {
                case 1:
                    tipoReporte = "RD";
                    break;
                case 2:
                    tipoReporte = "LC";
                    break;
                case 3:
                    tipoReporte = "SV";
                    break;
                default:
                    break;
            }


            if (selectReporte != 0)
            {
                if (tipoReporte == "RD")
                {
                    Reports.visorReportes frmReporteRD = new Reports.visorReportes();
                    frmReporteRD.setSourceReporteDiario(fecInicio, fecFin, "Admin","");
                    frmReporteRD.Show();
                }
                else if (tipoReporte == "LC")
                {
                    Reports.visorReportes frmReporteLC = new Reports.visorReportes();
                    frmReporteLC.setSourceLiquidacionCaja(fecInicio, fecFin, 2, "Admin");
                    frmReporteLC.Show();
                    
                }
                else if (tipoReporte == "SV")
                {
                    string placaIng = txtPlacaR.Text;
                    Reports.visorReportes frmReporteRDT = new Reports.visorReportes();
                    frmReporteRDT.setSourceReporteDiario(fecInicio, fecFin, "Admin", placaIng);
                    frmReporteRDT.Show();
                }
            }
            else MessageBox.Show("Debe de elejir un tipo de reporte");
     
        }
        
        private void cargarDatos()
        {
            cbxUsuarios.Enabled = false;
            txtPlacaR.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                selectReporte = 1;
                cbxUsuarios.Enabled = true;
                cargarUsuarios();
            }
            else
            {
                cbxUsuarios.Enabled = false;
                
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) selectReporte = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                selectReporte = 3;
                txtPlacaR.Enabled = true;
            }
            else txtPlacaR.Enabled = false;
        }

        private void cargarUsuarios()
        {
            Usuario oUsers = new Usuario();

            String[] colums = { "ID_USER", "NOMBRE" };
            DataTable oDataTable = oUsers.select(colums).where("ACTIVO","=",-1).get();

            this.cbxUsuarios.DisplayMember = "NOMBRE";
            this.cbxUsuarios.ValueMember = "ID_USER";
            this.cbxUsuarios.DataSource = oDataTable;
            //this.cbxUsuarios.SelectedIndex = -1;
        }
    }
}
