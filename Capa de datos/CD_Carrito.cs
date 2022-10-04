using Capa_Entidad;
using System.Data;
using System.Data.SqlClient;

namespace Capa_de_datos
{
    public class CD_Carrito
    {
        CD_Conexion con = new CD_Conexion();
        CE_Carrito carrito = new CE_Carrito();

        #region Buscar
        public CE_Carrito Buscar(string buscar)
        {
            SqlDataAdapter da = new("SP_C_Buscar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@buscar", SqlDbType.VarChar).Value = buscar;
            DataSet ds = new();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                carrito.Nombre = Convert.ToString(row[1]);
                carrito.Precio = Convert.ToDecimal(row[4]);
                con.CerrarConexion();
                return carrito;
            }
            else
            {
                return carrito;
            }
        }
        #endregion

        #region agregar
        public DataTable Agregar(string producto, decimal cantidad)
        {
            SqlDataAdapter da = new ("SP_C_Buscar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@buscar", SqlDbType.VarChar).Value = producto;
            DataSet ds = new();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            var precio = dt.Rows[0][4];
            decimal Cantidad = cantidad;
            decimal ProductoTotal = Cantidad * Convert.ToDecimal(precio);

            dt.Columns.Add("ProductoTotal", typeof(decimal));

            foreach (DataRow row in dt.Rows)
            {
                row["Cantidad"] = cantidad;
                row["ProductoTotal"] = ProductoTotal;
            }

            con.CerrarConexion();
            return dt;
        }
        #endregion

        #region venta
        public void Venta(string factura, decimal total, DateTime fecha, int idusuario)
        {
            SqlCommand com = new("SP_C_Venta", con.AbrirConexion());
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@No_Factura", SqlDbType.VarChar).Value = factura;
            com.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = fecha;
            com.Parameters.Add("@Total", SqlDbType.Decimal).Value = total;
            com.Parameters.Add("@idusuario", SqlDbType.Int).Value = idusuario;
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region ventadetalle
        public void Venta_Detalle(string codigo, decimal cantidad, string factura,decimal totalarticulo)
        {
            SqlCommand com = new("SP_C_Venta_Detalle", con.AbrirConexion());
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@codigo", SqlDbType.VarChar).Value = codigo;
            com.Parameters.Add("@cantidad", SqlDbType.Decimal).Value = cantidad;
            com.Parameters.Add("@No_Factura", SqlDbType.VarChar).Value = factura;
            com.Parameters.Add("@total", SqlDbType.Decimal).Value = totalarticulo;
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion
    }
}
