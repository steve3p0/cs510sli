
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNetNlp;

namespace IpaPosTagger
{
    public class IpaPosTagger
    {
        public void TagPos(string phrase)
        {
            try
            { 
                var sentence = new Sentence("Your text should go here");
                //var lemmas = sentence.Lemmas;

                
                var pos = sentence.PosTags;

            

                string blah = "blah";
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
        }
    }
}
