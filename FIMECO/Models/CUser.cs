using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
  public  class CUser
    {
        public int mId { get; set; }
        public string mNom { get; set; }
        public string mLogin { get; set; }
        public string mPassword { get; set; }
        public string mEmail { get; set; }

        public int mIsDelete { get; set; }


        public CUser()
        {
            mId = 0;
            mNom = string.Empty;
            mLogin = string.Empty;
            mPassword = string.Empty;
            mEmail = string.Empty;
            mIsDelete = 0;

        }

    }
}
