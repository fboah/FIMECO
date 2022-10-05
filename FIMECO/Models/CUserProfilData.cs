using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
    public class CUserProfilData
    {
        public int mId { get; set; }
        public int mIdProfil { get; set; }
        public string mNom { get; set; }
        public string mLogin { get; set; }
        public string mPassword { get; set; }
        public string mEmail { get; set; }
        public string mDescription { get; set; }
        public int mIdUserProfil { get; set; }


        public CUserProfilData()
        {
            mId = 0;
            mNom = string.Empty;
            mLogin = string.Empty;
            mPassword = string.Empty;
            mEmail = string.Empty;
            mIdProfil = 0;
            mDescription = string.Empty;
            mIdUserProfil = 0;

        }
    }
}
