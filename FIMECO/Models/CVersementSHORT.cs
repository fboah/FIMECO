using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
   public class CVersementSHORT
    {
       
        public int mIdSouscripteur { get; set; }//Chef de famille
        public int mIdClasseMetho { get; set; }//Chef de famille
  
        public long mMontantVersement { get; set; }
        public DateTime mDateVersement { get; set; }
  
        


        public CVersementSHORT()
        {
            mIdSouscripteur = 0;
            mIdClasseMetho = 0;

            mMontantVersement = 0;

            mDateVersement = new DateTime();
            
        }


    }
}
