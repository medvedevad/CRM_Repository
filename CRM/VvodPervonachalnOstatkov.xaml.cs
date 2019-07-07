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
    /// Interaction logic for VvodPervonachalnOstatkov.xaml
    /// </summary>
    public partial class VvodPervonachalnOstatkov : Window
    {
        List<InventVed> lstVvPervOst;

        public VvodPervonachalnOstatkov()
        {
            InitializeComponent();
            using (CRM_magEntities context = new CRM_magEntities())
            {
                try
                {
                    lstVvPervOst = new List<InventVed>();

                    foreach (var item in context.Materialy)
                    {
                        InventVed mt = new InventVed();

                        mt.id_material = item.id_material;
                        mt.artikul = item.artikul;
                        mt.NameMaterial = item.NameMaterial;
                        mt.id_ed_izm = item.id_ed_izm;
                        Ed_izm ei = context.Ed_izm.Find(item.id_ed_izm);
                        mt.nameEI = ei.nameEI;
                        if (item.ostatok_cur == null)
                        {
                            mt.ostatok_cur = 0;
                        }

                        lstVvPervOst.Add(mt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
                dgInventVedomost.ItemsSource = lstVvPervOst;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (CRM_magEntities context = new CRM_magEntities())
            {
                try
                {
                    foreach (var it in lstVvPervOst)
                    {
                        Materialy mat = new Materialy();
                        mat = context.Materialy.Find(it.id_material);
                        mat.ostatok_cur = it.ostatok_cur;
                        mat.date_ostatok_cur = (DateTime)dtVvPervOst.SelectedDate;
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
            }

        }
    }
}
