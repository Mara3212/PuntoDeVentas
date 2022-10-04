using Capa_de_datos;
using Capa_Entidad;
using System.Data;

namespace Capa_Negocio
{
    public class CN_Carrito
    {
        readonly CD_Carrito objCarrito = new CD_Carrito();

        #region Buscar
        public CE_Carrito Buscar(string buscar)
        {
            return objCarrito.Buscar(buscar);
        }
        #endregion

        #region agregar
        public DataTable Agregar(string producto, decimal cantidad)
        {
            return objCarrito.Agregar(producto, cantidad);
        }
        #endregion

        #region venta
        public void Venta(string factura, decimal total, DateTime fecha, int idusuario)
        {
            objCarrito.Venta(factura, total, fecha, idusuario);
        }
        #endregion

        #region ventadetalle
        public void Venta_Detalle(string codigo, decimal cantidad, string factura, decimal totalarticulo)
        {
            objCarrito.Venta_Detalle(codigo, cantidad, factura, totalarticulo);
        }
        #endregion
    }
}
