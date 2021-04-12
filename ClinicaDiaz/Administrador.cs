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
    public partial class Administrador : Form
    {
      
        public Administrador(string lbl)
        {
            InitializeComponent();
            lblAdmin.Text = lbl;
        }

        private void btnMover_Click(object sender, EventArgs e)
        {
            if (menuVertical.Width == 250)
            {
                menuVertical.Width = 60;
            }
            else {
                menuVertical.Width = 250;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void abrirPanel(object formHijo)
        {
            nuevaFactura nf = new nuevaFactura();
            AddOwnedForm(nf);
            nf.FormBorderStyle = FormBorderStyle.None;
            nf.TopLevel = false;
            nf.Dock = DockStyle.Fill;
            this.Controls.Add(nf);
            this.Tag=nf;
            nf.BringToFront();
            nf.Show();
            
        }
        private void panelAgregarPaciente(object formHijo)
        {
            agregarPaciente nf = new agregarPaciente();
            AddOwnedForm(nf);
            nf.FormBorderStyle = FormBorderStyle.None;
            nf.TopLevel = false;
            nf.Dock = DockStyle.Fill;
            this.Controls.Add(nf);
            this.Tag = nf;
            nf.BringToFront();
            nf.Show();

        }
        private void panelGestionarPaciente(object formHijo)
        {
            gestionarPaciente nf = new gestionarPaciente();
            AddOwnedForm(nf);
            nf.FormBorderStyle = FormBorderStyle.None;
            nf.TopLevel = false;
            nf.Dock = DockStyle.Fill;
            this.Controls.Add(nf);
            this.Tag = nf;
            nf.BringToFront();
            nf.Show();

        }
        private void panelCotizacion(object formHijo)
        {
            cotizacion nf = new cotizacion();
            AddOwnedForm(nf);
            nf.FormBorderStyle = FormBorderStyle.None;
            nf.TopLevel = false;
            nf.Dock = DockStyle.Fill;
            this.Controls.Add(nf);
            this.Tag = nf;
            nf.BringToFront();
            nf.Show();

        }
        private void panelAgregarServicios(object formHijo)
        {
            agregarServicios nf = new agregarServicios();
            AddOwnedForm(nf);
            nf.FormBorderStyle = FormBorderStyle.None;
            nf.TopLevel = false;
            nf.Dock = DockStyle.Fill;
            this.Controls.Add(nf);
            this.Tag = nf;
            nf.BringToFront();
            nf.Show();

        }
        private void panelCuadreDiario(object formHijo)
        {
            cuadreDiario nf = new cuadreDiario();
            AddOwnedForm(nf);
            nf.FormBorderStyle = FormBorderStyle.None;
            nf.TopLevel = false;
            nf.Dock = DockStyle.Fill;
            this.Controls.Add(nf);
            this.Tag = nf;
            nf.BringToFront();
            nf.Show();

        }
        private void btnNuevaFactura_Click(object sender, EventArgs e)
        {
            abrirPanel(new nuevaFactura());
        }
       
        private void lblAdmin_Click(object sender, EventArgs e)
        {

        }
        public void agregarPaciente() {
        
        
        
        }
        private void button2_Click(object sender, EventArgs e)
        {
            panelAgregarPaciente(new agregarPaciente());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelGestionarPaciente(new gestionarPaciente());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panelCotizacion(new cotizacion());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelAgregarServicios(new agregarServicios());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panelCuadreDiario(new cuadreDiario());
        }
    }
}
