using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace ClinicaDiaz
{
    class Metodos
    {
        SqlConnection connection = new SqlConnection(@"Data source=localhost\SQLEXPRESS; Initial Catalog=clinicaDiaz; Integrated Security=True");
        public void listarPersona(DataGridView data)
        {
            try
            {

                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter("listar_persona", connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                data.DataSource = dt;
                data.Columns[0].Width = 55;
                data.Columns[0].HeaderCell.Value = "ID";
                data.Columns[1].Width = 100;
                data.Columns[1].HeaderCell.Value = "Cedula";
                data.Columns[2].Width = 75;
                data.Columns[2].HeaderCell.Value = "Fecha";
                data.Columns[3].Width = 250;
                data.Columns[3].HeaderCell.Value = "Nombre";
                data.Columns[4].Width = 75;
                data.Columns[4].HeaderCell.Value = "Nacimiento";
                data.Columns[5].Width = 60;
                data.Columns[5].HeaderCell.Value = "Edad";
                data.Columns[6].Width = 60;
                data.Columns[6].HeaderCell.Value = "Sexo";
                data.Columns[7].Width = 250;
                data.Columns[7].HeaderCell.Value = "Direccion";
                data.Columns[8].Width = 100;
                data.Columns[8].HeaderCell.Value = "Telefono";
                data.Columns[9].Width = 500;
                data.Columns[9].HeaderCell.Value = "Motivo De Consulta";
            }
            catch (Exception)
            {
                throw;
            }
            finally { connection.Close(); }
        }

        public void filtrarPersonas(DataGridView data, string buscarCedula)
        {
            try
            {
                connection.Open();
                SqlCommand sql = new SqlCommand("filtro_persona", connection);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Add("@filtro", SqlDbType.VarChar, 200).Value = buscarCedula;
                sql.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql);
                da.Fill(dt);
                data.DataSource = dt;

            }
            catch (Exception) { throw; }
            finally { connection.Close(); }
        }

        /*---------------Mostrando en DataGridView los servicios*/
        public void listarServicios(DataGridView data)
        {
            try
            {

                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter("listarServicios", connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                data.DataSource = dt;
                data.Columns[0].Width = 100;
                data.Columns[0].HeaderCell.Value = "idServicio";
                data.Columns[1].Width = 400;
                data.Columns[1].HeaderCell.Value = "NombreServicio";
                data.Columns[2].Width = 80;
                data.Columns[2].HeaderCell.Value = "PrecioUnidad";
                data.Columns[3].Width = 80;
                data.Columns[3].HeaderCell.Value = "Precio";
            
            }
            catch (Exception)
            {
                throw;
            }
            finally { connection.Close(); }
        }

        public void filtrarServicios(DataGridView data, string buscarCedula)
        {
            try
            {
                connection.Open();
                SqlCommand sql = new SqlCommand("filtrarServicios", connection);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Add("@filtro", SqlDbType.VarChar, 200).Value = buscarCedula;
                sql.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql);
                da.Fill(dt);
                data.DataSource = dt;

            }
            catch (Exception) { throw; }
            finally { connection.Close(); }
        }
        /*--------------- final de Mostrando en DataGridView los servicios-----------------------*/
    }

}
