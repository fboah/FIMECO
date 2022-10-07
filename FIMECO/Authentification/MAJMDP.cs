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

namespace FIMECO.Authentification
{
    public partial class MAJMDP : Form
    {

        private CUser myObjectUser;
        private string myObjectChaineConnex;

        private string Appli = "VISIONPLUS";
        

        private readonly DAOFimeco mDao = new DAOFimeco();


        public MAJMDP(CUser User, string ChaineConnex)
        {
            InitializeComponent();

            this.myObjectChaineConnex = ChaineConnex;
            this.myObjectUser = User;
        }

        //public MAJMDP()
        //{
        //    InitializeComponent();
        //}

        private void sBtnValider_Click(object sender, EventArgs e)
        {

            bool rep = false;
            try
            {
                if (txtMotPasse.Text == txtMotPasseConfirm.Text)
                {
                    if (txtMotPasse.Text.ToUpper().Trim() == "2022Jourdain")
                    {
                        //Choisir un autre mot de passe 
                        MessageBox.Show("Veuillez renseigner un autre mot de passe!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        //Nouveau MDP

                        myObjectUser.mPassword = mDao.Encrypt(txtMotPasse.Text);

                        rep = mDao.updateUser(myObjectUser, myObjectChaineConnex);

                        if (rep)
                        {
                            MessageBox.Show("Votre Mot de passe a été changé!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Une erreur est survenue lors du changement du mot de passe!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        this.Close();

                    }
                }
                else
                {
                    //Les deux mots de passe ne concordent pas

                    MessageBox.Show("Les mots de passe ne sont pas les mêmes!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception ex)
            {
                var msg = "MAJMDP -> sBtnValider_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }

        private void txtMotPasse_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                bool rep = false;
                try
                {
                    if (txtMotPasse.Text == txtMotPasseConfirm.Text)
                    {
                        if (txtMotPasse.Text.ToUpper().Trim() == "2022Jourdain")
                        {
                            //Choisir un autre mot de passe 
                            MessageBox.Show("Veuillez renseigner un autre mot de passe!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                        else
                        {
                            //Nouveau MDP

                            myObjectUser.mPassword = mDao.Encrypt(txtMotPasse.Text);

                            rep = mDao.updateUser(myObjectUser, myObjectChaineConnex);

                            if (rep)
                            {
                                MessageBox.Show("Votre Mot de passe a été changé!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Une erreur est survenue lors du changement du mot de passe!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            this.Close();

                        }
                    }
                    else
                    {
                        //Les deux mots de passe ne concordent pas

                        MessageBox.Show("Les mots de passe ne sont pas les mêmes!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
                catch (Exception ex)
                {
                    var msg = "MAJMDP -> txtMotPasse_KeyPress-> TypeErreur: " + ex.Message;
                    CAlias.Log(msg);
                }
            }
        }

        private void txtMotPasseConfirm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                bool rep = false;
                try
                {
                    if (txtMotPasse.Text == txtMotPasseConfirm.Text)
                    {
                        if (txtMotPasse.Text.ToUpper().Trim() == "2022Jourdain")
                        {
                            //Choisir un autre mot de passe 
                            MessageBox.Show("Veuillez renseigner un autre mot de passe!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                        else
                        {
                            //Nouveau MDP

                            myObjectUser.mPassword = mDao.Encrypt(txtMotPasse.Text);

                            rep = mDao.updateUser(myObjectUser, myObjectChaineConnex);

                            if (rep)
                            {
                                MessageBox.Show("Votre Mot de passe a été changé!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Une erreur est survenue lors du changement du mot de passe!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            this.Close();

                        }
                    }
                    else
                    {
                        //Les deux mots de passe ne concordent pas

                        MessageBox.Show("Les mots de passe ne sont pas les mêmes!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
                catch (Exception ex)
                {
                    var msg = "MAJMDP -> txtMotPasseConfirm_KeyPress-> TypeErreur: " + ex.Message;
                    CAlias.Log(msg);
                }
            }
            }
    }
}
