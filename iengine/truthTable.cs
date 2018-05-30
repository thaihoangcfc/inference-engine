using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace iengine
{
    class truthTable
    {
        private int modelEntailments = 0;

        public int ModelEntailments
        {
            get
            {
                return modelEntailments;
            }
        }

        public truthTable()
        {
        }


        //Check entailment of alpha from KB
        public void Entails(knowledgeBase kB, sentence query, List<propositionalSymbol> symbols)
        {
            CheckAll(kB, query, symbols, new model());
        }


        //Check all models based on given symbols
        public void CheckAll(knowledgeBase kB, sentence query, List<propositionalSymbol> symbols, model model)
        {
            if (symbols.Count == 0)
            {
                if (model.isTrue(kB))
                {
                    if (model.isTrue(query))
                    {
                        modelEntailments++;
                    }
                    //return model.isTrue(query);
                }
                //else return true;
            }
            else
            {
                propositionalSymbol s = new propositionalSymbol(symbols.First());

                //rest of the symbols
                List<propositionalSymbol> rest = new List<propositionalSymbol>();
                rest.AddRange(symbols);
                rest.RemoveAt(0);

                //Initialize model for true branch
                model mleft = new model();
                mleft.getCellList.AddRange(model.getCellList);
                mleft.getCellList.Add(new cell(s, true));

                //Initialize model for false branch
                model mright = new model();
                mright.getCellList.AddRange(model.getCellList);
                mright.getCellList.Add(new cell(s, false));

                //Recursive call
                CheckAll(kB, query, rest, mleft);
                CheckAll(kB, query, rest, mright);
            }
        }
    }
}
