using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using System.Data.SqlClient;

namespace Capa_de_datos
{
    public class CD_Grupos
    {
        CD_Conexion con = new CD_Conexion();
        CE_Grupos cE_Grupos = new CE_Grupos();

        #region ListarGrupos
        public List<String> ListarGrupos()
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "SP_G_CargarGrupos",
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader reader = com.ExecuteReader();
            List<String> retorno = new();
            while (reader.Read())
            {
                retorno.Add(Convert.ToString(reader["Nombre"]));
            }
            con.CerrarConexion();
            return retorno;

        }
        #endregion

        #region NombreGrupo
        public CE_Grupos Nombre(int IdGrupo)
        {
            SqlDataAdapter da = new SqlDataAdapter("SP_G_NombreGrupo", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@IdGrupo", SqlDbType.Int).Value = IdGrupo;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            cE_Grupos.Nombre = Convert.ToString(row[0]);

            return cE_Grupos;
        }
        #endregion

        #region IdGrupo
        public int IdGrupo(String Nombre)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "SP_G_IdGrupo",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@Nombre", Nombre);
            object valor = com.ExecuteScalar();
            int idGrupo = (int)valor;
            con.CerrarConexion();

            return idGrupo;
        }
        #endregion
    }
}
