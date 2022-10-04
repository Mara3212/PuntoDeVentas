using System.Windows;
using System.Windows.Controls;
using Capa_Negocio;
using System.Collections.Generic;
using System.Windows.Media;
using Capa_Entidad;
using Microsoft.Win32;
using System.IO;
using System;
using PuntoDeVentas.SCS.Boxes;

namespace PuntoDeVentas.Views
{

    public partial class CRUDProductos : Page
    {
        public int IdProducto;
        public string Patron = "InfoToolsSV";
        CN_Grupos objeto_CN_Grupos = new CN_Grupos();
        CN_Productos objeto_CN_Productos = new CN_Productos();
        CE_Productos objeto_CE_Productos = new CE_Productos();
        Error error;

        #region Inicial
        public CRUDProductos()
        {
            InitializeComponent();
            Cargar();
        }
        #endregion

        #region Regresar
        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Productos();
        }
        #endregion

        #region Llenar Grupos
        void Cargar()
        {
            try
            {
                List<string> grupos = objeto_CN_Grupos.ListarGrupos();
                for (int i = 0; i < grupos.Count; i++)
                {
                    cbGrupo.Items.Add(grupos[i]);
                }
            }
            catch(Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
        #endregion

        #region Validar Campos
        public bool CamposLlenos()
        {
            if(tbNombres.Text == ""|| tbCodigo.Text=="" || cbGrupo.Text ==""|| tbPrecio.Text == ""||
                tbCantidad.Text == ""|| tbUnidadMedida.Text==""|| tbDescripcion.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region CRUD

        #region Create
        private void Crear(object sender, RoutedEventArgs e)
        {
            if(CamposLlenos() == true)
            {
                try
                {
                    int idGrupo = objeto_CN_Grupos.IdGrupo(cbGrupo.Text);

                    objeto_CE_Productos.Nombre = tbNombres.Text;
                    objeto_CE_Productos.Codigo = tbCodigo.Text;
                    objeto_CE_Productos.Precio = Convert.ToDecimal(tbPrecio.Text);
                    objeto_CE_Productos.Cantidad = Convert.ToDecimal(tbCantidad.Text);
                    objeto_CE_Productos.Activo = (bool)tbActivo.IsChecked;
                    objeto_CE_Productos.UnidadMedida = tbUnidadMedida.Text;
                    objeto_CE_Productos.Img = data;
                    objeto_CE_Productos.Descripcion = tbDescripcion.Text;
                    objeto_CE_Productos.Grupo = idGrupo;

                    objeto_CN_Productos.Insertar(objeto_CE_Productos);

                    Content = new Productos();
                }
                catch (Exception ex)
                {
                    error = new Error();
                    error.lblerror.Text = ex.Message;
                    error.ShowDialog();
                }
            }
            else
            {
                error = new Error();
                error.lblerror.Text = "Los campos no pueden quedar vacios!";
                error.ShowDialog();
            }
        }
        #endregion

        #region Read
        public void Consultar()
        {
            try
            {
                var a = objeto_CN_Productos.Consulta(IdProducto);
                tbNombres.Text = a.Nombre.ToString();
                tbCodigo.Text = a.Codigo.ToString();
                tbPrecio.Text = a.Precio.ToString();
                tbActivo.IsChecked = a.Activo;
                tbCantidad.Text = a.Cantidad.ToString();
                tbUnidadMedida.Text = a.UnidadMedida.ToString();
                ImageSourceConverter imgs = new ImageSourceConverter();
                imagen.Source = (ImageSource)imgs.ConvertFrom(a.Img);
                tbDescripcion.Text = a.Descripcion.ToString();

                var b = objeto_CN_Grupos.Nombre(a.Grupo);
                cbGrupo.Text = b.Nombre;
            }
            catch (Exception ex)
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
                objeto_CE_Productos.IdArticulo = IdProducto;
                objeto_CN_Productos.Eliminar(objeto_CE_Productos);
                Content = new Productos();
            }
            catch (Exception ex)
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
            if (CamposLlenos() == true)
            {
                try
                {
                    int idGrupo = objeto_CN_Grupos.IdGrupo(cbGrupo.Text);

                    objeto_CE_Productos.IdArticulo = IdProducto;
                    objeto_CE_Productos.Nombre = tbNombres.Text;
                    objeto_CE_Productos.Codigo = tbCodigo.Text;
                    objeto_CE_Productos.Precio = Convert.ToDecimal(tbPrecio.Text);
                    objeto_CE_Productos.Cantidad = Convert.ToDecimal(tbCantidad.Text);
                    objeto_CE_Productos.Activo = (bool)tbActivo.IsChecked;
                    objeto_CE_Productos.UnidadMedida = tbUnidadMedida.Text;
                    objeto_CE_Productos.Descripcion = tbDescripcion.Text;
                    objeto_CE_Productos.Grupo = idGrupo;

                    objeto_CN_Productos.ActualizarDatos(objeto_CE_Productos);

                    Content = new Productos();
                }
                catch (Exception ex)
                {
                    error = new Error();
                    error.lblerror.Text = ex.Message;
                    error.ShowDialog();
                }
            }
            else
            {
                error = new Error();
                error.lblerror.Text = "Los campos no pueden quedar vacios!";
                error.ShowDialog();
            }
            if(imagenSubida == true)
            {
                try
                {
                    objeto_CE_Productos.IdArticulo = IdProducto;
                    objeto_CE_Productos.Img = data;

                    objeto_CN_Productos.ActualizarIMG(objeto_CE_Productos);
                    Content = new Productos();
                }
                catch (Exception ex)
                {
                    error = new Error();
                    error.lblerror.Text = ex.Message;
                    error.ShowDialog();
                }
            }
        }
        #endregion

        #endregion

        #region Subir Imagen
        byte[] data;
        private bool imagenSubida = false;
        private void Subir(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true)
                {
                    FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                    data = new byte[fs.Length];
                    fs.Read(data, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    ImageSourceConverter imgs = new ImageSourceConverter();
                    imagen.SetValue(Image.SourceProperty, imgs.ConvertFromString(ofd.FileName.ToString()));
                }
                imagenSubida = true;
            }
            catch (Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
        #endregion
    }
}
