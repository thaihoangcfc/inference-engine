using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace iengine
{
    class forwardChaining
    {
        List<propositionalSymbol> inferred = new List<propositionalSymbol>();

        public forwardChaining()
        {
        }

        internal List<propositionalSymbol> InferredSymbols
        {
            get
            {
                return inferred;
            }
        }


        //Forward chaining algorithm
        public bool FCEntails(knowledgeBase kB, sentence query)
        {
            Dictionary<sentence, int> count = new Dictionary<sentence, int>();
            Queue<propositionalSymbol> agenda = new Queue<propositionalSymbol>();
            propositionalSymbol p;
            List<sentence> hornClause = new List<sentence>();

            //Construct simpler sentences in each KB sentences and allocate KB atomic sentences into agenda
            foreach (sentence s in kB.Sentences)
            {
                if (!s.isAtomicSentence())
                {
                    s.constructChildren();
                }
                else
                {
                    agenda.Enqueue(s.Symbols.Last());
                }
            }

            
            //Allocate horn clause list from KB
            foreach (sentence s in kB.Sentences)
            {
                if (s.isImplicationClause())
                {
                    hornClause.Add(s);
                }
            }

            
            //Count number of symbols on conjunction side
            foreach (sentence s in hornClause)
            {
                count.Add(s, s.Children.First().Symbols.Count);
            }


            while (agenda.Count != 0)
            {
                p = agenda.Dequeue();

                if (!InferredSymbols.Contains(p))
                {
                    inferred.Add(p);

                    foreach (sentence s in hornClause)
                    {
                        if (s.Children.First().Symbols.Any(x => x.getSymbol == p.getSymbol))
                        {
                            count[s]--;

                            if (count[s] == 0)
                            {       
                                if (s.Symbols.Last().getSymbol == query.Symbols.First().getSymbol)
                                {
                                    inferred.Add(s.Symbols.Last());
                                    return true;
                                }
                                agenda.Enqueue(s.Symbols.Last());
                            }
                        }
                    }
                }
            }
            return false;

        }

    }
}
