using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iengine
{
    class connectiveDatabase
    {
        private static connectiveDatabase instance;
        private static List<connective> connectives = new List<connective>();

        private connectiveDatabase()
        {
            //Add logical connectives
            connectives.Add(new connective("&", 1, 2));
            connectives.Add(new connective("=>", 0, 2));
        }

        public static connectiveDatabase getConnectives
        {
            get
            {
                if (instance == null)
                {
                    instance = new connectiveDatabase();
                }
                return instance;
            }
        }

        internal List<connective> List
        {
            get
            {
                return connectives;
            }

            set
            {
                connectives = value;
            }
        }
    }
}
