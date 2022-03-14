using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.Models
{
    public class CProfession
    {

        public int mId { get; set; }
        public string mLibelle { get; set; }
        public int mIsDelete { get; set; }


        public CProfession()
        {
            mId = 0;
            mLibelle = string.Empty;
            mIsDelete = 0;



        }

    }
}
