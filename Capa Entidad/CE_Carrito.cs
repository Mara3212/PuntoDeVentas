namespace Capa_Entidad
{
    public class CE_Carrito
    {
        private string _Codigo;
        private string _Nombre;
        private decimal _Cantidad;
        private decimal _Precio;
        private decimal _Total;

        public string Codigo { get => _Codigo; set => _Codigo = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public decimal Cantidad { get => _Cantidad; set => _Cantidad = value; }
        public decimal Precio { get => _Precio; set => _Precio = value; }
        public decimal Total { get => _Total; set => _Total = value; }
    }
}
