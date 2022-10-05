using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
   public  class CEtatSouscriptMembre
    {
      //  public int mIdSouscripteur { get; set; }
        public string mNomPersonne { get; set; }
        public string mPrenomsPersonne { get; set; }
        public string mStatutAgeSous { get; set; }

        public string mNomMembre { get; set; }
        public string mPrenomsMembre { get; set; }
        public string mStatutAgeMembre { get; set; }

        public string mClasse { get; set; }

        public int mIdClasseMetho { get; set; }

        public int mIdProfession { get; set; }

        


        public CEtatSouscriptMembre()
        {
          //  mIdSouscripteur = 0;
            mNomPersonne = string.Empty;
            mPrenomsPersonne = string.Empty;
            mStatutAgeSous = string.Empty;
            mNomMembre = string.Empty;
            mPrenomsMembre = string.Empty;
            mStatutAgeMembre = string.Empty;

            mClasse = string.Empty;
            mIdClasseMetho = 0;
            mIdProfession = 0;

        }




    }
}
