using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
   public class CCotisationAnnuelle
    {
        public int mId { get; set; }
        public int mIdSouscripteur { get; set; }//Chef de famille
        public int mAnnee { get; set; }
        public long mMontantCotisation { get; set; }
        public string mNom { get; set; }
        public string mPrenoms { get; set; }
        public string mUserCreation { get; set; }
        public string mUserLastModif { get; set; }
        public DateTime mDateCreation { get; set; }
        public DateTime mDateLastModif { get; set; }

        public int mIsDelete { get; set; }

        public CCotisationAnnuelle()
        {
            mId = 0;
            mIdSouscripteur = 0;
            mAnnee = 0;
            mMontantCotisation = 0;
            mPrenoms = string.Empty;
            mNom = string.Empty;
            mUserCreation = string.Empty;
            mUserLastModif = string.Empty;
            mDateCreation = new DateTime();
            mDateLastModif = new DateTime();

            mIsDelete = 0;
        }

    }
}
