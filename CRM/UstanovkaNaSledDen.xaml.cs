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
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace CRM
{
    public partial class UstNaCledDen
    {
        public string FIO_zakazchika { get; set; }
        public string Adres { get; set; }
        public int NumberRequest { get; set; }
        public DateTime DateExecution { get; set; }
        public int IdMont { get; set; }
        public string NameMont { get; set; }
    }

    /// <summary>
    /// Interaction logic for UstanovkaNaSledDen.xaml
    /// </summary>
    public partial class UstanovkaNaSledDen : Window
    {
        List<UstNaCledDen> unsd;

        DateTime dtUstFrom;
        DateTime dtUstTo;

        public UstanovkaNaSledDen()
        {
            InitializeComponent();
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            unsd = new List<UstNaCledDen>();

            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    dtUstFrom = (DateTime)dtpDateUstFrom.SelectedDate;
                    dtUstTo = (DateTime)dtpDateUstTo.SelectedDate;

                    var v = contextDB.spProizvodOtdel1(dtUstFrom, dtUstTo).ToList();

                    foreach(var item in v)
                    {
                        UstNaCledDen iu = new UstNaCledDen();

                        iu.FIO_zakazchika = item.FIO_zakazchika;

                        iu.Adres = item.Adres;
                        iu.NumberRequest = item.id_request;
                        iu.DateExecution = (DateTime)item.datetime_vyezda_montazhnika;
                        iu.IdMont = item.id_mont;
                        iu.NameMont = item.full_name;
                        unsd.Add(iu);
                    }

                    dgUstanovka.ItemsSource = unsd;

                    var dataMont = from m in contextDB.Montazhniki
                                   select new { m.id_mont, m.full_name };

                    cmbMont.ItemsSource = dataMont.ToList();
                    cmbMont.DisplayMemberPath = "full_name";
                    cmbMont.SelectedValuePath = "id_mont";
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    foreach (var item in unsd)
                    {
                        int idRA = Convert.ToInt32(contextDB.Request_reception.Find(item.NumberRequest).id_request_execution);
                        Request_attachment ra = contextDB.Request_attachment.Find(idRA);
                        ra.id_mont = item.IdMont;

                    }
                    contextDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
