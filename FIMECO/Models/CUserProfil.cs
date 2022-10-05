using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
  public  class CUserProfil
    {
        public int mId { get; set; }
        public int mIdUser { get; set; }
        public int mIdProfil { get; set; }

        public CUserProfil()
        {
            mId = 0;
            mIdUser = 0;
            mIdProfil = 0;

        }

    }
}
