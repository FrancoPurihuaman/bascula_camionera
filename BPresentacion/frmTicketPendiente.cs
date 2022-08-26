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
    public partial class frmTicketPendiente : Form
    {
        public frmTicketPendiente()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTicketPendiente_Load(object sender, EventArgs e)
        {
            cargarVentasPendientes();
        }
        private void cargarVentasPendientes()
        {
            Venta listarVentas = new Venta();
            String[] columnas = { "VEN.ID_TKT AS TICKET"
                                   , "VEN.PLACA"
                                   , "FORMAT(VEN.FEC_ENTRADA, 'dd-mm-yyyy hh:mm AM/PM') AS FEC_ENTRADA"
                                   , "FORMAT(VEN.PESO_ENTRADA, '##,##0') AS P_ENTRADA"
                                   , "PREC.MONTO", "VEN.OBSERVACION" };

            DataTable dt = listarVentas.select(columnas)
                                        .join("PRECIO PREC", "PREC.ID_PRECIO", "=", "VEN.ID_PRECIO")
                                        .where("VEN.PENDIENTE", "=", -1)
                                        .where("ANULADO", "=", false)
                                        .orderBy("VEN.FEC_ENTRADA", "DESC").get();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Error al cargar los datos.");
            }
            else
            {
                this.dgvListarPendientes.DataSource = dt;
            }


        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            ObtenerDatos();   
        }

        private void dgvListarPendientes_DoubleClick(object sender, EventArgs e) 
        {

            ObtenerDatos();

        }

        private void ObtenerDatos()
        {

            if (this.dgvListarPendientes.SelectedRows.Count > 0)
            {

                int id_tkt = (int)dgvListarPendientes.CurrentRow.Cells[0].Value;

                Venta oVenta = new Venta();
                String[] columnas = { "VEN.ID_TKT", "VEN.PLACA", "VEN.PESO_ENTRADA", "VEN.OBSERVACION"
                                    , "PREC.ID_PRECIO", "PREC.MONTO"
                                    , "PROD.ID_PROD", "PROD.NOMBRE AS PROD_NOMBRE"
                                    , "CLI.ID_CLI", "CLI.NOMBRE AS CLI_NOMBRE"};

                DataTable oDataTable = oVenta.select(columnas)
                                            .join("PRECIO PREC", "VEN.ID_PRECIO", "=", "PREC.ID_PRECIO")
                                            .join("PRODUCTO PROD", "VEN.ID_PRODUCTO", "=", "PROD.ID_PROD")
                                            .join("CLIENTE CLI", "VEN.ID_CLIENTE", "=", "CLI.ID_CLI")
                                            .where("VEN.ID_TKT", "=", id_tkt).get();

                this.agregarDatosFormPrincipal(oDataTable);

                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccione un Ticket.");
            }
        }

        private void agregarDatosFormPrincipal(DataTable oDataTable)
        {
            FrmPrincipal frm = Owner as FrmPrincipal;

            // Pasar el control al texbox de peso de salida
            frm.tipoPesada = 1;

            // Llenar cliente
            frm.cbxClientes.DataSource = oDataTable;
            frm.cbxClientes.ValueMember = "ID_CLI";
            frm.cbxClientes.DisplayMember = "CLI_NOMBRE";

            // Llenar precio
            frm.cbxPrecios.DataSource = oDataTable;
            frm.cbxPrecios.ValueMember = "ID_PRECIO";
            frm.cbxPrecios.DisplayMember = "MONTO";

            // Llenar producto
            frm.cbxProductos.DataSource = oDataTable;
            frm.cbxProductos.ValueMember = "ID_PROD";
            frm.cbxProductos.DisplayMember = "PROD_NOMBRE";

            // INGRESAR DATOS EN LOS DEMAS CONTROLES
            string ticketC = oDataTable.Rows[0]["ID_TKT"].ToString(); 
            frm.lblTicket.Text = ticketC.PadLeft(6, '0');
            frm.lblTipoPesaje.Text = "PESAJE DE SALIDA";
            frm.txtPlaca.Text = oDataTable.Rows[0]["PLACA"].ToString();
            frm.txtObservacion.Text = oDataTable.Rows[0]["OBSERVACION"].ToString();
            frm.txtPesoEntrada.Text = oDataTable.Rows[0]["PESO_ENTRADA"].ToString();

            // MODIFICACION EN CONTROLES DEL FORM PRINCIPAL
            
            frm.txtPlaca.Enabled = false;
            frm.txtPesoSalida.Enabled = true;
            frm.cbxClientes.Enabled = false;
            frm.cbxPrecios.Enabled = false;
            frm.cbxProductos.Enabled = false;
            frm.txtPesoEntrada.Enabled = false;
            frm.txtObservacion.Enabled = false;
            frm.btnLimpiar.Enabled = false;
            frm.btnCancelar.Enabled = true;

        }

    }
}
