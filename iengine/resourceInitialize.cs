using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace iengine
{
    class resourceInitialize
    {
        private System.IO.StreamReader file;
        private string line;
        private knowledgeBase kB = new knowledgeBase();
        private sentence query;
        private string[] sentences;
        //private List<connective> connectives = new List<connective>();
        private List<propositionalSymbol> propositionalSymbols = new List<propositionalSymbol>();
        //private List<model> models = new List<model>();

        public List<propositionalSymbol> PropositionalSymbols
        {
            get
            {
                return propositionalSymbols;
            }

            set
            {
                propositionalSymbols = value;
            }
        }

        internal knowledgeBase KB
        {
            get
            {
                return kB;
            }

            set
            {
                kB = value;
            }
        }

        internal sentence Query
        {
            get
            {
                return query;
            }

            set
            {
                query = value;
            }
        }

        //Construtor
        public resourceInitialize(string filePath)
        {
            file = new System.IO.StreamReader(filePath);
        }


        //Populate data from text file
        public void populateData()
        {
            int counter = 0;
            Regex regex = new Regex("[a-zA-Z0-9]");

            while ((line = file.ReadLine()) != null)
            {
                if (counter == 0)
                {
                    if (line.ToUpper() == "TELL")
                    {
                        counter++;
                        continue;
                    }
                    else break;
                }

                if (counter == 1)
                {
                    sentences = line.TrimEnd(';').Split(';');
                    foreach (string s in sentences)
                    {
                        KB.Sentences.Add(new sentence(s));
                    }
                }

                if (counter == 2)
                {
                    if (line.ToUpper() == "ASK")
                    {
                        counter++;
                        continue;
                    }
                    else break;
                }

                if (counter == 3)
                {
                    Query = new sentence(line.TrimEnd(';'));
                }

                counter++;
            }

            //Extract propositional symbols from KB
            foreach (sentence s in KB.Sentences)
            {
                foreach (propositionalSymbol p in s.Symbols)
                {
                    if (!propositionalSymbols.Any(x => x.getSymbol == p.getSymbol))
                        propositionalSymbols.Add(p);
                }
            }

            //Extract propositional symbols from alpha
            foreach (propositionalSymbol p in query.Symbols)
            {
                if (!propositionalSymbols.Any(x => x.getSymbol == p.getSymbol))
                    propositionalSymbols.Add(p);
            }

            propositionalSymbols = propositionalSymbols.OrderBy(p => p.getSymbol).ToList();

            

        }
    }
}
