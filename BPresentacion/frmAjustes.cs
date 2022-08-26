using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPresentacion
{
    public partial class frmAjustes : Form
    {
        public frmAjustes()
        {
            InitializeComponent();
        }

        private void abrirFormualarioHijo(object formHija)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);

            Form fh = formHija as Form;

            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();

        }

        private void btnConfigUsuarios_Click(object sender, EventArgs e)
        {
            abrirFormualarioHijo(new frmAjustesUsuarios());
        }

        private void btnConfigAjustes_Click(object sender, EventArgs e)
        {
            abrirFormualarioHijo(new frmConfigSis());
        }
    }
}
