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
    public partial class FenCotisationAn : Form
    {
        //Liste des cotisations
        private List<CCotisationAnnuelle> ListeCotisation = new List<CCotisationAnnuelle>();

        //Cotisation à modifier
        private CCotisationAnnuelle CCotisation;

        private readonly DAOFimeco daoReport = new DAOFimeco();

        private readonly CAlias daoMain = new CAlias();

        //Chaine de connexion FIMECO
        private string ChaineConFIMECO;

        //Tester si on ajoute ou modif 
        private bool IsAjout;

        private int IdAppli;

        public FenCotisationAn(int idta)
        {
            InitializeComponent();

            this.IdAppli = idta;
        }


       
        private void sBtnAjoutContribution_Click(object sender, EventArgs e)
        {
            try
            {
                IsAjout = true;

                CCotisation = null;

                //Liste des souscripteurs

                List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeSS = daoReport.GetAllSouscripteur(ChaineConFIMECO);

                //Liste des cotisations
                ListeCotisation = daoReport.GetAllCotisationAnnuelle(ChaineConFIMECO, ListeSS,IdAppli.ToString());

                var fenAjout = new FenGestionContribution(IsAjout, ListeCotisation, CCotisation, ChaineConFIMECO, ListeSS, IdAppli);
                fenAjout.Text = "Ajouter un Objectif Annuel";
                fenAjout.ShowDialog();

                RefreshGrid(ChaineConFIMECO);
            }
            catch(Exception ex)
            {
                var msg = "FenCotisationAn -> sBtnAjoutContribution_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }


        private void FenCotisationAn_Load(object sender, EventArgs e)
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

                sNumAnDeb.EditValue = Int32.Parse(DateTime.Now.Year.ToString());
                sNumAnFin.EditValue = Int32.Parse(DateTime.Now.Year.ToString());
                
                RefreshGrid(ChaineConFIMECO);

            }
            catch (Exception ex)
            {
                var msg = "FenCotisationAn -> FenCotisationAn_Load-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }



        private void RefreshGrid(string Chaine)
        {
            try
            {
                List<CCotisationAnnuelle> ListeCotApercu = new List<CCotisationAnnuelle>();

                string andeb = sNumAnDeb.Value.ToString();
                string anfin = sNumAnFin.Value.ToString();

                //Liste des souscripteurs

                List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeSS = daoReport.GetAllSouscripteur(ChaineConFIMECO);

                ListeCotApercu = daoReport.GetAllCotisationAnnuelleByAn(ChaineConFIMECO, andeb, anfin, ListeSS,IdAppli.ToString());

                gridControlCotisationAn.DataSource = ListeCotApercu.OrderBy(c=>c.mNom);

                ////Liste des souscripteurs

                //List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                ////   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                //ListeSS = daoReport.GetAllSouscripteur(ChaineConFIMECO);
                ////Recupérer toutes les cotisations listés

                //ListeCotisation = daoReport.GetAllCotisationAnnuelle(Chaine,ListeSS);

                //gridControlCotisationAn.DataSource = ListeCotisation;
            }
            catch (Exception ex)
            {
                var msg = "FenCotisationAn -> RefreshGrid-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }



    
        private void sBtnApercu_Click(object sender, EventArgs e)
        {
            try
            {
                List<CCotisationAnnuelle> ListeCotApercu = new List<CCotisationAnnuelle>();

                string andeb = sNumAnDeb.Value.ToString();
                string anfin = sNumAnFin.Value.ToString();

                //Liste des souscripteurs

                List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeSS = daoReport.GetAllSouscripteur(ChaineConFIMECO);

                ListeCotApercu = daoReport.GetAllCotisationAnnuelleByAn(ChaineConFIMECO, andeb, anfin, ListeSS, IdAppli.ToString());

                gridControlCotisationAn.DataSource = ListeCotApercu.OrderBy(c=>c.mNom);
                
            }
            catch(Exception ex)
            {
                var msg = "FenCotisationAn -> sBtnApercu_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }


   
        private void sBtnModifierContribution_Click(object sender, EventArgs e)
        {
            try
            {
                IsAjout = false;

                CCotisationAnnuelle ClientOp = new CCotisationAnnuelle();

                List<CSouscripteur> ListeSS = new List<CSouscripteur>();

                //   if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                ListeSS = daoReport.GetAllSouscripteur(ChaineConFIMECO);

                var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());

                if (Identif > 0)
                {

                    //if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                    List<CCotisationAnnuelle> ListeOPACTU = new List<CCotisationAnnuelle>();

                    ListeOPACTU = daoReport.GetAllCotisationAnnuelle (ChaineConFIMECO,ListeSS,IdAppli.ToString());

                    ClientOp = ListeOPACTU.FirstOrDefault(c => c.mId == Identif);
                    //   if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();

                    var fenAjout = new FenGestionContribution(IsAjout, ListeOPACTU, ClientOp, ChaineConFIMECO, ListeSS,IdAppli);
                    fenAjout.Text = "Modifier un Objectif Annuel";
                    fenAjout.ShowDialog();

                    RefreshGrid(ChaineConFIMECO);

                }
                else
                {
                    //   if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Veuillez sélectionner un élément à modifier!", "VISIONPLUS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception ex)
            {
                var msg = "FenCotisationAn -> sBtnModifierContribution_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }


       
        private void sBtnSupprimerContribution_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());


                if (Identif > 0)
                {
                    var rep = MessageBox.Show("Voulez-vous supprimer la cotisation selectionnée ?", "VISIONPLUS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rep == DialogResult.Yes)
                    {
                        res = daoReport.DeleteCotisationAnnuelle(Identif, ChaineConFIMECO);

                        if (res)
                        {

                            #region Tracabilité

                            CTracabilite Ct = new CTracabilite();

                            string content = "Id de la Contribution: " + Identif.ToString();

                            Ct.mContenu = content;

                            Ct.mTypeOperation = "Suppression_ContributionAnnuelle";
                            Ct.mDateAction = DateTime.Now;
                            Ct.mMachineAction = Environment.UserDomainName + "\\" + Environment.UserName;

                            bool ret = false;

                            ret = daoReport.AddTrace(Ct, ChaineConFIMECO);


                            #endregion

                            //if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("Cotisation supprimée avec succès!", "VISIONPLUS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            RefreshGrid(ChaineConFIMECO);

                        }
                        else
                        {
                            // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("Une erreur est survenue lors de la suppression de l'opération!", "VISIONPLUS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                var msg = "FenCotisationAn -> sBtnSupprimerContribution_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }
    }
}
