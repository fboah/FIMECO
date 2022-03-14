using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
   public class CMembreSouscripteur
    {
        public int mId { get; set; }
        public int mIdSouscripteur { get; set; }//Chef de famille
        public string mNomMembre { get; set; }
        public string mPrenomsMembre { get; set; }
        public string mStatutFamilial { get; set; }
        public string mSexe { get; set; }
        public DateTime mDateNaissance { get; set; }
        public string mLieuNaissance { get; set; }
        public int mIdProfession { get; set; }
        public string mProfession { get; set; }
        public string mTelephone { get; set; }
        public string mCellulaire { get; set; }
        public string mEmail { get; set; }
        public string mUserCreation { get; set; }
        public string mUserLastModif { get; set; }
        public DateTime mDateCreation { get; set; }
        public DateTime mDateLastModif { get; set; }

        public string mIsAdulteMembre { get; set; }

        public int mIsDelete { get; set; }

        public CMembreSouscripteur()
        {
            mId = 0;
            mIdSouscripteur = 0;
         
            mNomMembre = string.Empty;
            mPrenomsMembre = string.Empty;
            mStatutFamilial = string.Empty;
            mSexe = string.Empty;
            mDateNaissance = new DateTime();

            mLieuNaissance = string.Empty;
            mProfession = string.Empty;
         
            mTelephone = string.Empty;
            mCellulaire = string.Empty;
            mEmail = string.Empty;
            
            mUserCreation = string.Empty;
            mUserLastModif = string.Empty;
            mDateCreation = new DateTime();
            mDateLastModif = new DateTime();

            mIsAdulteMembre = string.Empty;

            mIsDelete = 0;
        }





    }
}
