using Capa_de_datos;
using System.Data;

namespace Capa_Negocio
{
    public class CN_Dashboard
    {
        readonly CD_Dashboard objDash = new CD_Dashboard();

        public int CantidadVentas()
        {
            return objDash.CantidadVentas();
        }

        public decimal Articulos()
        {
            return objDash.Articulos();
        }

        public DataTable Grafico()
        {
            return objDash.Grafico();
        }
    }
}
