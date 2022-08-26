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

using System.Runtime.InteropServices;

namespace BPresentacion
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void btnCerrar_Click(object sender, EventArgs e) => Application.Exit();

        private void btnMinimizar_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            
        }
       
        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "USUARIO")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.LightGray;

            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                txtUser.Text = "USUARIO";
                txtUser.ForeColor = Color.LightGray;
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "CONTRASEÑA")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.LightGray;
                txtPass.UseSystemPasswordChar = true;

            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "CONTRASEÑA";
                txtPass.ForeColor = Color.LightGray;
                txtPass.UseSystemPasswordChar = false;
            }
        }

        private void Inicio_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            string user, pass;
            bool error = false;

            user = txtUser.Text;
            pass = txtPass.Text;

            if (user == "USUARIO")
            {
                error = true;
                MessageBox.Show("Debe ingresar un usuario");
                txtUser.Focus();
            }
            else if (pass == "CONTRASEÑA")
            {
                error = true;
                MessageBox.Show("Por favor ingrese una contraseña");
                txtPass.Focus();
            }


            // SI TODO ES CONFORME Y ERRORES  ES FALSO, CONTINUAMOS

            if (error == false)
            {
                try
                {
                    Usuario oUser = new Usuario();

                    String[] colum = { "ID_USER", "NOMBRE" , "PERMISOS", "ACTIVO" };
                    List<Object> oList = oUser.select(colum).where("NOMBRE", "=", user).where("CONTRASENA", "=", pass).first();
 

                    if (oList.Count > 0)
                    {
                        if (((bool)oList[3]))
                        {
                            FrmPrincipal frmP = new FrmPrincipal();
                            frmP.codUser = oList[0].ToString();
                            frmP.nameUser = oList[1].ToString();
                            frmP.typeUser = oList[2].ToString();

                            frmP.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Usuario inactivo, ingrese otro.");
                            txtPass.Text = "";
                            txtUser.Text = "";
                            txtUser.Focus();
                        }
                            
                    }
                    else
                    {
                        MessageBox.Show("Usuario o Contraseña incorrecto");
                    }
                }
                catch (Exception a)
                {
                    MessageBox.Show("Lo sentimos hubo un error: " + a);
                }
            }

        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarSinEspacios(e);
        }

        private void validarSinEspacios(KeyPressEventArgs e)
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

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Contactarse con el hacedor del Sistema.");
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnAcceder.PerformClick();
            }
        }
    }
}
