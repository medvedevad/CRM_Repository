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
    public partial class ClientUpdate : Window
    {
        private Process _pr;

        public delegate void delClientUpdate();
        public event delClientUpdate evClientUpdate;
        void xClientUpdate()
        {
            if (evClientUpdate != null)
            {
                evClientUpdate();
            }
        }

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
        int idReqRec;
        int idReqAtt;
        int idReqSnab;
        int idReqBuhg;
        int idAdrInst;
        int idPerenosReq;

        int idSotrudnik;
        int idWhoRegist;

        public ClientUpdate(int pIdClient)
        {
            InitializeComponent();
            idClient = pIdClient;

            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    ClientView_Result clv = contextDB.ClientView(idClient).FirstOrDefault();
                    idAdrInst = Convert.ToInt32(clv.id_adr_installation);
                    idClient = Convert.ToInt32(clv.id_client);
                    idReqSnab = Convert.ToInt32(clv.id_request_snab);
                    idReqAtt = Convert.ToInt32(clv.id_request_execution);
                    idReqBuhg = Convert.ToInt32(clv.id_request_buhg);
                    idReqRec = Convert.ToInt32(clv.id_request);

                    if (clv.famil != null)
                    {
                        txtbFamilCl.Text = clv.famil;
                    }
                    if (clv.f_name != null)
                    {
                        txtbNameCl.Text = clv.f_name;
                    }
                    if (clv.s_name != null)
                    {
                        txtbSurnameCl.Text = clv.s_name;
                    }
                    if (clv.number_pasp != null)
                    {
                        txtbPasp.Text = clv.number_pasp;
                    }
                    if (clv.tel != null)
                    {
                        txtbTel.Text = clv.tel;
                    }
                    if (clv.city_town != null)
                    {
                        txtbCityTown.Text = clv.city_town;
                    }

                    if (clv.city_town == "Москва")
                    {
                        rbtnMoscow.IsChecked = true;
                    }
                    else
                    {
                        rbtnMO.IsChecked = true;
                    }

                    if (clv.street != null)
                    {
                        txtbStreet.Text = clv.street;
                    }
                    if (clv.house != null)
                    {
                        txtbHouse.Text = clv.house.ToString();
                    }
                    if (clv.korp != null)
                    {
                        txtbKorp.Text = clv.korp;
                    }
                    if (clv.flat != null)
                    {
                        txtbFlat.Text = clv.flat.ToString();
                    }
                    if (clv.podezd != null)
                    {
                        txtbPodezd.Text = clv.podezd.ToString();
                    }
                    if (clv.etag != null)
                    {
                        txtbEtag.Text = clv.etag.ToString();
                    }
                    if (clv.domofon != null)
                    {
                        txtbDomofon.Text = clv.domofon;
                    }

                    var dataMetro = from me in contextDB.Metro
                                    select new { me.id_metro, me.name_metro };

                    cmbMetro.ItemsSource = dataMetro.ToList();
                    cmbMetro.DisplayMemberPath = "name_metro";
                    cmbMetro.SelectedValuePath = "id_metro";

                    if (clv.id_metro != null)
                    {
                        cmbMetro.SelectedValue = clv.id_metro;
                    }

                    if (clv.raion != null)
                    {
                        txtbRajon.Text = clv.raion;
                    }

                    if (clv.kol_hol != null)
                    {
                        txtbKolHol.Text = clv.kol_hol.ToString();
                    }

                    var dataMarkaHol = from mh in contextDB.H_Schetchik
                                       select new { mh.id_hol_schetch, mh.marka_hol_schetch };
                    cmbMarkaHol.ItemsSource = dataMarkaHol.ToList();
                    cmbMarkaHol.DisplayMemberPath = "marka_hol_schetch";
                    cmbMarkaHol.SelectedValuePath = "id_hol_schetch";

                    if (clv.id_hol_schetch != null)
                    {
                        cmbMarkaHol.SelectedValue = clv.id_hol_schetch;
                    }
                    if (clv.kol_gor != null)
                    {
                        txtbKolGor.Text = clv.kol_gor.ToString();
                    }

                    var dataMarkaGor = from mg in contextDB.G_Schetchik
                                       select new { mg.id_gor_schetch, mg.marka_gor_schetch };
                    cmbMarkaGor.ItemsSource = dataMarkaGor.ToList();
                    cmbMarkaGor.DisplayMemberPath = "marka_gor_schetch";
                    cmbMarkaGor.SelectedValuePath = "id_gor_schetch";

                    if (clv.id_gor_schetch != null)
                    {
                        cmbMarkaGor.SelectedValue = clv.id_gor_schetch;

                    }
                    if (clv.kol_teplo != null)
                    {
                        txtbKolTeplo.Text = clv.kol_teplo.ToString();
                    }

                    var dataMarkaTeplo = from mt in contextDB.Teplo_Schetchik
                                         select new { mt.id_teplo_schetch, mt.marka_teplo_schetch };
                    cmbMarkaTeplo.ItemsSource = dataMarkaTeplo.ToList();
                    cmbMarkaTeplo.DisplayMemberPath = "marka_teplo_schetch";
                    cmbMarkaTeplo.SelectedValuePath = "id_teplo_schetch";

                    if (clv.id_teplo_schetch != null)
                    {
                        cmbMarkaTeplo.SelectedValue = clv.id_teplo_schetch;
                    }

                    if (clv.datetime_vyezda_montazhnika != null)
                    {
                        txtbVyezd.Text = ((DateTime)clv.datetime_vyezda_montazhnika).ToString("dd.MM.yyyy");
                    }

                    if (clv.date_execution != null)
                    {
                        txtbDateExecution.Text = ((DateTime)clv.date_execution).ToString("dd.MM.yyyy");
                    }

                    var dataMont = from m in contextDB.Montazhniki
                                   select new { m.id_mont, m.full_name };
                    cmbMont.ItemsSource = dataMont.ToList();
                    cmbMont.DisplayMemberPath = "full_name";
                    cmbMont.SelectedValuePath = "id_mont";

                    if (clv.id_mont != null)
                    {
                        cmbMont.SelectedValue = clv.id_mont;
                    }

                    if (clv.dispetcher != null)
                    {
                        txtbDisp.Text = clv.dispetcher;
                    }

                    if (clv.date_request != null)
                    {
                        txtbDTRegistr.Text = ((DateTime)clv.date_request).ToString("dd.MM.yyyy");
                    }
                    if (clv.sum_contract != null)
                    {
                        txtbSum.Text = clv.sum_contract.ToString();
                    }

                    if (clv.id_request != null)
                    {
                        txtbNumberRequest.Text = clv.id_request.ToString();
                    }
                    if (clv.date_otkaza != null)
                    {
                        txtbDateOtkaz.Text = ((DateTime)clv.date_otkaza).ToString("dd.MM.yyyy");
                    }
                    if (clv.reason_otkaza != null)
                    {
                        txtbReasonOtkaza.Text = clv.reason_otkaza;
                    }

                    var dataKindOfWork = from k in contextDB.Kind_of_work
                                         select new { k.id_kind_of_work, k.kind_of_work1 };
                    cmbKindOfWork.ItemsSource = dataKindOfWork.ToList();
                    cmbKindOfWork.DisplayMemberPath = "kind_of_work1";
                    cmbKindOfWork.SelectedValuePath = "id_kind_of_work";

                    if (clv.kind_of_work != null)
                    {
                        cmbKindOfWork.SelectedValue = clv.id_kind_of_work;
                    }

                    var dataStatus = from s in contextDB.Status
                                         select new { s.id_status, s.status1 };
                    cmbStatus.ItemsSource = dataStatus.ToList();
                    cmbStatus.DisplayMemberPath = "status1";
                    cmbStatus.SelectedValuePath = "id_status";

                    if (clv.status != null)
                    {
                        cmbStatus.SelectedValue = clv.id_status;
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
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
                        Client cl = contextDB.Client.Find(idClient);

                        cl.famil = txtbFamilCl.Text;
                        cl.f_name = txtbNameCl.Text;
                        cl.s_name = txtbSurnameCl.Text;
                        cl.number_pasp = txtbPasp.Text;
                        cl.tel = txtbTel.Text;

                        contextDB.SaveChanges();
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
                        Adr_installation adr = contextDB.Adr_installation.Find(idAdrInst);


                        if (rbtnMoscow.IsChecked == true)
                        {
                            adr.city_town = "Москва";
                        }
                        else if (rbtnMO.IsChecked == true)
                        {
                            adr.city_town = txtbCityTown.Text;
                        }
                        adr.raion = txtbRajon.Text;
                        adr.street = txtbStreet.Text;
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
                        contextDB.SaveChanges();
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
                        Request_snab rs = contextDB.Request_snab.Find(idReqSnab);

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
                        Request_attachment ra = contextDB.Request_attachment.Find(idReqAtt);
                        ra.datetime_vyezda_montazhnika = Convert
                            .ToDateTime(txtbVyezd.Text);

                        if (txtbDateExecution.Text != "")
                        {
                            ra.date_execution = Convert
                                .ToDateTime(txtbDateExecution.Text);
                        }


                        if (cmbMont.SelectedValue != null)
                        {
                            ra.id_mont = Convert.ToInt32(cmbMont.SelectedValue);
                        }

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
                        Request_buhg rb = contextDB.Request_buhg.Find(idReqBuhg);
                        decimal sum;
                        bool resultSum = decimal.TryParse(txtbSum.Text, out sum);
                        if (resultSum == true)
                            rb.sum_contract = sum;

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
                        Request_reception rr = contextDB.Request_reception.Find(idReqRec);
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
                        rr.dispetcher = txtbDTRegistr.Text;
                        
                        rr.date_request = Convert.ToDateTime(txtbDTRegistr.Text);

                        rr.id_status = Convert.ToInt32(cmbStatus.SelectedValue);

                        if (txtbDateOtkaz.Text != "")
                        {
                            rr.date_otkaza =Convert.ToDateTime(txtbDateOtkaz.Text);
                        }
                        if (txtbReasonOtkaza.Text != "")
                        {
                            rr.reason_otkaza = txtbReasonOtkaza.Text;
                        }

                        contextDB.SaveChanges();
                        txtbNumberRequest.Text = rr.id_request.ToString();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                xClientUpdate();
            }
            Close();

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
            else if (!string.IsNullOrEmpty(kolGor) && cmbMarkaTeplo.SelectedValue == null)
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Выберите марку теплосчётчика(ов)\r\n";
            }

            dtVyezd = txtbVyezd.Text;
            if (string.IsNullOrEmpty(dtVyezd))
            {
                errorIsTrueFizLico = true;
                sErrorFizLico = sErrorFizLico + "Выберите желаемое время выезда\r\n";
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

        private void btnPrintReqUpdate_Click(object sender, RoutedEventArgs e)
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
