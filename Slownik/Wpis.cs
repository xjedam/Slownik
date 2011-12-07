using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Slownik
{
    class Wpis
    {
		public long id = -1;
        public String polskie;
        public String angielskie;
		
        public Wpis(String pol, String eng)
        {
            polskie = pol;
            angielskie = eng;
        }
		
		
    }
}
