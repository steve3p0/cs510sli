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
using IpaEntities;

namespace IpaTranscriber
{
    public class IpaTranscriber
    {
        private static RapidAPI RapidApi = new RapidAPI("WordsAPI", "I7USuD9668mshgjnzGpcUTRZagbxp1Bty1Ejsn3hnAwwjDH9lM");

        public string Transcribe(string text)
        {

            //Newtonsoft.Json.Linq.JObject
            //string json = unirest_net.http.Unirest.get("https://wordsapiv1.p.mashape.com/words/effect/pronunciation")
            string phonetic;
            string json;

            try
            {
                json = unirest_net.http.Unirest.get("https://wordsapiv1.p.mashape.com/words/" + text + "/pronunciation")
                    .header("X-Mashape-Key", "I7USuD9668mshgjnzGpcUTRZagbxp1Bty1Ejsn3hnAwwjDH9lM")
                    .header("Accept", "application/json")
                    .asJson<string>().Body;

                JToken outer = JToken.Parse(json);
                //List<JToken> tokens = outer.SelectTokens("$.pronunciation").ToList<JToken>();

                Word word = new Word();
                word.orthography = text;
                word.homonyms = new List<Homonym>();
                int count = outer["pronunciation"].Count<JToken>();

                // There is just one element pronunciation
                if (count == 0)
                {
                    Homonym h = new Homonym();
                    h.isOov = true;
                    word.homonyms.Add(h);
                    phonetic = outer.SelectToken("$.pronunciation").ToString();
                }
                else if (!outer["pronunciation"].HasValues)
                {
                    Homonym h = new Homonym();
                    h.isOov = true;
                    word.homonyms.Add(h);
                    phonetic = "<OOV>";
                }
                else
                {
                    JObject inner = outer["pronunciation"].Value<JObject>();

                    foreach (var item in inner)
                    {
                        Homonym h = new Homonym();
                        h.pos = item.Key;
                        h.phonentic = item.Value.ToString();
                        word.homonyms.Add(h);
                    }
                    phonetic = word.homonyms.First<Homonym>().phonentic;
                }

                return phonetic;
            }
            catch (Exception e)
            {
                return "<OOV>";
            }
        }

        public string TranscribePhrase(string phrase)
        {
            IpaTranscriber ipa = new IpaTranscriber();

            var regex = new Regex(@"\b[\s,\.-:;]*");
            //var phrase = "I am a student";
            var words = regex.Split(phrase.ToLower()).Where(x => !string.IsNullOrEmpty(x));

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
