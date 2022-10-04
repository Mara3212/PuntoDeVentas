using Capa_de_datos;
using Capa_Entidad;
using System.Data;

namespace Capa_Negocio
{
    public class CN_Usuarios
    {
        private readonly CD_Usuarios objDatos = new CD_Usuarios();

        //crud usuarios

        #region Consultar
        public CE_Usuarios Consulta(int IdUsuario)
        {
            return objDatos.CD_Consulta(IdUsuario);
        }
        #endregion

        #region Insertar
        public void Insertar(CE_Usuarios Usuarios)
        {
            objDatos.CD_Insertar(Usuarios);
        }
        #endregion

        #region Eliminar
        public void Eliminar(CE_Usuarios Usuarios)
        {
            objDatos.CD_Eliminar(Usuarios);
        }
        #endregion

        #region Actualizar Datos
        public void ActualizarDatos(CE_Usuarios Usuarios)
        {
            objDatos.CD_ActualizarDatos(Usuarios);
        }
        #endregion

        #region Actualizar Pass
        public void ActualizarPass(CE_Usuarios Usuarios)
        {
            objDatos.CD_ActualizarPass(Usuarios);
        }
        #endregion

        #region Actualizar Imagen
        public void ActualizarIMG(CE_Usuarios Usuarios)
        {
            objDatos.CD_ActualizarIMG(Usuarios);
        }
        #endregion

        //vista usuarios

        #region Buscar Usuarios
        public DataTable Buscar(string buscar)
        {
            return objDatos.Buscar(buscar);
        }
        #endregion

        // login
        #region login
        public CE_Usuarios LogIn(string usuario, string contra)
        {
            return objDatos.Login(usuario, contra);
        }
        #endregion

        #region cargardatos
        public CE_Usuarios Cargar(int idusuario)
        {
            return objDatos.CD_Consulta(idusuario);
        }
        #endregion
    }
}
