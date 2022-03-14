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
    public partial class FenGestMembreSouscripteur : Form
    {
        private bool myObjectAjout;
        private readonly DAOFimeco daoReport = new DAOFimeco();
        private string myObjectChaineConFimeco;
        private List<CMembreSouscripteur> myObjectListeMembreSouscripteur;
        private CMembreSouscripteur myObjectMembreSouscripteur;
        private List<CSouscripteur> myObjectListeChefFamille;
        private List<CProfession> myObjectListeProfession;
        private int myObjectIdSouscripteur;

        public FenGestMembreSouscripteur()
        {
            InitializeComponent();
        }

        public FenGestMembreSouscripteur(bool IsAjout, List<CMembreSouscripteur> ListeMembreSouscripteur, CMembreSouscripteur CSous, string chainefimeco,int IdSouscripteur,List<CSouscripteur>ListeChefFamille, List<CProfession> ListeProfession)
        {
            InitializeComponent();
            
            this.myObjectAjout = IsAjout;
            this.myObjectListeMembreSouscripteur = ListeMembreSouscripteur;
            this.myObjectMembreSouscripteur = CSous;
            this.myObjectChaineConFimeco = chainefimeco;
            this.myObjectListeChefFamille = ListeChefFamille;
            this.myObjectIdSouscripteur = IdSouscripteur;
            this.myObjectListeProfession = ListeProfession;
        }

        private void sBtnEnregistrer_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                if (myObjectAjout && myObjectMembreSouscripteur == null)
                {
                    //Tester qu'on a pas un doublon
                    var IsExist = myObjectListeMembreSouscripteur.Exists(c => c.mIdSouscripteur == Int32.Parse(CmbChefFamille.EditValue.ToString()) && c.mCellulaire.ToUpper().Trim() == txtCellulaire.Text.Replace("'", "''").ToUpper().Trim() && c.mDateNaissance == DateTime.Parse(dateNaissance.EditValue.ToString()) && c.mLieuNaissance.ToUpper().Trim() == txtLieuNaissance.Text.Replace("'", "''").ToUpper().Trim() && c.mEmail.ToUpper().Trim() == txtEmail.Text.Replace("'", "''").ToUpper().Trim() && c.mNomMembre.ToUpper().Trim() == txtNom.Text.Replace("'", "''").ToUpper().Trim() && c.mPrenomsMembre.ToUpper().Trim() == txtPrenoms.Text.Replace("'", "''").ToUpper().Trim() && c.mIdProfession ==Int32.Parse(CmbProfession.EditValue.ToString()) && c.mSexe.ToUpper().Trim() == cmbSexe.Text.Replace("'", "''").ToUpper().Trim() && c.mStatutFamilial.ToUpper().Trim() == cmbStatut.Text.Replace("'", "''").ToUpper().Trim() && c.mTelephone.ToUpper().Trim() == txtTelephone.Text.Replace("'", "''").ToUpper().Trim());

                    if (!IsExist)
                    {
                        CMembreSouscripteur COp = new CMembreSouscripteur();
                        COp.mCellulaire = txtCellulaire.Text.Replace("'", "''").Trim();
                        COp.mDateNaissance = DateTime.Parse(dateNaissance.EditValue.ToString()).Date;
                        COp.mLieuNaissance = txtLieuNaissance.Text.Replace("'", "''").Trim();
                        COp.mEmail = txtEmail.Text.Replace("'", "''").Trim();
                        COp.mNomMembre = txtNom.Text.Replace("'", "''").Trim();
                        COp.mPrenomsMembre = txtPrenoms.Text.Replace("'", "''").Trim();
                        COp.mIdSouscripteur = myObjectIdSouscripteur;
                        COp.mProfession = CmbProfession.Text.Replace("'", "''").Trim();
                        COp.mIdProfession = Int32.Parse(CmbProfession.EditValue.ToString());

                        COp.mSexe = cmbSexe.Text.Replace("'", "''").Trim();
                        COp.mStatutFamilial = cmbStatut.Text.Replace("'", "''").Trim();
                        COp.mTelephone = txtTelephone.Text.Replace("'", "''").Trim();
                        COp.mDateLastModif = DateTime.Now;
                        COp.mDateCreation = DateTime.Now;
                        COp.mUserCreation = Environment.UserDomainName + "\\" + Environment.UserName;
                        COp.mUserLastModif = Environment.UserDomainName + "\\" + Environment.UserName;
                        COp.mIsAdulteMembre = CalculAge(COp.mDateNaissance, DateTime.Now);


                        res = daoReport.AddMembreSouscripteur(COp, myObjectChaineConFimeco);

                        if (res)
                        {
                            MessageBox.Show("Membre créé avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                        MessageBox.Show("Ce Membre existe déjà ! Veuillez vérifier vos données", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }


                }
                else
                {
                    //Modification

                    if (!myObjectAjout && myObjectMembreSouscripteur != null)
                    {
                        //Tester qu'on a pas un doublon
                        var IsExist = myObjectListeMembreSouscripteur.Exists(c => c.mIdSouscripteur == Int32.Parse(CmbChefFamille.EditValue.ToString()) && c.mCellulaire.ToUpper().Trim() == txtCellulaire.Text.Replace("'", "''").ToUpper().Trim() && c.mDateNaissance == DateTime.Parse(dateNaissance.EditValue.ToString()) && c.mLieuNaissance.ToUpper().Trim() == txtLieuNaissance.Text.Replace("'", "''").ToUpper().Trim() && c.mEmail.ToUpper().Trim() == txtEmail.Text.Replace("'", "''").ToUpper().Trim() && c.mNomMembre.ToUpper().Trim() == txtNom.Text.Replace("'", "''").ToUpper().Trim() && c.mPrenomsMembre.ToUpper().Trim() == txtPrenoms.Text.Replace("'", "''").ToUpper().Trim() && c.mIdProfession == Int32.Parse(CmbProfession.EditValue.ToString()) && c.mSexe.ToUpper().Trim() == cmbSexe.Text.Replace("'", "''").ToUpper().Trim() && c.mStatutFamilial.ToUpper().Trim() == cmbStatut.Text.Replace("'", "''").ToUpper().Trim() && c.mTelephone.ToUpper().Trim() == txtTelephone.Text.Replace("'", "''").ToUpper().Trim());

                        if (!IsExist)
                        {
                            myObjectMembreSouscripteur.mIdSouscripteur = Int32.Parse(CmbChefFamille.EditValue.ToString());
                            myObjectMembreSouscripteur.mCellulaire = txtCellulaire.Text.Replace("'", "''").Trim();
                            myObjectMembreSouscripteur.mDateNaissance = DateTime.Parse(dateNaissance.EditValue.ToString()).Date;
                            myObjectMembreSouscripteur.mLieuNaissance = txtLieuNaissance.Text.Replace("'", "''").Trim();
                            myObjectMembreSouscripteur.mEmail = txtEmail.Text.Replace("'", "''").Trim();
                            myObjectMembreSouscripteur.mNomMembre = txtNom.Text.Replace("'", "''").Trim();
                            myObjectMembreSouscripteur.mPrenomsMembre = txtPrenoms.Text.Replace("'", "''").Trim();
                            myObjectMembreSouscripteur.mProfession = CmbProfession.Text.Replace("'", "''").Trim();
                            myObjectMembreSouscripteur.mIdProfession = Int32.Parse(CmbProfession.EditValue.ToString());
                            myObjectMembreSouscripteur.mSexe = cmbSexe.Text.Replace("'", "''").Trim();
                            myObjectMembreSouscripteur.mStatutFamilial = cmbStatut.Text.Replace("'", "''").Trim();
                            myObjectMembreSouscripteur.mTelephone = txtTelephone.Text.Replace("'", "''").Trim();
                            myObjectMembreSouscripteur.mDateLastModif = DateTime.Now;
                            //myObjectSouscripteur.mDateCreation = DateTime.Now;
                            //myObjectSouscripteur.mUserCreation = Environment.UserDomainName + "\\" + Environment.UserName;
                            myObjectMembreSouscripteur.mUserLastModif = Environment.UserDomainName + "\\" + Environment.UserName;

                            myObjectMembreSouscripteur.mIsAdulteMembre = CalculAge(myObjectMembreSouscripteur.mDateNaissance, DateTime.Now);

                            res = daoReport.UpdateMembreSouscripteur(myObjectMembreSouscripteur, myObjectChaineConFimeco);

                            if (res)
                            {
                                MessageBox.Show("Membre  modifié avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                            MessageBox.Show("Ce Membre existe déjà ! Veuillez vérifier vos données", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        private string CalculAge(DateTime datenaissance, DateTime Mtn)
        {
            string ret = string.Empty;
            try
            {
                if (Mtn > datenaissance)
                {
                    var nbrejrs = Mtn.Date - datenaissance.Date;

                    var age = nbrejrs.Days / 365;

                    if (age >= 18)
                    {
                        ret = "Adulte";
                    }
                    else
                    {
                        ret = "Enfant";
                    }

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



        private void FenGestMembreSouscripteur_Load(object sender, EventArgs e)
        {
            try
            {
                //Charger Combo des Chef de famille

                if (myObjectListeChefFamille.Count > 0)
                {
                    //Remplir Combo ClasseMetho
                    CmbChefFamille.Properties.DataSource = myObjectListeChefFamille;
                    CmbChefFamille.Properties.DisplayMember = "mNom";
                    CmbChefFamille.Properties.ValueMember = "mId";

                    //Choisir lA VALEUR du chef de famille par defaut(Celui quon a selectionné)
                    CmbChefFamille.EditValue = myObjectIdSouscripteur;
                    // CmbChefFamille.ItemIndex = 0;
                }

                if (myObjectListeProfession.Count > 0)
                {
                    //Remplir Combo ClasseMetho
                    CmbProfession.Properties.DataSource = myObjectListeProfession;
                    CmbProfession.Properties.DisplayMember = "mLibelle";
                    CmbProfession.Properties.ValueMember = "mId";
                    //Choisir les premières valeurs
                    CmbProfession.ItemIndex = 0;
                }
                
                //Date souscription 

                dateNaissance.EditValue = DateTime.Now;

                //SEXE
                cmbSexe.SelectedIndex = 0;
               
                //Statut 
                cmbStatut.SelectedIndex = 0;


                if (!myObjectAjout)//Modification
                {

                    txtCellulaire.Text = myObjectMembreSouscripteur.mCellulaire;
                  
                    dateNaissance.EditValue = myObjectMembreSouscripteur.mDateNaissance;
                    
                    CmbChefFamille.EditValue = myObjectMembreSouscripteur.mIdSouscripteur;
                    CmbProfession.EditValue = myObjectMembreSouscripteur.mIdProfession;
                    txtLieuNaissance.Text = myObjectMembreSouscripteur.mLieuNaissance;
                    txtEmail.Text = myObjectMembreSouscripteur.mEmail;
                    txtNom.Text = myObjectMembreSouscripteur.mNomMembre;
                    txtPrenoms.Text = myObjectMembreSouscripteur.mPrenomsMembre;
                  
                    cmbSexe.Text = myObjectMembreSouscripteur.mSexe;
                    cmbStatut.Text = myObjectMembreSouscripteur.mStatutFamilial;
                    txtTelephone.Text = myObjectMembreSouscripteur.mTelephone;

                }

            }
            catch(Exception ex)
            {

            }
        }
    }
}
