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
using DevExpress.XtraTreeList.Nodes;

namespace FIMECO
{
    public partial class FenGestProfil : Form
    {
        public string MDPReinit = "2022Jourdain";

        private string Appli = "VISIONPLUS";

        ////  private string Chaine = @"Initial Catalog=AITSOFTWARE;Data Source=FRANCKBOAH-PC\SAGE300;Integrated Security=SSPI";
        private string Chaine;
        //  private string Chaine = @"Initial Catalog=AITSOFTWARE;Data Source=FRANCK\SAGE200;Integrated Security=SSPI";
        //// private string Chaine = @"Initial Catalog=AITSOFTWARE;Data Source=AITSQL001\SAGE100C;user=SA;password=$AGE100";

        private readonly DAOFimeco mDao = new DAOFimeco();

        private List<CProfil> ListeProfil = new List<CProfil>();

        private ListView.SelectedListViewItemCollection _itemselected;

        private TreeListNode _node;

        private List<CUserProfilData> _listUser;

        private List<CUserProfilData> ListeUserProfilData;

        private int IdUtilisateurSelect;

        private ListViewItem _listeviewitem3;
        private int _listviewIndiceSelect = 0;

        public FenGestProfil(string CH)
        {
            InitializeComponent();

            this.Chaine = CH;
        }


      
        public bool RemplirUser()
        {
            bool errorMsg = false;
            try
            {
                listViewUsers.Items.Clear();
                _listUser = mDao.getUserProfilData(Chaine);
                if (_listUser != null && _listUser.Count > 0)
                {
                    var ListeUtilisateur = _listUser.Select(c => c.mLogin).Distinct();

                    foreach (var item in ListeUtilisateur)
                    {
                        _listeviewitem3 = new ListViewItem(new string[] { "", item }, -1) { StateImageIndex = 0 };
                        this.listViewUsers.Items.AddRange(new ListViewItem[] { _listeviewitem3 });

                    }

                    errorMsg = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "FenGestProfil -> RemplirUser-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                errorMsg = false;
            }

            return errorMsg;
        }

    

        public void GetProfilListTreeNodes(List<CProfil> Lprof, List<CUserProfilData> LUprofdata, int IdUser)
        {
            try
            {
                //Cas où le user n'a pas de role 

                if (LUprofdata != null && LUprofdata.Count > 0)
                {
                    var ListeRoleUser = LUprofdata.Where(c => c.mId == IdUser).ToList();

                    treeListDroitUser.ClearNodes();

                    foreach (var item in Lprof)
                    {
                        //  CuRole newNode = new CuRole();

                        _node = treeListDroitUser.Nodes.Add(new object[] { item.mDescription });

                        //Voir si l'utilisateur a le droit(Par defaut les nodes ont la valeur 0)

                        var trouve = ListeRoleUser.Exists(c => c.mIdProfil == _node.Id + 1);

                        if (!trouve)
                        {
                            _node.Checked = false;
                        }
                        else
                        {
                            _node.Checked = true;
                        }

                        //newNode = new CuRole(item.mId.ToString(), _node, item.mDescription.ToString());

                        //newNode.Nodex.Checked = _node.Checked;

                        //LRole.Add(newNode);

                    }
                }
                else
                {
                    //PAs de role ,on ne coche rien 

                    treeListDroitUser.ClearNodes();

                    foreach (var item in Lprof)
                    {
                        _node = treeListDroitUser.Nodes.Add(new object[] { item.mDescription });

                        _node.Checked = false;

                    }

                }

                //return LRole;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "FenGestProfil -> GetProfilListTreeNodes-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }

        }

      
        public void SelectionUser()
        {
            try
            {
                _itemselected = listViewUsers.SelectedItems;
                var h = listViewUsers.SelectedIndices[0];
                _listviewIndiceSelect = h;
                txtLogin.Text = _itemselected[0].SubItems[1].Text.ToLower();
                var last = _listUser.LastOrDefault(c => c.mLogin.ToLower() == txtLogin.Text.ToLower());
                if (last != null)
                {
                    txtNom.Text = last.mNom;
                    txtLogin.Text = last.mLogin;
                    txtPassword.Text = last.mPassword;
                    txtEmail.Text = last.mEmail;

                    IdUtilisateurSelect = last.mId;
                }

                GetProfilListTreeNodes(ListeProfil, _listUser, last.mId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "FenGestProfil -> SelectionUser-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }

        }


      
        private void sBtnValider_Click(object sender, EventArgs e)
        {
            List<CProfil> ListeProfilMAJ = new List<CProfil>();

            try
            {
                //Récupérer les Profil cocher et faire des mises à jour 

                foreach (TreeListNode nd in treeListDroitUser.Nodes)
                {
                    if (nd.Checked == true)
                    {
                        CProfil CP = new CProfil();

                        CP.mId = nd.Id + 1;

                        ListeProfilMAJ.Add(CP);

                    }

                }

                //On peut pas avoir 2 profil ou plus ,seulement 1 seul 

                if (ListeProfilMAJ.Count > 1)
                {
                    //On a choisi aucune appli dans la liste des droits(ni 4 ni 5)

                    bool find4 = false;
                    bool find5 = false;

                    foreach(var elt in ListeProfilMAJ)
                    {
                        if (elt.mId == 4) find4 = true;
                        if (elt.mId == 5) find5 = true;
                    }
                    
                    if(!find4 && !find5)
                    {
                        MessageBox.Show("On doit OBLIGATOIREMENT donner accès à une Application!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    //Empecher plusieurs droits users

                    bool find1 = false;
                    bool find2 = false;
                    bool find3 = false;

                    foreach(var obj in ListeProfilMAJ)
                    {
                        if (obj.mId == 1) find1 = true;
                        if (obj.mId == 2) find2 = true;
                        if (obj.mId == 3) find3 = true;
                    }
                    
                    if((find1 && find2)||(find1 && find3) ||(find3 && find2))
                    {
                        MessageBox.Show("On ne peut pas avoir plus que 1 profil!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //CAS ou on a rien choisi
                    if (!find1 && !find2 && !find3)
                    {
                        MessageBox.Show("Veuillez choisir 1 profil!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                else
                {
                    //S'assurer qu'on a choisi 1 profil et au moins une appli
                    MessageBox.Show("Veuillez choisir 1 profil et au moins 1 Application!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (ListeProfilMAJ.Count > 0)
                {
                    var ListeIdProf = ListeProfilMAJ.Select(x => x.mId).Distinct().ToList();

                    bool rep = false;

                    rep = mDao.deleteUserProfil(IdUtilisateurSelect, Chaine);

                    //Ajouter UserProfil

                    if (rep)
                    {
                        bool res = false;

                        res = mDao.addUserProfil(IdUtilisateurSelect,  ListeIdProf, Chaine);
                        if (res)
                        {
                            MessageBox.Show("Profils créés avec succès!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Une erreur est survenue lors de la création des profils!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else
                {
                    //Aucun Profil à Ajouter
                    MessageBox.Show("Aucun profil sélectionné!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //MAJ des listes ===================================================

                //Liste des profils

                ListeProfil = mDao.getProfils(Chaine);

                //Liste UserProfilData

                ListeUserProfilData = new List<CUserProfilData>();

                ListeUserProfilData = mDao.getUserProfilData(Chaine);

                _listUser = mDao.getUserProfilData(Chaine);

                bool IsBienRempli = false;

                IsBienRempli = RemplirUser();

                listViewUsers.Items[0].Selected = true;

                SelectionUser();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "FenGestProfil -> sBtnValider_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }

        private void sBtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "FenGestProfil -> sBtnCancel_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }


       
        private void sBtnReinitialiser_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                if (IdUtilisateurSelect > 0)
                {
                    var rep = MessageBox.Show("Confirmez-vous la réinitialisation du mot de passe de l'utilisateur " + txtLogin.Text + "?", Appli, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rep != DialogResult.Yes) return;

                    var trouve = _listUser.FirstOrDefault(c => c.mId == IdUtilisateurSelect);

                    CUser User = new CUser();

                    User.mId = trouve.mId;
                    User.mNom = trouve.mNom;
                    User.mLogin = trouve.mLogin;
                    User.mPassword = mDao.Encrypt(MDPReinit);
                    User.mEmail = trouve.mEmail;


                    res = mDao.updateUser(User, Chaine);

                    if (res)
                    {
                        MessageBox.Show("Mot de passe réinitialisé avec succès!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        MessageBox.Show("Une erreur est survenue lors de la mise à jour du mot de passe!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //MAJ des listes =================================================

                    //Liste des profils

                    ListeProfil = mDao.getProfils(Chaine);

                    //Liste UserProfilData

                    ListeUserProfilData = new List<CUserProfilData>();

                    ListeUserProfilData = mDao.getUserProfilData(Chaine);

                    _listUser = mDao.getUserProfilData(Chaine);

                    bool IsBienRempli = false;

                    IsBienRempli = RemplirUser();

                    listViewUsers.Items[0].Selected = true;

                    SelectionUser();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "FenGestProfil -> sBtnReinitialiser_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);

            }
        }


       
        private void sBtnEnregistrerUser_Click(object sender, EventArgs e)
        {
            try
            {
                List<CUser> ListeUtilisateur = new List<CUser>();

                ListeUtilisateur = mDao.getAllUsers(Chaine);

                var trouve = ListeUtilisateur.FirstOrDefault(c => c.mLogin.ToUpper() == txtLogin.Text.ToUpper());

                if (trouve != null)
                {
                    //Informer que l'utilisateur existe déjà donc ce sera une mise à jour

                    var rep = MessageBox.Show("Cet Utilisateur existe déjà. Les informations seront donc mises à jour" + Environment.NewLine + "Confirmez-vous ce choix?", Appli, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rep != DialogResult.Yes) return;

                    //Maj
                    trouve.mEmail = txtEmail.Text;
                    trouve.mNom = txtNom.Text;
                    trouve.mLogin = txtLogin.Text;
                    trouve.mPassword = mDao.Encrypt(txtPassword.Text);

                    bool res = false;
                    res = mDao.updateUser(trouve, Chaine);

                    if (res)
                    {
                        MessageBox.Show("Utilisateur mis à jour!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Une erreur est survenue lors de la mise à jour!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    CUser Ut = new CUser();

                    Ut.mEmail = txtEmail.Text;
                    Ut.mNom = txtNom.Text;
                    Ut.mLogin = txtLogin.Text;
                    Ut.mPassword = mDao.Encrypt(txtPassword.Text);
                    Ut.mIsDelete = 0;
                    //Ajout
                    bool res = false;
                    res = mDao.addUser(Ut, Chaine);

                    if (res)
                    {
                        MessageBox.Show("Utilisateur ajouté avec succès!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Une erreur est survenue lors de la mise à jour!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //MAJ des listes 

                //Liste des profils

                ListeProfil = mDao.getProfils(Chaine);

                //Liste UserProfilData

                ListeUserProfilData = new List<CUserProfilData>();

                ListeUserProfilData = mDao.getUserProfilData(Chaine);

                _listUser = mDao.getUserProfilData(Chaine);

                bool IsBienRempli = false;

                IsBienRempli = RemplirUser();

                listViewUsers.Items[0].Selected = true;

                SelectionUser();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "FenGestProfil -> sBtnEnregistrerUser_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }

      
   
        private void sBtnSupprimerUser_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                if (IdUtilisateurSelect > 0)
                {
                    var rep = MessageBox.Show("Confirmez-vous la suppression de l'utilisateur " + txtLogin.Text + "?", Appli, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rep != DialogResult.Yes) return;

                    bool resProfil = false;

                    resProfil = mDao.deleteUserProfil(IdUtilisateurSelect, Chaine);

                    if (resProfil)
                    {
                        res = mDao.deleteUser(IdUtilisateurSelect, Chaine);

                        if (res)
                        {
                            MessageBox.Show("Contact supprimé avec succès!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Une erreur est survenue lors de la suppression!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Une erreur est survenue lors de la suppression des profils!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                    //MAJ des listes =================================================

                    //Liste des profils

                    ListeProfil = mDao.getProfils(Chaine);

                    //Liste UserProfilData

                    ListeUserProfilData = new List<CUserProfilData>();

                    ListeUserProfilData = mDao.getUserProfilData(Chaine);

                    _listUser = mDao.getUserProfilData(Chaine);

                    bool IsBienRempli = false;

                    IsBienRempli = RemplirUser();

                    listViewUsers.Items[0].Selected = true;

                    SelectionUser();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "FenGestProfil -> sBtnSupprimerUser_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }


       
        private void FenGestProfil_Load(object sender, EventArgs e)
        {
            try
            {
                //Liste des profils

                ListeProfil = mDao.getProfils(Chaine);

                listViewUsers.Items.Clear();
                txtLogin.Text = string.Empty;

                var rempliruserResult = RemplirUser();

                if (rempliruserResult)
                {
                    if (listViewUsers.Items.Count > 0)
                    {
                        var first = _listUser.FirstOrDefault();
                        if (first != null)
                        {
                            txtNom.Text = first.mNom;
                            txtLogin.Text = first.mLogin;
                            txtPassword.Text = mDao.Decrypt(first.mPassword);
                            txtEmail.Text = first.mEmail;

                            listViewUsers.Items[0].Selected = true;

                            GetProfilListTreeNodes(ListeProfil, _listUser, first.mId);

                            if (txtNom.Text.ToUpper() == "SA")
                            {
                                sBtnSupprimerUser.Enabled = false;
                                sBtnValider.Enabled = false;
                            }
                            else
                            {
                                sBtnSupprimerUser.Enabled = true;
                                sBtnValider.Enabled = true;
                            }


                        }

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "FenGestProfil -> FenGestProfil_Load-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }



        private void listViewUsers_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                _itemselected = listViewUsers.SelectedItems;
                var h = listViewUsers.SelectedIndices[0];
                _listviewIndiceSelect = h;
                txtLogin.Text = _itemselected[0].SubItems[1].Text.ToLower();
                var last = _listUser.LastOrDefault(c => c.mLogin.ToLower() == txtLogin.Text.ToLower());
                if (last != null)
                {
                    txtNom.Text = last.mNom;
                    txtLogin.Text = last.mLogin;
                    txtPassword.Text = last.mPassword;
                    txtEmail.Text = last.mEmail;
                    // CmbFonction.EditValue = Int32.Parse(last.mIdFonction.ToString());

                    IdUtilisateurSelect = last.mId;

                    if (txtNom.Text.ToUpper() == "SA")
                    {
                        sBtnSupprimerUser.Enabled = false;
                        sBtnValider.Enabled = false;
                    }
                    else
                    {
                        sBtnSupprimerUser.Enabled = true;
                        sBtnValider.Enabled = true;
                    }

                }

                GetProfilListTreeNodes(ListeProfil, _listUser, last.mId);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);

                var msg = "FenGestProfil -> listViewUsers_KeyUp-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }



      
        private void listViewUsers_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                _itemselected = listViewUsers.SelectedItems;
                var h = listViewUsers.SelectedIndices[0];
                _listviewIndiceSelect = h;
                txtLogin.Text = _itemselected[0].SubItems[1].Text.ToLower();
                var last = _listUser.LastOrDefault(c => c.mLogin.ToLower() == txtLogin.Text.ToLower());
                if (last != null)
                {
                    txtNom.Text = last.mNom;
                    txtLogin.Text = last.mLogin;
                    txtPassword.Text = last.mPassword;
                    txtEmail.Text = last.mEmail;

                    IdUtilisateurSelect = last.mId;

                    if (txtNom.Text.ToUpper() == "SA")
                    {
                        sBtnSupprimerUser.Enabled = false;
                        sBtnValider.Enabled = false;
                    }
                    else
                    {
                        sBtnSupprimerUser.Enabled = true;
                        sBtnValider.Enabled = true;
                    }

                }

                GetProfilListTreeNodes(ListeProfil, _listUser, last.mId);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);

                var msg = "FenGestProfil -> listViewUsers_KeyDown-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }


  
        private void listViewUsers_Click(object sender, EventArgs e)
        {
            try
            {
                _itemselected = listViewUsers.SelectedItems;
                var h = listViewUsers.SelectedIndices[0];
                _listviewIndiceSelect = h;
                txtLogin.Text = _itemselected[0].SubItems[1].Text.ToLower();
                var last = _listUser.LastOrDefault(c => c.mLogin.ToLower() == txtLogin.Text.ToLower());
                if (last != null)
                {
                    txtNom.Text = last.mNom;
                    txtLogin.Text = last.mLogin;
                    txtPassword.Text = last.mPassword;
                    txtEmail.Text = last.mEmail;

                    IdUtilisateurSelect = last.mId;

                    if (txtNom.Text.ToUpper() == "SA")
                    {
                        sBtnSupprimerUser.Enabled = false;
                        sBtnValider.Enabled = false;
                    }
                    else
                    {
                        sBtnSupprimerUser.Enabled = true;
                        sBtnValider.Enabled = true;
                    }

                }

                GetProfilListTreeNodes(ListeProfil, _listUser, last.mId);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                var msg = "FenGestProfil -> listViewUsers_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }
    }
}
