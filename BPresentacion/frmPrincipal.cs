using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BModelo;
using BControlers;
using System.IO.Ports;


namespace BPresentacion
{
    public partial class FrmPrincipal : Form 
    {
        // VARIABLES DE ASIGNACION PARA LOS USUARIOS
        public string codUser, nameUser, typeUser= "";
        public string printerActual = "";

        // 0 => PRIMERA PESADA, 1 => SEGUNDA PESADA
        public int tipoPesada = 0; 

        // VARIABLES PARA LA TOMA DE DATOS CON LA BALANZA
        private delegate void DelegateAccess(String oString);
        private String bufferIn;
        private SerialPort serialPort1;
        // FIN - Variables para obtener datos de la balanza

        public FrmPrincipal()
        {
            InitializeComponent();
            buscarPuertos();
            conectarPuertos();
        }
        // ------------------------------ FUNCIONES PARA LA TOMA DE DATOS CON LA BALANZA --------------------------------
        private void inicializarRecursos()
        {
            this.bufferIn = "";
        }

        private void buscarPuertos()
        {
            String[] puertosDisponibles = SerialPort.GetPortNames();

            if (puertosDisponibles.Length == 0)
            {
                MessageBox.Show("No hay puertos conectados");
            }
        }


        private void conectarPuertos()
        {
            try
            {
                this.serialPort1 = new SerialPort();
                this.serialPort1.PortName = "COM1";
                this.serialPort1.BaudRate = 9600;
                this.serialPort1.DataBits = 7;
                this.serialPort1.Parity = Parity.Even;
                this.serialPort1.StopBits = StopBits.One;
                this.serialPort1.Handshake = Handshake.None;
                this.serialPort1.DataReceived += new SerialDataReceivedEventHandler(getDataBascula);


                this.serialPort1.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puedo conectar con la balanza, continuar de forma manual?");
                //MessageBox.Show(ex.Message);
            }
        }


        private void desconectarPuertos()
        {
            try
            {
                if (this.serialPort1.IsOpen)
                {
                    this.serialPort1.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getDataBascula(
                object sender,
                SerialDataReceivedEventArgs e)
        {
            SerialPort oSerialPort = (SerialPort)sender;
            this.handlerAddPesoForm(oSerialPort.ReadExisting());
        }

        private void addPesoForm(String peso)
        {
            this.bufferIn = peso;
            if (this.tipoPesada == 0)
            {
                this.txtPesoEntrada.Text = formatearPeso(this.bufferIn);
            }
            else
            {
                this.txtPesoSalida.Text = formatearPeso(this.bufferIn);
            }

        }

        private void handlerAddPesoForm(String peso)
        {
            DelegateAccess oDelegateAccess = new DelegateAccess(addPesoForm);
            object[] args = { peso };
            base.Invoke(oDelegateAccess, args);
        }

        private String formatearPeso(String peso)
        {
            String pesoFormateado = peso.Substring(3, 7).Trim();
            return pesoFormateado;
        }

        // ------------------------------------------------ FIN TOMA DE DATOS EN BALANZA

        // FUNCIONES PARA CARGAR CONTROLES EN EL FORM PRINCIPAL
        // FIN CARGAR CONTROLES


        // CONTROLES PARA REALIZAR ACCIONES EN EL FORM PRINCIPAL: PESAR, NUEVO, ACTUALIZAR,TICKETS,REPORTES
        private void btnPesar_Click(object sender, EventArgs e)
        {
            int idPrecio, idProducto, psoEnt, psoSal, ticket, codCliente;
            String  placa, observacion, fechaHora;

            placa = txtPlaca.Text;
            psoEnt = Convert.ToInt32(txtPesoEntrada.Text);
            idPrecio = Convert.ToInt32(cbxPrecios.SelectedValue);
            psoSal = Convert.ToInt32(txtPesoSalida.Text);

            if (this.tipoPesada == 0)
            {
                    if (psoEnt != 0 && placa != "")
                    {

                        if (idPrecio != 0)
                        {

                            ticket = Convert.ToInt32(lblTicket.Text);
                            codCliente = Convert.ToInt32(txtCodCliente.Text);
                            idProducto = Convert.ToInt32(cbxProductos.SelectedValue);
                            observacion = txtObservacion.Text;
                            if (observacion == "")
                            {
                                observacion = "  ";
                            }
                            
                            fechaHora = lblHoraExacta.Text;

                            // VALIDADOS TODOS LOS DATOS, ALMACENO Y ENVIO DATOS A REGISTRAR LA VENTA
                            try
                            {
                                Venta nVenta = new Venta();

                                //// Ejemplo para insertar datos
                                Dictionary<String, Object> oDictionary1 = new Dictionary<String, Object>();
                                oDictionary1.Add("ID_PRODUCTO", idProducto);
                                oDictionary1.Add("PESO_ENTRADA", psoEnt);
                                oDictionary1.Add("FEC_ENTRADA", fechaHora);
                                oDictionary1.Add("ID_PRECIO", idPrecio);
                                oDictionary1.Add("PLACA", placa);
                                oDictionary1.Add("TIPO", "E");
                                oDictionary1.Add("PENDIENTE", 1);
                                oDictionary1.Add("OBSERVACION", observacion);
                                oDictionary1.Add("ID_CLIENTE", codCliente);
                                oDictionary1.Add("ID_USER",Convert.ToInt32(this.codUser));

                                int respuesta = nVenta.insert(oDictionary1).run();
                                

                                if (chbxImpTicket.Checked)
                                {
                                    if (printerActual == "A5")
                                    {
                                        Reports.visorPrincipal oImprimir = new Reports.visorPrincipal(ticket,"E","D");
                                        oImprimir.imprimirMatricialEntrada();
                                    }
                                    else if (printerActual == "TK")
                                    {
                                        PrintTicket oPrinT = new PrintTicket("E", ticket);
                                        oPrinT.cargaDatosImprime();
                                    }
                                    
                                }
                                else
                                {
                                    MessageBox.Show("Registrado correctamente.");
                                    this.chbxImpTicket.Checked = true;
                                }
                                funcionNuevo();
                            //// Fin Ejemplo para insertar datos
                            }
                            catch (Exception Ax)
                            {
                                MessageBox.Show("No se pudo Registrar: " + Ax);
                            }
                    }
                     else MessageBox.Show("Debe ingresar un Precio.");

                }
                else MessageBox.Show("Revice los campos placa y peso.");

            }
            else if (this.tipoPesada == 1)
            {
                if (psoSal != 0 && psoSal >= 10)
                {
                    ticket = Convert.ToInt32(lblTicket.Text);
                    fechaHora = lblHoraExacta.Text;

                    try
                    {
                        Venta aVenta = new Venta();

                        //// ACTUALIZADO DATOS
                        Dictionary<String, Object> oDictionary = new Dictionary<String, Object>();
                        oDictionary.Add("PESO_SALIDA", psoSal);
                        oDictionary.Add("FEC_SALIDA", fechaHora);
                        oDictionary.Add("TIPO", "S");
                        oDictionary.Add("PENDIENTE", 0);
                        int rpta = aVenta.update(oDictionary).where("ID_TKT", "=", ticket).run();

                       
                        if (chbxImpTicket.Checked)
                        {
                            if (printerActual == "A5")
                            {
                                Reports.visorPrincipal oImprimir = new Reports.visorPrincipal(ticket, "S", "D");
                                oImprimir.imprimirMatricialSalida();

                            }
                            else if (printerActual == "TK")
                            {
                                PrintTicket oPrinT = new PrintTicket("S", ticket);
                                oPrinT.cargaDatosImprime();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Registrado correctamente.");
                            this.chbxImpTicket.Checked = true;
                        }
                        funcionNuevo();
                    }
                    catch (Exception Ax)
                    {
                        MessageBox.Show("Hubo un error al guardar el peso de salida: " + Ax);
                        
                    }
                }
                else MessageBox.Show("El peso mínimo a considerar es 10 Kg.");
 
            }
            else
            {

            }

        }
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            this.inicializarRecursos(); // -------------- Control de puertos 
            cargarDatos();
        }
        
        private void cargarDatos()
        {
            cargarIdTicket();
            llenarComboPrecio();
            llenarComboProductos();
            llenarComboClientes();
            
            viewControlrsUser(typeUser);

            txtUsuario.Text = "Usuario: " + nameUser;

            this.txtPesoSalida.Enabled = false;
            this.btnCancelar.Enabled = false;

            printerDefault();


        }
        public void printerDefault()
        {
            try
            {
                Printer oPrinter = new Printer();
                String[] colum = { "FORMATO" };
                List<Object> oList = oPrinter.select(colum).where("PREDETERMINADO", "=", true).first();
                printerActual = Convert.ToString(oList[0]);
            }
            catch (Exception e)
            {
                MessageBox.Show("Se establecio la impresora predeterminada: Matricial");
                this.printerActual = "A5";
            }


        }

        private void viewControlrsUser(String tipo)
        {
            switch (tipo)
            {
                case "1":
                    btnAjustes.Enabled = true;
                    btnReporte.Enabled = true;
                    break;
                case "2":
                    btnAjustes.Enabled = false;
                    btnReporte.Enabled = false;
                    break;
                default:
                    break;
            }
        }
        private void cargarIdTicket()
        {
            
                Venta oVenta = new Venta();

                String[] colum = { "ID_TKT" };
                List<Object> oList = oVenta.select(colum).orderBy("ID_TKT", "DESC").first();

                String nuevoIdTicked;
                if (oList.Count == 0)
                {
                    nuevoIdTicked = "1";
                    nuevoIdTicked = nuevoIdTicked.PadLeft(6, '0');
                }
                else
                {
                    nuevoIdTicked = Convert.ToString((int)oList[0] + 1);
                    nuevoIdTicked = nuevoIdTicked.PadLeft(6, '0');
                    this.lblTicket.Text = nuevoIdTicked;
                }

        }

        private void btnTicket_Click(object sender, EventArgs e)
        {
            frmTickets frmTicket = new frmTickets(this.typeUser, this.printerActual);

            AddOwnedForm(frmTicket);
            frmTicket.ShowDialog();
        }

        private void btnPendientes_Click(object sender, EventArgs e)
        {
            frmTicketPendiente vtaPendiente = new frmTicketPendiente();

            AddOwnedForm(vtaPendiente);
            vtaPendiente.lblHelp.Visible = true;
            vtaPendiente.ShowDialog();

        }

        private void btnSalir_Click(object sender, EventArgs e) {
            desconectarPuertos(); // --------------- Control de puertos
            this.Close();
            Inicio frmInicio = new Inicio();
            frmInicio.Show();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            frmReports vtaReporte = new frmReports();
            vtaReporte.Show();
        }

        // FUNCIONES PARA LEER DATOS DE LA BALANZA --------------------------------------------------------
      
        // FIN- Funciones para leer datos de la balanza --------------------------------------------------

        private void btnCerrarAplicativo_Click(object sender, EventArgs e) => Application.Exit();

        private void txtPesoEntrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNumeros(e);
        }

        private void txtPesoSalida_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNumeros(e);
        }
        private void validarNumeros(KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsNumber(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        // FUNCIONES PARA LLENAR DATOS EN LOS CONTROLES COMBO BOX

        private void llenarComboPrecio()
        {

            Precio oPrecio = new Precio();

            String[] colums = { "ID_PRECIO", "MONTO"};
            DataTable oDataTable = oPrecio.select(colums).get();
            
            this.cbxPrecios.DisplayMember = "MONTO";

            this.cbxPrecios.ValueMember = "ID_PRECIO";

            this.cbxPrecios.DataSource = oDataTable;

            this.cbxPrecios.SelectedIndex = -1;
        }

        private void llenarComboProductos()
        {

            Products oProducto = new Products();

            String[] colums = { "ID_PROD", "NOMBRE" };
            DataTable oDataTable = oProducto.select(colums).get();

            this.cbxProductos.DisplayMember = "NOMBRE";

            this.cbxProductos.ValueMember = "ID_PROD";

            this.cbxProductos.DataSource = oDataTable;

        }

        private void llenarComboClientes()
        {

            Cliente oCliente = new Cliente();

            String[] colums = { "ID_CLI", "NOMBRE" };
            DataTable oDataTable = oCliente.select(colums).get();

            this.cbxClientes.DisplayMember = "NOMBRE";

            this.cbxClientes.ValueMember = "ID_CLI";

            this.cbxClientes.DataSource = oDataTable;

            //this.cbxClientes.SelectedIndex = -1;
        }
        // FIN - Funciones para llenar datos
        private void cbxClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Object codigo = cbxClientes.SelectedValue;
            if (codigo == null)
            {
                txtCodCliente.Text = "0";
            }
            else
            {
                txtCodCliente.Text = codigo.ToString();
            }
            

        }

        private void fechaHora_Tick(object sender, EventArgs e)
        {
            lblHoraExacta.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("HH:mm");
            lblHora.Text = DateTime.Now.ToString("h:mm:ss tt");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

       
        private void funcionNuevo()
        {
            
            limpiarControles();
            cargarDatos();

            //txtPesoSalida.Enabled = false;
            btnLimpiar.Enabled = true;
            btnCancelar.Enabled = false;

            this.tipoPesada = 0;
        }
        private void limpiarControles()
        {
            // LIMPIAMOS CONTROLES
            lblTipoPesaje.Text = "PESAJE DE ENTRADA";
            txtPlaca.Text = "";
            txtPesoEntrada.Text = "0";
            txtPesoSalida.Text = "0";
            txtObservacion.Text = "";

            cbxClientes.DataSource = null;
            cbxClientes.Items.Clear();

            cbxProductos.DataSource = null;
            cbxProductos.Items.Clear();

            // ACTIVAR CONTROLES
            txtPesoEntrada.Enabled = true;
            cbxClientes.Enabled = true;
            cbxProductos.Enabled = true;
            cbxPrecios.Enabled = true;
            txtObservacion.Enabled = true;
            txtPlaca.Enabled = true;
            txtPlaca.Focus();
            
        }

        private void txtPesoEntrada_Click(object sender, EventArgs e) 
        {
            txtPesoEntrada.SelectAll();
        }

        private void txtPesoSalida_Click(object sender, EventArgs e)
        {
            txtPesoSalida.SelectAll();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            funcionNuevo();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // LIMPIAMOS CONTROLES
            txtPlaca.Text = "";
            txtPesoEntrada.Text = "0";
            txtPesoSalida.Text = "0";
            txtObservacion.Text = "";

            this.cbxClientes.SelectedIndex = 0;
            this.cbxProductos.SelectedIndex = 0;
            this.cbxPrecios.SelectedIndex = -1;


            txtPlaca.Focus();

        }

        private void btnAjustes_Click(object sender, EventArgs e)
        {
            frmAjustes fAjestes = new frmAjustes();
            fAjestes.Show();
        }

        private void txtPesoEntrada_Leave(object sender, EventArgs e)
        {
            if (txtPesoEntrada.Text == "")
                txtPesoEntrada.Text = "0";
            
        }

        private void txtPesoSalida_Leave(object sender, EventArgs e)
        {
            if (txtPesoSalida.Text == "")
                txtPesoSalida.Text = "0";
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsWhiteSpace(e.KeyChar))
                {
                    e.Handled = true;
                }
                else if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        
    }
}
