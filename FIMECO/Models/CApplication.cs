using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
   public class CApplication
    {

        public int mId { get; set; }//Chef de famille
        public string mApplication { get; set; }
    

        public CApplication()
        {
            mId = 0;
            mApplication = string.Empty;
          

        }

    }
}
