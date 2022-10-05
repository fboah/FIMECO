using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
    public  class CProfil
    {
        public int mId { get; set; }
        public string mDescription { get; set; }

        public CProfil()
        {
            mId = 0;
            mDescription = string.Empty;

        }
    }
}
