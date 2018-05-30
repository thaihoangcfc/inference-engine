using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace iengine
{
    class sentence
    {
        private string clause;
        private Boolean booleanValue;
        private List<propositionalSymbol> symbols = new List<propositionalSymbol>();
        private List<connective> connectives = new List<connective>();
        private connective logicalConnective;
        private List<sentence> children = new List<sentence>();

        internal List<propositionalSymbol> Symbols
        {
            get
            {
                return symbols;
            }
        }

        public bool BooleanValue
        {
            get
            {
                return booleanValue;
            }

            set
            {
                booleanValue = value;
            }
        }

        internal List<connective> Connectives
        {
            get
            {
                return connectives;
            }
        }

        internal connective LogicalConnective
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

        internal List<sentence> Children
        {
            get
            {
                return children;
            }

            set
            {
                children = value;
            }
        }

        public string toString
        {
            get
            {
                return clause;
            }
        }


        //Constructor
        public sentence(string q)
        {
            clause = q.Replace(" ", String.Empty);
            clauseParser();
        }


        //Constructor for atomic sentence
        public sentence(propositionalSymbol s)
        {
            symbols.Add(s);
            clause = s.getSymbol;
        }


        //Constructor for initializing children sentence
        public sentence(List<propositionalSymbol> childSymbols, List<connective> childConnectives)
        {
            symbols = childSymbols;
            connectives = childConnectives;
            constructChildren();
        }


        //Check if sentence consists of just one propositional symbol
        public bool isAtomicSentence()
        {
            if ((symbols.Count == 1) && (children.Count == 0))
            {
                return true;
            }
            else return false;
        }


        //Check if sentence consists of implication symbol
        public bool isImplicationClause()
        {
            if (connectives.Any(x => x.toString == "=>"))
            {
                return true;
            }
            else return false;
        }


        //Construct simpler sentences
        public void constructChildren()
        {
            if ((symbols.Count > 1) && (connectives.Count > 0))
            {
                List<propositionalSymbol> childSymbols = new List<propositionalSymbol>();
                List<connective> childConnectives = new List<connective>();
                sentence left;
                sentence right;

                if ((symbols.Count == 2) && (connectives.Count == 1))
                {
                    left = new sentence(symbols[0]);
                    right = new sentence(symbols[1]);
                }
                else
                {
                    right = new sentence(symbols[symbols.Count - 1]);

                    childConnectives = connectives.ToList();
                    childConnectives.RemoveAt(childConnectives.Count - 1);
                    

                    childSymbols = symbols.ToList();
                    childSymbols.RemoveAt(childSymbols.Count - 1);

                    left = new sentence(childSymbols, childConnectives);

                    connectives = connectives.GetRange(connectives.Count - 1, 1);
                    //symbols = symbols.GetRange(symbols.Count - 1, 1);
                }

                this.children.Add(left);
                this.children.Add(right);
            }
        }


        //Extract propositional symbols and connectives from sentence
        public void clauseParser()
        {
            string psString;

            int skip = -1;

            Regex regex = new Regex("[a-zA-Z0-9]");

            for (int i = 0; i < clause.Length; i++)
            {
                if (skip == i)
                {
                    continue;
                }

                if (regex.IsMatch(clause[i].ToString()))
                {
                    psString = clause[i].ToString();
                    if (i != clause.Length - 1)
                    {
                        if (regex.IsMatch(clause[i + 1].ToString()))
                        {
                            psString = psString + clause[i + 1].ToString();
                            skip = i + 1;
                        }
                    }
                    symbols.Add(new propositionalSymbol(psString));
                }
                else
                {
                    psString = clause[i].ToString();
                    if (i != clause.Length - 1)
                    {
                        if (!regex.IsMatch(clause[i + 1].ToString()))
                        {
                            psString = psString + clause[i + 1].ToString();
                            skip = i + 1;
                        }
                    }

                    foreach (connective c in connectiveDatabase.getConnectives.List)
                    {
                        if (psString == c.toString)
                        {
                            connectives.Add(new connective(c.toString, c.Precedence, c.NumberOfSymbols));
                        }
                    }
                }
            }
        }
    }
}
