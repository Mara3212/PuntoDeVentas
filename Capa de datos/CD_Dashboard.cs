using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_datos
{
    public class CD_Dashboard
    {
        CD_Conexion con = new CD_Conexion();

        #region CantidadVentas
        public int CantidadVentas()
        {
            int total;
            SqlCommand da = new ("SP_D_CantidadVentas", con.AbrirConexion());
            da.CommandType = System.Data.CommandType.StoredProcedure;
            total = Convert.ToInt32(da.ExecuteScalar());
            con.CerrarConexion();

            return total;
        }
        #endregion

        #region Articulos Vendidos
        public decimal Articulos()
        {
            SqlCommand da = new("SP_D_Articulos", con.AbrirConexion());
            da.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader rd = da.ExecuteReader();
            decimal total;
            rd.Read();
            total = Convert.ToDecimal(rd[0]);
            rd.Close();
            con.CerrarConexion();

            return total;
        }
        #endregion

        #region Grafico
        public DataTable Grafico()
        {
            SqlDataAdapter da = new ("SP_D_Grafico", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }
        #endregion
    }
}
