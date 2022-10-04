using Capa_Negocio;
using LiveCharts;
using PuntoDeVentas.SCS.Boxes;
using System.Data;
using System.Windows.Controls;

namespace PuntoDeVentas.Views
{
    public partial class Dashboard : UserControl
    {
        public ChartValues<decimal> Values { get; set; }
        Error error;
        public Dashboard()
        {
            InitializeComponent();

            try
            {
                CN_Dashboard dash = new CN_Dashboard();
                lbltotales.Content = dash.CantidadVentas().ToString();
                lblartdisponibles.Content = dash.Articulos().ToString();

                Values = new ChartValues<decimal>();

                foreach (DataRow row in dash.Grafico().Rows)
                {
                    decimal i = decimal.Parse(row["Monto_Total"].ToString());
                    Values.Add(i);
                }

                DataContext = this;
            }
            catch (System.Exception ex)
            {
                error = new Error();
                error.lblerror.Text = ex.Message;
                error.ShowDialog();
            }
        }
    }
}
