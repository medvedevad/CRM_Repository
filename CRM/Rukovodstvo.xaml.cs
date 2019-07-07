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
    /// <summary>
    /// Interaction logic for Rukovodstvo.xaml
    /// </summary>
    public partial class Rukovodstvo : Window
    {
        int idSotrudnik;

        public Rukovodstvo(int rIdSotrudnik)
        {
            InitializeComponent();
            idSotrudnik = rIdSotrudnik;

        }

        private void btnZayavki_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(idSotrudnik);
            mw.Show();
        }

        private void btnLogistika_Click(object sender, RoutedEventArgs e)
        {
            TekushOstatkiPoSkladu lg = new TekushOstatkiPoSkladu();
            lg.Show();
        }
    }
}
