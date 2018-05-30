using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace iengine
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = args[1];
            resourceInitialize rI;

            try
            {
                rI = new resourceInitialize(@"resources\" + filename);
            }
            catch (Exception e)
            {
                throw new Exception(
                   "No file with such name!",
                   e);
            }

            if (rI != null)
            {
                rI.populateData();
                switch (args[0].ToLower())
                {
                    case "tt":
                        truthTable TT = new truthTable();
                        TT.Entails(rI.KB, rI.Query, rI.PropositionalSymbols);
                        if (TT.ModelEntailments > 0)
                        {
                            Console.WriteLine("YES: " + TT.ModelEntailments.ToString());
                        }
                        else Console.WriteLine("NO");
                        break;
                    case "bc":
                        backwardChaining BC = new backwardChaining();
                        if (BC.BCEntails(rI.KB, rI.Query))
                        {
                            Console.Write("YES: ");
                            foreach (propositionalSymbol p in BC.Inferred)
                            {
                                Console.Write(p.getSymbol + "; ");
                            }
                        }
                        else Console.WriteLine("NO");
                        break;
                    case "fc":
                        forwardChaining FC = new forwardChaining();
                        if (FC.FCEntails(rI.KB, rI.Query))
                        {
                            Console.Write("YES: ");
                            foreach (propositionalSymbol p in FC.InferredSymbols)
                            {
                                Console.Write(p.getSymbol + "; ");
                            }
                        }
                        else Console.WriteLine("NO");
                        break;
                    default:
                        Console.WriteLine("No inference method called " + args[0]);
                        break;
                }
            }
        }
    }
}
