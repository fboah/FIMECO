using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
   public class CFiltre
    {
       
        public string mSouscripteurDE { get; set; }
        public string mSouscripteurA { get; set; }
        public bool mTousSouscripteurs { get; set; }

        public bool mTousClasseMetho { get; set; }
        public string mListeClasseMetho { get; set; }

        public bool mTousProfession { get; set; }
        public string mListeProfession { get; set; }



        public CFiltre()
        {

            mSouscripteurDE = string.Empty;
            mSouscripteurA = string.Empty;
            mTousSouscripteurs = false;

            mTousClasseMetho = false;
            mListeClasseMetho = string.Empty;

            mTousProfession = false;
            mListeProfession = string.Empty;


        }



    }
}
