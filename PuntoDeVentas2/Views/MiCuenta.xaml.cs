using Capa_Negocio;
using PuntoDeVentas.SCS.Boxes;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PuntoDeVentas.Views
{
    /// <summary>
    /// Lógica de interacción para MiCuenta.xaml
    /// </summary>
    public partial class MiCuenta : Window
    {
        Error error;
        public MiCuenta()
        {
            InitializeComponent();
            cargardatos();
        }

        void cargardatos()
        {
            CN_Usuarios cn = new CN_Usuarios();
            var a = cn.Cargar(Properties.Settings.Default.IdUsuario);

            try
            {
                lblNombre.Text = "Nombres: " + a.Nombres;
                lblApellidos.Text = "Apellidos: "+a.Apellidos;
                lblCorreo.Text = "Correo: " + a.Correo;
                lblPrivilegio.Text = "Privilegio: Nivel " + a.Privilegio;

                ImageSourceConverter imgs = new ImageSourceConverter();
                imagen.Source = (ImageSource)imgs.ConvertFrom(a.Img);
            }
            catch (Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }

        private void Cerrar(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
