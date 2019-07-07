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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace CRM
{
    public class RequestListC
    {
        public int IdItem { get; set; }
        public string Number { get; set; }
        public string FullNameExecutor { get; set; }
        public string Client { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Process _pr;

        int idSotrudnik;
        bool isView;

        private List<RequestListC> listsRLC;

        public MainWindow(int mIdSotrudnik)
        {
            InitializeComponent();
            isView = true;

            idSotrudnik = mIdSotrudnik;

            MakeFresh();
        }
        private void MakeFresh()
        {
            lstvClient.ItemsSource = null;

            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    var dataClient = from c in contextDB.RequestList
                                     select new
                                     {
                                         c.id_request,
                                         c.famil,
                                         c.f_name,
                                         c.s_name
                                     };
                    listsRLC = new List<RequestListC>();
                    foreach (var itemC in dataClient)
                    {
                        RequestListC rlc = new RequestListC();
                        rlc.IdItem = itemC.id_request;
                        rlc.Client = itemC.famil + " " + itemC.f_name + " " + itemC.s_name;
                        listsRLC.Add(rlc);
                    }
                    lstvClient.ItemsSource = listsRLC.OrderBy(n => n.Number);
                    lstvClient.SelectedValuePath = "IdItem";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void btnNewClient_Click(object sender, RoutedEventArgs e)
        {
            ClientAdd nc = new ClientAdd(idSotrudnik);
            nc.evNewClient += new ClientAdd.delNewClient(MakeFresh);
            nc.Show();
        }

        private void lstvClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idCl = 0;
            using (CRM_magEntities contextDB = new CRM_magEntities())
            {
                int idAdr = Convert.ToInt32(contextDB.Request_reception.Find(Convert.ToInt32(lstvClient.SelectedValue)).id_adr_installation);
                idCl = Convert.ToInt32(contextDB.Adr_installation.Find(idAdr).id_client);
            }

            if ((lstvClient.SelectedValue != null) && isView)
            {
                ClientView cv = new ClientView(idCl);
                cv.ShowDialog();
            }
            else if ((lstvClient.SelectedValue != null) && !isView)
            {
                ClientUpdate cu = new ClientUpdate(idCl);
                cu.ShowDialog();
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtnProsm_Checked(object sender, RoutedEventArgs e)
        {
            isView = true;
        }

        private void rbtnUpd_Checked(object sender, RoutedEventArgs e)
        {
            isView = false;
        }

        private void lstvClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lstvClient.SelectedValue = null;
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

        private void miInventarizacionnajaVedomost_Click(object sender, RoutedEventArgs e)
        {
            InventarizacionnajaVedomost iv = new InventarizacionnajaVedomost();
            iv.Show();
        }

        private void miVvodPervonachalnOstatkov_Click(object sender, RoutedEventArgs e)
        {
            VvodPervonachalnOstatkov vpo = new VvodPervonachalnOstatkov();
            vpo.Show();
        }

        private void miOborotnVedomosti_Click(object sender, RoutedEventArgs e)
        {
            OborotnVedomosti ov = new OborotnVedomosti();
            ov.Show();
        }

        private void miUstNaCledDen_Click(object sender, RoutedEventArgs e)
        {
            UstanovkaNaSledDen ustanovka = new UstanovkaNaSledDen();
            ustanovka.Show();
        }

        private void miOtkazy_Click(object sender, RoutedEventArgs e)
        {
            OtkazyKlientov ok = new OtkazyKlientov();
            ok.Show();
        }

        private void miOstatkiPoSkladu_Click(object sender, RoutedEventArgs e)
        {
            TekushOstatkiPoSkladu tops = new TekushOstatkiPoSkladu();
            tops.Show();
        }

        private void btnUstNaOprDen_Click(object sender, RoutedEventArgs e)
        {
            UstanovkaNaSledDen ustanovka = new UstanovkaNaSledDen();
            ustanovka.Show();
        }

        private void btnOtkazy_Click(object sender, RoutedEventArgs e)
        {
            OtkazyKlientov ok = new OtkazyKlientov();
            ok.Show();
        }

        private void txtBNameClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBNameClient.Text != "")
            {
                try
                {
                    using (CRM_magEntities contextDB = new CRM_magEntities())
                    {
                        var dataClientSearch = from c in contextDB.RequestList
                                               where c.famil.ToLower()
                                                 .Contains(txtBNameClient.Text
                                                 .ToLower())
                                                 select new
                                                 {
                                                     c.id_request,
                                                     c.famil,
                                                     c.f_name,
                                                     c.s_name
                                                 };
                        listsRLC = new List<RequestListC>();
                        foreach (var item in dataClientSearch)
                        {
                            RequestListC sRequest = new RequestListC();
                            sRequest.IdItem = item.id_request;
                            sRequest.Client = item.famil + " " + item.f_name + " " + item.s_name;



                            listsRLC.Add(sRequest);
                        }

                        lstvClient.ItemsSource = listsRLC.OrderBy(s => s.Client);
                        lstvClient.SelectedValuePath = "IdItem";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MakeFresh();
            }
        }

        private void FiltCancel_Click(object sender, RoutedEventArgs e)
        {
            MakeFresh();
            txtBNameClient.Text = "";
        }
    }
}
