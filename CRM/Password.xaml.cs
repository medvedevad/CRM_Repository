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
    /// Interaction logic for Password.xaml
    /// </summary>
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    var dataDB = from s in contextDB.Sotrudniki
                                 select new { s.id_sotrudnik, s.full_name};
                    cmbSotrudnik.ItemsSource = dataDB.ToList();
                    cmbSotrudnik.DisplayMemberPath = "full_name";
                    cmbSotrudnik.SelectedValuePath = "id_sotrudnik";
                    cmbSotrudnik.SelectedValue = -1;
                    cmbSotrudnik.SelectedValue = "1";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void Vhod_Click(object sender, RoutedEventArgs e)
        {
            string pw = "";
            string rol = "";
            int idSotrudnik = Convert.ToInt32(cmbSotrudnik.SelectedValue);
            using (CRM_magEntities contextDB = new CRM_magEntities())
            {
                pw = (((from st in contextDB.Sotrudniki
                        where (st.id_sotrudnik == idSotrudnik)
                        select new { st.parol }).FirstOrDefault()).parol).ToString();
                rol = (((from st in contextDB.Sotrudniki
                         where (st.id_sotrudnik == idSotrudnik)
                         select new { st.rol }).FirstOrDefault()).rol).ToString();
            }

            //if (pw == parolBox.Password.ToString())
            //{
                if (rol == "л")
                {
                    TekushOstatkiPoSkladu lg = new TekushOstatkiPoSkladu();
                    lg.Show();
                }
                else if (rol == "д")
                {
                    MainWindow mw = new MainWindow(idSotrudnik);
                    mw.Show();
                }
                 else 
                {
                    Rukovodstvo r = new Rukovodstvo(idSotrudnik);
                    r.Show();
                }
            //}
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
