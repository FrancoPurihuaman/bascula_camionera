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
    public partial class frmAjustesUsuarios : Form
    {

        string id_user, nombres, pass = "";
        int permisos;
        public frmAjustesUsuarios()
        {
            InitializeComponent();
        }

        private void frmAjustesUsuarios_Load(object sender, EventArgs e)
        {
            cargarUsuarios();
        }

        private void cargarUsuarios()
        {
            Usuario oUsers = new Usuario();
            String[] columnas = { "ID_USER",
                "NOMBRE",
                "CONTRASENA",
                "PERMISOS"};
            DataTable dt = oUsers.select(columnas).where("ACTIVO","=",-1).get();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Error al cargar los datos");
            }
            else
            {
                this.dgvClientes.DataSource = dt;
            }
        }

        private void dgvClientes_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvClientes.SelectedRows.Count > 0)
            {

                try
                {
                    id_user = dgvClientes.CurrentRow.Cells[0].Value.ToString();

                    Usuario oUsers = new Usuario();
                    //// Actualizo datos
                    Dictionary<String, Object> oColumnas = new Dictionary<String, Object>();
                    
                    oColumnas.Add("ACTIVO", 0);
                    int rpta = oUsers.update(oColumnas).where("ID_USER", "=", Convert.ToInt32(id_user)).run();

                    cargarUsuarios();
                    funcionNuevo();

                    btnGuardar.Text = "Guardar";
                }
                catch (Exception ex)
                {

                    MessageBox.Show("No se pudo Eliminar: " + ex);
                }

            }
            else
            {
                MessageBox.Show("Seleccione un Usuario.");
            }
        }

        

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            Usuario oUser = new Usuario();

            String[] colum = { "ID_USER" };
            List<Object> oList = oUser.select(colum).orderBy("ID_USER", "DESC").first();

            String nuevoIdUser = "0";
            if (oList.Count != 0)
            {
                nuevoIdUser = Convert.ToString((int)oList[0] + 1);
                this.txtCodigo.Text = nuevoIdUser;
                this.txtNombres.Text = "";
                this.txtPass.Text = "";
                this. cbxPermisos.SelectedIndex = -1;
                txtNombres.Focus();
                this.btnGuardar.Text = "Guardar";
            }
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvClientes.SelectedRows.Count > 0)
            {

                 id_user = dgvClientes.CurrentRow.Cells[0].Value.ToString();
                 nombres = (string)dgvClientes.CurrentRow.Cells[1].Value;
                 pass = (string)dgvClientes.CurrentRow.Cells[2].Value;
                 permisos = (int)dgvClientes.CurrentRow.Cells[3].Value;

                txtCodigo.Text = id_user;
                txtNombres.Text = nombres;
                txtPass.Text = pass;
                cbxPermisos.SelectedIndex = permisos - 1;

                btnGuardar.Text = "Actualizar";
                
            }
            else
            {
                MessageBox.Show("Seleccione un Usuario.");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            id_user = txtCodigo.Text;
            nombres = txtNombres.Text;
            pass = txtPass.Text;
            permisos = cbxPermisos.SelectedIndex + 1;

            //MessageBox.Show("Permiso seleccionado: " + permisos);

            if (this.id_user != "" && nombres != "" && pass != "")
            {
                if (cbxPermisos.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe asignar un tipo de permiso.");
                }
                else
                {

                    Usuario oUsers = new Usuario();

                    if (btnGuardar.Text == "Actualizar")
                    {
                        try
                        {

                            //// Actualizo datos
                            Dictionary<String, Object> oColumnas = new Dictionary<String, Object>();
                            oColumnas.Add("NOMBRE", nombres);
                            oColumnas.Add("CONTRASENA", pass);
                            oColumnas.Add("PERMISOS", permisos);
                            oColumnas.Add("ACTIVO", 1);
                            int rpta = oUsers.update(oColumnas).where("ID_USER", "=", Convert.ToInt32(id_user)).run();

                            cargarUsuarios();
                            funcionNuevo();

                            btnGuardar.Text = "Guardar";
                        }
                        catch (Exception Ax)
                        {

                            MessageBox.Show("Se produjo un error al Actualizar: " + Ax);
                        }


                    }
                    else if (btnGuardar.Text == "Guardar")
                    {

                        try
                        {
                            //// Inserto datos
                            Dictionary<String, Object> oDictionary1 = new Dictionary<String, Object>();

                            oDictionary1.Add("NOMBRE", this.nombres);
                            oDictionary1.Add("CONTRASENA", this.pass);
                            oDictionary1.Add("PERMISOS", this.permisos);
                            oDictionary1.Add("ACTIVO", 1);

                            int rpta = oUsers.insert(oDictionary1).run();
                            if (rpta > 0)
                                MessageBox.Show("Guardado Correctamente");

                            cargarUsuarios();
                            funcionNuevo();
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show("Se produjo un error en: " + Ex);
                        }

                    }
                }

            }
            else MessageBox.Show("Verifique los campos, no se aceptan vacios.");
        }

        private void funcionNuevo()
        {
            txtCodigo.Text = "";
            txtNombres.Text = "";
            txtPass.Text = "";
            cbxPermisos.SelectedIndex = -1;
            
        }
    }
}
