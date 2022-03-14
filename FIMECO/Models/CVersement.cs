using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
   public class CVersement
    {
        public int mId { get; set; }
        public int mIdSouscripteur { get; set; }//Chef de famille
        public string mNumeroRecu { get; set; }
        public string mNom { get; set; }
        public string mPrenoms { get; set; }
        public long mMontantVersement { get; set; }
        public DateTime mDateVersement { get; set; }
        public string mNomReceveur { get; set; }

        public string mUserCreation { get; set; }
        public string mUserLastModif { get; set; }
        public DateTime mDateCreation { get; set; }
        public DateTime mDateLastModif { get; set; }

        public int mIsDelete { get; set; }


        public CVersement()
        {
            mId = 0;
            mIdSouscripteur = 0;
            mNumeroRecu = string.Empty ;
            mMontantVersement = 0;
            mNomReceveur = string.Empty;
            mUserCreation = string.Empty;
            mUserLastModif = string.Empty;
            mDateCreation = new DateTime();
            mDateLastModif = new DateTime();
            mDateVersement = new DateTime();

            mIsDelete = 0;

        }


    }
}
