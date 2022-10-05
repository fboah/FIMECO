﻿using FIMECO.DAOFIMECO;
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
    public partial class FenGestProfession : Form
    {
        private bool myObjectAjout;
        private readonly DAOFimeco daoReport = new DAOFimeco();
        private string myObjectChaineConFimeco;
        private List<CProfession> myObjectListeProf;
        private CProfession myObjectCProf;

        private string Appli = "FIMECO";

        public FenGestProfession()
        {
            InitializeComponent();
        }


        public FenGestProfession(bool IsAjout, List<CProfession> ListeProf, CProfession CProf, string chainefimeco)
        {
            InitializeComponent();
            
            this.myObjectAjout = IsAjout;
            this.myObjectListeProf = ListeProf;
            this.myObjectCProf = CProf;
            this.myObjectChaineConFimeco = chainefimeco;

        }


      
        private void sBtnEnregistrer_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                if (myObjectAjout && myObjectCProf == null)
                {
                    //Tester qu'on a pas un doublon
                    var IsExist = myObjectListeProf.Exists(c => c.mLibelle.ToUpper().Trim() == txtLibelle.Text.Replace("'", "''").ToUpper().Trim());

                    if (!IsExist)
                    {
                        CProfession COp = new CProfession();
                        COp.mLibelle = txtLibelle.Text.Replace("'", "''").Trim();
                       
                        res = daoReport.AddProfession(COp, myObjectChaineConFimeco);

                        if (res)
                        {
                            MessageBox.Show("Profession ajoutée avec succès!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                        MessageBox.Show("Cette Profession existe déjà ! Veuillez vérifier vos données", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }


                }
                else
                {
                    //Modification

                    if (!myObjectAjout && myObjectCProf != null)
                    {
                        //Tester qu'on a pas un doublon
                        var IsExist = myObjectListeProf.Exists(c => c.mLibelle.ToUpper().Trim() == txtLibelle.Text.Replace("'", "''").ToUpper().Trim());


                        if (!IsExist)
                        {

                            myObjectCProf.mLibelle = txtLibelle.Text.Replace("'", "''").Trim();
                          

                            res = daoReport.UpdateProfession(myObjectCProf, myObjectChaineConFimeco);

                            if (res)
                            {


                                #region Tracabilité

                                CTracabilite Ct = new CTracabilite();

                                string content = "mLibelle:" + myObjectCProf.mLibelle ;

                                Ct.mContenu = content;

                                Ct.mTypeOperation = "Modification_Profession";
                                Ct.mDateAction = DateTime.Now;
                                Ct.mMachineAction = Environment.UserDomainName + "\\" + Environment.UserName;

                                bool ret = false;

                                ret = daoReport.AddTrace(Ct, myObjectChaineConFimeco);


                                #endregion


                                MessageBox.Show("Profession modifiée avec succès!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                            MessageBox.Show("Cette Profession existe déjà ! Veuillez vérifier vos données", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "FenGestProfession -> sBtnEnregistrer_Click-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
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


      
        private void FenGestProfession_Load(object sender, EventArgs e)
        {
            try
            {
                if (!myObjectAjout)//Modification
                {

                    txtLibelle.Text = myObjectCProf.mLibelle;
                  
                }

            }
            catch (Exception ex)
            {
                var msg = "FenGestProfession -> FenGestProfession_Load-> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }
    }
}
