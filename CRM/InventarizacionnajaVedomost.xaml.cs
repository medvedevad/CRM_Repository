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
    public class InventVed
    {
        public int id_material { get; set; }
        public string artikul { get; set; }
        public string NameMaterial { get; set; }
        public int id_ed_izm { get; set; }
        public string nameEI { get; set; }
        public decimal ostatok_cur { get; set; }
        public DateTime date_ostatok { get; set; }
        public decimal prih { get; set; }
        public decimal rash { get; set; }
        public decimal ostatok_new { get; set; }
    }

    /// <summary>
    /// Interaction logic for InventarizacionnajaVedomost.xaml
    /// </summary>
    public partial class InventarizacionnajaVedomost : Window
    {

        private Process _pr;

        DateTime datePrevInvent;

        List<InventVed> lstIntVed;

        public InventarizacionnajaVedomost()
        {
            InitializeComponent();
            datePrevInvent = new DateTime();
            using (CRM_magEntities context = new CRM_magEntities())
            {
                try
                {
                    if (context.Materialy.FirstOrDefault().date_ostatok_cur != null)
                    {
                        txtBPrevInvent.Text = ((DateTime)context.spInventVed(dtCurInvent.SelectedDate).FirstOrDefault().date_ostatok_cur).ToString("dd/MM/yyyy");

                        datePrevInvent = (DateTime)context.spInventVed(dtCurInvent.SelectedDate).FirstOrDefault().date_ostatok_cur;
                    }
                    else
                    {
                        btnRun.IsEnabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }

            }
            Zapoln((DateTime)dtCurInvent.SelectedDate);
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            Zapoln((DateTime)dtCurInvent.SelectedDate);
        }

        private void Zapoln(DateTime dt)
        {
            using (CRM_magEntities context = new CRM_magEntities())
            {
                try
                {
                    lstIntVed = new List<InventVed>();
                    foreach (var item in context.spInventVed(dt))
                    {
                        InventVed mt = new InventVed();

                        mt.id_material = item.id_material;
                        mt.artikul = item.artikul;
                        mt.NameMaterial = item.NameMaterial;
                        mt.id_ed_izm = item.id_ed_izm;
                        Ed_izm ei = context.Ed_izm.Find(item.id_ed_izm);
                        mt.date_ostatok = (DateTime) item.date_ostatok_cur;
                        mt.nameEI = ei.nameEI;
                        if (item.prih == null)
                        {
                            mt.prih = 0;
                        }
                        else
                        {
                            mt.prih = (decimal) item.prih;
                        }

                        if (item.rash == null)
                        {
                            mt.rash = 0;
                        }
                        else
                        {
                            mt.rash = (decimal) item.rash;
                        }


                        if (item.ostatok_cur == null)
                        {
                            mt.ostatok_cur = 0;
                        }
                        else
                        {
                            mt.ostatok_cur = (decimal) item.ostatok_cur;
                        }

                        if (item.ostatok_new == null)
                        {
                            mt.ostatok_new = 0;
                        }
                        else
                        {
                            mt.ostatok_new = (decimal) item.ostatok_new;
                        }

                        lstIntVed.Add(mt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
                dgInventVedomost.ItemsSource = null;
                dgInventVedomost.ItemsSource = lstIntVed;
            }
        }


        private void IntVedomost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    Params p = contextDB.Params.Find(1);

                    p.p_date_to = (DateTime)dtCurInvent.SelectedDate;

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
            _pr = Process.Start("D:\\Projects\\CRM\\CRM_Access.accdb", "/X mcrIntVedomost");
            //_pr = Process.Start("D:\\Fujitsu\\CRM\\2019-06-12_1\\CRM\\CRM_Access.accdb", "/X mcrIntVedomost");
        }

    }
}
