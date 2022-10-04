using System;
using System.Data;
using System.Data.SqlClient;
using Capa_Entidad;

namespace Capa_de_datos
{
    public class CD_Productos
    {
        CD_Conexion con = new CD_Conexion();
        CE_Productos productos = new CE_Productos();

        //vista productos

        #region Buscar
        public DataTable Buscar(String buscar)
        {
            SqlDataAdapter da = new SqlDataAdapter("SP_A_Buscar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = buscar;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }
        #endregion

        //vistacrudproductos

        #region CRUD

        #region Consulta
        public CE_Productos Consulta(int IdProducto)
        {
            SqlDataAdapter da = new SqlDataAdapter("SP_A_Consultar",con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@IdArticulo", SqlDbType.Int).Value = IdProducto;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            productos.Nombre = Convert.ToString(row[1]);
            productos.Grupo = Convert.ToInt32(row[2]);
            productos.Codigo = Convert.ToString(row[3]);
            productos.Precio = Convert.ToDecimal(row[4]);
            productos.Activo = Convert.ToBoolean(row[5]);
            productos.Cantidad = Convert.ToDecimal(row[6]);
            productos.UnidadMedida = Convert.ToString(row[7]);
            productos.Img = (byte[])row[8];
            productos.Descripcion = Convert.ToString(row[9]);

            return productos;
        }
        #endregion

        #region Insertar
        
        public void CD_Insertar(CE_Productos productos)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "SP_A_Insertar",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@Nombre", productos.Nombre);
            com.Parameters.AddWithValue("@Grupo", productos.Grupo);
            com.Parameters.AddWithValue("@Codigo", productos.Codigo);
            com.Parameters.AddWithValue("@Precio", productos.Precio);
            com.Parameters.AddWithValue("@Cantidad", productos.Cantidad);
            com.Parameters.AddWithValue("@Activo", productos.Activo);
            com.Parameters.AddWithValue("@UnidadMedida", productos.UnidadMedida);
            com.Parameters.AddWithValue("@Img", productos.Img);
            com.Parameters.AddWithValue("@Descripcion", productos.Descripcion);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region Eliminar

        public void CD_Eliminar(CE_Productos productos)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "SP_A_Eliminar",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@IdArticulo", productos.IdArticulo);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region Actualizar Datos

        public void CD_Atualizar(CE_Productos productos)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "SP_A_Actualizar",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@IdArticulo", productos.IdArticulo);
            com.Parameters.AddWithValue("@Nombre", productos.Nombre);
            com.Parameters.AddWithValue("@Grupo", productos.Grupo);
            com.Parameters.AddWithValue("@Codigo", productos.Codigo);
            com.Parameters.AddWithValue("@Precio", productos.Precio);
            com.Parameters.AddWithValue("@Cantidad", productos.Cantidad);
            com.Parameters.AddWithValue("@Activo", productos.Activo);
            com.Parameters.AddWithValue("@UnidadMedida", productos.UnidadMedida);
            com.Parameters.AddWithValue("@Descripcion", productos.Descripcion);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region ActualizarIMG

        public void CD_AtualizarIMG(CE_Productos productos)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "SP_A_ActualizarIMG",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@IdArticulo", productos.IdArticulo);
            com.Parameters.AddWithValue("@Img", productos.Img);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #endregion
    }
}

