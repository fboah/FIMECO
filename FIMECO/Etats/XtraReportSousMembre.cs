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

        public XtraReportSousMembre(List<CEtatSouscriptMembre>  ListSousCriptMembre)
        {
            try
            {
                InitializeComponent();
                bindingSource1.DataSource = ListSousCriptMembre;
                
            }
            catch (Exception ex)
            {
                var msg = "FIMECO -> XtraReportSousMembre -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
            }
        }


    }
}
