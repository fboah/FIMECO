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
    public partial class Login : Form
    {
        private string Appli = "FIMECO";

        string MDPInit = "2022Jourdain";
       // string MDPInit = "2022Aitek";


        private List<CUserProfilData> ListeUserProfil;

        //   private string Chaine = @"Initial Catalog=AITSOFTWARE;Data Source=FRANCK\SAGE300;Integrated Security=SSPI";
        private string Chaine;
     
        public string LoginTAL;

        private bool IsCandidat;

        private readonly DAOFimeco mDao = new DAOFimeco();

        private readonly CAlias daoMain = new CAlias();


        public Login()
        {
            InitializeComponent();
        }

        private void sBtnFermer_Click(object sender, EventArgs e)
        {
            try
            {
                var rep = MessageBox.Show("Voulez-vous fermer le logiciel ?", Appli, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rep != DialogResult.Yes) return;

                Application.Exit();
            }
            catch (Exception ex)
            {
                var msg = "Login -> sBtnFermer_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }

        private void sBtnValider_Click(object sender, EventArgs e)
        {
            try
            {
                //    string enc = mDao.Encrypt("DUBAB1987", true);

                ListeUserProfil = new List<CUserProfilData>();

                ListeUserProfil = mDao.getUserProfilData(Chaine);

                var TrouveListeUserProfil = ListeUserProfil.Where(c => c.mLogin == txtUtilisateur.Text && c.mPassword == mDao.Encrypt(txtMotPasse.Text)).ToList();

                if (txtMotPasse.Text.ToUpper() == MDPInit.ToUpper())
                {
                    //Premiere connexion ,Définir les mots de passe

                    if (TrouveListeUserProfil != null && TrouveListeUserProfil.Count > 0)
                    {
                        var util = TrouveListeUserProfil.FirstOrDefault();

                        CUser Us = new CUser();
                        Us.mId = util.mId;
                        Us.mLogin = util.mLogin;
                        Us.mEmail = util.mEmail;
                        Us.mNom = util.mNom;
                        Us.mPassword = util.mPassword;

                        var FenMAJMDP = new MAJMDP(Us, Chaine);

                        FenMAJMDP.ShowDialog();

                        this.Hide();

                        //Ouverture fenêtre principale ,MAJ des listes dans le Load de l'appli,
                        //Faire passer le login 
                        LoginTAL = txtUtilisateur.Text;

                        IsCandidat = false;


                        //Vérifier si on doit choisir une application(l'utilisateur à accès aux 2 applis)

                        var LIsExistChoixAppli = TrouveListeUserProfil.Where(c => c.mIdProfil == 4 || c.mIdProfil == 5).ToList();

                        if (LIsExistChoixAppli.Count > 1)
                        {
                            FenChoixAppli fenetreB = new FenChoixAppli();
                            fenetreB.ShowDialog();
                            int IdTypeAppli = fenetreB.idAppli;

                            //var CTypeAppli = LIsExistChoixAppli.FirstOrDefault();

                            //if (CTypeAppli.mIdProfil == 4) IdTypeAppli = 1;
                            //if (CTypeAppli.mIdProfil == 5) IdTypeAppli = 2;

                            var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, util.mId, Chaine, false, IdTypeAppli);

                            FenPrincipale.ShowDialog();
                        }
                        else
                        {
                            var CTypeAppli = LIsExistChoixAppli.FirstOrDefault();

                            int IdTypeAppli = 0;

                            if (CTypeAppli.mIdProfil == 4) IdTypeAppli = 1;
                            if (CTypeAppli.mIdProfil == 5) IdTypeAppli = 2;

                            var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, util.mId, Chaine, false, IdTypeAppli);

                            FenPrincipale.ShowDialog();
                        }
                        

                    }
                    else
                    {
                        //Erreur Authentification
                        MessageBox.Show("Erreur d'authentification!" + Environment.NewLine + "Veuillez revoir votre login et/ou mot de passe", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }

                }
                else
                {
                    if (TrouveListeUserProfil != null && TrouveListeUserProfil.Count > 0)
                    {
                        this.Hide();

                        var util = TrouveListeUserProfil.FirstOrDefault();

                        IsCandidat = false;

                        //Faire passer le login 
                        LoginTAL = txtUtilisateur.Text;


                        //Vérifier si on doit choisir une application(l'utilisateur à accès aux 2 applis)

                        var LIsExistChoixAppli = TrouveListeUserProfil.Where(c => c.mIdProfil == 4 || c.mIdProfil == 5).ToList();

                        if (LIsExistChoixAppli.Count > 1)
                        {
                            FenChoixAppli fenetreB = new FenChoixAppli();
                            fenetreB.ShowDialog();
                            int IdTypeAppli = fenetreB.idAppli;

                            //var CTypeAppli = LIsExistChoixAppli.FirstOrDefault();

                            //if (CTypeAppli.mIdProfil == 4) IdTypeAppli = 1;
                            //if (CTypeAppli.mIdProfil == 5) IdTypeAppli = 2;

                            var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, util.mId, Chaine, false, IdTypeAppli);

                            FenPrincipale.ShowDialog();
                        }
                        else
                        {
                            var CTypeAppli = LIsExistChoixAppli.FirstOrDefault();

                            int IdTypeAppli = 0;

                            if (CTypeAppli.mIdProfil == 4) IdTypeAppli = 1;
                            if (CTypeAppli.mIdProfil == 5) IdTypeAppli = 2;

                            var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, util.mId, Chaine, false, IdTypeAppli);

                            FenPrincipale.ShowDialog();
                        }
                        
                    }
                    else
                    {
                        //Erreur Authentification
                        MessageBox.Show("Erreur d'authentification!" + Environment.NewLine + "Veuillez revoir votre login et/ou mot de passe", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
            }
            catch (Exception ex)
            {
                var msg = "Login -> sBtnValider_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //Chaine Connexion AITSOFTWARE
            List<CAlias> ListChaineAIT = new List<CAlias>();

            ListChaineAIT = daoMain.GetAliasFIMECO();

            var ChooseAIT = ListChaineAIT.FirstOrDefault(c => c.IsAbidjan == true);

            if (ChooseAIT != null)
            {
                Chaine = ChooseAIT.mAliasName;
            }

            


        }

        private void txtUtilisateur_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    ListeUserProfil = new List<CUserProfilData>();

                    ListeUserProfil = mDao.getUserProfilData(Chaine);

                    var TrouveListeUserProfil = ListeUserProfil.Where(c => c.mLogin == txtUtilisateur.Text && c.mPassword == mDao.Encrypt(txtMotPasse.Text)).ToList();

                    if (txtMotPasse.Text.ToUpper() == MDPInit.ToUpper())
                    {
                        //Premiere connexion ,Définir les mots de passe

                        if (TrouveListeUserProfil != null && TrouveListeUserProfil.Count > 0)
                        {
                            var util = TrouveListeUserProfil.FirstOrDefault();

                            CUser Us = new CUser();
                            Us.mId = util.mId;
                            Us.mLogin = util.mLogin;
                            Us.mEmail = util.mEmail;
                            Us.mNom = util.mNom;
                            Us.mPassword = util.mPassword;


                            var FenMAJMDP = new MAJMDP(Us, Chaine);

                            FenMAJMDP.ShowDialog();

                            this.Hide();

                            //Ouverture fenêtre principale ,MAJ des listes dans le Load de l'appli,
                            //Faire passer le login 
                            LoginTAL = txtUtilisateur.Text;
                            
                            //Vérifier si on doit choisir une application(l'utilisateur à accès aux 2 applis)

                            var LIsExistChoixAppli = TrouveListeUserProfil.Where(c => c.mIdProfil == 4 || c.mIdProfil == 5).ToList();

                            if (LIsExistChoixAppli.Count > 1)
                            {
                                FenChoixAppli fenetreB = new FenChoixAppli();
                                fenetreB.ShowDialog();
                                int IdTypeAppli = fenetreB.idAppli;

                                //var CTypeAppli = LIsExistChoixAppli.FirstOrDefault();

                                //if (CTypeAppli.mIdProfil == 4) IdTypeAppli = 1;
                                //if (CTypeAppli.mIdProfil == 5) IdTypeAppli = 2;

                                var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, util.mId, Chaine, false, IdTypeAppli);

                                FenPrincipale.ShowDialog();
                            }
                            else
                            {
                                var CTypeAppli = LIsExistChoixAppli.FirstOrDefault();

                                int IdTypeAppli = 0;

                                if (CTypeAppli.mIdProfil == 4) IdTypeAppli = 1;
                                if (CTypeAppli.mIdProfil == 5) IdTypeAppli = 2;

                                var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, util.mId, Chaine, false, IdTypeAppli);

                                FenPrincipale.ShowDialog();
                            }


                            //var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, Us.mId, Chaine, false);

                            //FenPrincipale.ShowDialog();

                        }
                        else
                        {
                            //Erreur Authentification
                            MessageBox.Show("Erreur d'authentification!" + Environment.NewLine + "Veuillez revoir votre login et/ou mot de passe", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }

                    }
                    else
                    {
                        if (TrouveListeUserProfil != null && TrouveListeUserProfil.Count > 0)
                        {
                            this.Hide();

                            var util = TrouveListeUserProfil.FirstOrDefault();

                            //Faire passer le login 
                            LoginTAL = txtUtilisateur.Text;

                            //Vérifier si on doit choisir une application(l'utilisateur à accès aux 2 applis)

                            var LIsExistChoixAppli = TrouveListeUserProfil.Where(c => c.mIdProfil == 4 || c.mIdProfil == 5).ToList();

                            if(LIsExistChoixAppli.Count>1)
                            {
                                FenChoixAppli fenetreB = new FenChoixAppli();
                                fenetreB.ShowDialog();
                                int IdTypeAppli = fenetreB.idAppli;

                                //var CTypeAppli = LIsExistChoixAppli.FirstOrDefault();

                                //if (CTypeAppli.mIdProfil == 4) IdTypeAppli = 1;
                                //if (CTypeAppli.mIdProfil == 5) IdTypeAppli = 2;

                                var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, util.mId, Chaine, false, IdTypeAppli);

                                FenPrincipale.ShowDialog();
                            }
                            else
                            {
                                var CTypeAppli = LIsExistChoixAppli.FirstOrDefault();

                                int IdTypeAppli = 0;

                                if (CTypeAppli.mIdProfil == 4) IdTypeAppli = 1;
                                if (CTypeAppli.mIdProfil == 5) IdTypeAppli = 2;
                                
                                var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, util.mId, Chaine, false, IdTypeAppli);

                                FenPrincipale.ShowDialog();
                            }

                        }
                        else
                        {
                            //Erreur Authentification
                            MessageBox.Show("Erreur d'authentification!" + Environment.NewLine + "Veuillez revoir votre login et/ou mot de passe", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = "Login -> txtUtilisateur_KeyPress-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }



        private void txtMotPasse_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    ListeUserProfil = new List<CUserProfilData>();

                    ListeUserProfil = mDao.getUserProfilData(Chaine);

                    var TrouveListeUserProfil = ListeUserProfil.Where(c => c.mLogin == txtUtilisateur.Text && c.mPassword == mDao.Encrypt(txtMotPasse.Text)).ToList();

                    if (txtMotPasse.Text.ToUpper() == MDPInit.ToUpper())
                    {
                        //Premiere connexion ,Définir les mots de passe

                        if (TrouveListeUserProfil != null && TrouveListeUserProfil.Count > 0)
                        {
                            var util = TrouveListeUserProfil.FirstOrDefault();

                            CUser Us = new CUser();
                            Us.mId = util.mId;
                            Us.mLogin = util.mLogin;
                            Us.mEmail = util.mEmail;
                            Us.mNom = util.mNom;
                            Us.mPassword = util.mPassword;


                            var FenMAJMDP = new MAJMDP(Us, Chaine);

                            FenMAJMDP.ShowDialog();

                            this.Hide();

                            //Ouverture fenêtre principale ,MAJ des listes dans le Load de l'appli,
                            //Faire passer le login 
                            LoginTAL = txtUtilisateur.Text;


                            //Vérifier si on doit choisir une application(l'utilisateur à accès aux 2 applis)

                            var LIsExistChoixAppli = TrouveListeUserProfil.Where(c => c.mIdProfil == 4 || c.mIdProfil == 5).ToList();

                            if (LIsExistChoixAppli.Count > 1)
                            {
                                FenChoixAppli fenetreB = new FenChoixAppli();
                                fenetreB.ShowDialog();
                                int IdTypeAppli = fenetreB.idAppli;

                                //var CTypeAppli = LIsExistChoixAppli.FirstOrDefault();

                                //if (CTypeAppli.mIdProfil == 4) IdTypeAppli = 1;
                                //if (CTypeAppli.mIdProfil == 5) IdTypeAppli = 2;

                                var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, util.mId, Chaine, false, IdTypeAppli);

                                FenPrincipale.ShowDialog();
                            }
                            else
                            {
                                var CTypeAppli = LIsExistChoixAppli.FirstOrDefault();

                                int IdTypeAppli = 0;

                                if (CTypeAppli.mIdProfil == 4) IdTypeAppli = 1;
                                if (CTypeAppli.mIdProfil == 5) IdTypeAppli = 2;

                                var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, util.mId, Chaine, false, IdTypeAppli);

                                FenPrincipale.ShowDialog();
                            }


                            //var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, Us.mId, Chaine, false);

                            //FenPrincipale.ShowDialog();

                        }
                        else
                        {
                            //Erreur Authentification
                            MessageBox.Show("Erreur d'authentification!" + Environment.NewLine + "Veuillez revoir votre login et/ou mot de passe", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }

                    }
                    else
                    {
                        if (TrouveListeUserProfil != null && TrouveListeUserProfil.Count > 0)
                        {
                            this.Hide();

                            var util = TrouveListeUserProfil.FirstOrDefault();

                            //Faire passer le login 
                            LoginTAL = txtUtilisateur.Text;
                            
                            //Vérifier si on doit choisir une application(l'utilisateur à accès aux 2 applis)

                            var LIsExistChoixAppli = TrouveListeUserProfil.Where(c => c.mIdProfil == 4 || c.mIdProfil == 5).ToList();

                            if (LIsExistChoixAppli.Count > 1)
                            {
                                FenChoixAppli fenetreB = new FenChoixAppli();
                                fenetreB.ShowDialog();
                                int IdTypeAppli = fenetreB.idAppli;

                                //var CTypeAppli = LIsExistChoixAppli.FirstOrDefault();

                                //if (CTypeAppli.mIdProfil == 4) IdTypeAppli = 1;
                                //if (CTypeAppli.mIdProfil == 5) IdTypeAppli = 2;

                                var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, util.mId, Chaine, false, IdTypeAppli);

                                FenPrincipale.ShowDialog();
                            }
                            else
                            {
                                var CTypeAppli = LIsExistChoixAppli.FirstOrDefault();

                                int IdTypeAppli = 0;

                                if (CTypeAppli.mIdProfil == 4) IdTypeAppli = 1;
                                if (CTypeAppli.mIdProfil == 5) IdTypeAppli = 2;


                                 var FenPrincipale = new MainForm(TrouveListeUserProfil, LoginTAL, util.mId, Chaine, false, IdTypeAppli);

                                FenPrincipale.ShowDialog();
                            }

                            
                        }
                        else
                        {
                            //Erreur Authentification
                            MessageBox.Show("Erreur d'authentification!" + Environment.NewLine + "Veuillez revoir votre login et/ou mot de passe", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = "Login -> txtMotPasse_KeyPress-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }
    }
}
