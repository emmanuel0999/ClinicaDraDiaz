using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaDiaz
{
    class Funcionalidades
    {
       
        public static DataSet Ejecutar(string query) 
        {
            SqlConnection cn = new SqlConnection(@"Data source=localhost\SQLEXPRESS; Initial Catalog=clinicaDiaz; Integrated Security=True");

            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(query, cn);

            sda.Fill(ds);

            cn.Close();
            return ds;

        }
        public static DataSet IncrementoNumFactura(string query) 
        {
            SqlConnection cn = new SqlConnection(@"Data source=localhost\SQLEXPRESS; Initial Catalog=clinicaDiaz; Integrated Security=True");
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(query, cn);

            sda.Fill(ds);
            cn.Close();
            return  ds;

        }
    }
}
