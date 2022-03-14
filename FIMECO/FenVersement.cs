using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using FIMECO.DAOFIMECO;
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
    
    public partial class FenVersement : Form
    {

      private string ListNomLESouscripteurDE;

      private string ListNomLESouscripteurA;

        private string ListIdSous;

        //Liste des Versement
        private List<CVersement> ListeVersement = new List<CVersement>();

        //Versement à modifier
        private CVersement Cvers;

        private readonly DAOFimeco daoReport = new DAOFimeco();

        private readonly CAlias daoMain = new CAlias();

        //Chaine de connexion FIMECO
        private string ChaineConFIMECO;

        //Tester si on ajoute ou modif 
        private bool IsAjout;

        public FenVersement()
        {
            InitializeComponent();
        }





        private void sBtnAjoutVersement_Click(object sender, EventArgs e)
        {
            try
            {
                IsAjout = true;

                Cvers = null;

                //Liste des souscripteurs

                List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeSS = daoReport.GetAllSouscripteur(ChaineConFIMECO);

                //envoyer la liste des versements réaliser pour s'assurer qu'on a pas de doublons(au niveau des reçu)

                List<CVersement> ListeVersement = new List<CVersement>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeVersement = daoReport.GetAllVersement(ChaineConFIMECO, ListeSS);

                //Recuperer la liste des souscription annuelles

                List<CCotisationAnnuelle> ListeCotisation = new List<CCotisationAnnuelle>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeCotisation = daoReport.GetAllCotisationAnnuelle(ChaineConFIMECO, ListeSS);


                var fenAjout = new FenGestVersement(IsAjout, ListeVersement, Cvers, ChaineConFIMECO,0, ListeSS,true, ListeCotisation);

                fenAjout.ShowDialog();

                RefreshGrid(ChaineConFIMECO);
            }
            catch (Exception ex)
            {

            }
        }

       
        private void RefreshGrid(string Chaine)
        {
            try
            {

                //Liste des souscripteurs

                List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeSS = daoReport.GetAllSouscripteur(ChaineConFIMECO);

                //Recupérer toutes les classes listés
                
                    
                string andeb = dateDeb.DateTime.Date.ToString();
                string anfin = dateFin.DateTime.Date.ToString();

                if (ListNomLESouscripteurDE == string.Empty || ListNomLESouscripteurDE == null)
                {
                    ListNomLESouscripteurDE = CmbSouscripteurDE.Text;
                }

                if (ListNomLESouscripteurA == string.Empty || ListNomLESouscripteurA == null)
                {
                    ListNomLESouscripteurA = CmbSouscripteurA.Text;
                }

                ListeVersement = daoReport.GetAllVersementApercu(ChaineConFIMECO, andeb, anfin, ListeSS, chkTousSouscripteur.Checked, ListNomLESouscripteurDE, ListNomLESouscripteurA, SMulSouscripteur.Checked, ListIdSous);
                //ChaineConFIMECO, andeb, anfin, ListeSS, chkTousSouscripteur.Checked, ListNomLESouscripteurDE, ListNomLESouscripteurA, SMulSouscripteur.Checked, ListIdSous)
                
                gridControlVersement.DataSource = ListeVersement;
            }
            catch (Exception ex)
            {

            }
        }

        private void FenVersement_Load(object sender, EventArgs e)
        {
            try
            {
                //Chaine Connexion FIMECO
                List<CAlias> ListChaine = new List<CAlias>();

                ListChaine = daoMain.GetAliasFIMECO();

                var ChooseAIT = ListChaine.FirstOrDefault(c => c.IsAbidjan == true);

                if (ChooseAIT != null)
                {
                    ChaineConFIMECO = ChooseAIT.mAliasName;
                }

                dateDeb.EditValue = DateTime.Now;
                dateFin.EditValue = DateTime.Now;

                ///============ Charger Combo des souscripteurs =======================

                //charger classe metho

               List<CClasseMetho> ListeClasseMetho = daoReport.GetAllClasseMetho(ChaineConFIMECO);

                List<CSouscripteur> ListeSouscripteur = new List<CSouscripteur>();

                ListeSouscripteur = daoReport.GetAllSouscripteur(ChaineConFIMECO, ListeClasseMetho);

                CmbSouscripteurDE.Properties.DataSource = ListeSouscripteur;
                CmbSouscripteurA.Properties.DataSource = ListeSouscripteur;

                chkCmbMultSouscripteur.Properties.DataSource = ListeSouscripteur;
                FillMultiCheckComboSouscripteur(chkCmbMultSouscripteur, ListeSouscripteur);

                CmbSouscripteurDE.Properties.DisplayMember = "mNom";
                CmbSouscripteurA.Properties.DisplayMember = "mNom";

                //Choisir les premières valeurs
                CmbSouscripteurDE.ItemIndex = 0;
                CmbSouscripteurA.ItemIndex = 0;
                
                RefreshGrid(ChaineConFIMECO);

            }
            catch (Exception ex)
            {

            }
        }


        public void FillMultiCheckComboSouscripteur(CheckedComboBoxEdit cmb, List<CSouscripteur> Lste)
        {
            try
            {
                if (Lste != null && Lste.Count > 0)
                {
                    var MySelectBases = new DataTable();

                    //MySelectBases.Columns.Add("mNom");

                    //MySelectBases.Columns.Add("mPrenoms");
                    MySelectBases.Columns.Add("mId");

                    MySelectBases.Columns.Add("mNomPrenoms");


                    foreach (var item in Lste)
                    {
                        if (item.mNom != string.Empty && item.mPrenoms != string.Empty)
                        {
                            MySelectBases.Rows.Add(item.mId, item.mNom + " " + item.mPrenoms);

                        }

                        //if (item.mNom == string.Empty && item.mPrenoms != string.Empty)
                        //{
                        //    MySelectBases.Rows.Add(item.mNom, item.mPrenoms, item.mPrenoms);

                        //}

                        //if (item.mNom != string.Empty && item.mPrenoms == string.Empty)
                        //{
                        //    MySelectBases.Rows.Add(item.mNom, item.mNom, item.mNom);

                        //}

                        cmb.Properties.DataSource = MySelectBases;

                        cmb.Properties.ValueMember = "mId";

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


        private void sBtnModifVersement_Click(object sender, EventArgs e)
        {
            try
            {

                IsAjout = false;

                CVersement ClientOp = new CVersement();

                var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());

                var IdSouscripteur = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mIdSouscripteur").ToString());

                if (Identif > 0)
                {


                    //Liste des souscripteurs

                    List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                    //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                    ListeSS = daoReport.GetAllSouscripteur(ChaineConFIMECO);
                    //if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                    List<CVersement> ListeOPACTU = new List<CVersement>();

                    ListeOPACTU = daoReport.GetAllVersement(ChaineConFIMECO,ListeSS);

                    ClientOp = ListeOPACTU.FirstOrDefault(c => c.mId == Identif);
                    //   if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();


                    //Recuperer la liste des souscription annuelles

                    List<CCotisationAnnuelle> ListeCotisation = new List<CCotisationAnnuelle>();

                    //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                    ListeCotisation = daoReport.GetAllCotisationAnnuelle(ChaineConFIMECO, ListeSS);


                    var fenAjout = new FenGestVersement(IsAjout, ListeOPACTU, ClientOp, ChaineConFIMECO, IdSouscripteur, ListeSS,true, ListeCotisation);

                    fenAjout.ShowDialog();

                    RefreshGrid(ChaineConFIMECO);

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

        private void sBtnSupprVersement_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());


                if (Identif > 0)
                {
                    var rep = MessageBox.Show("Voulez-vous supprimer le versement selectionné ?", "FIMECO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rep == DialogResult.Yes)
                    {
                        res = daoReport.DeleteVersement(Identif, ChaineConFIMECO);

                        if (res)
                        {
                            //if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("Versement supprimé avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            RefreshGrid(ChaineConFIMECO);

                        }
                        else
                        {
                            // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("Une erreur est survenue lors de la suppression du versement!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void sBtnApercu_Click(object sender, EventArgs e)
        {
            try
            {
                List<CVersement> ListeCotApercu = new List<CVersement>();

                string andeb = dateDeb.DateTime.Date.ToString();
                string anfin = dateFin.DateTime.Date.ToString();

                //Liste des souscripteurs

                List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeSS = daoReport.GetAllSouscripteur(ChaineConFIMECO);

                if (ListNomLESouscripteurDE == string.Empty || ListNomLESouscripteurDE == null)
                {
                    ListNomLESouscripteurDE = CmbSouscripteurDE.Text;
                }

                if (ListNomLESouscripteurA == string.Empty || ListNomLESouscripteurA == null)
                {
                    ListNomLESouscripteurA = CmbSouscripteurA.Text;
                }

                ListeCotApercu = daoReport.GetAllVersementApercu(ChaineConFIMECO, andeb, anfin, ListeSS,chkTousSouscripteur.Checked, ListNomLESouscripteurDE, ListNomLESouscripteurA,SMulSouscripteur.Checked,ListIdSous);

                gridControlVersement.DataSource = ListeCotApercu;
            }
            catch(Exception ex)
            {

            }
        }


        private void CleanGrid()
        {
            try
            {
                //Nettoyer la grid=================================================
                List<CSouscripteur> Lempty = new List<CSouscripteur>();
                
                gridControlVersement.DataSource = Lempty;
            }
            catch (Exception ex)
            {

            }
        }

        
        private void chkTousSouscripteur_CheckedChanged(object sender, EventArgs e)
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

        private void SMulSouscripteur_CheckedChanged(object sender, EventArgs e)
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
            catch (Exception ex)
            {

            }
        }

        private void chkCmbMultSouscripteur_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            try
            {
                ListIdSous = string.Empty;

                foreach (CheckedListBoxItem item in chkCmbMultSouscripteur.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {

                        if (Int32.Parse(item.Value.ToString()) > 0)
                        {
                            //le value c'est le prenom ,
                            ListIdSous += item.Value.ToString() + ",";

                        }


                    }

                }


                if (ListIdSous.Length > 0) ListIdSous = ListIdSous.Substring(0, ListIdSous.Length - 1);

            }
            catch (Exception ex)
            {

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
            catch (Exception ex)
            {

            }
        }
    }
}
