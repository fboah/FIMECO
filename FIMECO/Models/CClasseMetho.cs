using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
   public  class CClasseMetho
    {
        public int mId { get; set; }
        public string mNomClasse { get; set; }
        public string mNomConducteur1 { get; set; }
        public string mPrenomConducteur1 { get; set; }
        public string mTelephoneConducteur1 { get; set; }
        public string mEmailConducteur1 { get; set; }
        public string mNomConducteur2 { get; set; }
        public string mPrenomConducteur2 { get; set; }
        public string mTelephoneConducteur2 { get; set; }
        public string mEmailConducteur2 { get; set; }
        public string mQuartier { get; set; }

        public CClasseMetho()
        {
            mId = 0;
            mNomClasse = string.Empty;
            mNomConducteur1 = string.Empty;
            mPrenomConducteur1 = string.Empty;
            mTelephoneConducteur1 = string.Empty;
            mEmailConducteur1 = string.Empty;
            mNomConducteur2 = string.Empty;
            mPrenomConducteur2 = string.Empty;
            mTelephoneConducteur2 = string.Empty;
            mEmailConducteur2 = string.Empty;
            mQuartier = string.Empty;

            
        }



    }
}
