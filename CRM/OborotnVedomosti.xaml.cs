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


namespace CRM
{
    /// <summary>
    /// Interaction logic for OborotnVedomosti.xaml
    /// </summary>
    public partial class OborotnVedomosti : Window
    {
        private Process _pr;

        DateTime dateFrom;
        DateTime dateTo;
        int idMont;

        public OborotnVedomosti()
        {
            InitializeComponent();
        }

        private void btnOborVedPoSkladu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    dateFrom = (DateTime)dtpDateFrom.SelectedDate;
                    dateTo = (DateTime)dtpDateTo.SelectedDate;
                    Params p = contextDB.Params.Find(1);
                    p.p_date_from = dateFrom;
                    p.p_date_to = dateTo;
                    contextDB.SaveChanges();

                    var v = contextDB.spOborotkaSklad(dateFrom, dateTo);
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
            //_pr = Process.Start("D:\\Fujitsu\\CRM\\2019-06-12_1\\CRM\\CRM_Access.accdb", "/X mcrOborotkaSklad");
            _pr = Process.Start("D:\\Projects\\CRM\\CRM_Access.accdb", "/X mcrOborotkaSklad");
        }

    }
}
