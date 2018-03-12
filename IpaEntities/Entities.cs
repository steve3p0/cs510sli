using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IpaEntities
{

    public class Word
    {
        public string orthography { get; set; }

        public List<Homonym> homonyms { get; set; }
    }

    public class JsonWord
    {
        [JsonProperty("pronounciation")]
        public List<KeyValuePair<string, object>> pronounciation { get; set; }
    }

    public class Homonym : IHomonym
    {
        public string pos { get; set; }
        public string phonentic { get; set; }
        public bool isOov { get; set; }

        public Homonym()
        {
            isOov = false;
        }
    }

}
