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
    public partial class FenSouscripteur : Form
    {
        private bool myObjectAjout;
        private readonly DAOFimeco daoReport = new DAOFimeco();
        private string myObjectChaineConFimeco;
        private List<CSouscripteur> myObjectListeSouscripteur;
        private CSouscripteur myObjectSouscripteur;

        private List<CProfession> myObjectListeProfession;

        public string Appli = "VISIONPLUS";

        public FenSouscripteur()
        {
            InitializeComponent();
        }

        public FenSouscripteur(bool IsAjout, List<CSouscripteur> ListeSouscripteur, CSouscripteur CSous, string chainefimeco, List<CProfession> ListeProfession)
        {
            InitializeComponent();

            this.myObjectAjout = IsAjout;
            this.myObjectListeSouscripteur = ListeSouscripteur;
            this.myObjectSouscripteur = CSous;
            this.myObjectChaineConFimeco = chainefimeco;
            this.myObjectListeProfession = ListeProfession;
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

    

        private void sBtnEnregistrer_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                if(txtNom.Text.Trim()!=string.Empty && txtPrenoms.Text.Trim() != string.Empty && txtCodeSouscripteur.Text.Trim() != string.Empty && txtCellulaire.Text.Trim() != string.Empty)
                {

                    if (myObjectAjout && myObjectSouscripteur == null)
                    {
                        //Tester qu'on a pas un doublon
                        var IsExist = myObjectListeSouscripteur.Exists(c => c.mCellulaire.ToUpper().Trim() == txtCellulaire.Text.Replace("'", "''").ToUpper().Trim() && c.mCircuit.ToUpper().Trim() == txtCircuit.Text.Replace("'", "''").ToUpper().Trim() && c.mCodeCircuit.ToUpper().Trim() == txtCodeCircuit.Text.Replace("'", "''").ToUpper().Trim() && c.mCode.ToUpper().Trim() == txtCodeSouscripteur.Text.Replace("'", "''").ToUpper().Trim() && c.mCodeDistrict.ToUpper().Trim() == txtCodeDistrict.Text.Replace("'", "''").ToUpper().Trim() && c.mCodeEgliseLocale.ToUpper().Trim() == txtCodeEglise.Text.Replace("'", "''").ToUpper().Trim() && c.mDateSouscription == DateTime.Parse(dateSouscription.EditValue.ToString()) && c.mDateNaissance == DateTime.Parse(dateNaissance.EditValue.ToString()) && c.mDistrict.ToUpper().Trim() == txtDistrict.Text.Replace("'", "''").ToUpper().Trim() && c.mEgliseLocale.ToUpper().Trim() == txtEglise.Text.Replace("'", "''").ToUpper().Trim() && c.mIdClasseMetho == Int32.Parse(CmbClasseMetho.EditValue.ToString()) && c.mLieuNaissance.ToUpper().Trim() == txtLieuNaissance.Text.Replace("'", "''").ToUpper().Trim() && c.mEmail.ToUpper().Trim() == txtEmail.Text.Replace("'", "''").ToUpper().Trim() && c.mNom.ToUpper().Trim() == txtNom.Text.Replace("'", "''").ToUpper().Trim() && c.mPrenoms.ToUpper().Trim() == txtPrenoms.Text.Replace("'", "''").ToUpper().Trim() && c.mIdProfession == Int32.Parse(CmbProfession.EditValue.ToString()) && c.mSexe.ToUpper().Trim() == cmbSexe.Text.Replace("'", "''").ToUpper().Trim() && c.mStatutFamilial.ToUpper().Trim() == cmbStatut.Text.Replace("'", "''").ToUpper().Trim() && c.mTelephone.ToUpper().Trim() == txtTelephone.Text.Replace("'", "''").ToUpper().Trim());

                        if (!IsExist)
                        {
                            CSouscripteur COp = new CSouscripteur();
                            COp.mCellulaire = txtCellulaire.Text.Replace("'", "''").Trim();
                            COp.mCircuit = txtCircuit.Text.Replace("'", "''").Trim();
                            COp.mCodeCircuit = txtCodeCircuit.Text.Replace("'", "''").Trim();
                            COp.mCode = txtCodeSouscripteur.Text.Replace("'", "''").Trim();
                            COp.mCodeDistrict = txtCodeDistrict.Text.Replace("'", "''").Trim();
                            COp.mCodeEgliseLocale = txtCodeEglise.Text.Replace("'", "''").Trim();
                            COp.mDateSouscription = DateTime.Parse(dateSouscription.EditValue.ToString()).Date;
                            COp.mDateNaissance = DateTime.Parse(dateNaissance.EditValue.ToString()).Date;

                            COp.mDistrict = txtDistrict.Text.Replace("'", "''").Trim();
                            COp.mEgliseLocale = txtEglise.Text.Replace("'", "''").Trim();
                            COp.mIdClasseMetho = Int32.Parse(CmbClasseMetho.EditValue.ToString());
                            COp.mIdProfession = Int32.Parse(CmbProfession.EditValue.ToString());
                            COp.mLieuNaissance = txtLieuNaissance.Text.Replace("'", "''").Trim();
                            COp.mEmail = txtEmail.Text.Replace("'", "''").Trim();
                            COp.mNom = txtNom.Text.Replace("'", "''").Trim();
                            COp.mPrenoms = txtPrenoms.Text.Replace("'", "''").Trim();
                            COp.mProfession = CmbProfession.Text.Replace("'", "''").Trim();
                            COp.mSexe = cmbSexe.Text.Replace("'", "''").Trim();
                            COp.mStatutFamilial = cmbStatut.Text.Replace("'", "''").Trim();
                            COp.mTelephone = txtTelephone.Text.Replace("'", "''").Trim();
                            COp.mDateLastModif = DateTime.Now;
                            COp.mDateCreation = DateTime.Now;
                            COp.mUserCreation = Environment.UserDomainName + "\\" + Environment.UserName;
                            COp.mUserLastModif = Environment.UserDomainName + "\\" + Environment.UserName;

                            //Adulte ou enfant Age >=18 Adulte

                            COp.mIsAdulte = CalculAge(COp.mDateNaissance, DateTime.Now);
                            
                            res = daoReport.AddSouscripteur(COp, myObjectChaineConFimeco);

                            if (res)
                            {
                                MessageBox.Show("Souscripteur créé avec succès!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Une erreur est survenue!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        else
                        {
                            //Objet déjà existant

                            MessageBox.Show("Ce souscripteur existe déjà ! Veuillez vérifier vos données", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }


                    }
                    else
                    {
                        //Modification

                        if (!myObjectAjout && myObjectSouscripteur != null)
                        {
                            //Tester qu'on a pas un doublon
                            var IsExist = myObjectListeSouscripteur.Exists(c => c.mCellulaire.ToUpper().Trim() == txtCellulaire.Text.Replace("'", "''").ToUpper().Trim() && c.mCircuit.ToUpper().Trim() == txtCircuit.Text.Replace("'", "''").ToUpper().Trim() && c.mCodeCircuit.ToUpper().Trim() == txtCodeCircuit.Text.Replace("'", "''").ToUpper().Trim() && c.mCode.ToUpper().Trim() == txtCodeSouscripteur.Text.Replace("'", "''").ToUpper().Trim() && c.mCodeDistrict.ToUpper().Trim() == txtCodeDistrict.Text.Replace("'", "''").ToUpper().Trim() && c.mCodeEgliseLocale.ToUpper().Trim() == txtCodeEglise.Text.Replace("'", "''").ToUpper().Trim() && c.mDateSouscription == DateTime.Parse(dateSouscription.EditValue.ToString()) && c.mDateNaissance == DateTime.Parse(dateNaissance.EditValue.ToString()) && c.mDistrict.ToUpper().Trim() == txtDistrict.Text.Replace("'", "''").ToUpper().Trim() && c.mEgliseLocale.ToUpper().Trim() == txtEglise.Text.Replace("'", "''").ToUpper().Trim() && c.mIdClasseMetho == Int32.Parse(CmbClasseMetho.EditValue.ToString()) && c.mLieuNaissance.ToUpper().Trim() == txtLieuNaissance.Text.Replace("'", "''").ToUpper().Trim() && c.mEmail.ToUpper().Trim() == txtEmail.Text.Replace("'", "''").ToUpper().Trim() && c.mNom.ToUpper().Trim() == txtNom.Text.Replace("'", "''").ToUpper().Trim() && c.mPrenoms.ToUpper().Trim() == txtPrenoms.Text.Replace("'", "''").ToUpper().Trim() && c.mIdProfession == Int32.Parse(CmbProfession.EditValue.ToString()) && c.mSexe.ToUpper().Trim() == cmbSexe.Text.Replace("'", "''").ToUpper().Trim() && c.mStatutFamilial.ToUpper().Trim() == cmbStatut.Text.Replace("'", "''").ToUpper().Trim() && c.mTelephone.ToUpper().Trim() == txtTelephone.Text.Replace("'", "''").ToUpper().Trim());

                            if (!IsExist)
                            {

                                myObjectSouscripteur.mCellulaire = txtCellulaire.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mCircuit = txtCircuit.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mCodeCircuit = txtCodeCircuit.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mCode = txtCodeSouscripteur.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mCodeDistrict = txtCodeDistrict.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mCodeEgliseLocale = txtCodeEglise.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mDateSouscription = DateTime.Parse(dateSouscription.EditValue.ToString()).Date;

                                myObjectSouscripteur.mDateNaissance = DateTime.Parse(dateNaissance.EditValue.ToString()).Date;

                                myObjectSouscripteur.mDistrict = txtDistrict.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mEgliseLocale = txtEglise.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mIdClasseMetho = Int32.Parse(CmbClasseMetho.EditValue.ToString());
                                myObjectSouscripteur.mIdProfession = Int32.Parse(CmbProfession.EditValue.ToString());
                                myObjectSouscripteur.mLieuNaissance = txtLieuNaissance.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mEmail = txtEmail.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mNom = txtNom.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mPrenoms = txtPrenoms.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mProfession = CmbProfession.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mSexe = cmbSexe.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mStatutFamilial = cmbStatut.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mTelephone = txtTelephone.Text.Replace("'", "''").Trim();
                                myObjectSouscripteur.mDateLastModif = DateTime.Now;
                                //myObjectSouscripteur.mDateCreation = DateTime.Now;
                                //myObjectSouscripteur.mUserCreation = Environment.UserDomainName + "\\" + Environment.UserName;
                                myObjectSouscripteur.mUserLastModif = Environment.UserDomainName + "\\" + Environment.UserName;

                                //Adulte ou enfant Age >=18 Adulte

                                myObjectSouscripteur.mIsAdulte = CalculAge(myObjectSouscripteur.mDateNaissance, DateTime.Now);

                                res = daoReport.UpdateSouscripteur(myObjectSouscripteur, myObjectChaineConFimeco);

                                if (res)
                                {
                                    MessageBox.Show("Souscripteur  modifiée avec succès!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("Une erreur est survenue!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                            else
                            {
                                //Objet déjà existant

                                MessageBox.Show("Ce Souscripteur existe déjà ! Veuillez vérifier vos données", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }

                        }
                    }
                }
                else
                {
                    //Renseigner Nom ,prenom ,code ,cellulaire obigatoirement

                    MessageBox.Show("Veuillez renseigner obligatoirement le nom,prenoms ,Identifiant et Cellulaire", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }


            }
            catch(Exception ex)
            {
                var msg = "FenSouscripteur -> sBtnEnregistrer_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }



      
        private string CalculAge(DateTime datenaissance,DateTime Mtn)
        {
            string ret = string.Empty;
            try
            {
                if(Mtn> datenaissance)
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
            catch(Exception ex )
            {
                var msg = "FenSouscripteur -> CalculAge-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return ret;
            }
        }


        private void FenSouscripteur_Load(object sender, EventArgs e)
        {
            try
            {
                //Charger Combo des classes metho
                List<CClasseMetho> ListClasseMetho = new List<CClasseMetho>();

                ListClasseMetho = daoReport.GetAllClasseMetho(myObjectChaineConFimeco);

                if(ListClasseMetho.Count>0)
                {
                    //Remplir Combo ClasseMetho
                    CmbClasseMetho.Properties.DataSource = ListClasseMetho;
                    CmbClasseMetho.Properties.DisplayMember = "mNomClasse";
                    CmbClasseMetho.Properties.ValueMember = "mId";
                    //Choisir les premières valeurs
                    if(myObjectAjout) CmbClasseMetho.ItemIndex = 0;
                }

                if (myObjectListeProfession.Count > 0)
                {
                    //Remplir pROFESSION
                    CmbProfession.Properties.DataSource = myObjectListeProfession;
                    CmbProfession.Properties.DisplayMember = "mLibelle";
                    CmbProfession.Properties.ValueMember = "mId";
                    //Choisir les premières valeurs
                    if (myObjectAjout) CmbProfession.ItemIndex = 0;
                }

                //Date souscription 

                dateNaissance.EditValue = DateTime.Now;

                //Date Naissance
                dateSouscription.EditValue = DateTime.Now;

                //sexe

                cmbSexe.SelectedIndex = 0;

                //Statut familiale
                cmbStatut.SelectedIndex = 0;


                if (!myObjectAjout)//Modification
                {

                    txtCellulaire.Text = myObjectSouscripteur.mCellulaire;
                     txtCircuit.Text = myObjectSouscripteur.mCircuit;
                   txtCodeCircuit.Text = myObjectSouscripteur.mCodeCircuit;
                     txtCodeSouscripteur.Text= myObjectSouscripteur.mCode;
                    txtCodeDistrict.Text = myObjectSouscripteur.mCodeDistrict;
                    txtCodeEglise.Text= myObjectSouscripteur.mCodeEgliseLocale;
                    dateSouscription.EditValue= myObjectSouscripteur.mDateSouscription;

                   dateNaissance.EditValue = myObjectSouscripteur.mDateNaissance;

                    txtDistrict.Text = myObjectSouscripteur.mDistrict;
                     txtEglise.Text= myObjectSouscripteur.mEgliseLocale;
                    CmbClasseMetho.EditValue= myObjectSouscripteur.mIdClasseMetho;
                     txtLieuNaissance.Text = myObjectSouscripteur.mLieuNaissance;
                      txtEmail.Text= myObjectSouscripteur.mEmail;
                     txtNom.Text = myObjectSouscripteur.mNom;
                      txtPrenoms.Text= myObjectSouscripteur.mPrenoms;
                      CmbProfession.EditValue= myObjectSouscripteur.mIdProfession;
                      cmbSexe.Text= myObjectSouscripteur.mSexe;
                     cmbStatut.Text = myObjectSouscripteur.mStatutFamilial;
                     txtTelephone.Text = myObjectSouscripteur.mTelephone;

                }

            }
            catch(Exception ex)
            {
                var msg = "FenSouscripteur -> FenSouscripteur_Load-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
            
        }
    }
}
