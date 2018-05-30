using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace iengine
{
    class model
    {
        private List<cell> m;


        //Constructor
        public model ()
        {
            m = new List<cell>();
        }


        //Second constructor for cloning model object
        public model(model m2)
        {
            this.m = m2.getCellList;
        }


        //Extend model with the next symbol
        public model extend(propositionalSymbol s, bool bValue)
        {
            cell c = new cell(s, bValue);
            this.m.Add(c);
            return this;
        }


        //Determine KB logic based on given model
        public bool isTrue (knowledgeBase kB)
        {
            bool kbLogic = true;

            foreach (sentence s in kB.Sentences)
            {
                kbLogic = kbLogic && isTrue(s);
            }

            return kbLogic;
        }

        
        //Determine sentence logic based on given model
        public bool isTrue (sentence s)
        {
            if (!s.isAtomicSentence() && s.Children.Count == 0)
            {
                s.constructChildren();
            }

            if (s.isAtomicSentence())
            {
                foreach (cell c in m)
                {
                    if (s.Symbols[0].getSymbol == c.Symbol.getSymbol)
                    {
                        s.BooleanValue = c.BooleanValue;
                    }
                }
            }

            if (s.Children.Count == 2)
            {
                if (s.Connectives[s.Connectives.Count - 1].toString == "&")
                {
                    return isTrue(s.Children[0]) && isTrue(s.Children[1]);
                }
                else return !(isTrue(s.Children[0]) && !isTrue(s.Children[1]));
            }

            return s.BooleanValue;
        }


        //Get list of cell
        public List<cell> getCellList
        {
            get
            {
                return m;
            }

            set
            {
                m = value;
            }
        }
    }
}
