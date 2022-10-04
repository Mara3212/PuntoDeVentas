using Capa_Negocio;
using Microsoft.Win32;
using PuntoDeVentas.SCS.Boxes;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

using iTextSharp.text;
using iTextSharp.tool.xml;
using iTextSharp.text.pdf;
using System.IO;

namespace PuntoDeVentas.Views
{
    /// <summary>
    /// Lógica de interacción para POS.xaml
    /// </summary>
    public partial class POS : UserControl
    {
        Error error;

        #region inicio
        public POS()
        {
            InitializeComponent();
            precio();
        }
        #endregion

        #region Buscar
        private void BuscarProducto(object sender, RoutedEventArgs e)
        {
            if (tbBuscar.Text == string.Empty)
            {
                error = new Error();
                error.lblerror.Text = "Busqueda Vacia!";
                error.ShowDialog();
            }
            else
            {
                try
                {
                    CN_Carrito cc = new CN_Carrito();
                    var carrito = cc.Buscar(tbBuscar.Text);

                    if (carrito.Nombre != null)
                    {
                        tbNombre.Text = carrito.Nombre.ToString();
                        tbPrecio.Text = carrito.Precio.ToString();
                        tbCantidad.Focus();
                    }
                    else
                    {
                        error = new Error();
                        error.lblerror.Text = "No se ha encontrado el producto! ";
                        error.ShowDialog();
                        tbBuscar.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    error = new Error();
                    error.lblerror.Text = ex.Message;
                    error.ShowDialog();
                }            }
        }
        #endregion

        #region Limpiar
        void Limpiar()
        {
            tbBuscar.Text = "";
            tbNombre.Text = "";
            tbCantidad.Text = "";
            tbPrecio.Text = "";
            precio();
        }
        #endregion

        #region Agregar

        private void AgregarProducto(object sender, RoutedEventArgs e)
        {
            if (tbNombre.Text == string.Empty || tbCantidad.Text == string.Empty)
            {
                error = new Error();
                error.lblerror.Text = "No se ha encontrado un producto";
                error.ShowDialog();
            }
            else
            {
                string producto = tbNombre.Text;
                decimal cantidad = Convert.ToDecimal(tbCantidad.Text);
                Agregar(producto, cantidad);
                Limpiar();
            }
        }

        decimal can =0;
        public ref decimal Existe(string valor)
        {
            for (int i = 0; i < GridProductos.Items.Count; i++)
            {
                int j = 1;
                DataGridCell celda = Getcelda(i, j);
                TextBlock tb = celda.Content as TextBlock;

                int k = 3;
                DataGridCell celda2 = Getcelda(i, k);
                TextBlock tb2 = celda2.Content as TextBlock;
                can = decimal.Parse(tb2.Text);

                if (tb.Text == valor)
                {
                    GridProductos.Items.RemoveAt(i);
                    return ref can;
                }
                else
                {
                    can = 0;
                }
            }
            return ref can;
        }

        void Agregar(string producto, decimal cantidad)
        {
            try
            {
                CN_Carrito carrito = new CN_Carrito();
                DataTable dt;
                if (GridProductos.HasItems)
                {
                    cantidad += Existe(producto);
                    dt = carrito.Agregar(producto, cantidad);
                    GridProductos.Items.Add(dt);
                }
                else
                {
                    dt = carrito.Agregar(producto, cantidad);
                    GridProductos.Items.Add(dt);
                }
                precio();
            }
            catch (Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
        #endregion

        #region precio
        decimal efectivo, cambio, total;
        void precio()
        {
            try
            {
                total = 0;
                for (int i = 0; i < GridProductos.Items.Count; i++)
                {
                    decimal precio;
                    int j = 4;
                    DataGridCell celda = Getcelda(i, j);
                    TextBlock tb = celda.Content as TextBlock;
                    precio = decimal.Parse(tb.Text);
                    total += precio;
                }
                cambio = efectivo - total;

                lblCambio.Content = "Cambio: $" + cambio.ToString();
                lblEfectivo.Content = "Efectivo: $" + efectivo.ToString("###,###.00");
                lblTotal.Content = "Total: $" + total.ToString();
            }
            catch (Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
        #endregion

        #region anularOrden
        private void EliminarProducto(object sender, RoutedEventArgs e)
        {
            try
            {
                var selelcionado = GridProductos.SelectedItem;
                if (selelcionado != null)
                {
                    GridProductos.Items.Remove(selelcionado);
                    if (GridProductos.Items.Count < 1)
                    {
                        efectivo = 0;
                    }
                }
                precio();
            }
            catch (Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
        #endregion

        #region cambiarCantidad
        private void CambiarCantidad(object sender, RoutedEventArgs e)
        {
            try
            {
                var seleccionado = GridProductos.SelectedItem;
                if (seleccionado != null)
                {
                    var celda = GridProductos.SelectedCells[0];
                    var codigo = (celda.Column.GetCellContent(celda.Item) as TextBlock).Text;

                    var ingresar = new Ingresar();
                    ingresar.ShowDialog();

                    if (ingresar.Total > 0)
                    {
                        GridProductos.Items.Remove(seleccionado);
                        Agregar(codigo, ingresar.Total);
                        precio();
                    }
                }
            }
            catch (Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
        #endregion

        #region ingresar efectivo
        private void Efectivo(object sender, RoutedEventArgs e)
        {
            try
            {
                var ingresar = new Ingresar();
                ingresar.ShowDialog();
                if (ingresar.Efectivo > 0)
                {
                    efectivo = ingresar.Efectivo;
                    precio();
                }
            }
            catch (Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
        #endregion

        #region anularOrden
        private void AnularOrden(object sender, RoutedEventArgs e)
        {
            efectivo = 0;
            GridProductos.Items.Clear();
            Limpiar();
        }
        #endregion

        #region pagar e imprimir
        private void Pagar(object sender, RoutedEventArgs e)
        {
            if (GridProductos.Items.Count >= 1)
            {
                venta();
                efectivo = 0;
                precio();
            }
            else
            {
                error = new Error();
                error.lblerror.Text = "No se han agregado productos";
                error.ShowDialog();
            }
        }
        CN_Carrito cN_Carrito;
        void venta()
        {
            try
            {
                string factura = "F-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + "-" + Properties.Settings.Default.IdUsuario;
                int idusuario = Properties.Settings.Default.IdUsuario;
                DateTime fecha = DateTime.Now;
                cN_Carrito = new CN_Carrito();

                if (cambio >= 0)
                {
                    cN_Carrito.Venta(factura, total, fecha, idusuario);
                    venta_detalle(factura);
                    GridProductos.Items.Clear();
                }
                else
                {
                    error = new Error();
                    error.lblerror.Text = "Ingrese un pago mayor ho igual a la venta";
                    error.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
        void venta_detalle(string factura)
        {
            try
            {
                cN_Carrito = new CN_Carrito();
                for (int i = 0; i < GridProductos.Items.Count; i++)
                {
                    string codigo;
                    decimal totalarticulo, cantidad;

                    int j = 0;
                    DataGridCell cell = Getcelda(i, j);
                    TextBlock tb = cell.Content as TextBlock;
                    codigo = tb.Text;

                    int k = 3;
                    DataGridCell cell2 = Getcelda(i, k);
                    TextBlock tb2 = cell2.Content as TextBlock;
                    cantidad = Convert.ToDecimal(tb2.Text);

                    int l = 4;
                    DataGridCell cell3 = Getcelda(i, l);
                    TextBlock tb3 = cell3.Content as TextBlock;
                    totalarticulo = Convert.ToDecimal(tb3.Text);

                    cN_Carrito.Venta_Detalle(codigo, cantidad, factura, totalarticulo);
                }
                Imprimir(factura);
            }
            catch (Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }

        void Imprimir(string factura)
        {
            try
            {
                System.Windows.Forms.SaveFileDialog savefile = new System.Windows.Forms.SaveFileDialog
                {
                    FileName = factura + ".pdf"
                };

                string Pagina = Properties.Resources.Ticket.ToString();
                Pagina = Pagina.Replace("@Ticket", factura);
                Pagina = Pagina.Replace("@efectivo", efectivo.ToString("###,###.00"));
                Pagina = Pagina.Replace("@cambio", cambio.ToString());
                Pagina = Pagina.Replace("@Usuario", Properties.Settings.Default.IdUsuario.ToString());
                Pagina = Pagina.Replace("@Fecha", DateTime.Now.ToString("dd/MM/yyyy"));

                string filas = string.Empty;

                for (int i = 0; i < GridProductos.Items.Count; i++)
                {
                    string nombre, cantidad;
                    decimal preciounitario, totalarticulo;

                    int j = 1;
                    DataGridCell cell1 = Getcelda(i, j);
                    TextBlock tb1 = cell1.Content as TextBlock;
                    nombre = tb1.Text;

                    int k = 3;
                    DataGridCell cell2 = Getcelda(i, k);
                    TextBlock tb2 = cell2.Content as TextBlock;
                    cantidad = tb2.Text;

                    int l = 4;
                    DataGridCell cell3 = Getcelda(i, l);
                    TextBlock tb3 = cell3.Content as TextBlock;
                    totalarticulo = Convert.ToDecimal(tb3.Text);

                    int m = 2;
                    DataGridCell cell4 = Getcelda(i, m);
                    TextBlock tb4 = cell4.Content as TextBlock;
                    preciounitario = Convert.ToDecimal(tb4.Text);

                    filas += "<tr>";
                    filas += "<td align=\"center\">" + cantidad.ToString() + "</td>";
                    filas += "<td align=\"center\">" + nombre.ToString() + "</td>";
                    filas += "<td align=\"right\">" + preciounitario.ToString() + "</td>";
                    filas += "<td align=\"right\">" + totalarticulo.ToString() + "</td>";
                    filas += "</tr>";
                }
                int cant = GridProductos.Items.Count;
                Pagina = Pagina.Replace("@cant_articulos", cant.ToString());
                Pagina = Pagina.Replace("@grid", filas);
                Pagina = Pagina.Replace("@TOTAL", total.ToString());

                if (savefile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                    {
                        int artfilas = GridProductos.Items.Count;
                        Rectangle pagesize = new Rectangle(298, 420 + (artfilas * 12));
                        Document pdfDoc = new Document(pagesize, 25, 25, 25, 25);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        using (StringReader sr = new StringReader(Pagina))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        }
                        pdfDoc.Close();
                        stream.Close();
                    }
                    error = new Error();
                    error.lblerror.Text = "Venta realizada con exito!!!";
                    error.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
        #endregion

        #region filas,columnas y celdas

        public static T GetVisualChild<T>(Visual padre) where T : Visual
        {
            T hijo = default;
            int num = VisualTreeHelper.GetChildrenCount(padre);
            for (int i = 0; i < num; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(padre, i);
                hijo = v as T;
                if (hijo == null)
                {
                    hijo = GetVisualChild<T>(v);
                }

                if (hijo != null)
                {
                    break;
                }
            }

            return hijo;
        }

        public DataGridRow Getfila(int index)
        {
            DataGridRow fila = (DataGridRow)GridProductos.ItemContainerGenerator.ContainerFromIndex(index);
            if (fila == null)
            {
                GridProductos.UpdateLayout();
                GridProductos.ScrollIntoView(GridProductos.Items[index]);
                fila = (DataGridRow)GridProductos.ItemContainerGenerator.ContainerFromIndex(index);
            }

            return fila;
        }

        public DataGridCell Getcelda(int fila, int columna)
        {
            DataGridRow filaCon = Getfila(fila);
            if (filaCon != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(filaCon);
                DataGridCell celda = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columna);

                if (celda == null)
                {
                    GridProductos.ScrollIntoView(filaCon, GridProductos.Columns[columna]);
                    celda = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columna);
                }
                return celda;
            }
            return null;
        }

        #endregion
    }
}
