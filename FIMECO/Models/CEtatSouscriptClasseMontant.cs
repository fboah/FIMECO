using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
   public class CEtatSouscriptClasseMontant
    {
        public int mId { get; set; }
        public int mIdMembre { get; set; }
        public string mNomClasse { get; set; }
        public string mNomPersonne { get; set; }
        public string mPrenomsPersonne { get; set; }
        public long mMontantCotisation { get; set; }

        public string mNomMembre { get; set; }
        public string mPrenomsMembre { get; set; }

        public DateTime mDateVersement { get; set; }
        public long mMontantVersement { get; set; }

        public int mIdClasseMetho { get; set; }
        public int mIdProfession { get; set; }
      
    
        public long mMontantTotalVerse { get; set; }
     
        public long mMontantTotalSouscrit { get; set; }

       
        
        public CEtatSouscriptClasseMontant()
        {
            mId = 0;
            mIdMembre = 0;
            mIdClasseMetho = 0;
            mIdProfession = 0;
            mNomClasse = string.Empty;
            mNomPersonne = string.Empty;
            mPrenomsPersonne = string.Empty;
            mMontantCotisation = 0;
            mMontantVersement = 0;
            mNomMembre = string.Empty;
            mPrenomsMembre = string.Empty;
            mDateVersement = new DateTime();
            mMontantTotalVerse = 0;

            mMontantTotalSouscrit = 0;


        }


    }
}
