using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BControlers;
using BModelo;

namespace BPresentacion
{
    public partial class frmTickets : Form
    {
        String tipoUser,Printer = "";
        public frmTickets(string user, string printer)
        {
            InitializeComponent();
            this.tipoUser = user;
            this.Printer = printer;

        }

        private void button1_Click(object sender, EventArgs e) => this.Close();
        
        
        private void frmTickets_Load(object sender, EventArgs e)
        {
            cargarVentas();
            mostrarControles(tipoUser);
            
            this.cbxBuscar.SelectedIndex = 0;
        }
        private void mostrarControles(String tipo)
        {
            switch (tipo)
            {
                case "1":
                    break;
                case "2":
                    Size = new Size(811, 512);
                    break;
                default:
                    break;
            }
        }
        public void cargarVentas()
        {
            Venta listaVentas = new Venta();
            String[] columnas = { "ID_TKT AS TICKET"
                    ,"FORMAT(FEC_ENTRADA, 'dd-mm-yyyy hh:mm AM/PM') AS FEC_ENTRADA"
                    ,"PLACA"
                    ,"FORMAT(PESO_ENTRADA, '##,##0') AS P_ENTRADA"
                    ,"FORMAT(PESO_SALIDA, '##,##0') AS P_SALIDA"
                    ,"OBSERVACION"};
             
            DataTable dt = listaVentas.select(columnas).where("ANULADO","=",false)
                                                        .orderBy("FEC_ENTRADA", "DESC").get();



            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Error al cargar los datos");
            }
            else
            {
                this.dgvTickets.DataSource = dt;
            }
        }

        private void btnImprimirTicket_Click(object sender, EventArgs e)
        {
            //int numeroDeFilaSeleccionada = 0;

            if (this.dgvTickets.Rows.Count > 0) {
                
                if (this.dgvTickets.SelectedRows.Count > 0)
                 {
                    int id_tkt = (int)dgvTickets.CurrentRow.Cells[0].Value;
                    decimal valor = Convert.ToDecimal(dgvTickets.CurrentRow.Cells[4].Value);

                    if (Printer == "TK")
                    {
                        if (valor == 0)
                        {
                            PrintTicket oPrinT = new PrintTicket("E", id_tkt);
                            oPrinT.cargaDatosImprime();
                        }
                        else
                        {
                            PrintTicket oPrinT = new PrintTicket("S", id_tkt);
                            oPrinT.cargaDatosImprime();
                        }

                    }
                    else if (Printer == "A5")
                    {
                        if (valor == 0)
                        {
                            Reports.visorPrincipal oImprimir = new Reports.visorPrincipal(id_tkt, "E", "V");
                            oImprimir.Show();
                            oImprimir.imprimirMatricialEntrada();
                        } else
                        {
                            Reports.visorPrincipal oImprimir = new Reports.visorPrincipal(id_tkt, "S", "V");
                            oImprimir.Show();
                            oImprimir.imprimirMatricialSalida();
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Seleccione un registro.");
                }
            }
            else
            {
                MessageBox.Show("No Hay Datos");
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        
        private void btnAnularTicket_Click(object sender, EventArgs e)
        {
            string motivo = txtAnulacion.Text;
            if (this.dgvTickets.SelectedRows.Count > 0)
            {
                if (motivo == "")
                {
                    MessageBox.Show("Es necesario precisar el motivo de la anulacion.");
                    txtAnulacion.Focus();
                }
                else
                {
                    const string message = "Esta seguro que desea eliminar el registro?";
                    const string caption = "Eliminar Venta";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                        string id_ticket = dgvTickets.CurrentRow.Cells[0].Value.ToString();
                        Venta oVenta = new Venta();
                        //// Actualizo datos
                        Dictionary<String, Object> oColumnas = new Dictionary<String, Object>();

                        oColumnas.Add("MOTIVO", motivo);
                        oColumnas.Add("ANULADO", 1);
                        
                        int rpta = oVenta.update(oColumnas).where("ID_TKT", "=", Convert.ToInt32(id_ticket)).run();

                        cargarVentas();
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("No se pudo Eliminar: " + ex);
                        }

                    }
                    txtAnulacion.Text = "";
                }
            }
            else MessageBox.Show("Seleccione un Ticket");

                
        }

        private void btnAnularServicio_Click(object sender, EventArgs e)
        {
            frmAnularPeso frmA = new frmAnularPeso();

            AddOwnedForm(frmA);
            frmA.ShowDialog();

        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            string campo = "";

            switch (cbxBuscar.SelectedItem.ToString())
            {
                case "PLACA":
                    campo = "PLACA";
                    break;
                case "TICKET":
                    campo = "ID_TKT";
                    break;
                case "FECHA ENT":
                    campo = "FEC_ENTRADA";
                    break;
                default:
                    break;
            }
            
            try
            {
                Venta listaVentas = new Venta();
                String[] columnas = { "ID_TKT AS TICKET",
                "FEC_ENTRADA",
                "PLACA",
                "PESO_ENTRADA",
                "PESO_SALIDA",
                "OBSERVACION" };

                DataTable dt = listaVentas.select(columnas)
                                            .where(campo, "LIKE ", this.txtBuscar.Text + "%")
                                            .where("ANULADO", "=", 0)
                                            .orderBy("FEC_ENTRADA", "DESC")
                                            .get();

                this.dgvTickets.DataSource = dt;
            }
            catch (Exception Ax)
            {

                MessageBox.Show("Error: " + Ax);
            }
        }
    }
}
