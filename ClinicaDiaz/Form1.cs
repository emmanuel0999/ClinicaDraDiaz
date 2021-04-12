using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaDiaz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(@"Data source=localhost\SQLEXPRESS; Initial Catalog=clinicaDiaz; Integrated Security=True");

        private void ptbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUser_MouseEnter(object sender, EventArgs e)
        {
            if (txtUser.Text == "Usuario") {
                txtUser.Text = "";
                txtUser.ForeColor = Color.Black;

            }
        }

        private void txtUser_MouseLeave(object sender, EventArgs e)
        {
            if (txtUser.Text == "") {
                txtUser.Text = "";

            }
        }

        private void txtPass_MouseEnter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Contraseña")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.Black;
                txtPass.UseSystemPasswordChar = true;

            }

        }

        private void txtPass_MouseLeave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "";
            }
        }
        public void mensajeError(string cadena) {

            if (txtUser.Text == "")
            {
                lblError.Text = "     " + cadena;
                lblError.Visible = true;
                txtUser.Focus();
            }
            else if (txtPass.Text == "")
            {
                lblError.Text = "     " + cadena;
                lblError.Visible = true;
                txtPass.Focus();
            }
            else {
                lblError.Visible = false;
            }
        }
        public void loguear(string user, string pass)
        {

            try
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Usuario, Tipo_Usuario FROM Login Where Usuario=@user and Pass = @pass", cn);
                cmd.Parameters.AddWithValue("@user", txtUser.Text);
                cmd.Parameters.AddWithValue("@pass", txtPass.Text);

                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    this.Hide();
                    if (dt.Rows[0][1].ToString() == "admin")
                    {
                        new Administrador(dt.Rows[0][1].ToString()).Show();

                    }
                    else if (dt.Rows[0][1].ToString() == "Normal")
                    {
                        Normal normal = new Normal();
                        normal.Show();

                    }
                }
                else { MessageBox.Show("Usuario y/o Contraseña incorrectos"); }

            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            finally { cn.Close(); }

        }

        private void btnAcceso_Click(object sender, EventArgs e)
        {
            mensajeError("COMPLETE CAMPOS");
            loguear(this.txtUser.Text, this.txtPass.Text);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        
    }
}
