using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iengine
{
    class connective
    {
        private string logicalConnective;
        private int precedence;
        private int numberOfSymbols;

        public connective(string s, int connectivePrecedence, int symbolsNo)
        {
            logicalConnective = s;
            precedence = connectivePrecedence;
            numberOfSymbols = symbolsNo;
        }

        public string toString
        {
            get
            {
                return logicalConnective;
            }

            set
            {
                logicalConnective = value;
            }
        }

        public int Precedence
        {
            get
            {
                return precedence;
            }

        }

        public int NumberOfSymbols
        {
            get
            {
                return numberOfSymbols;
            }
        }
    }
}
