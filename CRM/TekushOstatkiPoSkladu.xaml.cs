using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class TekOstPoOsnSkl
    {
        public string Artikul { get; set; }
        public string Nomenklatura { get; set; }
        public string EdIzm { get; set; }
        public decimal Kolvo { get; set; }
        public decimal SrSutRash { get; set; }
        public decimal OstVDnjah { get; set; }
    }

    /// <summary>
    /// Interaction logic for TekushOstatkiPoSkladu.xaml
    /// </summary>
    public partial class TekushOstatkiPoSkladu : Window
    {
        private Process _pr;

        List<TekOstPoOsnSkl> topos;

        DateTime dateTek = new DateTime();

        public TekushOstatkiPoSkladu()
        {
            InitializeComponent();
            DateTime dateTek = new DateTime();

            dateTek = DateTime.Now;

            txtBDateOstatki.Text = dateTek.ToString("dd/MM/yyyy");

            topos = new List<TekOstPoOsnSkl>();
            TekOstRefresh(dateTek);
        }

        private void TekOstRefresh(DateTime dt)
        {
            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    var v = contextDB.spOstatok(dt).ToList().OrderBy(od => od.OstatokDni);

                    foreach (var item in v)
                    {
                        TekOstPoOsnSkl t = new TekOstPoOsnSkl();
                        t.Artikul = item.Artikul;

                        t.Nomenklatura = item.NameMaterial;
                        t.EdIzm = item.NameEI;
                        t.Kolvo = Convert.ToDecimal(item.Ostatok);
                        t.SrSutRash = Convert.ToDecimal(item.SredSytRash);
                        t.OstVDnjah = Convert.ToDecimal(item.OstatokDni);
                        topos.Add(t);
                    }

                    dgTekOstatki.ItemsSource = topos;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void PrihodOrder_Click(object sender, RoutedEventArgs e)
        {
            PrihodWindow pw = new PrihodWindow();
            pw.Show();
        }

        private void RashodOrder_Click(object sender, RoutedEventArgs e)
        {
            RashodWindow rw = new RashodWindow();
            rw.Show();
        }

        private void miOstatkiPoSkladu_Click(object sender, RoutedEventArgs e)
        {
            TekushOstatkiPoSkladu tops = new TekushOstatkiPoSkladu();
            tops.Show();
        }

        private void miVvodPervonachalnOstatkov_Click(object sender, RoutedEventArgs e)
        {
            VvodPervonachalnOstatkov vpo = new VvodPervonachalnOstatkov();
            vpo.Show();
        }

        private void miPrintMaterialy_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            this.Opacity = 0.5;
            if (_pr != null)
            {
                try
                {
                    _pr.Kill();
                }
                catch { };
            }
            //_pr = Process.Start("D:\\Fujitsu\\CRM\\2019-06-12_1\\CRM\\CRM_Access.accdb", "/X mcrMaterialy");
            _pr = Process.Start("D:\\Projects\\CRM\\CRM_Access.accdb", "/X mcrMaterialy");
            this.Cursor = Cursors.Arrow;
            this.Opacity = 1.0;
        }

        private void miInventarizacionnajaVedomost_Click(object sender, RoutedEventArgs e)
        {
            InventarizacionnajaVedomost iv = new InventarizacionnajaVedomost();
            iv.Show();
        }

        private void miOborotnVedomosti_Click(object sender, RoutedEventArgs e)
        {
            OborotnVedomosti ov = new OborotnVedomosti();
            ov.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            dgTekOstatki.ItemsSource = null;
            topos = new List<TekOstPoOsnSkl>();
            dateTek = DateTime.Now;
            TekOstRefresh(dateTek);
        }
    }
}
