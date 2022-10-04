using System.Data.SqlClient;
using System.Data;
using Capa_Entidad;
using System;

namespace Capa_de_datos
{
    public class CD_Usuarios
    {
        private readonly CD_Conexion con = new CD_Conexion();
        private readonly CE_Usuarios ce = new CE_Usuarios();

        //crud usuarios

        #region Insertar
        public void CD_Insertar(CE_Usuarios Usuarios)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "SP_U_Insertar",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("@Nombres", Usuarios.Nombres);
            com.Parameters.AddWithValue("@Apellidos", Usuarios.Apellidos);
            com.Parameters.AddWithValue("@DUI", Usuarios.DUI);
            com.Parameters.AddWithValue("@NIT", Usuarios.NIT);
            com.Parameters.AddWithValue("@Correo", Usuarios.Correo);
            com.Parameters.AddWithValue("@Telefono", Usuarios.Telefono);
            com.Parameters.Add("@Fecha_Nac", SqlDbType.Date).Value = Usuarios.Fecha_Nac;
            com.Parameters.AddWithValue("@Privilegio", Usuarios.Privilegio);
            com.Parameters.AddWithValue("@img", Usuarios.Img);
            com.Parameters.AddWithValue("@usuario", Usuarios.Usuario);
            com.Parameters.AddWithValue("@contraseña", Usuarios.Contraseña);
            com.Parameters.AddWithValue("@patron", Usuarios.Patron);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region Consultar
        public CE_Usuarios CD_Consulta(int IdUsuario)
        {
            SqlDataAdapter da = new SqlDataAdapter("SP_U_Consultar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = IdUsuario;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.Nombres = Convert.ToString(row[1]);
            ce.Apellidos = Convert.ToString(row[2]);
            ce.DUI = Convert.ToInt32(row[3]);
            ce.NIT = Convert.ToInt32(row[4]);
            ce.Correo = Convert.ToString(row[5]);
            ce.Telefono = Convert.ToInt32(row[6]);
            ce.Fecha_Nac = Convert.ToDateTime(row[7]);
            ce.Privilegio = Convert.ToInt32(row[8]);
            ce.Img = (byte[])row[9];
            ce.Usuario = Convert.ToString(row[10]);

            return ce;
        }
        #endregion

        #region Eliminar
        public void CD_Eliminar(CE_Usuarios Usuarios)
        {
            SqlCommand com = new SqlCommand
            {
                Connection = con.AbrirConexion(),
                CommandText = "SP_U_Eliminar",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@IdUsuario", Usuarios.IdUsuario);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region Actualizar Datos
        public void CD_ActualizarDatos(CE_Usuarios Usuarios)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "SP_U_ActualizarDatos",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@IdUsuario", Usuarios.IdUsuario);
            com.Parameters.AddWithValue("@Nombres", Usuarios.Nombres);
            com.Parameters.AddWithValue("@Apellidos", Usuarios.Apellidos);
            com.Parameters.AddWithValue("@DUI", Usuarios.DUI);
            com.Parameters.AddWithValue("@NIT", Usuarios.NIT);
            com.Parameters.AddWithValue("@Correo", Usuarios.Correo);
            com.Parameters.AddWithValue("@Telefono", Usuarios.Telefono);
            com.Parameters.Add("@Fecha_Nac", SqlDbType.Date).Value = Usuarios.Fecha_Nac;
            com.Parameters.AddWithValue("@Privilegio", Usuarios.Privilegio);
            com.Parameters.AddWithValue("@Usuario", Usuarios.Usuario);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region Actualizar Pass
        public void CD_ActualizarPass(CE_Usuarios Usuarios)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "SP_U_ActualizarPass",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@IdUsuario", Usuarios.IdUsuario);
            com.Parameters.AddWithValue("@Contraseña", Usuarios.Contraseña);
            com.Parameters.AddWithValue("@patron", Usuarios.Patron);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region Actualizar IMG
        public void CD_ActualizarIMG(CE_Usuarios Usuarios)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "SP_U_ActualizarIMG",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@IdUsuario", Usuarios.IdUsuario);
            com.Parameters.AddWithValue("@img", Usuarios.Img);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        //vista Usuarios
        #region Buscar Usuarios
        public DataTable Buscar(String buscar)
        {
            SqlDataAdapter da = new SqlDataAdapter("SP_U_Buscar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@buscar", SqlDbType.VarChar).Value = buscar;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }
        #endregion

        // login

        public CE_Usuarios Login(string usuario, string contra)
        {
            String patron = "Mara";
            SqlDataAdapter da = new ("SP_U_Validar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = usuario;
            da.SelectCommand.Parameters.Add("@Contra", SqlDbType.VarChar).Value = contra;
            da.SelectCommand.Parameters.Add("@Patron", SqlDbType.VarChar).Value = patron;
            DataSet ds = new();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            if(dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                ce.IdUsuario = Convert.ToInt32(row[0]);
                ce.Privilegio = Convert.ToInt32(row[1]);
            }
            return ce;
        }
    }
}
