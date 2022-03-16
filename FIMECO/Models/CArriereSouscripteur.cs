using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
   public class CArriereSouscripteur
    {
        public int mIdSouscripteur { get; set; }//Chef de famille
        public string mCode { get; set; }
        public string mNom { get; set; }
        public string mPrenoms { get; set; }
        public string mNumeroRecu { get; set; }
        public long mMontantVersement { get; set; }
        public int mAnnee { get; set; }
        public DateTime DateVersement { get; set; }
        public long mMontantCotisationObjectif { get; set; }

        //Delete
        public int mIsDeleteSouscripteur { get; set; }
        public int mIsDeleteVersement { get; set; }
        public int mIsDeleteCotisation { get; set; }

        public CArriereSouscripteur()
        {
            mIdSouscripteur = 0;
            mCode = string.Empty;
            mNom = string.Empty;
            mPrenoms = string.Empty;
            mNumeroRecu = string.Empty;
            mMontantVersement = 0;
            mAnnee = 0;
            DateVersement = new DateTime();
            mMontantCotisationObjectif = 0;

            mIsDeleteSouscripteur = 0;
            mIsDeleteVersement = 0;
            mIsDeleteCotisation = 0;

    }


    }
}
