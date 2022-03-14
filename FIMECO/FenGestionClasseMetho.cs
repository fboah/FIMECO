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
    public partial class FenGestionClasseMetho : Form
    {
        private bool myObjectAjout;
        private readonly DAOFimeco daoReport = new DAOFimeco();
        private string myObjectChaineConFimeco;
        private List<CClasseMetho> myObjectListeClasseMetho;
        private CClasseMetho myObjectClasseMetho;

        public FenGestionClasseMetho()
        {
            InitializeComponent();
        }

        public FenGestionClasseMetho(bool IsAjout, List<CClasseMetho> ListeClasseMetho, CClasseMetho CMetho,string chainefimeco)
        {
            InitializeComponent();

            this.myObjectAjout = IsAjout;
            this.myObjectListeClasseMetho = ListeClasseMetho;
            this.myObjectClasseMetho = CMetho;
            this.myObjectChaineConFimeco = chainefimeco;

        }

        private void sBtnEnregistrer_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                //Renseigner au moins le nomClasse,quartier ,Nom et prenoms et Tel du conduct1 au moins

                if(txtNomClasse.Text.Trim()!=string.Empty || txtNomConducteur1.Text.Trim() != string.Empty || txtPrenomsConducteur1.Text.Trim()!=string.Empty || txtQuartier.Text.Trim()!=string.Empty || txtTelConducteur1.Text.Trim()!=string.Empty)
                {
                    if (myObjectAjout && myObjectClasseMetho == null)
                    {
                        //Tester qu'on a pas un doublon
                        var IsExist = myObjectListeClasseMetho.Exists(c => c.mNomClasse.ToUpper().Trim() == txtNomClasse.Text.Replace("'", "''").ToUpper().Trim());

                        if (!IsExist)
                        {
                            CClasseMetho COp = new CClasseMetho();
                            COp.mNomClasse = txtNomClasse.Text.Replace("'", "''").Trim();
                            COp.mNomConducteur1 = txtNomConducteur1.Text.Replace("'", "''").Trim();
                            COp.mPrenomConducteur1 = txtPrenomsConducteur1.Text.Replace("'", "''").Trim();
                            COp.mEmailConducteur1 = txtEmailConducteur1.Text.Replace("'", "''").Trim();
                            COp.mTelephoneConducteur1 = txtTelConducteur1.Text.Replace("'", "''").Trim();
                            COp.mNomConducteur2 = txtNomConducteur2.Text.Replace("'", "''").Trim();
                            COp.mPrenomConducteur2 = txtPrenomsConducteur2.Text.Replace("'", "''").Trim();
                            COp.mEmailConducteur2 = txtEmailConducteur2.Text.Replace("'", "''").Trim();
                            COp.mTelephoneConducteur2 = txtTelConducteur2.Text.Replace("'", "''").Trim();
                            COp.mQuartier = txtQuartier.Text.Replace("'", "''").Trim();

                            res = daoReport.AddClasseMetho(COp, myObjectChaineConFimeco);

                            if (res)
                            {
                                MessageBox.Show("Classe ajoutée avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                            MessageBox.Show("Cette Classe existe déjà ! Veuillez vérifier vos données", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }


                    }
                    else
                    {
                        //Modification

                        if (!myObjectAjout && myObjectClasseMetho != null)
                        {
                            //Tester qu'on a pas un doublon
                            var IsExist = myObjectListeClasseMetho.Exists(c => c.mNomClasse.ToUpper().Trim() == txtNomClasse.Text.ToUpper().Trim() && c.mNomConducteur1.ToUpper().Trim() == txtNomConducteur1.Text.ToUpper().Trim() && c.mPrenomConducteur1.ToUpper().Trim() == txtPrenomsConducteur1.Text.ToUpper().Trim() && c.mEmailConducteur1.ToUpper().Trim() == txtEmailConducteur1.Text.ToUpper().Trim() && c.mTelephoneConducteur1.ToUpper().Trim() == txtTelConducteur1.Text.ToUpper().Trim() && c.mNomConducteur2.ToUpper().Trim() == txtNomConducteur2.Text.ToUpper().Trim() && c.mPrenomConducteur2.ToUpper().Trim() == txtPrenomsConducteur2.Text.ToUpper().Trim() && c.mEmailConducteur2.ToUpper().Trim() == txtEmailConducteur2.Text.ToUpper().Trim() && c.mTelephoneConducteur2.ToUpper().Trim() == txtTelConducteur2.Text.ToUpper().Trim() && c.mQuartier.ToUpper().Trim() == txtQuartier.Text.ToUpper().Trim());

                            if (!IsExist)
                            {

                                myObjectClasseMetho.mNomClasse = txtNomClasse.Text.Replace("'", "''").Trim();
                                myObjectClasseMetho.mNomConducteur1 = txtNomConducteur1.Text.Replace("'", "''").Trim();
                                myObjectClasseMetho.mPrenomConducteur1 = txtPrenomsConducteur1.Text.Replace("'", "''").Trim();
                                myObjectClasseMetho.mEmailConducteur1 = txtEmailConducteur1.Text.Replace("'", "''").Trim();
                                myObjectClasseMetho.mTelephoneConducteur1 = txtTelConducteur1.Text.Replace("'", "''").Trim();
                                myObjectClasseMetho.mNomConducteur2 = txtNomConducteur2.Text.Replace("'", "''").Trim();
                                myObjectClasseMetho.mPrenomConducteur2 = txtPrenomsConducteur2.Text.Replace("'", "''").Trim();
                                myObjectClasseMetho.mEmailConducteur2 = txtEmailConducteur2.Text.Replace("'", "''").Trim();
                                myObjectClasseMetho.mTelephoneConducteur2 = txtTelConducteur2.Text.Replace("'", "''").Trim();
                                myObjectClasseMetho.mQuartier = txtQuartier.Text.Replace("'", "''").Trim();

                                res = daoReport.UpdateClasseMetho(myObjectClasseMetho, myObjectChaineConFimeco);

                                if (res)
                                {
                                    MessageBox.Show("Classe modifiée avec succès!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                                MessageBox.Show("Cette Classe existe déjà ! Veuillez vérifier vos données", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }

                        }
                    }
                }
                else
                {
                    //Renseigner au moins ces éléments

                    MessageBox.Show("Veuillez renseigner au moins le nom de la classe ,le quartier ,le nom ,prenom et telephone du conducteur 1!", "FIMECO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

            }
            catch(Exception ex)
            {

            }
        }

        private void FenGestionClasseMetho_Load(object sender, EventArgs e)
        {
            try
            {
                if (!myObjectAjout)//Modification
                {
                    
                    txtNomClasse.Text = myObjectClasseMetho.mNomClasse;
                    txtNomConducteur1.Text = myObjectClasseMetho.mNomConducteur1;
                   txtPrenomsConducteur1.Text = myObjectClasseMetho.mPrenomConducteur1;
                     txtEmailConducteur1.Text = myObjectClasseMetho.mEmailConducteur1;
                     txtTelConducteur1.Text = myObjectClasseMetho.mTelephoneConducteur1;
                   txtNomConducteur2.Text = myObjectClasseMetho.mNomConducteur2;
                   txtPrenomsConducteur2.Text = myObjectClasseMetho.mPrenomConducteur2;
                    txtEmailConducteur2.Text = myObjectClasseMetho.mEmailConducteur2;
                    txtTelConducteur2.Text = myObjectClasseMetho.mTelephoneConducteur2;
                    txtQuartier.Text = myObjectClasseMetho.mQuartier;

                }

            }
            catch(Exception ex)
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
    }
}
