using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
   public class CTracabilite
    {
        public int mId { get; set; }
       
        public string mTypeOperation { get; set; }
      //  public string mLogin { get; set; }
        public string mMachineAction { get; set; }
      
        public DateTime mDateAction { get; set; }
        public string mContenu { get; set; }
        


        public CTracabilite()
        {
            mId = 0;
            mTypeOperation = string.Empty;
        //    mLogin = string.Empty;
            mMachineAction = string.Empty;
            mContenu = string.Empty;
            mDateAction = new DateTime();
          
        }

    }
}
