using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;

namespace ClinicaDiaz
{
    public partial class nuevaFactura : Form
    {
        SqlConnection cn = new SqlConnection(@"Data source=localhost\SQLEXPRESS; Initial Catalog=clinicaDiaz; Integrated Security=True");
        public static int conteo = 0;
        public static double costoTotal;
        string[,] ventas = new string[250, 6];
        int fila = 0;
        int cantidad;
        double precio = 0;
        double descuento_producto = 0;
        double subtotal = 0;

        public nuevaFactura()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*filtrando servicios y clientes*/
        private void nuevaFactura_Load(object sender, EventArgs e)
        {
            var aux = new Metodos();
            aux.listarPersona(dtgvClientes);

            var la = new Metodos();
            la.listarServicios(dtgvBuscarServicios);

            this.timer1.Enabled = true;
        }
        /* final filtrando servicios y clientes*/

        /*Buscando clientes*/
        private void txtBuscarCliente_TextChanged(object sender, EventArgs e)
        {
            var aux = new Metodos();
            aux.filtrarPersonas(dtgvClientes, this.txtBuscarCliente.Text.Trim()); 
        }
        /*Buscando clientes*/

        /*poniendo valores de dtgvClientes en sus respectivos textboxs*/
        private void dtgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvClientes.Rows[e.RowIndex].Cells[e.ColumnIndex].Value !=null)
            {
                dtgvClientes.CurrentRow.Selected = true;
                txtCedula.Text = dtgvClientes.Rows[e.RowIndex].Cells["Cedula"].FormattedValue.ToString();
                txtNombre.Text = dtgvClientes.Rows[e.RowIndex].Cells["Nombre"].FormattedValue.ToString();
                txtDireccion.Text = dtgvClientes.Rows[e.RowIndex].Cells["Direccion"].FormattedValue.ToString();
                txtTelefono.Text = dtgvClientes.Rows[e.RowIndex].Cells["Telefono"].FormattedValue.ToString();
            }
        }
        /*final poniendo valores de dtgvClientes en sus respectivos textboxs*/

        /*Buscando servicios*/
        private void txtBuscarServicios_TextChanged(object sender, EventArgs e)
        {
            var la = new Metodos();
            la.filtrarServicios(dtgvBuscarServicios, this.txtBuscarServicios.Text.Trim()); 
        }
        /* finaaaal Buscando servicios*/

        /*poniendo valores de dtgvServicios en sus respectivos textboxs*/
        private void dtgvBuscarServicios_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvBuscarServicios.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dtgvBuscarServicios.CurrentRow.Selected = true;
                    txtServicios.Text = dtgvBuscarServicios.Rows[e.RowIndex].Cells["NombreServicio"].FormattedValue.ToString();
                    txtPrecioUnidad.Text = dtgvBuscarServicios.Rows[e.RowIndex].Cells["PrecioUnidad"].FormattedValue.ToString();
                    txtPrecio.Text = dtgvBuscarServicios.Rows[e.RowIndex].Cells["Precio"].FormattedValue.ToString();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "fallo");
            }
        }
        /* final poniendo valores de dtgvServicios en sus respectivos textboxs*/

        /*cargando hora y fecha a textboxs*/
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToShortDateString();
            lblHora.Text = DateTime.Now.ToLongTimeString();
        }
        /*cargando hora y fecha a textboxs*/

        /*Cargando productos a DataGridView Final*/
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtServicios.Text != "" && txtPrecioUnidad.Text != "" && txtCantidad.Text != "")
                {
                    cantidad = int.Parse(txtCantidad.Text.Trim());
                    precio = double.Parse(txtPrecioUnidad.Text.Trim());

                    if (txtDescuento.Text.Length > 0)
                    {
                        descuento_producto = cantidad * precio * (double.Parse(txtDescuento.Text.Trim()) / 100);
                        subtotal = cantidad * precio - descuento_producto;

                        ventas[fila, 0] = txtServicios.Text;
                        ventas[fila, 1] = txtCantidad.Text;
                        ventas[fila, 2] = txtPrecioUnidad.Text;
                        ventas[fila, 3] = (float.Parse(txtDescuento.Text) / 100).ToString();
                        ventas[fila, 4] = subtotal.ToString();
                        dtgvListaFactura.Rows.Add(ventas[fila, 0], ventas[fila, 1], ventas[fila, 2], ventas[fila, 3], ventas[fila, 4]);
                        fila++;

                        txtServicios.Text = txtPrecioUnidad.Text = txtDescuento.Text = txtCantidad.Text = "";
                    }
                    else {
                        ventas[fila, 0] = txtServicios.Text;
                        ventas[fila, 1] = txtCantidad.Text;
                        ventas[fila, 2] = txtPrecioUnidad.Text;
                        ventas[fila, 3] = null;
                        ventas[fila, 4] = (cantidad * precio).ToString();
                        dtgvListaFactura.Rows.Add(ventas[fila, 0], ventas[fila, 1], ventas[fila, 2], ventas[fila, 3], ventas[fila, 4]);
                        fila++;

                        txtServicios.Text = txtPrecioUnidad.Text = txtDescuento.Text = txtCantidad.Text = "";
                    }
                }
                else 
                {
                    MessageBox.Show("Debe llenar todos los Campos correspondientes a servicios para poder agregar a factura","ALERTA");
               
                }
            } 
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
            costoPagar();
        }
        /*final Cargando productos a DataGridView Final*/


       /*Cargando precios de servicios a labeltotal*/
        public void costoPagar()
        {
            costoTotal = 0;
            foreach (DataGridViewRow Fila in dtgvListaFactura.Rows)
            {
                costoTotal += Convert.ToDouble(Fila.Cells[4].Value);
            }
            lblTotal.Text = costoTotal.ToString();
        }
        /* final Cargando precios de servicios a labeltotal*/

        /*cargando devoluciones*/
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                lblDevolucion.Text = (float.Parse(txtEfectivo.Text) - float.Parse(lblTotal.Text)).ToString();
 
            }
            catch  { lblDevolucion.Text = "0.00"; }
            
        }
        /*final cargando devoluciones*/

        /*GENERANDO CODIGO(NUM) DE FACTURA*/
        int secuencia_facturas;
        public void secuenciaFactura(int secuencia) 
        {
            try
            {
                SqlDataReader lectorSecuencia;

                cn.Open();
                SqlCommand comando = new SqlCommand();
                comando.Connection = cn;
                comando.CommandText = "SELECT * FROM Secuencias_Facturas where id_Secuencia=" + secuencia;
                lectorSecuencia = comando.ExecuteReader();

                if (lectorSecuencia.Read() == true)
                {
                    if (secuencia == 1)
                    {
                        secuencia_facturas = lectorSecuencia.GetInt32(2) + 1;
                        txtnumfactura.Text = secuencia_facturas.ToString("EF00000000");
                    }
                    if (secuencia == 2)
                    {
                        secuencia_facturas = lectorSecuencia.GetInt32(2) + 1;
                        txtnumfactura.Text = secuencia_facturas.ToString("TJ00000000");
                    }
                }
                else {MessageBox.Show("problema"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);}
            finally {cn.Close(); }
        }
        /* final  GENERANDO CODIGO(NUM) DE FACTURA*/

        /*RadioButtons efectivo y tarjeta*/
        private void rbtEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtEfectivo.Checked==true) 
            {
                secuenciaFactura(1);
            }
        }

        private void rbtTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtTarjeta.Checked == true)
            {
                secuenciaFactura(2);
            }
        }
        /* finall RadioButtons efectivo y tarjeta*/

        /*restando cantidad total y removiendo servicio de dtgvListaFactura*/
        private void button5_Click(object sender, EventArgs e)
        {
            conteo = dtgvListaFactura.RowCount;
           
            if (conteo > 0)
            {
                costoTotal = costoTotal - (Convert.ToDouble(dtgvListaFactura.Rows[dtgvListaFactura.CurrentRow.Index].Cells[4].Value));
                lblTotal.Text= costoTotal.ToString();

                dtgvListaFactura.Rows.RemoveAt(dtgvListaFactura.CurrentRow.Index);
                conteo--;
            }
            else { MessageBox.Show("No tiene elementos para eliminar.", "AVISO");}
        }
        /* final restando cantidad total y removiendo servicio de dtgvListaFactura*/


        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += Imprimir;
            printDocument1.Print();
        }
        private void Imprimir(object sender, PrintPageEventArgs e)
        {
            int y = 40;
            int x = 40;
           
            int yd = 40;
            int xd = 500;
            int width = 200;
          

            Font fonth1 = new Font("Arial", 18, FontStyle.Bold, GraphicsUnit.Point);
            Font fonth2 = new Font("Arial", 15, FontStyle.Bold, GraphicsUnit.Point);
            Font font2 = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
            Font fonth3 = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point);
            Font font3 = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point);
            Font fonth4 = new Font("Arial", 9, FontStyle.Regular, GraphicsUnit.Point);

            e.Graphics.DrawString("Clinica DR. Diaz", fonth1, Brushes.Black, new RectangleF(x, y+=20, width+100, 50));
            e.Graphics.DrawString("Centro Odontologico", font2, Brushes.Black, new RectangleF(x+20, y+=25, width, 50));
            e.Graphics.DrawString("RNC: ", fonth3, Brushes.Black, new RectangleF(x, y+=20, width, 50));
            e.Graphics.DrawString("DRA.Jarinirca Montero Diaz", fonth3, Brushes.Black, new RectangleF(x, y+=20, width+150, 50));
            e.Graphics.DrawString("C/yolanda Guzman, #41, Los Frailes II", fonth3, Brushes.Black, new RectangleF(x, y+=20, width+150, 50));
            e.Graphics.DrawString("Telefonos: 829-608-8112 / 829-430-5612", fonth4, Brushes.Black, new RectangleF(x, y+=20, width+200, 50));
            /*----------------*/

            e.Graphics.DrawString("NO. Factura: "+txtnumfactura.Text, font3, Brushes.Black, new RectangleF(xd, yd += 20, width, 50));
          
            e.Graphics.DrawString("Fecha: "+ lblFecha.Text, fonth4, Brushes.Black, new RectangleF(xd, yd+=30, width, 50));
            e.Graphics.DrawString("Hora: "+ lblHora.Text, fonth4, Brushes.Black, new RectangleF(xd, yd+=20, width, 50));
            /*-------paciente---------*/

            e.Graphics.DrawString("----------Informacion del paciente---------", fonth2, Brushes.Black, new RectangleF(x+200, y+=40, width+200, 50));
            e.Graphics.DrawString("Nombre: "+txtNombre.Text.ToUpper(),fonth3, Brushes.Black, new RectangleF(x, y+=20, width, 50));
            e.Graphics.DrawString("Cedula: "+txtCedula.Text,fonth3, Brushes.Black, new RectangleF(x, y+=20, width, 50));
            e.Graphics.DrawString("Telefono: "+txtTelefono.Text,fonth3, Brushes.Black, new RectangleF(x, y+=20, width, 50));
            e.Graphics.DrawString("Direccion: "+txtDireccion.Text,fonth3, Brushes.Black, new RectangleF(x, y+=20, width+150, 50));

            /*datagridview*/
            Bitmap bm = new Bitmap(this.dtgvListaFactura.Width, this.dtgvListaFactura.Height);
            dtgvListaFactura.DrawToBitmap(bm, new Rectangle(0,0, this.dtgvListaFactura.Width, this.dtgvListaFactura.Height));
            e.Graphics.DrawImage(bm, x, y += 40);


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
