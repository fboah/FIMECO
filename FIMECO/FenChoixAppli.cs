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
    public partial class FenChoixAppli : Form
    {
        public int idAppli = 0;

        public string Appli = "FIMECO";

        public FenChoixAppli()
        {
            InitializeComponent();
        }

        private void sBtnValider_Click(object sender, EventArgs e)
        {
            idAppli = 0;
            try
            {
                idAppli =Int16.Parse( rAppli.EditValue.ToString());

                if(idAppli==0)
                {
                    MessageBox.Show("Veuillez choisir une Application!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Close();
            }
            catch(Exception ex)
            {

            }
        }

       

       

        private void rAppli_SelectedIndexChanged(object sender, EventArgs e)
        {
            sBtnValider.Enabled = true;
        }
    }
}
