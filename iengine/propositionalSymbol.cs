using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iengine
{
    class propositionalSymbol
    {
        private string symbol;

        public propositionalSymbol(string s)
        {
            symbol = s;
        }

        public propositionalSymbol(propositionalSymbol s)
        {
            symbol = s.getSymbol;
        }

        public string getSymbol
        {
            get
            {
                return symbol;
            }

            set
            {
                symbol = value;
            }
        }
    }
}
