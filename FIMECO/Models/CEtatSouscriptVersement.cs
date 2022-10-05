using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
  public  class CEtatSouscriptVersement
    {
        public int mId { get; set; }
        public int mIdClasseMetho { get; set; }
        public int mIdProfession { get; set; }
        public string mNomPersonne { get; set; }
        public string mPrenomsPersonne { get; set; }
        public long mMontantCotisation { get; set; }
        public long mMontantTotalVerse { get; set; }
        public long mMontantVersement { get; set; }
        public long mMontantRestant { get; set; }

        public DateTime mDateVersement { get; set; }
       


        public CEtatSouscriptVersement()
        {
            mId = 0;
            mIdClasseMetho = 0;
            mIdProfession = 0;
             mNomPersonne = string.Empty;
            mPrenomsPersonne = string.Empty;
            mMontantCotisation = 0;
            mMontantVersement = 0;
            mMontantRestant = 0;
            mMontantTotalVerse = 0;

            mDateVersement = new DateTime();
            

        }


    }
}
