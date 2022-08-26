using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BModelo;

namespace BPresentacion
{
    public partial class frmConfigSis : Form
    {
        public frmConfigSis()
        {
            InitializeComponent();
        }

        private void frmConfigSis_Load(object sender, EventArgs e)
        {
            cargarPrinterImpreLocal();
            InstalledPrintersCombo();
        }
        private void cargarPrinterImpreLocal()
        {
            Printer oPrinter = new Printer();

            String[] colums = { "ID_IMP", "NOMBRE" };
            DataTable oDataTable = oPrinter.select(colums).get();

            this.cbxPrinterLocal.DisplayMember = "NOMBRE";
            this.cbxPrinterLocal.ValueMember = "ID_IMP";
            this.cbxPrinterLocal.DataSource = oDataTable;

            //this.cbxPrecios.SelectedIndex = -1;

        }
        

        private void btnBuscarPrinter_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            int codPrinter;

            codPrinter = Convert.ToInt32(cbxPrinterLocal.SelectedValue); 

            Printer aPrinter = new Printer();

            // 1. ACTUALIZO IMPRESORA COMO NINGUNA
            Dictionary<String, Object> oDictionary = new Dictionary<String, Object>();
            oDictionary.Add("PREDETERMINADO", false);
            aPrinter.update(oDictionary).run();


            // 2. ACTUALIZO COMO PREDETERMINADO IMPRESORA SELECCIONADA
            Printer aPrinter2 = new Printer();
            Dictionary<String, Object> oDictionary2 = new Dictionary<String, Object>();
            oDictionary2.Add("PREDETERMINADO", true);
            int rpta = aPrinter2.update(oDictionary2).where("ID_IMP","=", codPrinter).run();

            if (rpta > 0)
                MessageBox.Show("Configuracion exitosa, reinicie el Sistema.");

            var result = MessageBox.Show("Desea reiniciar el sistema ahora?"
                                             ,"ADVERTENCIA"
                                             ,MessageBoxButtons.YesNo
                                             ,MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                Application.Exit();
            
        }

        private void btnIntegrar_Click(object sender, EventArgs e)
        {
            string nombreImpGlobal;
            nombreImpGlobal = cbxIntegrar.Text;
            

            Printer aPrinter = new Printer();

            // ACTUALIZO NOMBRE DE LA IMPRESORA PARA LOS TICKETS
            Dictionary<String, Object> oDictionary = new Dictionary<String, Object>();
            oDictionary.Add("NOMBRE", nombreImpGlobal);
            int rpta = aPrinter.update(oDictionary).where("FORMATO","=","TK").run();
            if (rpta > 0)
            {
                MessageBox.Show("Impresora, integrado correactamente.");
                cargarPrinterImpreLocal();
            }

        }
        private void InstalledPrintersCombo()
        {
            // Agregue la lista de impresoras instaladas encontradas al cuadro combinado.
            // La cadena pkInstalledPrinters se utilizará para proporcionar la cadena de visualización.
            String pkInstalledPrinters;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                cbxIntegrar.Items.Add(pkInstalledPrinters);

            }
            // Ademas agrega una impresora mas
            cbxIntegrar.Text = "PDFLite";
        }

        private void cbxPrinterLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codPrinter = Convert.ToInt32(cbxPrinterLocal.SelectedValue);

            if (codPrinter == 2)
            {
                cbxIntegrar.Enabled = true;
                btnIntegrar.Enabled = true;
            }
            else
            {
                cbxIntegrar.Enabled = false;
                btnIntegrar.Enabled = false;
            }
        }
    }
}
