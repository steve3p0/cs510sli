using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RapidAPISDK;
using unirest_net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace IpaTranscriber
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

    public class Homonym
    {
        public string pos { get; set; }
        public string phonentic { get; set; }
    }


    public class IpaTranscriber
    {

        private static RapidAPI RapidApi = new RapidAPI("WordsAPI", "I7USuD9668mshgjnzGpcUTRZagbxp1Bty1Ejsn3hnAwwjDH9lM");

        public string Transcribe(string text)
        {
            //Newtonsoft.Json.Linq.JObject
            //string json = unirest_net.http.Unirest.get("https://wordsapiv1.p.mashape.com/words/effect/pronunciation")
            string json = unirest_net.http.Unirest.get("https://wordsapiv1.p.mashape.com/words/" + text + "/pronunciation")
                .header("X-Mashape-Key", "I7USuD9668mshgjnzGpcUTRZagbxp1Bty1Ejsn3hnAwwjDH9lM")
                .header("Accept", "application/json")
                .asJson<string>().Body;

            JToken outer = JToken.Parse(json);
            JObject inner = outer["pronunciation"].Value<JObject>();

            Word word = new Word();
            word.orthography = text;
            word.homonyms = new List<Homonym>();
            foreach(var item in inner)
            {
                Homonym h = new Homonym();
                h.pos = item.Key;
                h.phonentic = item.Value.ToString();
                word.homonyms.Add(h);
            }

            string phonetic = word.homonyms.First<Homonym>().phonentic;
            return phonetic;
        }

        public string TranscribePhrase(string phrase)
        {
            IpaTranscriber ipa = new IpaTranscriber();

            var regex = new Regex(@"\b[\s,\.-:;]*");
            //var phrase = "I am a student";
            var words = regex.Split(phrase).Where(x => !string.IsNullOrEmpty(x));

            string phrase_ipa = "";

            foreach (var word in words)
            {
                //string result = ipa.Transcribe("effect", "noun");
                phrase_ipa += ipa.Transcribe(word) + " ";
            }

            //Assert.AreEqual(result, "'ɪ,fɛkt");
            phrase_ipa = "/" + phrase_ipa.Trim() + "/";
            return phrase_ipa;
        }


        public string Transcribe(string text, string pos)
        {
            //Newtonsoft.Json.Linq.JObject
            //string json = unirest_net.http.Unirest.get("https://wordsapiv1.p.mashape.com/words/effect/pronunciation")
            string json = unirest_net.http.Unirest.get("https://wordsapiv1.p.mashape.com/words/" + text + "/pronunciation")
                .header("X-Mashape-Key", "I7USuD9668mshgjnzGpcUTRZagbxp1Bty1Ejsn3hnAwwjDH9lM")
                .header("Accept", "application/json")
                .asJson<string>().Body;

            JToken outer = JToken.Parse(json);
            string phonotic = outer["pronunciation"][pos].Value<string>();

            //string phonotic = outer["pronunciation"].Last.Value<string>();

            //Word word = new Word();
            //word.orthography = text;
            //word.homonyms = new List<Homonym>();
            //foreach (var item in inner)
            //{
            //    Homonym h = new Homonym();
            //    h.pos = item.Key;
            //    h.phonentic = item.Value.ToString();
            //    word.homonyms.Add(h);
            //}


            return json;
        }

    }
}
