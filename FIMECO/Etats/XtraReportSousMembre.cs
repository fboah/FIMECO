using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using FIMECO.Utils;
using FIMECO.Models;
using System.Collections.Generic;

namespace FIMECO.Etats
{
    public partial class XtraReportSousMembre : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportSousMembre()
        {
            InitializeComponent();
        }

        public XtraReportSousMembre(List<CEtatSouscriptMembre>  ListSousCriptMembre,CFiltre Filtre)
        {
            try
            {
                InitializeComponent();
                bindingSource1.DataSource = ListSousCriptMembre;

                //Filtre

                if(Filtre.mTousClasseMetho)
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
                    xrLblSouscripteur.Text = " DE "+Filtre.mSouscripteurDE+" A "+Filtre.mSouscripteurA;
                }
            }
            catch (Exception ex)
            {
                var msg = "FIMECO -> XtraReportSousMembre -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }


    }
}
