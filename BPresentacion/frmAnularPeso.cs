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
    public partial class frmAnularPeso : Form
    {
        public frmAnularPeso()
        {
            InitializeComponent();
        }

        private void fmrAnularPeso_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string idTicket;

            idTicket = txtTicket.Text;
            if (idTicket != "")
            {
                try
                {
                    Venta oVenta = new Venta();

                    String[] colum = { "ID_TKT", "PESO_ENTRADA", "PESO_SALIDA" };
                    List<Object> oList = oVenta.select(colum).where("ID_TKT", "=", Convert.ToInt32(idTicket)).first();

                    string ticketEncont;
                    if (oList.Count == 0)
                    {
                        MessageBox.Show("NO hay registros, verifique el Nº Ticket");
                    }
                    else
                    {
                        ticketEncont = Convert.ToString(oList[0]);
                        ticketEncont = ticketEncont.PadLeft(6, '0');
                        lblNumTicket.Text = ticketEncont;
                        this.txtPesoEnt.Text = Convert.ToString(oList[1]);
                        this.txtPesoSal.Text = Convert.ToString(oList[2]);
                        this.txtTicket.Text = "";
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Error: Verifique el Nº Ticket");
                }
                

            }
            else
                MessageBox.Show("El campo Ticket esta vacio.");

        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            frmTickets frmT = Owner as frmTickets;
            string motivo = frmT.txtAnulacion.Text;
            if (motivo == "")
            {
                MessageBox.Show("PASO 1: Precise el motivo de borrar la pesada.");
                frmT.txtAnulacion.Focus();
            }
            else
            { 
                if (chxSalida.Checked)
                {
                    int ticket = Convert.ToInt32(lblNumTicket.Text);
                    try
                    {
                        Venta aTicket = new Venta();

                        //// Actualizo datos
                        Dictionary<String, Object> oDictionary = new Dictionary<String, Object>();
                        oDictionary.Add("PESO_SALIDA", 0);
                        oDictionary.Add("FEC_SALIDA", DBNull.Value);
                        oDictionary.Add("TIPO", "E");
                        oDictionary.Add("PENDIENTE", 1);
                        oDictionary.Add("MOTIVO", motivo);

                        var result = MessageBox.Show("Esta seguro de modificar el registro?", "Anular Servicio",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
                        if (result  == DialogResult.Yes)
                        {
                            int rpta = aTicket.update(oDictionary).where("ID_TKT", "=", ticket).run();
                            if (rpta >= 0)
                                MessageBox.Show("Respuesta : Se cambio con exito.");
                        }

                        frmT.cargarVentas();
                        this.Close();
                    }
                    catch (Exception Ax)
                    {
                        MessageBox.Show("Hubo un error al Actualizar: " + Ax);
                    }
                }
                else
                    MessageBox.Show("PASO 2: Seleccione el Check.");
            
            }

        }
    }
}
