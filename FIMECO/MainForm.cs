using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using FIMECO.DAOFIMECO;
using FIMECO.Etats;
using FIMECO.Models;
using FIMECO.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIMECO
{
    public partial class MainForm : Form
    {
        private bool IsAjout;

        private bool IsAjoutVersement;

        private bool IsAjoutMembre;
        //Chaine de connexion Fimeco
        private string ChaineConFimeco;

        //Liste des Id Classes metho 

        private string ListNomMultiSouscripteurs;


        //Chaine pour NOM MulticheckClasseMetho

        private string ListIdClasseMtho;

        //Chaine pour NOM MulticheckProfession

        private string ListIdProfession;

        //Chaine pour PRENOM MulticheckSouscripteurs

        private string ListPrenomMultiSouscripteurs;

        //Chaine pour Commercial DE

        private string ListNomLESouscripteurDE;

        //Chaine pour Commercial A

        private string ListNomLESouscripteurA;

        //Liste des classes métho
        private List<CClasseMetho> ListeClasseMetho = new List<CClasseMetho>();

        private readonly CAlias daoMain = new CAlias();

        private readonly DAOFimeco daoReport = new DAOFimeco();

        public MainForm()
        {
            InitializeComponent();
            
            InitStatut(RepositoryItemImageComboBoxSt);
        }

        private void gestionDesClassesMethoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var fen = new FenClasseMetho();
                fen.ShowDialog();
            }
            catch (Exception ex)
            {

            }
        }

        private void gestionDesCotisationsAnnuellesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var fen = new FenCotisationAn();
                fen.ShowDialog();
            }
            catch (Exception ex)
            {

            }
        }

        private void sBtnAjouterSouscripteur_Click(object sender, EventArgs e)
        {
            try
            {
                IsAjout = true;

                //envoyer la liste des operation réaliser pour s'assurer qu'on a pas de doublons

                List<CSouscripteur> ListeOPACTU = new List<CSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeOPACTU = daoReport.GetAllSouscripteur(ChaineConFimeco, ListeClasseMetho);
                // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();

                List<CProfession> ListeProf = new List<CProfession>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeProf = daoReport.GetAllProfession(ChaineConFimeco);


                //var fenAjout = new FenSouscripteur(LRev, ChaineConAITSOFTWARE, IsAjout, null, ListeOPACTU);
                var fenAjout = new FenSouscripteur(IsAjout, ListeOPACTU, null, ChaineConFimeco, ListeProf);

                fenAjout.ShowDialog();

                ReloadGridSouscripteur();


            }
            catch (Exception ex)
            {

            }
        }


        public void ReloadGridSouscripteur()
        {
            string AnObj = string.Empty;
            try
            {
                List<CSouscripteur> ListeOPACTUTMP = new List<CSouscripteur>();
                // if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();

                ListeOPACTUTMP = daoReport.GetAllSouscripteur(ChaineConFimeco, ListeClasseMetho);

                //Recupérer tous les arriérés

                AnObj = sNumAnnee.EditValue.ToString();

                List<CArriereSouscripteur> ListeArriereSous = new List<CArriereSouscripteur>();

                if(ListNomLESouscripteurDE==string.Empty || ListNomLESouscripteurDE==null)
                {
                    ListNomLESouscripteurDE = CmbSouscripteurDE.Text;
                }
                
                if (ListNomLESouscripteurA == string.Empty || ListNomLESouscripteurA==null)
                {
                    ListNomLESouscripteurA= CmbSouscripteurA.Text;
                }
                
                ListeArriereSous = daoReport.GetAllArriereApercu(ChaineConFimeco, AnObj, SMulSouscripteur.Checked, chkTousSouscripteur.Checked, ListNomLESouscripteurDE, ListNomLESouscripteurA, ListNomMultiSouscripteurs,ListPrenomMultiSouscripteurs,chkTousClasseMetho.Checked,ListIdClasseMtho,chkTousProfession.Checked,ListIdProfession);

                //Recuperer ceux qui ont des souscriptions Dans l'année (Cotisation annuelle)

                List<CCotisationAnnuelle> ListeCotationAn = new List<CCotisationAnnuelle>();

                ListeCotationAn = daoReport.GetAllCotisationAnnuelleApercu(ChaineConFimeco, ListeOPACTUTMP);

                List<CSouscripteur> ListeOPACTU = new List<CSouscripteur>();
                
                ListeCotationAn = ListeCotationAn.Where(c => c.mAnnee == Int32.Parse(AnObj)).ToList();

              //  var Lartmp= ListeCotationAn.Select(z => z.mIdSouscripteur).Distinct();
                var Lartmp = ListeArriereSous.Select(z => z.mIdSouscripteur).Distinct();

                foreach(var it in Lartmp)
                {
                    var CSousToLoad = ListeOPACTUTMP.Where(c => c.mId == it).ToList();

                    ListeOPACTU.AddRange(CSousToLoad);
                }


                if (ListeOPACTU.Count>0)
                {
                    foreach(var item in ListeOPACTU)
                    {
                        long MTantTotalObjectif = 0;
                        long MTantTotalVersement = 0;

                        //Année en cours
                        long MTantTotalVersementEnCours = 0;
                        long MTantTotalAnneeEnCours = 0;
                        
                        List<CArriereSouscripteur> ListeArriereTMP = new List<CArriereSouscripteur>();

                        ListeArriereTMP = ListeArriereSous.Where(c => c.mIdSouscripteur == item.mId && c.mAnnee<Int32.Parse(AnObj)).ToList();

                        //ListeVersement de l'année considérée
                        List<CArriereSouscripteur> ListeAnneeEncours = new List<CArriereSouscripteur>();

                        ListeAnneeEncours = ListeArriereSous.Where(c => c.mIdSouscripteur == item.mId && c.mAnnee == Int32.Parse(AnObj)).ToList();
                        
                        if (ListeArriereTMP!=null)
                        {
                            var ListeAnWork = ListeArriereTMP.Select(z => z.mAnnee).Distinct();

                            foreach(var elt in ListeAnWork)
                            {
                                var CAn = ListeArriereTMP.First(c => c.mAnnee == elt);

                                long MontantAnObjectif = CAn.mMontantCotisationObjectif;

                                MTantTotalObjectif += MontantAnObjectif;
                            }

                            //Montant de tous les versements

                            foreach(var obj in ListeArriereTMP)
                            {
                                MTantTotalVersement += obj.mMontantVersement;
                            }

                            //Affecter le relicat (positif cest un surplus ,negation cest un arriéré)

                            var reliquat = MTantTotalVersement - MTantTotalObjectif;

                            if(reliquat>0)
                            {
                                //Surplus
                                item.mSurplus = reliquat;
                            }
                            else
                            {
                                item.mImpayesAnPrecedentes = reliquat;
                            }

                            //Versement Année en cours
                            if(ListeAnneeEncours!=null)
                            {
                                if(ListeAnneeEncours.Count>0)
                                {
                                    foreach (var ind in ListeAnneeEncours)
                                    {
                                        MTantTotalVersementEnCours += ind.mMontantVersement;
                                        MTantTotalAnneeEnCours = ind.mMontantCotisationObjectif;
                                    }
                                }
                                else
                                {
                                    //On se base sur les montants souscrit en debut dannée
                                   if(ListeCotationAn!=null)
                                    {
                                        foreach (var ind in ListeCotationAn)
                                        {
                                            MTantTotalVersementEnCours += 0;
                                            MTantTotalAnneeEnCours = ind.mMontantCotisation;
                                        }
                                    }
                                }

                              
                            }

                            //Montant versé et reste à payer(Montant versé ne doit pas etre 
                            //influencé par surplus
                            
                            if(reliquat>0)
                            {
                                item.mMontantVerse = MTantTotalVersementEnCours ;
                            }
                            else
                            {
                                item.mMontantVerse = MTantTotalVersementEnCours + reliquat;
                            }
                           

                            //reste à payer
                            item.mArriere = MTantTotalAnneeEnCours - item.mMontantVerse;

                            if (item.mArriere<=0)
                            {
                                //On a plutot un surplus donc arriere =0
                                var srpl = -item.mArriere;
                                item.mSurplus = srpl;

                                item.mArriere = 0;

                                item.mStatutCotisation = 1;

                            }
                            else
                            {
                                //Il n'y a plus de surplus alors 
                                item.mSurplus = 0;

                                item.mStatutCotisation = 0;
                            }

                        }

                    }
                    
                }

                
                gridControlSouscripteur.DataSource = ListeOPACTU;
                //  if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show("Une erreur est survenue ! Veuillez contacter votre Administrateur!", "AITSERIAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "MainForm -> ReloadOperation -> TypeErreur: " + ex.Message; ;
                CAlias.Log(msg);
            }

        }
        

        private void InitStatut(RepositoryItemImageComboBox edit)
        {
            try
            {
                var icollection = new ImageCollection();

                icollection.AddImage(Properties.Resources.RougeTrN);
                icollection.AddImage(Properties.Resources.VertTrN);

                edit.Items.Add(new ImageComboBoxItem("", 0, 0));

                edit.Items.Add(new ImageComboBoxItem("  ", 1, 1));

                //edit.Items.Add(New ImageComboBoxItem("   ", 2, 2))

                edit.SmallImages = icollection;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "MainForm -> InitStatut -> TypeErreur: " + ex.Message; ;
                CAlias.Log(msg);
            }
        }

        

        public void ReloadGridMembreSouscripteur()
        {
            try
            {
                List<CMembreSouscripteur> ListeOPACTU = new List<CMembreSouscripteur>();
                // if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();

                ListeOPACTU = daoReport.GetAllMembreSouscripteur(ChaineConFimeco);

                var IdSous = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());

                List<CMembreSouscripteur> ListeToReturn = new List<CMembreSouscripteur>();

                ListeToReturn = ListeOPACTU.Where(c => c.mIdSouscripteur == IdSous).ToList();

                gridControlMembre.DataSource = ListeToReturn;
                //  if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show("Une erreur est survenue ! Veuillez contacter votre Administrateur!", "AITSERIAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "MainForm -> ReloadGridMembreSouscripteur -> TypeErreur: " + ex.Message; ;
                CAlias.Log(msg);
            }

        }


        public void ReloadGridVersement()
        {
            try
            {
                if(gridView1.RowCount>0)
                {
                    //Liste des souscripteurs

                    List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                    //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                    ListeSS = daoReport.GetAllSouscripteur(ChaineConFimeco);

                    List<CVersement> ListeOPACTU = new List<CVersement>();
                    // if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();

                    ListeOPACTU = daoReport.GetAllVersement(ChaineConFimeco,ListeSS);

                    var IdSous = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());

                    List<CVersement> ListeToReturn = new List<CVersement>();

                    ListeToReturn = ListeOPACTU.Where(c => c.mIdSouscripteur == IdSous && c.mDateVersement.Year==Int32.Parse(sNumAnnee.EditValue.ToString())).ToList();

                    gridControlVersement.DataSource = ListeToReturn;
                    //  if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                }


            }
            catch (Exception ex)
            {
                // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show("Une erreur est survenue ! Veuillez contacter votre Administrateur!", "AITSERIAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "MainForm -> ReloadGridVersement -> TypeErreur: " + ex.Message; ;
                CAlias.Log(msg);
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //Chaine Connexion AITSOFTWARE
                List<CAlias> ListChaineAIT = new List<CAlias>();

                ListChaineAIT = daoMain.GetAliasFIMECO();

                var ChooseAIT = ListChaineAIT.FirstOrDefault(c => c.IsAbidjan == true);

                if (ChooseAIT != null)
                {
                    ChaineConFimeco = ChooseAIT.mAliasName;
                }

                //charger classe metho

                ListeClasseMetho = daoReport.GetAllClasseMetho(ChaineConFimeco);

                //date 

                sNumAnnee.EditValue = DateTime.Now.Date.Year;

                //============Charger Combo des souscripteurs=======================

                List<CSouscripteur> ListeSouscripteur = new List<CSouscripteur>();

                ListeSouscripteur = daoReport.GetAllSouscripteur(ChaineConFimeco, ListeClasseMetho);
                
                CmbSouscripteurDE.Properties.DataSource = ListeSouscripteur;
                CmbSouscripteurA.Properties.DataSource = ListeSouscripteur;

                chkCmbMultSouscripteur.Properties.DataSource = ListeSouscripteur;
                FillMultiCheckComboSouscripteur(chkCmbMultSouscripteur, ListeSouscripteur);

                CmbSouscripteurDE.Properties.DisplayMember = "mNom";
                CmbSouscripteurA.Properties.DisplayMember = "mNom";

                //Choisir les premières valeurs
                CmbSouscripteurDE.ItemIndex = 0;
                CmbSouscripteurA.ItemIndex = 0;

                //==========Charger combo Classes métho===========================
                List<CClasseMetho> ListeCMetho = new List<CClasseMetho>();

                ListeCMetho = daoReport.GetAllClasseMetho(ChaineConFimeco);
                
                chkCmbMultSouscripteur.Properties.DataSource = ListeCMetho;
                FillMultiCheckComboClasseMetho(chkCmbMultClasseMetho, ListeCMetho);

                //==========Charger combo Profession===========================
                List<CProfession> ListeCProfession = new List<CProfession>();

                ListeCProfession = daoReport.GetAllProfession(ChaineConFimeco);

                chkCmbMultiProfession.Properties.DataSource = ListeCProfession;
                FillMultiCheckComboProfession(chkCmbMultiProfession, ListeCProfession);

                //+++++++++Mettre à jour les Statut Adulte au cas où on a de nouveaux Adultes/enfants++++++++

                if(ListeSouscripteur.Count>0)
                {
                    var ListeIdSouscripteurAdulte = string.Empty;
                    var ListeIdSouscripteurEnfant = string.Empty;

                    foreach(var it in ListeSouscripteur)
                    {
                        var age = GetAge(it.mDateNaissance, DateTime.Now.Date);

                        if ((it.mIsAdulte=="Adulte" || it.mIsAdulte==string.Empty) && age<18)
                        {
                            //Mettre a enfant
                            ListeIdSouscripteurEnfant += it.mId + ",";
                        }

                        if ((it.mIsAdulte == "Enfant" || it.mIsAdulte == string.Empty) && age > 18)
                        {
                            //Mezttre a Adulte
                            ListeIdSouscripteurAdulte += it.mId + ",";
                        }
                    }

                    if (ListeIdSouscripteurEnfant != string.Empty)
                    {
                        ListeIdSouscripteurEnfant = ListeIdSouscripteurEnfant.Substring(0, ListeIdSouscripteurEnfant.Length - 1);

                        string Requete = "UPDATE FEC_Souscripteur SET IsAdulte='Enfant' WHERE Id in (" + ListeIdSouscripteurEnfant + ") ";

                        bool ret = daoReport.ExecuteRequete(Requete, ChaineConFimeco);

                        if(!ret)
                        {
                            MessageBox.Show("Une erreur est survenue Lors de la MAJ des souscripteurs! Veuillez contacter votre Administrateur!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }


                    if (ListeIdSouscripteurAdulte != string.Empty)
                    {
                        ListeIdSouscripteurAdulte = ListeIdSouscripteurAdulte.Substring(0, ListeIdSouscripteurAdulte.Length - 1);
                        string Requete = "UPDATE FEC_Souscripteur SET IsAdulte='Adulte' WHERE Id in (" + ListeIdSouscripteurAdulte + ") ";

                        bool ret = daoReport.ExecuteRequete(Requete, ChaineConFimeco);

                        if (!ret)
                        {
                            MessageBox.Show("Une erreur est survenue Lors de la MAJ des souscripteurs! Veuillez contacter votre Administrateur!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    

                }

                //Mettre à jour les Membres

                var ListeMembreS = daoReport.GetAllMembreSouscripteur(ChaineConFimeco);

                if(ListeMembreS.Count>0)
                {
                    var ListeIdMembreSouscripteurAdulte = string.Empty;
                    var ListeIdMembreSouscripteurEnfant = string.Empty;

                    foreach (var it in ListeMembreS)
                    {
                        var age = GetAge(it.mDateNaissance, DateTime.Now.Date);

                        if ((it.mIsAdulteMembre == "Adulte" || it.mIsAdulteMembre == string.Empty) && age < 18)
                        {
                            //Mettre a enfant
                            ListeIdMembreSouscripteurEnfant += it.mId + ",";
                        }

                        if ((it.mIsAdulteMembre == "Enfant" || it.mIsAdulteMembre == string.Empty) && age > 18)
                        {
                            //Mezttre a Adulte
                            ListeIdMembreSouscripteurAdulte += it.mId + ",";
                        }
                    }

                    if (ListeIdMembreSouscripteurEnfant != string.Empty)
                    {
                        ListeIdMembreSouscripteurEnfant = ListeIdMembreSouscripteurEnfant.Substring(0, ListeIdMembreSouscripteurEnfant.Length - 1);

                        string Requete = "UPDATE FEC_MembreSouscripteur SET IsAdulte='Enfant' WHERE Id in (" + ListeIdMembreSouscripteurEnfant + ") ";

                        bool ret = daoReport.ExecuteRequete(Requete, ChaineConFimeco);

                        if (!ret)
                        {
                            MessageBox.Show("Une erreur est survenue Lors de la MAJ des Membres souscripteurs! Veuillez contacter votre Administrateur!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }


                    if (ListeIdMembreSouscripteurAdulte != string.Empty)
                    {
                        ListeIdMembreSouscripteurAdulte = ListeIdMembreSouscripteurAdulte.Substring(0, ListeIdMembreSouscripteurAdulte.Length - 1);
                        string Requete = "UPDATE FEC_MembreSouscripteur SET IsAdulte='Adulte' WHERE Id in (" + ListeIdMembreSouscripteurAdulte + ") ";

                        bool ret = daoReport.ExecuteRequete(Requete, ChaineConFimeco);

                        if (!ret)
                        {
                            MessageBox.Show("Une erreur est survenue Lors de la MAJ des Membres souscripteurs! Veuillez contacter votre Administrateur!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }


        private int GetAge(DateTime datenaissance, DateTime Mtn)
        {
            int ret = 0;
            try
            {
                if (Mtn > datenaissance)
                {
                    var nbrejrs = Mtn.Date - datenaissance.Date;

                     ret = nbrejrs.Days / 365;
                    
                    return ret;
                }
                else
                {
                    return ret;
                }

            }
            catch (Exception ex)
            {
                return ret;
            }
        }


        public void FillMultiCheckComboSouscripteur(CheckedComboBoxEdit cmb, List<CSouscripteur> Lste)
        {
            try
            {
                if (Lste != null && Lste.Count > 0)
                {
                    var MySelectBases = new DataTable();

                    MySelectBases.Columns.Add("mNom");

                    MySelectBases.Columns.Add("mPrenoms");

                    MySelectBases.Columns.Add("mNomPrenoms");
                    

                    foreach (var item in Lste)
                    {
                        if (item.mNom != string.Empty && item.mPrenoms != string.Empty)
                        {
                            MySelectBases.Rows.Add(item.mNom, item.mPrenoms, item.mNom + " " + item.mPrenoms);

                        }

                        if (item.mNom == string.Empty && item.mPrenoms != string.Empty)
                        {
                            MySelectBases.Rows.Add(item.mNom, item.mPrenoms, item.mPrenoms);

                        }

                        if (item.mNom != string.Empty && item.mPrenoms == string.Empty)
                        {
                            MySelectBases.Rows.Add(item.mNom, item.mNom, item.mNom);

                        }

                        cmb.Properties.DataSource = MySelectBases;
                        
                        cmb.Properties.ValueMember = "mPrenoms";

                        cmb.Properties.DisplayMember = "mNomPrenoms";

                        // cmb.EditValue = item.mPrenomCommercial;

                    }
                    
                }
            }
            catch (Exception ex)
            {
              //  if (splashScreenManager2.IsSplashFormVisible) splashScreenManager2.CloseWaitForm();
                MessageBox.Show("Une erreur est survenue ! Veuillez contacter votre Administrateur!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                var msg = "MainForm ->FillMultiCheckComboSouscripteur -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }

        }



        public void FillMultiCheckComboClasseMetho(CheckedComboBoxEdit cmb, List<CClasseMetho> Lste)
        {
            try
            {
                if (Lste != null && Lste.Count > 0)
                {
                    var MySelectBases = new DataTable();

                    MySelectBases.Columns.Add("mId");

                    MySelectBases.Columns.Add("mNomClasse");
                    


                    foreach (var item in Lste)
                    {
                        if (item.mNomClasse != string.Empty && item.mId >0)
                        {
                            MySelectBases.Rows.Add(item.mId, item.mNomClasse);

                        }
                        

                        cmb.Properties.DataSource = MySelectBases;

                        cmb.Properties.ValueMember = "mId";

                        cmb.Properties.DisplayMember = "mNomClasse";

                        // cmb.EditValue = item.mPrenomCommercial;

                    }

                }
            }
            catch (Exception ex)
            {
                //  if (splashScreenManager2.IsSplashFormVisible) splashScreenManager2.CloseWaitForm();
                MessageBox.Show("Une erreur est survenue ! Veuillez contacter votre Administrateur!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                var msg = "MainForm ->FillMultiCheckComboSouscripteur -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }

        }


        public void FillMultiCheckComboProfession(CheckedComboBoxEdit cmb, List<CProfession> Lste)
        {
            try
            {
                if (Lste != null && Lste.Count > 0)
                {
                    var MySelectBases = new DataTable();

                    MySelectBases.Columns.Add("mId");

                    MySelectBases.Columns.Add("mLibelle");



                    foreach (var item in Lste)
                    {
                        if (item.mLibelle != string.Empty && item.mId > 0)
                        {
                            MySelectBases.Rows.Add(item.mId, item.mLibelle);

                        }


                        cmb.Properties.DataSource = MySelectBases;

                        cmb.Properties.ValueMember = "mId";

                        cmb.Properties.DisplayMember = "mLibelle";

                        // cmb.EditValue = item.mPrenomCommercial;

                    }

                }
            }
            catch (Exception ex)
            {
                //  if (splashScreenManager2.IsSplashFormVisible) splashScreenManager2.CloseWaitForm();
                MessageBox.Show("Une erreur est survenue ! Veuillez contacter votre Administrateur!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                var msg = "MainForm ->FillMultiCheckComboSouscripteur -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }

        }



        private void sBtnModifierSouscripteur_Click(object sender, EventArgs e)
        {
            try
            {

                IsAjout = false;

                CSouscripteur ClientOp = new CSouscripteur();

                var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());

                if (Identif > 0)
                {

                    //if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                    List<CSouscripteur> ListeOPACTU = new List<CSouscripteur>();

                    ListeOPACTU = daoReport.GetAllSouscripteur(ChaineConFimeco, ListeClasseMetho);

                    ClientOp = ListeOPACTU.FirstOrDefault(c => c.mId == Identif);
                    //   if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();

                    List<CProfession> ListeProf = new List<CProfession>();

                    //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                    ListeProf = daoReport.GetAllProfession(ChaineConFimeco);

                    var fenAjout = new FenSouscripteur(IsAjout, ListeOPACTU, ClientOp, ChaineConFimeco, ListeProf);

                    fenAjout.ShowDialog();

                    ReloadGridSouscripteur();

                }
                else
                {
                    //   if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Veuillez sélectionner un élément à modifier!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void sBtnVisualiser_Click(object sender, EventArgs e)
        {
            try
            {
                ReloadGridSouscripteur();
            }
            catch (Exception ex)
            {

            }
        }

        private void sBtnSupprimerSouscripteur_Click(object sender, EventArgs e)
        {
            bool res = false;
            string requete = string.Empty;
            bool resMembreSous = false;
            bool resVersement = false;
            bool resCot = false;
            try
            {
                if (gridView1.RowCount > 0)
                {

                    var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());

                    var nomSous = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mNom").ToString();
                    var prenomSous = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mPrenoms").ToString();


                    if (Identif > 0)
                    {
                        var rep = MessageBox.Show("Voulez-vous supprimer le souscripteur " + nomSous + " " + prenomSous + " selectionné ?" + Environment.NewLine + " Attention,cela entraînera la suppression de tous les membres associés et des versements!", "FIMECO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (rep == DialogResult.Yes)
                        {
                            //Supprimer les  MEMBRES associés à l'operation enregistrée

                            //suppriser les cotisation annuelle liées

                            List<CMembreSouscripteur> Ltemp = new List<CMembreSouscripteur>();
                            List<CMembreSouscripteur> LMembre = new List<CMembreSouscripteur>();


                            LMembre = daoReport.GetAllMembreSouscripteur(ChaineConFimeco);

                            Ltemp = Ltemp.Where(c => c.mIdSouscripteur == Identif).ToList();

                            if (1 > 0)
                            {
                                foreach (var item in Ltemp)
                                {
                                    if (item.mId > 0)
                                    {
                                        requete += " DELETE FROM FEC_MembreSouscripteur WHERE Id='" + item.mId + "' " + Environment.NewLine;
                                    }

                                }

                                if(requete==string.Empty)
                                {
                                    resMembreSous = true;
                                }
                                else
                                {
                                    resMembreSous = daoReport.DeleteMembreSouscripteur(requete, ChaineConFimeco);
                                }
                                

                                if (resMembreSous)
                                {
                                    //Supprimer les versements associés

                                    List<CVersement> LtempVers = new List<CVersement>();
                                    List<CVersement> LMembreVers = new List<CVersement>();

                                    LMembreVers = daoReport.GetAllVersement(ChaineConFimeco);

                                    LtempVers = LMembreVers.Where(c => c.mIdSouscripteur == Identif).ToList();

                                    if (1 > 0)
                                    {
                                        foreach (var item in Ltemp)
                                        {
                                            if (item.mId > 0)
                                            {
                                                requete += " DELETE FROM FEC_Versement WHERE Id='" + item.mId + "' " + Environment.NewLine;
                                            }

                                        }

                                        if(requete==string.Empty)
                                        {
                                            resVersement = true;
                                        }
                                        else
                                        {
                                            resVersement = daoReport.DeleteVersementChaine(requete, ChaineConFimeco);

                                        }


                                        if (resVersement)
                                        {
                                            //Supprimer souscription liées
                                            
                                            List<CCotisationAnnuelle> LCotSouscripteur = new List<CCotisationAnnuelle>();

                                            List<CCotisationAnnuelle> LtempCot = new List<CCotisationAnnuelle>();

                                            List<CSouscripteur> LSSouscrip = new List<CSouscripteur>();

                                            LSSouscrip = daoReport.GetAllSouscripteur(ChaineConFimeco);

                                            LCotSouscripteur = daoReport.GetAllCotisationAnnuelle(ChaineConFimeco, LSSouscrip);

                                            LtempCot = LCotSouscripteur.Where(c => c.mIdSouscripteur == Identif).ToList();

                                            if (1 > 0)
                                            {
                                                foreach (var item in LtempCot)
                                                {
                                                    if (item.mId > 0)
                                                    {
                                                        requete += " DELETE FROM FEC_CotisationAnnuelle WHERE Id='" + item.mId + "' " + Environment.NewLine;
                                                    }

                                                }

                                                if(requete==string.Empty)
                                                {
                                                    resCot = true;
                                                }
                                                else
                                                {
                                                    resCot = daoReport.DeleteCotisationAnnuelle(requete, ChaineConFimeco);

                                                }
                                                
                                                if (resCot)
                                                {
                                                    //Supprimer souscripteur
                                                    res = daoReport.DeleteSouscripteur(Identif, ChaineConFimeco);

                                                    if (res)
                                                    {
                                                        //  if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                                        MessageBox.Show("Souscripteur supprimé avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                        ReloadGridSouscripteur();
                                                        gridControlMembre.DataSource = null;
                                                        gridControlVersement.DataSource = null;
                                                    }
                                                    else
                                                    {
                                                        // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                                        MessageBox.Show("Une erreur est survenue lors de la suppression du souscripteur!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Une erreur est survenue lors de la suppression des cotisations annuelles!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                }

                                            }

                                        }
                                        else
                                        {
                                            //Erreur de suppression numserie
                                            //   if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                            MessageBox.Show("Une erreur est survenue lors de la suppression des versements du souscripteur!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        }

                                    }
                                
                                }
                                else
                                {
                                    //Il n'y a pas de membre a supprimer ,juste le souscripteur
                                    res = daoReport.DeleteSouscripteur(Identif, ChaineConFimeco);

                                    if (res)
                                    {
                                        //   if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                        MessageBox.Show("Souscripteur supprimé avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        ReloadGridSouscripteur();
                                        gridControlMembre.DataSource = null;
                                        gridControlVersement.DataSource = null;
                                    }
                                    else
                                    {
                                        // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                        MessageBox.Show("Une erreur est survenue lors de la suppression de l'opération!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    }
                                }

                            }

                        }
                        else
                        {
                            //  if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("Veuillez sélectionner un élément à supprimer!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show("Une erreur est survenue ! Veuillez contacter votre Administrateur!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "MainForm -> sBtnSupprimerOperation_Click -> TypeErreur: " + ex.Message; ;
                CAlias.Log(msg);
            }


            //if (Identif > 0)
            //{
            //    var rep = MessageBox.Show("Voulez-vous supprimer le souscripteur " + nomSous +" "+prenomSous+ " selectionné ?", "FIMECO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (rep == DialogResult.Yes)
            //    {
            //        res = daoReport.DeleteSouscripteur(Identif, ChaineConFimeco);

            //        if (res)
            //        {
            //            //if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
            //            MessageBox.Show("Souscripteur supprimé avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //            ReloadGridSouscripteur();

            //        }
            //        else
            //        {
            //            // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
            //            MessageBox.Show("Une erreur est survenue lors de la suppression de l'opération!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //        }

            //    }
            //}
        
        }

        private void sBtnAjouterMembre_Click(object sender, EventArgs e)
        {
            try
            {
                var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());
                
                IsAjoutMembre = true;

                //envoyer la liste des Membres réaliser pour s'assurer qu'on a pas de doublons

                List<CMembreSouscripteur> ListeOPACTU = new List<CMembreSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeOPACTU = daoReport.GetAllMembreSouscripteur(ChaineConFimeco);
                // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();

                //Liste des souscripteurs

                List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeSS = daoReport.GetAllSouscripteur(ChaineConFimeco);

                List<CProfession> ListeProfession = new List<CProfession>();

                ListeProfession= daoReport.GetAllProfession(ChaineConFimeco);

                //var fenAjout = new FenSouscripteur(LRev, ChaineConAITSOFTWARE, IsAjout, null, ListeOPACTU);
                var fenAjout = new FenGestMembreSouscripteur(IsAjoutMembre, ListeOPACTU, null, ChaineConFimeco, Identif, ListeSS, ListeProfession);

                fenAjout.ShowDialog();

                ReloadGridMembreSouscripteur();
            }
            catch(Exception ex)
            {

            }
        }

        private void sBtnAjouterVersement_Click(object sender, EventArgs e)
        {
            try
            {
                var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());
                
                IsAjoutVersement = true;

                //Liste des souscripteurs

                List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeSS = daoReport.GetAllSouscripteur(ChaineConFimeco);

                //envoyer la liste des versements réaliser pour s'assurer qu'on a pas de doublons(au niveau des reçu)

                List<CVersement> ListeVersement = new List<CVersement>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeVersement = daoReport.GetAllVersement(ChaineConFimeco,ListeSS);

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();

                // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();


                //Recuperer la liste des souscription annuelles

                List<CCotisationAnnuelle> ListeCotisation = new List<CCotisationAnnuelle>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeCotisation = daoReport.GetAllCotisationAnnuelle(ChaineConFimeco, ListeSS);
                
                //var fenAjout = new FenSouscripteur(LRev, ChaineConAITSOFTWARE, IsAjout, null, ListeOPACTU);
                var fenAjout = new FenGestVersement(IsAjoutVersement, ListeVersement, null, ChaineConFimeco, Identif, ListeSS,false, ListeCotisation);
               
                fenAjout.ShowDialog();

                ReloadGridVersement();
            }
            catch(Exception ex)
            {

            }
        }

        private void sBtnModifierMembre_Click(object sender, EventArgs e)
        {
            try
            {
                var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());

                var IdModif = Int32.Parse(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "mId").ToString());


                IsAjoutMembre = false;

                //envoyer la liste des Membres réaliser pour s'assurer qu'on a pas de doublons

                List<CMembreSouscripteur> ListeOPACTU = new List<CMembreSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeOPACTU = daoReport.GetAllMembreSouscripteur(ChaineConFimeco);
                // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();

                var CToModif = ListeOPACTU.FirstOrDefault(c => c.mId == IdModif);

                //Liste des souscripteurs

                List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeSS = daoReport.GetAllSouscripteur(ChaineConFimeco);


                List<CProfession> ListeProfession = new List<CProfession>();

                ListeProfession = daoReport.GetAllProfession(ChaineConFimeco);

                //var fenAjout = new FenSouscripteur(LRev, ChaineConAITSOFTWARE, IsAjout, null, ListeOPACTU);
                var fenAjout = new FenGestMembreSouscripteur(IsAjoutMembre, ListeOPACTU, CToModif, ChaineConFimeco, Identif, ListeSS, ListeProfession);

                fenAjout.ShowDialog();

                ReloadGridMembreSouscripteur();
            }
            catch(Exception ex)
            {

            }
        }

        private void gridControlSouscripteur_Click(object sender, EventArgs e)
        {
            List<CMembreSouscripteur> ListNum = new List<CMembreSouscripteur>();
            List<CVersement> ListVers = new List<CVersement>();
            try
            {
                if(gridView1.RowCount>0)
                {

                    var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());

                    if (Identif > 0)
                    {
                        var ListeMembre = daoReport.GetAllMembreSouscripteur(ChaineConFimeco);

                        ListNum = ListeMembre.Where(c => c.mIdSouscripteur == Identif).ToList();

                        List<CMembreSouscripteur> ListEmpty = new List<CMembreSouscripteur>();

                        gridControlMembre.DataSource = ListEmpty;
                        gridControlMembre.DataSource = ListNum;

                        //Liste des souscripteurs

                        List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                        //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                        ListeSS = daoReport.GetAllSouscripteur(ChaineConFimeco);

                        //Versement(Tenir compte aussi de l'année objectif)

                        var ListeV = daoReport.GetAllVersement(ChaineConFimeco, ListeSS);

                        ListVers = ListeV.Where(c => c.mIdSouscripteur == Identif && c.mDateVersement.Year==Int32.Parse(sNumAnnee.EditValue.ToString())).ToList();

                        List<CVersement> Lvide = new List<CVersement>();

                        gridControlVersement.DataSource = Lvide;
                        gridControlVersement.DataSource = ListVers;
                    }
                }

            }
            catch (Exception ex)
            {
               // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show("Une erreur est survenue ! Veuillez contacter votre Administrateur!", "AITSERIAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "MainForm -> gridControlClientSerial_Click -> TypeErreur: " + ex.Message; ;
                CAlias.Log(msg);
            }
        }

        private void sBtnModifierVersement_Click(object sender, EventArgs e)
        {
            try
            {
                var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());

                var IdModif = Int32.Parse(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "mId").ToString());


                IsAjoutVersement = false;

                //Liste des souscripteurs

                List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeSS = daoReport.GetAllSouscripteur(ChaineConFimeco);


                //envoyer la liste des Membres réaliser pour s'assurer qu'on a pas de doublons

                List<CVersement> ListeOPACTU = new List<CVersement>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeOPACTU = daoReport.GetAllVersement(ChaineConFimeco, ListeSS);
                // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();

                var CToModif = ListeOPACTU.FirstOrDefault(c => c.mId == IdModif);

                //Recuperer la liste des souscription annuelles

                List<CCotisationAnnuelle> ListeCotisation = new List<CCotisationAnnuelle>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeCotisation = daoReport.GetAllCotisationAnnuelle(ChaineConFimeco, ListeSS);



                //var fenAjout = new FenSouscripteur(LRev, ChaineConAITSOFTWARE, IsAjout, null, ListeOPACTU);
                var fenAjout = new FenGestVersement(IsAjoutVersement, ListeOPACTU, CToModif, ChaineConFimeco, Identif, ListeSS,false, ListeCotisation);

                fenAjout.ShowDialog();

                ReloadGridVersement();
            }
            catch(Exception ex)
            {

            }
        }

        private void sBtnDeleteVersement_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                if(gridView2.RowCount>0)
                {

                    var Identif = Int32.Parse(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "mId").ToString());

                    if (Identif > 0)
                    {
                        var rep = MessageBox.Show("Voulez-vous supprimer le versement selectionné ?", "FIMECO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (rep == DialogResult.Yes)
                        {
                            res = daoReport.DeleteVersement(Identif, ChaineConFimeco);

                            if (res)
                            {
                                //  if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                MessageBox.Show("Versement supprimé avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                ReloadGridVersement();
                            }
                            else
                            {
                                //  if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                MessageBox.Show("Une erreur est survenue lors de la suppression du Numéro de serie!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
               // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show("Une erreur est survenue ! Veuillez contacter votre Administrateur!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "MainForm -> sBtnDelete_Click -> TypeErreur: " + ex.Message; ;
                CAlias.Log(msg);
            }
        }

        private void sBtnDeleteMembre_Click(object sender, EventArgs e)
        {
                bool res = false;
                try
                {
                    if(gridView3.RowCount>0)
                    {

                    var Identif = Int32.Parse(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "mId").ToString());
                    var nom = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "mNomMembre").ToString();
                    var prenoms = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "mPrenomsMembre").ToString();
                    
                    if (Identif > 0)
                    {
                        var rep = MessageBox.Show("Voulez-vous supprimer le membre " + nom + " " + prenoms + " selectionné ?", "FIMECO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (rep == DialogResult.Yes)
                        {
                            res = daoReport.DeleteMembreSouscripteur(Identif, ChaineConFimeco);

                            if (res)
                            {
                                //  if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                MessageBox.Show("Membre supprimé avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                ReloadGridMembreSouscripteur();
                            }
                            else
                            {
                                //  if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                MessageBox.Show("Une erreur est survenue lors de la suppression du Membre!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }

                        }

                    }

                }

                }
                catch (Exception ex)
                {
                    // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Une erreur est survenue ! Veuillez contacter votre Administrateur!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    var msg = "MainForm -> sBtnDelete_Click -> TypeErreur: " + ex.Message; ;
                    CAlias.Log(msg);
                }
            }

        private void gestionDesProfessionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var fen = new FenProfession();
                    fen.ShowDialog();
            }
            catch(Exception)
            {

            }
        }

     

        private void gestionDesVersementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var fen = new FenVersement();
                fen.ShowDialog();
                
            }
            catch (Exception ex)
            {

            }
        }

        private void CleanGrid()
        {
            try
            {
                //Nettoyer la grid=================================================
                List<CSouscripteur> Lempty = new List<CSouscripteur>();

                gridControlMembre.DataSource = Lempty;
                gridControlSouscripteur.DataSource = Lempty;
                gridControlVersement.DataSource = Lempty;
            }
            catch(Exception ex)
            {

            }
        }



        private void chkTousRevendeur_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTousSouscripteur.CheckState == CheckState.Checked)
                {
                    CmbSouscripteurA.Enabled = false;
                    CmbSouscripteurDE.Enabled = false;
                  
                    CleanGrid();
                
                }
                else
                {
                    CmbSouscripteurA.Enabled = true;
                    CmbSouscripteurDE.Enabled = true;
                  
                    CleanGrid();
                
                }

            }
            catch (Exception ex)
            {
               // if (splashScreenManager2.IsSplashFormVisible) splashScreenManager2.CloseWaitForm();
                MessageBox.Show("Une erreur est survenue ! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);

                var msg = "MainForm -> chkTousCom_CheckStateChanged -> TypeErreur: " + ex.Message; ;
                CAlias.Log(msg);

            }
        }

        private void SMulSouscripteur_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (SMulSouscripteur.CheckState == CheckState.Checked)
                {
                    chkCmbMultSouscripteur.Visible = true;

                    chkTousSouscripteur.Visible = false;

                    CmbSouscripteurA.Visible = false;

                    CmbSouscripteurDE.Visible = false;

                    lblRevA.Visible = false;
                    lblRevDe.Visible = false;
        

                }
                else
                {
                    chkCmbMultSouscripteur.Visible = false;

                    chkTousSouscripteur.Visible = true;

                    CmbSouscripteurA.Visible = true;

                    CmbSouscripteurDE.Visible = true;

                    lblRevA.Visible = true;
                    lblRevDe.Visible = true;
              
                }
                
                CleanGrid();
            }
            catch(Exception ex)
            {

            }
        }

        private void chkCmbMultSouscripteur_Closed(object sender, ClosedEventArgs e)
        {
            try
            {
                ListNomMultiSouscripteurs = string.Empty;

                ListPrenomMultiSouscripteurs = string.Empty;
           
                foreach (CheckedListBoxItem item in chkCmbMultSouscripteur.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                  
                        if (!item.Value.ToString().Equals(item.Description))
                        {
                            //le value c'est le prenom ,
                            ListPrenomMultiSouscripteurs += item.Value.ToString() + ",";

                            //si la description ramenée= prénom donc on a pas de nom
                            if (item.Value.ToString().Equals(item.Description))
                            {
                                ListNomMultiSouscripteurs += " " + ",";
                            }
                            else
                            {
                                //on peut donc retirer le nom par déduction d'avec le texte(description =Nom +" "+Prenom)
                                string tmp = " " + item.Value.ToString();
                                ListNomMultiSouscripteurs += item.Description.Replace(tmp, "") + ",";
                            }
                            
                        }
                        else
                        {
                            //On a que le nom

                            //le value c'est le prenom donc il prend vide,
                            ListPrenomMultiSouscripteurs += " " + ",";

                            ListNomMultiSouscripteurs += item.Description + ",";


                        }

                    }

                }

                if (ListPrenomMultiSouscripteurs.Length > 0) ListPrenomMultiSouscripteurs = ListPrenomMultiSouscripteurs.Substring(0, ListPrenomMultiSouscripteurs.Length - 1);
                if (ListNomMultiSouscripteurs.Length > 0) ListNomMultiSouscripteurs = ListNomMultiSouscripteurs.Substring(0, ListNomMultiSouscripteurs.Length - 1);
                
                CleanGrid();
            }
            catch (Exception ex)
            {
             //   if (splashScreenManager2.IsSplashFormVisible) splashScreenManager2.CloseWaitForm();
                MessageBox.Show("Une erreur est survenue ! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);

                var msg = "MainForm -> chkCmbMultCom_Closed -> TypeErreur: " + ex.Message; ;
                CAlias.Log(msg);
            }
        }

        private void CmbSouscripteurDE_Closed(object sender, ClosedEventArgs e)
        {
            try
            {
                ListNomLESouscripteurDE = string.Empty;

                ListNomLESouscripteurDE = CmbSouscripteurDE.Text;
                
                CleanGrid();
            }
            catch(Exception ex)
            {

            }
        }

        private void CmbSouscripteurA_Closed(object sender, ClosedEventArgs e)
        {
            try
            {
                ListNomLESouscripteurA = string.Empty;

                ListNomLESouscripteurA = CmbSouscripteurA.Text;

                CleanGrid();
                
            }
            catch(Exception ex)
            {

            }
        }

        private void chkTousClasseMetho_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTousClasseMetho.CheckState == CheckState.Checked)
            {
                chkCmbMultClasseMetho.Enabled = false;

                CleanGrid();

            }
            else
            {
                chkCmbMultClasseMetho.Enabled = true;
     

                CleanGrid();

            }
        }

        private void chkTousProfession_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTousProfession.CheckState == CheckState.Checked)
            {
                chkCmbMultiProfession.Enabled = false;

                CleanGrid();

            }
            else
            {
                chkCmbMultiProfession.Enabled = true;
                
                CleanGrid();

            }
        }

        private void chkCmbMultClasseMetho_Closed(object sender, ClosedEventArgs e)
        {
            try
            {
                ListIdClasseMtho = string.Empty;
                
                foreach (CheckedListBoxItem item in chkCmbMultClasseMetho.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {

                        if (Int32.Parse(item.Value.ToString())>0)
                        {
                            //le value c'est le prenom ,
                            ListIdClasseMtho += item.Value.ToString() + ",";
                            
                        }
                      

                    }

                }


                if (ListIdClasseMtho.Length > 0) ListIdClasseMtho = ListIdClasseMtho.Substring(0, ListIdClasseMtho.Length - 1);

            }
            catch(Exception ex)
            {

            }
        }

        private void chkCmbMultiProfession_Closed(object sender, ClosedEventArgs e)
        {
            try
            {
                ListIdProfession = string.Empty;

                foreach (CheckedListBoxItem item in chkCmbMultiProfession.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        if (Int32.Parse(item.Value.ToString()) > 0)
                        {
                            //le value c'est le prenom ,
                            ListIdProfession += item.Value.ToString() + ",";

                        }
                        
                    }

                }
                
                if (ListIdProfession.Length > 0) ListIdProfession = ListIdProfession.Substring(0, ListIdProfession.Length - 1);

            }
            catch (Exception ex)
            {

            }
        }

        private void sBtnImprimer_Click(object sender, EventArgs e)
        {
            try
            {
                //Ramener liste des souscripteurs

                List<CEtatSouscriptMembre> ListeEtatSousMembre = new List<CEtatSouscriptMembre>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeEtatSousMembre = daoReport.GetEtatSouscripteurMembre(ChaineConFimeco);
                
                if(ListeEtatSousMembre.Count>0)
                {

                    var FenReport = new XtraReportSousMembre(ListeEtatSousMembre);

                    var pt = new ReportPrintTool(FenReport);
                    pt.AutoShowParametersPanel = true;

                    pt.PreviewForm.PrintControl.ShowPageMargins = false;
                    //  if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                    pt.ShowPreviewDialog();
                }


            }
            catch (Exception ex)
            {

            }
        }

        private void sBtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if(gridView1.RowCount>0)
                {
                    if (sFDExcel.ShowDialog() == DialogResult.OK)
                    {
                        string chem = sFDExcel.FileName;

                        gridControlSouscripteur.ExportToXlsx(chem);
                        //   if (splashScreenManager2.IsSplashFormVisible) splashScreenManager2.CloseWaitForm();
                        MessageBox.Show("Exportation Excel OK", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    MessageBox.Show("Pas de données à exporter", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                
            }
            catch (Exception ex)
            {
                //if (splashScreenManager2.IsSplashFormVisible) splashScreenManager2.CloseWaitForm();
                MessageBox.Show("Une erreur est survenue ! Veuillez contacter votre Administrateur!", "AITSTOCK", MessageBoxButtons.OK, MessageBoxIcon.Error);

                var msg = "MainForm ->simpleButton2_Click -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }

        private void sBtnExcelVersement_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount > 0)//On a des numéros de series associés à la facture
            {
                if (sFDExcel.ShowDialog() == DialogResult.OK)
                {

                    var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());

                    if(Identif>0)
                    {
                        //recupéper la liste des versements associées
                        List<CVersement> LV = new List<CVersement>();
                        LV = daoReport.GetAllVersement(ChaineConFimeco);

                        var COP = LV.Where(c => c.mIdSouscripteur == Identif && c.mDateVersement.Year==sNumAnnee.Value).ToList();

                        //code sousc
                        var codeSous = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mCode").ToString();
                        //nom
                        var NomSous = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mNom").ToString();
                        //Prenom
                        var PrenomSous = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mPrenoms").ToString();

                        //Montant Versé
                        var MontantSous = Double.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mMontantVerse").ToString());


                        // Creating a Excel object. 
                        Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
                        Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                        worksheet = workbook.ActiveSheet;

                        string cheminFichierXLXS = sFDExcel.FileName;

                        if (cheminFichierXLXS != string.Empty)
                        {
                            worksheet.Cells[2, 2] = "Code Souscripteur";
                            worksheet.Cells[2, 2].Font.Bold = 1;

                            worksheet.Cells[3, 2] = "Nom Souscripteur";
                            worksheet.Cells[3, 2].Font.Bold = 1;
                            worksheet.Cells[4, 2] = "Prénoms Souscripteur";
                            worksheet.Cells[4, 2].Font.Bold = 1;
                            worksheet.Cells[5, 2] = "Montant Versé";
                            worksheet.Cells[5, 2].Font.Bold = 1;

                            worksheet.Cells[2, 3] = codeSous;
                            worksheet.Cells[3, 3] = NomSous;
                            worksheet.Cells[4, 3] = PrenomSous;
                            worksheet.Cells[5, 3] = MontantSous;

                            //Les versements

                            worksheet.Cells[7, 2] = "N° du Réçu";
                            worksheet.Cells[7, 2].Font.Bold = 1;
                            worksheet.Cells[7, 3] = "Montant Versé";
                            worksheet.Cells[7, 3].Font.Bold = 1;
                            worksheet.Cells[7, 4] = "Date Versement";
                            worksheet.Cells[7, 4].Font.Bold = 1;
                            worksheet.Cells[7, 5] = "Nom Receveur";
                            worksheet.Cells[7, 5].Font.Bold = 1;

                            int ligne = 8;
                            int col = 1;

                            foreach (var item in COP)
                            {
                                worksheet.Cells[ligne, col + 1] = item.mNumeroRecu;
                                worksheet.Cells[ligne, col + 2] = item.mMontantVersement;
                                worksheet.Cells[ligne, col + 3] = item.mDateVersement;
                                worksheet.Cells[ligne, col + 4] = item.mNomReceveur;

                                ligne += 1;
                            }

                            worksheet.Name = "Versement Souscripteur";

                            workbook.SaveAs(cheminFichierXLXS);

                            //    if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();

                            MessageBox.Show("Export Excel bien terminé!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            excel.Quit();
                            workbook = null;
                            excel = null;
                        }

                    }
                }
                
                }
                else
                {
                //aucun numero de serie associé donc pas besoin de creer un excel
             //   if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show("Votre grille est vide!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

              }
        }

     
    }
}
