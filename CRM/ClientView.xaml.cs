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
    public partial class ClientView : Window
    {
        private Process _pr;

        public delegate void delNewClient();
        public event delNewClient evNewClient;
        void xNewClient()
        {
            if (evNewClient != null)
            {
                evNewClient();
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
        int idHol;
        int idGor;
        int idTeplo;
        int idReqAtt;
        int idReqSnab;
        int idReqBuhg;
        int idAdrInst;
        int idPerenosReq;

        int idClient;
        int idWhoRegist;

        public ClientView(int pIdClient)
        {
            InitializeComponent();
            idClient = pIdClient;

            try
            {
                using (CRM_magEntities contextDB = new CRM_magEntities())
                {
                    ClientView_Result clv = contextDB.ClientView(idClient).FirstOrDefault();

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

                    if (clv.id_metro != null)
                    {
                        txtBMetro.Text = contextDB.Metro
                            .Where(c => c.id_metro == clv.id_metro)
                            .FirstOrDefault()
                            .name_metro
                            .ToString();
                    }


                    if (clv.raion != null)
                    {
                        txtbRajon.Text = clv.raion;
                    }


                    if (clv.kol_hol != null)
                    {
                        txtbKolHol.Text = clv.kol_hol.ToString();
                    }
                    if (clv.id_hol_schetch != null)
                    {
                        txtbMarkaHol.Text = contextDB.Request_snab.Where(c => c.id_hol_schetch == clv.id_hol_schetch)
                             .FirstOrDefault().H_Schetchik.marka_hol_schetch.ToString();
                    }
                    if (clv.kol_gor != null)
                    {
                        txtbKolGor.Text = clv.kol_gor.ToString();
                    }
                    if (clv.id_gor_schetch != null)
                    {
                        txtbMarkaGor.Text = contextDB.Request_snab.Where(c => c.id_gor_schetch == clv.id_gor_schetch)
                             .FirstOrDefault().G_Schetchik.marka_gor_schetch.ToString();
                    }
                    if (clv.kol_teplo != null)
                    {
                        txtbKolTeplo.Text = clv.kol_teplo.ToString();
                    }
                    if (clv.id_teplo_schetch != null)
                    {
                        txtbMarkaTeplo.Text = contextDB.Request_snab.Where(c => c.id_teplo_schetch == clv.id_teplo_schetch)
                             .FirstOrDefault().Teplo_Schetchik.marka_teplo_schetch.ToString();
                    }

                    if (clv.datetime_vyezda_montazhnika != null)
                    {
                        txtbVyezd.Text = ((DateTime)clv.datetime_vyezda_montazhnika).ToString("dd.MM.yyyy");

                    }

                    if (clv.date_execution != null)
                    {
                        txtbDateExecution.Text = ((DateTime)clv.date_execution).ToString("dd.MM.yyyy");

                    }

                    if (clv.id_mont != null)
                    {
                        txtbMont.Text = contextDB.Request_attachment
                                                .Where(c => c.id_mont == clv.id_mont)
                                                .FirstOrDefault()
                                                .Montazhniki.full_name
                                                .ToString();
                    }

                    if (clv.sum_contract != null)
                    {
                        txtbSum.Text = clv.sum_contract.ToString();
                    }

                    if (clv.id_request != null)
                    {
                        txtbNumberRequest.Text = clv.id_request.ToString();
                    }
                    if (clv.date_request != null)
                    {
                        txtbDateReg.Text = ((DateTime)clv.date_request).ToString("dd.MM.yyyy");
                    }
                    if (clv.dispetcher != null)
                    {
                        txtbDispR.Text = clv.dispetcher;
                    }
                    if (clv.kind_of_work != null)
                    {
                        txtbKindOfWork.Text = contextDB.Request_reception
                                                .Where(c => c.id_kind_of_work == clv.id_kind_of_work)
                                                .FirstOrDefault()
                                                .Kind_of_work
                                                .kind_of_work1
                                                .ToString();
                    }
                    if (clv.status != null)
                    {
                        txtbStatus.Text = clv.status;
                    }

                    if (clv.date_otkaza != null)
                    {
                        txtbDateOtkaz.Text = ((DateTime)clv.date_otkaza).ToString("dd.MM.yyyy");

                    }
                    if (clv.reason_otkaza != null)
                    {
                        txtbReasonOtkaza.Text = clv.reason_otkaza;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        private void btnOKFizL_Click(object sender, RoutedEventArgs e)
        {
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

        private void btnPtintView_Click(object sender, RoutedEventArgs e)
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
            _pr = Process.Start("D:\\Projects\\CRM\\CRM_Access.accdb", "/X mcrPrintRequest");

            //_pr = Process.Start("D:\\Fujitsu\\CRM\\2019-06-12_1\\CRM\\CRM_Access.accdb", "/X mcrPrintRequest");
        }
    }
}
