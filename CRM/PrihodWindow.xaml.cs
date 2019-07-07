using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class MaterEdIzm
    {
        public int IdMater1 { get; set; }
        public string NameMater1 { get; set; }
        public int IdEdIzm1 { get; set; }
        public string Artikul { get; set; }

    }

    //public class Maters
    /// <summary>
    /// Interaction logic for PrihodWindow.xaml
    /// </summary>
    public partial class PrihodWindow : Window
    {
        private Process _pr;

        public ObservableCollection<Prihod2> prihod2s;

        List<MaterEdIzm> listMat;

        int idPrih1;

        public PrihodWindow()
        {
            InitializeComponent();
            prihod2s = new ObservableCollection<Prihod2>();

            using (CRM_magEntities contextDB = new CRM_magEntities())
            {
                dgPrihod.ItemsSource = prihod2s;

                var dataPostavshik = from p in contextDB.Postavshiki
                                select new { p.id_postavsh, p.name_postavsh };

                cmbPostavshik.ItemsSource = dataPostavshik.ToList();
                cmbPostavshik.DisplayMemberPath = "name_postavsh";
                cmbPostavshik.SelectedValuePath = "id_postavsh";
                cmbPostavshik.SelectedValue = -1;

                var materEdIzm = from m in contextDB.Materialy
                                 join ei in contextDB.tEdIzm on m.id_ed_izm equals ei.id
                                 select new
                                 {
                                     IdMater = m.id_material,
                                     NameMater = m.NameMaterial + "  (" + ei.NameEI + ")",
                                     IdEdIzm = m.id_ed_izm,
                                     Artikul = m.artikul
                                 };


                listMat = new List<MaterEdIzm>();
                foreach (var n in materEdIzm)
                {
                    MaterEdIzm e = new MaterEdIzm();
                    e.IdMater1 = n.IdMater;
                    e.NameMater1 = n.NameMater;
                    e.IdEdIzm1 = n.IdEdIzm;
                    e.Artikul = n.Artikul;
                    listMat.Add(e);
                }

                cmbMaterial.ItemsSource = listMat;

                cmbMaterial.DisplayMemberPath = "NameMater1";

                cmbMaterial.SelectedValuePath = "IdMater1";
                dtpDatePrih.SelectedDate = DateTime.Now;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    Prihod1 prih1 = new Prihod1();
                    prih1.data_prihod = (DateTime)dtpDatePrih.SelectedDate;
                    if (cmbPostavshik.SelectedValue != null)
                    {
                        prih1.id_postavsh = Convert.ToInt32(cmbPostavshik.SelectedValue);
                    }
                    contextDB.Prihod1.Add(prih1);
                    contextDB.SaveChanges();

                    idPrih1 = prih1.id;
                    txtbNumPrihOrder.Text = idPrih1.ToString();

                    foreach (Prihod2 item in prihod2s)
                    {
                        Prihod2 prihod2 = new Prihod2();
                        prihod2.id = idPrih1;
                        prihod2.id_material = item.id_material;

                        prihod2.kod_ed_izm = contextDB.Materialy.Find(item.id_material).id_ed_izm;

                        prihod2.kol = item.kol;
                        prihod2.cena = item.cena;
                        contextDB.Prihod2.Add(prihod2);
                    }
                    contextDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    Params p = contextDB.Params.Find(1);

                    p.param_int = Convert.ToInt32(txtbNumPrihOrder.Text);
                    contextDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (_pr != null)
            {
                try
                {
                    _pr.Kill();
                }
                catch { };
            }
            //_pr = Process.Start("D:\\Fujitsu\\CRM\\2019-06-12_1\\CRM\\CRM_Access.accdb", "/X mcrPrihod");
            _pr = Process.Start("D:\\Projects\\CRM\\CRM_Access.accdb", "/X mcrPrihod");
        }

    }
}
