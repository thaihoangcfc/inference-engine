using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace iengine
{
    class backwardChaining
    {
        Queue<propositionalSymbol> agenda = new Queue<propositionalSymbol>();
        List<propositionalSymbol> facts = new List<propositionalSymbol>();
        List<propositionalSymbol> open = new List<propositionalSymbol>();
        List<propositionalSymbol> trace = new List<propositionalSymbol>();

        public backwardChaining()
        {
        }

        internal Queue<propositionalSymbol> Agenda
        {
            get
            {
                return agenda;
            }
            set
            {
                agenda = value;
            }
        }

        internal List<propositionalSymbol> Inferred
        {
            get
            {
                return trace;
            }
        }

        internal List<propositionalSymbol> Facts
        {
            get
            {
                return facts;
            }
        }


        //Backward chaining algorithm
        public bool BCEntails(knowledgeBase kB, sentence query)
        {
            //Initialize list for storing implication clause only
            List<sentence> implication = new List<sentence>();
            int factsCount;


            //Add query to open list
            if (!open.Any(x => x.getSymbol == query.Symbols.First().getSymbol))
                open.Add(query.Symbols.First());

            //Return false if the query is not an atomic sentence
            if (!query.isAtomicSentence())
            {
                return false;
            }

            //Construct simpler sentences in each KB sentences and add atomic sentences to known facts list
            foreach (sentence s in kB.Sentences)
            {
                if (!s.isAtomicSentence())
                {
                    if (s.Children.Count == 0)
                    {
                        s.constructChildren();
                    }
                    implication.Add(s);
                }
                else
                {
                    if (!facts.Any(x => x.getSymbol == s.Symbols.First().getSymbol))
                    {
                        facts.Add(s.Symbols.First());
                    }
                }
            }

            //If the query is a known fact in KB
            if (facts.Any(x => x.getSymbol == query.Symbols.First().getSymbol))
            {
                trace.Add(query.Symbols.First());
                return true;
            }

            //Return false if the query is not a conclusion of any of KB sentences
            if (!implication.Any(x => x.Children.Last().Symbols.First().getSymbol == query.Symbols.First().getSymbol))
            {
                return false;
            }

            //Loop through KB
            foreach (sentence s in implication)
            {

                //Check if query match any conclusions in KB
                if (s.Children.Last().Symbols.Any(x => x.getSymbol == query.Symbols.First().getSymbol))
                {
                    //Check whether if conjunction premise consists of only one single atomic sentence
                    if (s.Children.First().isAtomicSentence())
                    {
                        //Check if 
                        if (!facts.Any(x => x.getSymbol == s.Children.First().Symbols.First().getSymbol))
                        {
                            if (!open.Any(x => x.getSymbol == s.Children.First().Symbols.First().getSymbol))
                                open.Add(s.Children.First().Symbols.First());
                        }
                        else
                        {
                            //Check for duplication
                            if (!trace.Any(x => x.getSymbol == s.Children.First().Symbols.First().getSymbol))
                            {
                                trace.Add(s.Children.First().Symbols.First());
                            }

                            trace.Add(query.Symbols.First());
                            facts.Add(query.Symbols.First());
                            open.RemoveAll(x => x.getSymbol == query.Symbols.First().getSymbol);
                        }
                    }
                    else
                    {
                        factsCount = 0;
                        foreach (propositionalSymbol p in s.Children.First().Symbols)
                        {
                            //Check if symbol is still yet to be known
                            if (open.Any(x => x.getSymbol == p.getSymbol))
                            {
                                agenda.Enqueue(p);
                                continue;
                            }

                            //Check if each symbol in conjunction premise is a known fact
                            if (facts.Any(x => x.getSymbol == p.getSymbol))
                            {
                                if (!trace.Any(x => x.getSymbol == p.getSymbol))
                                {
                                    trace.Add(p);
                                }
                                factsCount++;
                            }

                            //Check if conjunction premise fully consists of known facts
                            if (factsCount == s.Children.First().Symbols.Count)
                            {
                                facts.Add(s.Children.Last().Symbols.First());
                                trace.Add(s.Children.Last().Symbols.First());
                                open.RemoveAll(x => x.getSymbol == s.Children.Last().Symbols.First().getSymbol);
                            }

                            //Check if symbol is already inferred
                            if (!trace.Any(x => x.getSymbol == p.getSymbol))
                            {
                                open.Add(p);
                                agenda.Enqueue(p);
                            }
                        }
                    }
                }
            }

            //Make recursive call as long as there is more to expand in the agenda or open list
            if (agenda.Count != 0)
            {
                return BCEntails(kB, new sentence(agenda.Dequeue()));
            }
            else
            {
                if (open.Count == 0)
                {
                    return true;
                }
                else
                {
                    sentence openPullFirst = new sentence(open.First());
                    open.Remove(open.First());
                    return BCEntails(kB, openPullFirst);
                }
            }

        }
    }
}
