using FIMECO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FIMECO.DAOFIMECO;
using FIMECO.Utils;

namespace FIMECO
{
    public partial class FenClasseMetho : Form
    {
        //Liste des classes metho
        private List<CClasseMetho> ListeClasseMetho = new List<CClasseMetho>();

        //Classe à modifier
        private CClasseMetho ClasseMetho;

        private readonly DAOFimeco daoReport = new DAOFimeco();

        private readonly CAlias daoMain = new CAlias();

        //Chaine de connexion FIMECO
        private string ChaineConFIMECO;

        //Tester si on ajoute ou modif 
        private bool IsAjout;

        public FenClasseMetho()
        {
            InitializeComponent();
        }

        private void sBtnAjoutClasse_Click(object sender, EventArgs e)
        {
            try
            {
                IsAjout = true;

                ClasseMetho = null;

                var fenAjout = new FenGestionClasseMetho(IsAjout, ListeClasseMetho, ClasseMetho, ChaineConFIMECO);

                fenAjout.ShowDialog();

                RefreshGrid(ChaineConFIMECO);
            }
            catch(Exception ex)
            {

            }
        }

        private void FenClasseMetho_Load(object sender, EventArgs e)
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

                RefreshGrid(ChaineConFIMECO);

            }
            catch(Exception ex)
            {

            }
        }

        private void RefreshGrid(string Chaine)
        {
            try
            {
                //Recupérer toutes les classes listés

                ListeClasseMetho = daoReport.GetAllClasseMetho(Chaine);

                gridControlClasseMetho.DataSource = ListeClasseMetho;
            }
            catch(Exception ex)
            {

            }
        }

        private void sBtnModifClasse_Click(object sender, EventArgs e)
        {
            try
            {

                IsAjout = false;

                CClasseMetho ClientOp = new CClasseMetho();

                var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());

                if (Identif > 0)
                {

                    //if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                    List<CClasseMetho> ListeOPACTU = new List<CClasseMetho>();

                    ListeClasseMetho = daoReport.GetAllClasseMetho(ChaineConFIMECO);

                    ClientOp = ListeClasseMetho.FirstOrDefault(c => c.mId == Identif);
                 //   if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();

                    var fenAjout = new FenGestionClasseMetho(IsAjout, ListeClasseMetho, ClientOp, ChaineConFIMECO);

                    fenAjout.ShowDialog();

                    RefreshGrid(ChaineConFIMECO);

                }
                else
                {
                 //   if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Veuillez sélectionner un élément à modifier!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch(Exception ex)
            {

            }
        }

        private void sBtnSupprClasse_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());


                if (Identif > 0)
                {
                    var rep = MessageBox.Show("Voulez-vous supprimer la classe selectionnée ?", "FIMECO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rep == DialogResult.Yes)
                    {
                        res = daoReport.DeleteClasseMetho(Identif, ChaineConFIMECO);

                        if (res)
                        {
                            //if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("Classe supprimée avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            RefreshGrid(ChaineConFIMECO);
                            
                        }
                        else
                        {
                           // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("Une erreur est survenue lors de la suppression de l'opération!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
