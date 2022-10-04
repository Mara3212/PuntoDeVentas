using System.Windows;
using System.Windows.Controls;
using Capa_Negocio;
using PuntoDeVentas.SCS.Boxes;

namespace PuntoDeVentas.Views
{
    
    public partial class Productos : UserControl
    {
        Error error;

        #region Inicial
        public Productos()
        {
            InitializeComponent();
            Buscar("");
        }
        #endregion

        readonly CN_Productos obj_CN_Productos = new CN_Productos();

        #region Buscar
        public void Buscar (string buscar)
        {
            try
            {
                GridDatos.ItemsSource = obj_CN_Productos.BuscarProducto(buscar).DefaultView;
            }
            catch (System.Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
        #endregion

        #region Buscando
        private void Buscando(object sender, TextChangedEventArgs e)
        {
            Buscar(tbBuscar.Text);
        }
        #endregion

        #region CRUD
        #region Create
        private void Agregar_Producto(object sender, RoutedEventArgs e)
        {
            try
            {
                CRUDProductos ventana = new CRUDProductos();
                FrameProductos.Content = ventana;
                ventana.BtnCrear.Visibility = Visibility.Visible;
            }
            catch (System.Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }

        #endregion

        #region Read
        private void Consultar(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = (int)((Button)sender).CommandParameter;
                CRUDProductos ventana = new CRUDProductos();
                FrameProductos.Content = ventana;
                ventana.IdProducto = id;
                ventana.Consultar();
                ventana.Titulo.Text = "Consulta De Producto";
                ventana.tbNombres.IsEnabled = false;
                ventana.tbCodigo.IsEnabled = false;
                ventana.tbCantidad.IsEnabled = false;
                ventana.tbActivo.IsEnabled = false;
                ventana.tbPrecio.IsEnabled = false;
                ventana.cbGrupo.IsEnabled = false;
                ventana.tbUnidadMedida.IsEnabled = false;
                ventana.tbDescripcion.IsEnabled = false;
                ventana.BtnSubir.IsEnabled = false;
            }
            catch (System.Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
        #endregion

        #region Update
        private void Actualizar(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = (int)((Button)sender).CommandParameter;
                CRUDProductos ventana = new CRUDProductos();
                FrameProductos.Content = ventana;
                ventana.IdProducto = id;
                ventana.Consultar();
                ventana.Titulo.Text = "Actualizar Producto";
                ventana.tbNombres.IsEnabled = true;
                ventana.tbCodigo.IsEnabled = true;
                ventana.tbCantidad.IsEnabled = true;
                ventana.tbActivo.IsEnabled = true;
                ventana.tbPrecio.IsEnabled = true;
                ventana.cbGrupo.IsEnabled = true;
                ventana.tbUnidadMedida.IsEnabled = true;
                ventana.tbDescripcion.IsEnabled = true;
                ventana.BtnSubir.IsEnabled = true;
                ventana.BtnActualizar.Visibility = Visibility.Visible;
            }
            catch (System.Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
        #endregion

        #region Delete
        private void Eliminar(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = (int)((Button)sender).CommandParameter;
                CRUDProductos ventana = new CRUDProductos();
                FrameProductos.Content = ventana;
                ventana.IdProducto = id;
                ventana.Consultar();
                ventana.Titulo.Text = "Eliminar Producto";
                ventana.tbNombres.IsEnabled = false;
                ventana.tbCodigo.IsEnabled = false;
                ventana.tbCantidad.IsEnabled = false;
                ventana.tbActivo.IsEnabled = false;
                ventana.tbPrecio.IsEnabled = false;
                ventana.cbGrupo.IsEnabled = false;
                ventana.tbUnidadMedida.IsEnabled = false;
                ventana.tbDescripcion.IsEnabled = false;
                ventana.BtnSubir.IsEnabled = false;
                ventana.BtnEliminar.Visibility = Visibility.Visible;
            }
            catch (System.Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
        #endregion
        #endregion
    }
}
