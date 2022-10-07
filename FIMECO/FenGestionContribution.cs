using FIMECO.DAOFIMECO;
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

namespace FIMECO
{
    public partial class FenGestionContribution : Form
    {
        private bool myObjectAjout;
        private readonly DAOFimeco daoReport = new DAOFimeco();
        private string myObjectChaineConFimeco;
        private List<CCotisationAnnuelle> myObjectListeCot;
        private CCotisationAnnuelle myObjectCot;
        private List<CSouscripteur> myObjectListeChefFamille;

        private int idAppli;

        public FenGestionContribution()
        {
            InitializeComponent();
        }

        public FenGestionContribution(bool IsAjout, List<CCotisationAnnuelle> ListeCotisation, CCotisationAnnuelle CCot, string chainefimeco,List<CSouscripteur>ListeSouscripteur,int idap)
        {
            InitializeComponent();

            this.myObjectAjout = IsAjout;
            this.myObjectListeCot = ListeCotisation;
            this.myObjectCot = CCot;
            this.myObjectChaineConFimeco = chainefimeco;
            this.myObjectListeChefFamille = ListeSouscripteur;

            this.idAppli = idap;
        }

        private void sBtnEnregistrer_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                if(sNumMontant.Value>0)
                {
                    if (myObjectAjout && myObjectCot == null)
                    {
                        //Tester qu'on a pas un doublon(Unicité d'un objectif pour une année donnée)
                        var IsExist = myObjectListeCot.Exists(c => c.mIdSouscripteur == Int32.Parse(CmbSouscripteurCot.EditValue.ToString()) && c.mAnnee == Int32.Parse(sNumAnnee.Value.ToString()) && c.mIdTypeAppli==idAppli );

                        if (!IsExist)
                        {
                            CCotisationAnnuelle COp = new CCotisationAnnuelle();
                            COp.mIdSouscripteur = Int32.Parse(CmbSouscripteurCot.EditValue.ToString());
                            COp.mAnnee = Int32.Parse(sNumAnnee.Value.ToString());
                            COp.mMontantCotisation = Int64.Parse(sNumMontant.Value.ToString());

                            COp.mDateLastModif = DateTime.Now;
                            COp.mDateCreation = DateTime.Now;
                            COp.mUserCreation = Environment.UserDomainName + "\\" + Environment.UserName;
                            COp.mUserLastModif = Environment.UserDomainName + "\\" + Environment.UserName;

                            COp.mIdTypeAppli = idAppli;

                            res = daoReport.AddCotisationAnnuelle(COp, myObjectChaineConFimeco);

                            if (res)
                            {
                                #region Tracabilité

                                CTracabilite Ct = new CTracabilite();

                                string content = "mIdTypeAppli:" + COp.mIdTypeAppli + "_" + "mId:" + COp.mIdSouscripteur + "_" + "Souscripteur:" + CmbSouscripteurCot.Text + "_" + "mAnneeSouscription:" + COp.mAnnee + "_" + "Montant:" + COp.mMontantCotisation.ToString("n0");
                                  

                                Ct.mContenu = content;
                               
                                Ct.mTypeOperation = "Ajout_ContributionAnnuelle";
                                Ct.mDateAction = DateTime.Now;
                                Ct.mMachineAction = Environment.UserDomainName + "\\" + Environment.UserName;

                                bool ret = false;

                                ret = daoReport.AddTrace(Ct, myObjectChaineConFimeco);
                                
                                #endregion
                                
                                MessageBox.Show("Contribution Annuelle ajoutée avec succès!", "VISIONPLUS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Une erreur est survenue!", "VISIONPLUS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        else
                        {
                            //Objet déjà existant

                            MessageBox.Show("Une Contribution Annuelle existe déjà pour ce souscripteur ! Veuillez vérifier vos données", "VISIONPLUS", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }


                    }
                    else
                    {
                        //Modification

                        if (!myObjectAjout && myObjectCot != null)
                        {
                            //Tester qu'on a pas un doublon
                            //Tester qu'on a pas un doublon
                            var IsExist = myObjectListeCot.Exists(c => c.mIdSouscripteur == Int32.Parse(CmbSouscripteurCot.EditValue.ToString()) && c.mAnnee == Int32.Parse(sNumAnnee.Value.ToString()) && c.mMontantCotisation == Int32.Parse(sNumMontant.Value.ToString()) && c.mIdTypeAppli == idAppli);

                            if (!IsExist)
                            {

                                myObjectCot.mIdSouscripteur = Int32.Parse(CmbSouscripteurCot.EditValue.ToString());
                                myObjectCot.mAnnee = Int32.Parse(sNumAnnee.Value.ToString());
                                myObjectCot.mMontantCotisation = Int64.Parse(sNumMontant.Value.ToString());

                                myObjectCot.mDateLastModif = DateTime.Now;
                                // myObjectCot.mDateCreation = DateTime.Now;
                                // myObjectCot.mUserCreation = Environment.UserDomainName + "\\" + Environment.UserName;
                                myObjectCot.mUserLastModif = Environment.UserDomainName + "\\" + Environment.UserName;

                                myObjectCot.mIdTypeAppli = idAppli;

                                res = daoReport.UpdateCotisationAnnuelle(myObjectCot, myObjectChaineConFimeco);

                                if (res)
                                {
                                    #region Tracabilité

                                    CTracabilite Ct = new CTracabilite();

                                    string content = "mIdTypeAppli:" + myObjectCot.mIdTypeAppli + "_" + "mId:" + myObjectCot.mIdSouscripteur + "_" + "Souscripteur:" + CmbSouscripteurCot.Text + "_" + "mAnneeSouscription:" + myObjectCot.mAnnee + "_" + "Montant:" + myObjectCot.mMontantCotisation.ToString("n0");


                                    Ct.mContenu = content;

                                    Ct.mTypeOperation = "Modification_ContributionAnnuelle";
                                    Ct.mDateAction = DateTime.Now;
                                    Ct.mMachineAction = Environment.UserDomainName + "\\" + Environment.UserName;

                                    bool ret = false;

                                    ret = daoReport.AddTrace(Ct, myObjectChaineConFimeco);


                                    #endregion
                                    
                                    MessageBox.Show("Contribution Annuelle modifiée avec succès!", "VISIONPLUS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("Une erreur est survenue!", "VISIONPLUS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                            else
                            {
                                //Objet déjà existant

                                MessageBox.Show("Une Contribution Annuelle existe déjà pour ce souscripteur ! Veuillez vérifier vos données", "VISIONPLUS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                
                            }

                        }
                    }
                }
                else
                {
                    //Montant superieur à 0
                    MessageBox.Show("Le montant doit être supérieur à 0!", "VISIONPLUS", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                
            }
            catch (Exception ex)
            {

            }
        }

        private void sBtnFermer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch(Exception ex)
            {

            }

        }

        private void FenGestionContribution_Load(object sender, EventArgs e)
        {
            try
            {
                if (myObjectListeChefFamille.Count > 0)
                {
                    //Remplir Combo ClasseMetho
                    CmbSouscripteurCot.Properties.DataSource = myObjectListeChefFamille.OrderBy(c=>c.mNom) ;
                    CmbSouscripteurCot.Properties.DisplayMember = "mNom";
                    CmbSouscripteurCot.Properties.ValueMember = "mId";
                    //Choisir les premières valeurs
                    CmbSouscripteurCot.ItemIndex = 0;
                }

                //Date par defaut

                sNumAnnee.Value = Int32.Parse(DateTime.Now.Year.ToString());


                if (!myObjectAjout)//Modification
                {
                    sNumAnnee.Value = myObjectCot.mAnnee;
                    sNumMontant.Value = myObjectCot.mMontantCotisation;
                    CmbSouscripteurCot.EditValue = myObjectCot.mIdSouscripteur;
                    
                }

            }
            catch(Exception ex)
            {

            }
        }
    }
}
