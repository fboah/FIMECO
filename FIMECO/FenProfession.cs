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
    public partial class FenProfession : Form
    {
        //Liste des professions
        private List<CProfession> ListeCProf = new List<CProfession>();

        //Classe à modifier
        private CProfession CProf;

        private readonly DAOFimeco daoReport = new DAOFimeco();

        private readonly CAlias daoMain = new CAlias();

        //Chaine de connexion FIMECO
        private string ChaineConFIMECO;

        //Tester si on ajoute ou modif 
        private bool IsAjout;

        public FenProfession()
        {
            InitializeComponent();
        }

        private void sBtnAjoutProfession_Click(object sender, EventArgs e)
        {
            try
            {
                IsAjout = true;
                CProf = null;

                var fenAjout = new FenGestProfession(IsAjout, ListeCProf, CProf, ChaineConFIMECO);

                fenAjout.ShowDialog();

                RefreshGrid(ChaineConFIMECO);
            }
            catch(Exception ex)
            {

            }
            IsAjout = true;

        }

        private void RefreshGrid(string Chaine)
        {
            try
            {
                //Recupérer toutes les classes listés

                ListeCProf = daoReport.GetAllProfession(Chaine);

                gridControlProfession.DataSource = ListeCProf;
            }
            catch (Exception ex)
            {

            }
        }


        private void FenProfession_Load(object sender, EventArgs e)
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

        private void sBtnModifProfession_Click(object sender, EventArgs e)
        {

            try
            {

                IsAjout = false;

                CProfession ClientOp = new CProfession();

                var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());

                if (Identif > 0)
                {

                    //if (!splashScreenManager1.IsSplashFormVisible) splashScreenManager1.ShowWaitForm();
                    List<CProfession> ListeOPACTU = new List<CProfession>();

                    ListeOPACTU = daoReport.GetAllProfession (ChaineConFIMECO);

                    ClientOp = ListeOPACTU.FirstOrDefault(c => c.mId == Identif);
                    //   if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();

                    var fenAjout = new FenGestProfession(IsAjout, ListeOPACTU, ClientOp, ChaineConFIMECO);

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

        private void sBtnSupprProfession_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                var Identif = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mId").ToString());

                var nom = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "mLibelle").ToString();

                if (Identif > 0)
                {
                    var rep = MessageBox.Show("Voulez-vous supprimer la profession  "+nom+" ?", "FIMECO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rep == DialogResult.Yes)
                    {
                        res = daoReport.DeleteProfession(Identif, ChaineConFIMECO);

                        if (res)
                        {
                            //if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("profession supprimée avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            RefreshGrid(ChaineConFIMECO);

                        }
                        else
                        {
                            // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("Une erreur est survenue lors de la suppression de la profession!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
