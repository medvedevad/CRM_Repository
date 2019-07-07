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
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace CRM
{
    /// <summary>
    /// Interaction logic for NewClient.xaml
    /// </summary>
    public partial class ClientAdd : Window
    {
        private Process _pr;

        public delegate void delNewClient();
        public event delNewClient evNewClient;

        bool errorIsTrueFizLico;
        string sErrorFizLico = "";
        string familCl;
        string nameCl;
        string surnameCl;
        string nomPasp;
        string cityTown;
        string street;
        string house;
        string kolHol;
        string kolGor;
        string kolTeplo;
        string dtVyezd;
        string sum;

        int idkindOfWork;
        int idClient;
        int idHol;
        int idGor;
        int idTeplo;
        int idReqAtt;
        int idReqSnab;
        int idReqBuhg;
        int idAdrInst;
        int idPerenosReq;

        int idSotrudnik;
        int idWhoRegist;

        public ClientAdd(int ncidSotrudnik)
        {
            InitializeComponent();
            idSotrudnik = ncidSotrudnik;
            this.idWhoRegist = idSotrudnik;

            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    var dataWhoRegist = (from s in contextDB.Sotrudniki
                                         where s.id_sotrudnik == idWhoRegist
                                         select s).FirstOrDefault();
                    txtbDateReg.Text = DateTime.Now.ToString("dd/MM/yy HH:mm");
                    txtbDispR.Text = dataWhoRegist.full_name;

                    var dataKindOfWork = from k in contextDB.Kind_of_work
                                          select new { k.id_kind_of_work, k.kind_of_work1 };
                    cmbKindOfWork.ItemsSource = dataKindOfWork.ToList();
                    cmbKindOfWork.DisplayMemberPath = "kind_of_work1";
                    cmbKindOfWork.SelectedValuePath = "id_kind_of_work";
                    cmbKindOfWork.SelectedValue = -1;

                    var dataMarkaHol = from mh in contextDB.H_Schetchik
                                         select new { mh.id_hol_schetch, mh.marka_hol_schetch };
                    cmbMarkaHol.ItemsSource = dataMarkaHol.ToList();
                    cmbMarkaHol.DisplayMemberPath = "marka_hol_schetch";
                    cmbMarkaHol.SelectedValuePath = "id_hol_schetch";
                    cmbMarkaHol.SelectedValue = -1;

                    var dataMarkaGor = from mg in contextDB.G_Schetchik
                                       select new { mg.id_gor_schetch, mg.marka_gor_schetch };
                    cmbMarkaGor.ItemsSource = dataMarkaGor.ToList();
                    cmbMarkaGor.DisplayMemberPath = "marka_gor_schetch";
                    cmbMarkaGor.SelectedValuePath = "id_gor_schetch";
                    cmbMarkaGor.SelectedValue = -1;

                    var dataMarkaTeplo = from mt in contextDB.Teplo_Schetchik
                                         select new { mt.id_teplo_schetch, mt.marka_teplo_schetch };
                    cmbMarkaTeplo.ItemsSource = dataMarkaTeplo.ToList();
                    cmbMarkaTeplo.DisplayMemberPath = "marka_teplo_schetch";
                    cmbMarkaTeplo.SelectedValuePath = "id_teplo_schetch";
                    cmbMarkaTeplo.SelectedValue = -1;

                    var dataMont = from m in contextDB.Montazhniki
                                         select new { m.id_mont, m.full_name };
                    cmbMont.ItemsSource = dataMont.ToList();
                    cmbMont.DisplayMemberPath = "full_name";
                    cmbMont.SelectedValuePath = "id_mont";
                    cmbMont.SelectedValue = -1;

                    var dataMetro = from me in contextDB.Metro
                                    select new { me.id_metro, me.name_metro };

                    cmbMetro.ItemsSource = dataMetro.ToList();
                    cmbMetro.DisplayMemberPath = "name_metro";
                    cmbMetro.SelectedValuePath = "id_metro";
                    cmbMetro.SelectedValue = -1;

                    txtbNumberRequest.IsEnabled = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void rbtnMoscow_Checked(object sender, RoutedEventArgs e)
        {
            txtbCityTown.Text = "Москва";

        }

        private void rbtnMO_Checked(object sender, RoutedEventArgs e)
        {
            txtbCityTown.Text = "";
        }

        private void btnOKFizL_Click(object sender, RoutedEventArgs e)
        {
            if (IsErrorFizLico())
            {
                MessageBox.Show(sErrorFizLico, "Ошибка!");
            }
            else
            {
                try
                {
                    using (CRM_magEntities contextDB = new CRM_magEntities())
                    {
                        Client cl = new Client();

                        cl.famil = txtbFamilCl.Text;
                        cl.f_name = txtbNameCl.Text;
                        cl.s_name = txtbSurnameCl.Text;
                        cl.number_pasp = txtbPasp.Text;
                        cl.tel = txtbTel.Text;
                        contextDB.Client.Add(cl);
                        contextDB.SaveChanges();
                        idClient = cl.id_client;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }


            if (!IsErrorFizLico())
            {

                try
                {
                    using (CRM_magEntities contextDB = new CRM_magEntities())
                    {

                        Adr_installation adr = new Adr_installation();
                        if (rbtnMoscow.IsChecked == true)
                        {
                            adr.city_town = "Москва";
                        }
                        else if (rbtnMO.IsChecked == true)
                        {
                            adr.city_town = txtbCityTown.Text;
                        }
                        adr.raion = txtbRajon.Text;
                        if (txtbStreet.Text != null)
                        {
                            adr.street = txtbStreet.Text;
                        }
                        adr.house = Convert.ToInt32(txtbHouse.Text);
                        adr.korp = txtbKorp.Text;
                        adr.flat = Convert.ToInt32(txtbFlat.Text);
                        if (cmbMetro.SelectedValue != null)
                        {
                            adr.id_metro = Convert.ToInt32(cmbMetro.SelectedValue);
                        }

                        adr.podezd = Convert.ToInt32(txtbPodezd.Text);
                        adr.etag = Convert.ToInt32(txtbEtag.Text);
                        adr.domofon = txtbDomofon.Text;
                        adr.id_client = idClient;
                        contextDB.Adr_installation.Add(adr);
                        contextDB.SaveChanges();
                        idAdrInst = adr.id_adr_installation;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }


            if (!IsErrorFizLico())
            {
                try
                {
                    using (CRM_magEntities contextDB = new CRM_magEntities())
                    {
                        Request_snab rs = new Request_snab();
                        if (cmbMarkaGor.SelectedValue != null)
                        {
                            rs.id_gor_schetch = Convert.ToInt32(cmbMarkaGor.SelectedValue);
                        }
                        if (cmbMarkaHol.SelectedValue != null)
                        {
                            rs.id_hol_schetch = Convert.ToInt32(cmbMarkaHol.SelectedValue);
                        }
                        if (cmbMarkaTeplo.SelectedValue != null)
                        {
                            rs.id_teplo_schetch = Convert.ToInt32(cmbMarkaTeplo.SelectedValue);
                        }
                        int numbKolGor;
                        bool resultKolGor = int.TryParse(txtbKolGor.Text, out numbKolGor);
                        if (resultKolGor == true)
                            rs.kol_gor = numbKolGor;

                        int numbKolHol;
                        bool resultKolHol = int.TryParse(txtbKolHol.Text, out numbKolHol);
                        if (resultKolHol == true)
                            rs.kol_hol = numbKolHol;

                        int numbKolTeplo;
                        bool resultKolTeplo = int.TryParse(txtbKolTeplo.Text, out numbKolTeplo);
                        if (resultKolTeplo == true)
                            rs.kol_teplo = numbKolTeplo;

                        contextDB.Request_snab.Add(rs);
                        contextDB.SaveChanges();

                        idReqSnab = rs.id_request_snab;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }


            if (!IsErrorFizLico())
            {
                try
                {
                    using (CRM_magEntities contextDB = new CRM_magEntities())
                    {
                        Request_attachment ra = new Request_attachment();
                        ra.datetime_vyezda_montazhnika = Convert
                            .ToDateTime(txtbVyezd.Text);

                        if (cmbMont.SelectedValue != null)
                        {
                            ra.id_mont = Convert.ToInt32(cmbMont.SelectedValue);
                        }

                        contextDB.Request_attachment.Add(ra);
                        contextDB.SaveChanges();

                        idReqAtt = ra.id_request_execution;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }



            if (!IsErrorFizLico())
            {
                try
                {
                    using (CRM_magEntities contextDB = new CRM_magEntities())
                    {
                        Request_buhg rb = new Request_buhg();
                        decimal sum;
                        bool resultSum = decimal.TryParse(txtbSum.Text, out sum);
                        if (resultSum == true)
                            rb.sum_contract = sum;

                        contextDB.Request_buhg.Add(rb);
                        contextDB.SaveChanges();
                        idReqBuhg = rb.id_request_buhg;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            if (!IsErrorFizLico())
            {
                try
                {
                    using (CRM_magEntities contextDB = new CRM_magEntities())
                    {
                        Request_reception rr = new Request_reception();
                        rr.id_request_snab = idReqSnab;
                        rr.id_request_buhg = idReqBuhg;
                        rr.id_request_execution = idReqAtt;
                        rr.id_adr_installation = idAdrInst;
                        rr.id_status = 1;
                        rr.id_kind_of_work = Convert.ToInt32(cmbKindOfWork.SelectedValue);
                        if (idPerenosReq != 0)
                        {
                            rr.id_perenos_request = idPerenosReq;
                        }
                        rr.dispetcher = txtbDispR.Text;
                        rr.date_request = Convert.ToDateTime(txtbDateReg.Text);

                        contextDB.Request_reception.Add(rr);
                        contextDB.SaveChanges();
                        txtbNumberRequest.Text = rr.id_request.ToString();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (evNewClient != null)
                {
                    evNewClient();
                }
            }

        }

        private void rbtnVyezd_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    Perenos_request pr = new Perenos_request();
                    pr.vyezd_or_perezvon = true;
                    contextDB.Perenos_request.Add(pr);
                    contextDB.SaveChanges();
                    idPerenosReq = pr.id_perenos_request;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void rbtnPerezv_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    Perenos_request pr = new Perenos_request();
                    pr.vyezd_or_perezvon = false;
                    contextDB.Perenos_request.Add(pr);
                    contextDB.SaveChanges();
                    idPerenosReq = pr.id_perenos_request;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool IsErrorFizLico()
        {
            errorIsTrueFizLico = false;
            sErrorFizLico = "";

            familCl = txtbFamilCl.Text;
            if (String.IsNullOrEmpty(familCl))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Не заполнено поле 'Фамилия'\r\n";
            }
            else if (!familCl.All(c => char.IsLetter(c)))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Введите только буквы в поле 'Фамилия'\r\n";
            }

            nameCl = txtbNameCl.Text;
            if (String.IsNullOrEmpty(nameCl))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Не заполнено поле 'Имя'\r\n";
            }
            else if (!nameCl.All(c => char.IsLetter(c)))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Введите только буквы в поле 'Имя'\r\n";
            }

            surnameCl = txtbSurnameCl.Text;
            if (String.IsNullOrEmpty(surnameCl))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Не заполнено поле 'Отчество'\r\n";
            }
            else if (!surnameCl.All(c => char.IsLetter(c)))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Введите только буквы в поле 'Отчество'\r\n";
            }

            nomPasp = txtbPasp.Text;
            if (String.IsNullOrEmpty(nomPasp))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Не заполнено поле 'Паспорт'\r\n";
            }
            else if (nomPasp.Length != 10)
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "В поле 'Номер паспорта' должно быть 10 цифр\r\n";
            }
            else if (!nomPasp.All(c => char.IsNumber(c)))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Введите только цифры в поле 'Номер паспорта'\r\n";
            }

            Regex r = new Regex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$"); 

            if (!r.IsMatch(txtbTel.Text))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Телефонный номер\r\n";
            }

            cityTown = txtbCityTown.Text;
            if (String.IsNullOrEmpty(cityTown))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Заполните поле 'Город'\r\n";
            }

            street = txtbStreet.Text;
            if (String.IsNullOrEmpty(cityTown))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Заполните поле 'Улица'\r\n";
            }

            house = txtbHouse.Text;
            if (String.IsNullOrEmpty(cityTown))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Заполните поле 'Дом'\r\n";
            }

            if (cmbMetro.SelectedValue == null)
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Выберите метро\r\n";
            }

            kolHol = txtbKolHol.Text;
            kolGor = txtbKolGor.Text;
            kolTeplo = txtbKolTeplo.Text;

            if (string.IsNullOrEmpty(kolHol) && cmbMarkaHol.SelectedValue != null)
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Задайте количество счётчиков холодной воды\r\n";
            }
            else if (!string.IsNullOrEmpty(kolHol) && cmbMarkaHol.SelectedValue == null)
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Выберите марку счётчика(ов) холодной воды\r\n";
            }

            if (string.IsNullOrEmpty(kolGor) && cmbMarkaGor.SelectedValue != null)
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Задайте количество счётчиков горячей воды\r\n";
            }
            else if (!string.IsNullOrEmpty(kolGor) && cmbMarkaGor.SelectedValue == null)
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Выберите марку счётчика(ов) горячей воды\r\n";
            }

            if (string.IsNullOrEmpty(kolTeplo) && cmbMarkaTeplo.SelectedValue != null)
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Задайте количество теплосчётчиков\r\n";
            }
            else if (!string.IsNullOrEmpty(kolTeplo) && cmbMarkaTeplo.SelectedValue == null)
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Выберите марку теплосчётчика(ов)\r\n";
            }

            dtVyezd = txtbVyezd.Text;
            if (string.IsNullOrEmpty(dtVyezd))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Выберите желаемую дату установки\r\n";
            }

            sum = txtbSum.Text;
            if (string.IsNullOrEmpty(sum))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Введите сумму заказа\r\n";
            }

            if (cmbKindOfWork.SelectedValue == null)
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Выберите вид работ\r\n";
            }

            return errorIsTrueFizLico;
        }

        private void btnPrintReq_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    Params p = contextDB.Params.Find(1);
                    p.param_int = Convert.ToInt32(txtbNumberRequest.Text);
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
            //_pr = Process.Start("D:\\Fujitsu\\CRM\\2019-06-12_1\\CRM\\CRM_Access.accdb", "/X mcrPrintRequest");
            _pr = Process.Start("D:\\Projects\\CRM\\CRM_Access.accdb", "/X mcrPrintRequest");
        }

        private void btnCancelFizL_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
