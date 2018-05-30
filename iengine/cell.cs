using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iengine
{
    //Class defining a propositional symbol and boolean value it holds within a model
    class cell
    {
        private propositionalSymbol symbol;
        private bool booleanValue;

        public bool BooleanValue
        {
            get
            {
                return booleanValue;
            }
        }

        internal propositionalSymbol Symbol
        {
            get
            {
                return symbol;
            }
        }

        //constructor
        public cell(propositionalSymbol s, bool v)
        {
            symbol = s;
            booleanValue = v;
        }
    }
}
