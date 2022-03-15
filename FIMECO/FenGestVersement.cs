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
    public partial class FenGestVersement : Form
    {
        private bool myObjectAjout;
        private readonly DAOFimeco daoReport = new DAOFimeco();
        private string myObjectChaineConFimeco;
        private List<CVersement> myObjectListeVersement;

        private CVersement myObjectVersement;
        private List<CSouscripteur> myObjectListeChefFamille;
        private List<CCotisationAnnuelle> myObjectListeCotisation;
        private int myObjectIdSouscripteur;

        private bool myObjectIsGestionVersement;

        public FenGestVersement()
        {
            InitializeComponent();
        }

        public FenGestVersement(bool IsAjout, List<CVersement> ListeVersement, CVersement CVers, string chainefimeco, int IdSouscripteur, List<CSouscripteur> ListeChefFamille,bool IsGestionVersement,List<CCotisationAnnuelle> LCot)
        {
            InitializeComponent();

            this.myObjectAjout = IsAjout;
            this.myObjectListeVersement = ListeVersement;
            this.myObjectVersement = CVers;
            this.myObjectChaineConFimeco = chainefimeco;
            this.myObjectListeChefFamille = ListeChefFamille;
            this.myObjectIdSouscripteur = IdSouscripteur;
            this.myObjectIsGestionVersement = IsGestionVersement;
            this.myObjectListeCotisation = LCot;

        }

        private void sBtnEnregistrer_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                if(sNumMontant.Value>0 && txtNumeroRecu.Text.Trim()!=string.Empty && txtReceveur.Text.Trim()!=string.Empty)
                {
                    if (myObjectAjout && myObjectVersement == null)
                    {
                        //tester si la souscription a déjà été faite

                        var IsCotAn = myObjectListeCotisation.Exists(c => c.mIdSouscripteur == Int32.Parse(CmbSouscripteur.EditValue.ToString()) && c.mAnnee == dateVersement.DateTime.Year);

                        if(!IsCotAn)
                        {
                            MessageBox.Show("Vous n'avez pas encore fait de souscription pour cette année!" + Environment.NewLine + "Merci de régulariser!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;
                        }
                        
                        //Tester qu'on a pas un doublon
                        var IsExist = myObjectListeVersement.Exists(c => c.mIdSouscripteur == Int32.Parse(CmbSouscripteur.EditValue.ToString()) && c.mMontantVersement == Int32.Parse(sNumMontant.Value.ToString()) && c.mDateVersement == DateTime.Parse(dateVersement.EditValue.ToString()) && c.mNomReceveur.ToUpper().Trim() == txtReceveur.Text.Replace("'", "''").ToUpper().Trim() && c.mNumeroRecu.ToUpper().Trim() == txtNumeroRecu.Text.Replace("'", "''").ToUpper().Trim());

                        //Tester qu'on a pas de doublon de Reçu

                        var IsDoublonRecu = myObjectListeVersement.Exists(c => c.mNumeroRecu.ToUpper().Trim() == txtNumeroRecu.Text.Replace("'", "''").ToUpper().Trim());

                        if (!IsExist && !IsDoublonRecu)
                        {
                            CVersement COp = new CVersement();
                            COp.mNomReceveur = txtReceveur.Text.Replace("'", "''").Trim();
                            COp.mDateVersement = DateTime.Parse(dateVersement.EditValue.ToString()).Date;
                            COp.mNumeroRecu = txtNumeroRecu.Text.Replace("'", "''").Trim();

                            if (!myObjectIsGestionVersement)
                            {

                                COp.mIdSouscripteur = myObjectIdSouscripteur;
                            }
                            else
                            {

                                COp.mIdSouscripteur = Int32.Parse(CmbSouscripteur.EditValue.ToString());
                            }

                            COp.mMontantVersement = Int64.Parse(sNumMontant.Value.ToString());
                            COp.mDateLastModif = DateTime.Now;
                            COp.mDateCreation = DateTime.Now;
                            COp.mUserCreation = Environment.UserDomainName + "\\" + Environment.UserName;
                            COp.mUserLastModif = Environment.UserDomainName + "\\" + Environment.UserName;


                            res = daoReport.AddVersement(COp, myObjectChaineConFimeco);

                            if (res)
                            {
                                MessageBox.Show("Versement créé avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Une erreur est survenue!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        else
                        {
                            //Objet déjà existant

                            MessageBox.Show("Ce Versement ou ce numero de reçu existe déjà ! Veuillez vérifier vos données", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }

                    }
                    else
                    {
                        //Modification

                        if (!myObjectAjout && myObjectVersement != null)
                        {

                            //tester si la souscription a déjà été faite

                            var IsCotAn = myObjectListeCotisation.Exists(c => c.mIdSouscripteur == Int32.Parse(CmbSouscripteur.EditValue.ToString()) && c.mAnnee == dateVersement.DateTime.Year);

                            if (!IsCotAn)
                            {
                                MessageBox.Show("Vous n'avez pas encore fait de souscription pour cette année!" + Environment.NewLine + "Merci de régulariser!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                return;
                            }

                            //Tester qu'on a pas un doublon
                            var IsExist = myObjectListeVersement.Exists(c => c.mIdSouscripteur == Int32.Parse(CmbSouscripteur.EditValue.ToString()) && c.mMontantVersement == Int32.Parse(sNumMontant.Value.ToString()) && c.mDateVersement == DateTime.Parse(dateVersement.EditValue.ToString()) && c.mNomReceveur.ToUpper().Trim() == txtReceveur.Text.Replace("'", "''").ToUpper().Trim() && c.mNumeroRecu.ToUpper().Trim() == txtNumeroRecu.Text.Replace("'", "''").ToUpper().Trim());

                            if (!IsExist)
                            {
                                myObjectVersement.mIdSouscripteur = Int32.Parse(CmbSouscripteur.EditValue.ToString());
                                myObjectVersement.mNomReceveur = txtReceveur.Text.Replace("'", "''").Trim();
                                myObjectVersement.mDateVersement = DateTime.Parse(dateVersement.EditValue.ToString()).Date;
                                myObjectVersement.mNumeroRecu = txtNumeroRecu.Text.Replace("'", "''").Trim();
                                myObjectVersement.mMontantVersement = Int64.Parse(sNumMontant.EditValue.ToString());

                                myObjectVersement.mDateLastModif = DateTime.Now;

                                myObjectVersement.mUserLastModif = Environment.UserDomainName + "\\" + Environment.UserName;

                                res = daoReport.UpdateVersement(myObjectVersement, myObjectChaineConFimeco);

                                if (res)
                                {
                                    MessageBox.Show("Versement  modifié avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("Une erreur est survenue!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                            else
                            {
                                //Objet déjà existant

                                MessageBox.Show("Ce Versement existe déjà ! Veuillez vérifier vos données", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }

                        }
                    }
                }
                else
                {
                    //montant doit etre superieur à 0,reçu !=0 ,Receveur !=null

                    MessageBox.Show("Le montant doit être supérieur à 0, le réçu et le Receveur doivent être renseignés !", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void FenGestVersement_Load(object sender, EventArgs e)
        {
            try
            {
                //Charger Combo des Chef de famille

                if (myObjectListeChefFamille.Count > 0)
                {
                    //Remplir Combo ClasseMetho
                    CmbSouscripteur.Properties.DataSource = myObjectListeChefFamille;
                    CmbSouscripteur.Properties.DisplayMember = "mNom";
                    CmbSouscripteur.Properties.ValueMember = "mId";
                    //Choisir les premières valeurs

                    if(!myObjectIsGestionVersement) CmbSouscripteur.EditValue = myObjectIdSouscripteur;


                }

                //Date souscription 

                dateVersement.EditValue = DateTime.Now;


                if (!myObjectAjout)//Modification
                {

                    txtNumeroRecu.Text = myObjectVersement.mNumeroRecu;

                    dateVersement.EditValue = myObjectVersement.mDateVersement;
                    
                        CmbSouscripteur.EditValue = myObjectVersement.mIdSouscripteur;
                  

                    txtReceveur.Text = myObjectVersement.mNomReceveur;
                    sNumMontant.Value = myObjectVersement.mMontantVersement;
                 

                }

            }
            catch(Exception ex)
            {

            }
        }

        private void sBtnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
