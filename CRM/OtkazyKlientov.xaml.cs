using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRM
{
    public partial class Otkazy
    {
        public string FIO_zakazchika { get; set; }
        public int NumberRequest { get; set; }
        public DateTime DateRequest { get; set; }
        public DateTime DateOtkaza { get; set; }
        public string ReasonOtkaza { get; set; }
    }


    /// <summary>
    /// Interaction logic for OtkazyKlientov.xaml
    /// </summary>
    public partial class OtkazyKlientov : Window
    {
        List<Otkazy> otk;

        DateTime dOtkFrom;
        DateTime dOtkTo;

        public OtkazyKlientov()
        {
            InitializeComponent();
        }

        private void btnOtkazyShow_Click(object sender, RoutedEventArgs e)
        {
            dgOtkazy.ItemsSource = null;
            otk = new List<Otkazy>();
            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    dOtkFrom = (DateTime)dpOtkazyFrom.SelectedDate;
                    dOtkTo = (DateTime)dpOtkazyTo.SelectedDate;

                    var v = contextDB.spOtkazy(dOtkFrom, dOtkTo).ToList();

                    foreach (var item in v)
                    {
                        Otkazy o = new Otkazy();

                        o.FIO_zakazchika = item.FIO_zakazchika;

                        o.NumberRequest = item.id_request;
                        o.DateRequest = (DateTime)item.date_request;
                        o.DateOtkaza = (DateTime)item.date_otkaza;
                        o.ReasonOtkaza = item.reason_otkaza;
                        otk.Add(o);
                    }

                    dgOtkazy.ItemsSource = otk;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
