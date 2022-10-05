using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using FIMECO.Models;
using System.Collections.Generic;

namespace FIMECO.Etats
{
    public partial class XtraReportSouscripteurVersement : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportSouscripteurVersement()
        {
            InitializeComponent();
        }

        public XtraReportSouscripteurVersement(List<CEtatSouscriptVersement> ListSousCriptMembre,string Annee,CFiltre Filtre,int idAppli)
        {

            try
            {
                InitializeComponent();
                bindingSource1.DataSource = ListSousCriptMembre;
                

                xrLabelAnnee.Text = Annee;

                //Appli

                if(idAppli==1)
                {
                    //FIMECO

                    xrLblAppli.Text = "FIMECO";
                }
                else
                {
                    //MOISSON
                    xrLblAppli.Text = "MOISSON";
                }


                //Filtre

                if (Filtre.mTousClasseMetho)
                {
                    xrLblClasseMetho.Text = "Toutes";
                }
                else
                {
                    xrLblClasseMetho.Text = Filtre.mListeClasseMetho;
                }

                if (Filtre.mTousProfession)
                {
                    xrLblProfession.Text = "Toutes";
                }
                else
                {
                    xrLblProfession.Text = Filtre.mListeProfession;
                }

                if (Filtre.mTousSouscripteurs)
                {
                    xrLblSouscripteur.Text = "Tous";
                }
                else
                {
                    xrLblSouscripteur.Text = " DE " + Filtre.mSouscripteurDE + " A " + Filtre.mSouscripteurA;
                }

            }
            catch (Exception ex)
            {
                var msg = "FIMECO -> XtraReportSouscripteurVersement -> TypeErreur: " + ex.Message;
                //CAlias.Log(msg);
            }
        }


    }
}
