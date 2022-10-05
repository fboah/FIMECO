using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
   public class CSouscripteur
    {
        public int mId { get; set; }
        public string mCode { get; set; }//Code
        public string mNom { get; set; }
        public string mPrenoms { get; set; }
        public string mStatutFamilial { get; set; }
        public string mSexe { get; set; }
        public DateTime mDateNaissance { get; set; }
        public string mLieuNaissance { get; set; }
        public string mProfession { get; set; }
        public int mIdProfession { get; set; }
        public int mIdClasseMetho { get; set; }
        public string mClasseMetho { get; set; }
        public string mTelephone { get; set; }
        public string mCellulaire { get; set; }
        public string mEmail { get; set; }

        //Champs à gérer (pas dans la BD)==================================================
        public int mStatutCotisation { get; set; }//0 NOK-1 OK
        public long mMontantVerse { get; set; }
        public long mArriere { get; set; }
        public long mSurplus { get; set; }
        public long mImpayesAnPrecedentes { get; set; }//impayé sur les années précédentes 

        public long mMontantSouscritAnnuel { get; set; }//Montant Objectif annuel

        //==================================================================================

        public DateTime mDateSouscription { get; set; }
        public string mDistrict { get; set; }
        public string mCodeDistrict { get; set; }
        public string mCircuit { get; set; }
        public string mCodeCircuit { get; set; }
        public string mEgliseLocale { get; set; }
        public string mCodeEgliseLocale { get; set; }
        public string mUserCreation { get; set; }
        public string mUserLastModif { get; set; }
        public DateTime mDateCreation { get; set; }
        public DateTime mDateLastModif { get; set; }

        public string mIsAdulte { get; set; }

        public int mIsDelete { get; set; }

        public CSouscripteur()
        {
            mId = 0;
            mCode = string.Empty;
            mNom = string.Empty;
            mPrenoms = string.Empty;
            mStatutFamilial = string.Empty;
            mSexe = string.Empty;
            mDateNaissance = new DateTime();

            mLieuNaissance = string.Empty;
            mProfession = string.Empty;
            mIdClasseMetho = 0;
            mClasseMetho = string.Empty;
            mTelephone = string.Empty;
            mCellulaire = string.Empty;
            mEmail = string.Empty;
       
            mDateSouscription = new DateTime();
            mDistrict = string.Empty;
            mCodeDistrict = string.Empty;
            mCircuit = string.Empty;
            mCodeCircuit = string.Empty;
            mEgliseLocale = string.Empty;
            mCodeEgliseLocale = string.Empty;
            mUserCreation = string.Empty;
            mUserLastModif = string.Empty;
            mDateCreation = new DateTime();
            mDateLastModif = new DateTime();

            mStatutCotisation = 0;
            mMontantVerse = 0;
             mArriere = 0;
            mSurplus = 0;
            mImpayesAnPrecedentes = 0;

            mIsAdulte = string.Empty;

            mIsDelete = 0;

            mMontantSouscritAnnuel = 0;
            
    }


    }
}
