using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace ClinicaDiaz
{
    public partial class agregarPaciente : Form
    {
        SqlConnection cn = new SqlConnection(@"Data source=localhost\SQLEXPRESS; Initial Catalog=clinicaDiaz; Integrated Security=True");
        public agregarPaciente()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();

                string query = "INSERT INTO registroPaciente(Fecha,Nombre,Nacimiento,Edad,Sexo,Direccion,Cedula,Telefono,MotivoConsulta)" +
                    "Values(@fecha,@nombre,@nacimiento,@edad,@sexo,@direccion,@cedula,@telefono,@motivoconsulta)";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@fecha", dttFecha.Text);
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@nacimiento", dttNacimiento.Text);
                cmd.Parameters.AddWithValue("@edad", txtEdad.Text);
                cmd.Parameters.AddWithValue("@sexo", cmbGenero.Text);
                cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                cmd.Parameters.AddWithValue("@cedula", txtCedula.Text);
                cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@motivoconsulta", txtMotivoConsulta.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("listo");

            }
            catch (Exception ec) { MessageBox.Show(ec.Message); }
            finally { cn.Close(); 
            }
            dttFecha.Text = txtNombre.Text = dttNacimiento.Text = txtEdad.Text = cmbGenero.Text = txtDireccion.Text = txtCedula.Text = txtTelefono.Text = txtMotivoConsulta.Text = "";
          
        }
    }
}
