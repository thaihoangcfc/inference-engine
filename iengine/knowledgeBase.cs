using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iengine
{
    class knowledgeBase
    {
        private List<sentence> sentences = new List<sentence>();

        public knowledgeBase()
        {

        }
        

        public List<sentence> Sentences
        {
            get
            {
                return sentences;
            }

            set
            {
                sentences = value;
            }
        }
    }
}
