using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// <summary>
    /// Interaction logic for RashodWindow.xaml
    /// </summary>
    public partial class RashodWindow : Window
    {
        private Process _pr;

        public ObservableCollection<Rashod_2> rashod2s;

        List<MaterEdIzm> listMat;

        int idRash1;

        public RashodWindow()
        {
            InitializeComponent();
            rashod2s = new ObservableCollection<Rashod_2>();

            using (CRM_magEntities contextDB = new CRM_magEntities())
            {
                dgRashod.ItemsSource = rashod2s;

                var dataMontajnik = from p in contextDB.Montazhniki
                                    select new { p.id_mont, p.full_name };

                cmbPoluchatel.ItemsSource = dataMontajnik.ToList();
                cmbPoluchatel.DisplayMemberPath = "full_name";
                cmbPoluchatel.SelectedValuePath = "id_mont";
                cmbPoluchatel.SelectedValue = -1;

                var materEdIzm = from m in contextDB.Materialy
                                 join ei in contextDB.tEdIzm on m.id_ed_izm equals ei.id
                                 select new
                                 {
                                     IdMater = m.id_material,
                                     NameMater = m.NameMaterial + "  (" + ei.NameEI + ")",
                                     IdEdIzm = m.id_ed_izm,
                                 };

                listMat = new List<MaterEdIzm>();
                foreach (var n in materEdIzm)
                {
                    MaterEdIzm e = new MaterEdIzm();
                    e.IdMater1 = n.IdMater;
                    e.NameMater1 = n.NameMater;
                    e.IdEdIzm1 = n.IdEdIzm;
                    listMat.Add(e);
                }

                cmbMaterial.ItemsSource = listMat;

                cmbMaterial.DisplayMemberPath = "NameMater1";

                cmbMaterial.SelectedValuePath = "IdMater1";
                dtPRashod.SelectedDate = DateTime.Now;
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    Params p = contextDB.Params.Find(1);

                    p.param_int = Convert.ToInt32(txtBNomerNakladnoj.Text);
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
            //_pr = Process.Start("D:\\Fujitsu\\CRM\\2019-06-12_1\\CRM\\CRM_Access.accdb", "/X mcrRashod");
            _pr = Process.Start("D:\\Projects\\CRM\\CRM_Access.accdb", "/X mcrRashod");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {

                    Rashod1 rash1 = new Rashod1();
                    rash1.data_nakl = (DateTime)dtPRashod.SelectedDate;
                    if (cmbPoluchatel.SelectedValue != null)
                    {
                        rash1.id_mont = Convert.ToInt32(cmbPoluchatel.SelectedValue);
                    }
                    contextDB.Rashod1.Add(rash1);
                    contextDB.SaveChanges();

                    idRash1 = rash1.id;
                    txtBNomerNakladnoj.Text = idRash1.ToString();

                    foreach (Rashod_2 item in rashod2s)
                    {
                        Rashod_2 rashod2 = new Rashod_2();
                        rashod2.id = idRash1;
                        rashod2.id_material = item.id_material;

                        rashod2.kod_ed_izm = contextDB.Materialy.Find(item.id_material).id_ed_izm;


                        rashod2.kol = item.kol;
                        rashod2.cena = item.cena;
                        contextDB.Rashod_2.Add(rashod2);
                    }

                    contextDB.SaveChanges();
                    dgRashod.ItemsSource = null;
                    dgRashod.ItemsSource = rashod2s;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }


    public partial class Rashod_2 : IEditableObject, INotifyPropertyChanged
    {
        public int id_rashod_2 { get; set; }
        public int id { get; set; }
        public int id_material { get; set; }
        public int kod_ed_izm { get; set; }

        private decimal _kol;
        public decimal kol
        {
            get
            {
                return _kol;
            }
            set
            {
                if (_kol == value) return;
                _kol = value;
                OnPropertyChanged("kol");
            }
        }


        public decimal cena { get; set; }

        public virtual Materialy Materialy { get; set; }
        public virtual Rashod1 Rashod1 { get; set; }

        #region IEditableObject

        private Rashod_2 backupCopy;
        private bool inEdit;

        public void BeginEdit()
        {
            if (inEdit) return;
            inEdit = true;
            backupCopy = this.MemberwiseClone() as Rashod_2;
        }

        public void CancelEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            this.id_rashod_2 = backupCopy.id_rashod_2;
            this.id = backupCopy.id;
            this.id_material = backupCopy.id_material;
            this.kod_ed_izm = backupCopy.kod_ed_izm;
            this.kol = backupCopy.kol;
            this.cena = backupCopy.cena;
            this.Materialy = backupCopy.Materialy;
            this.Rashod1 = backupCopy.Rashod1;
        }

        public void EndEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            backupCopy = null;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

    }


    public class CourseValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
            System.Globalization.CultureInfo cultureInfo)
        {
            Rashod_2 r_2 = (value as BindingGroup).Items[0] as Rashod_2;
            decimal kolMat = 0;

            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    kolMat = Convert.ToDecimal(contextDB.spInventVed(DateTime.Now).FirstOrDefault().ostatok_new);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (r_2.kol > kolMat)
            {
                MessageBox.Show("На складе "+ kolMat.ToString() + ".", "Ошибка!");
                return new ValidationResult(false,
                    "Start Date must be earlier than End Date.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }

        }
    }

}