using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaDiaz
{
    public partial class agregarServicios : Form
    {
        public agregarServicios()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarSer_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombreSer.Text != "" && txtPrecioUni.Text != "")
                {

                    string query = string.Format("EXEC InsertServicios '{0}','{1}','{2}'", txtNombreSer.Text.Trim(), txtPrecioUni.Text.Trim(), txtPrecio.Text.Trim());
                    Funcionalidades.Ejecutar(query);
                    MessageBox.Show("Agregado correctamente", "Listo");
                    txtNombreSer.Text = txtPrecioUni.Text = txtPrecio.Text = "";

                }
                else {
                    if (txtNombreSer.Text == "" || txtNombreSer.Text == " ") {
                        MessageBox.Show("Debe de llenar el campo nombre de servicio.", "ADVERTENCIA");
                        txtNombreSer.Text = "";
                        txtNombreSer.Focus();
                    } else if (txtPrecioUni.Text=="" || txtPrecioUni.Text==" ") {
                        MessageBox.Show("Debe de llenar el campo Precio.", "ADVERTENCIA");
                        txtPrecioUni.Text = "";
                        txtPrecioUni.Focus();
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR", ex.Message);
            }
            
           
        }

        private void agregarServicios_Load(object sender, EventArgs e)
        {
            var aux = new Metodos();
            aux.listarServicios(dtgvVerSer);
        }

        private void txtNombreSer_TextChanged(object sender, EventArgs e)
        {
            var aux = new Metodos();
            aux.filtrarServicios(dtgvVerSer,this.txtNombreSer.Text.Trim());
        }
    }
}
