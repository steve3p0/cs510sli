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
            string json = unirest_net.http.Unirest.get("https://wordsapiv1.p.mashape.com/words/effect/pronunciation")
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


            return json;
        }

        public string Transcribe2(string text)
        {
            //Newtonsoft.Json.Linq.JObject
            string json = unirest_net.http.Unirest.get("https://wordsapiv1.p.mashape.com/words/effect/pronunciation")
                .header("X-Mashape-Key", "I7USuD9668mshgjnzGpcUTRZagbxp1Bty1Ejsn3hnAwwjDH9lM")
                .header("Accept", "application/json")
                .asJson<string>().Body;

            var result = (JObject)JsonConvert.DeserializeObject(json);
            var last = result.Last.Value<JObject>();
            


            //foreach (var item in last)
            //{
            //    item.pr
            //}
            
            //.Value<List<Homonym>>();

            //var last = result.Last.ToDictionary(pair => pair.Key, pair => pair.Value);

            //last.to
            //{"word":"effect","pronunciation":{"noun":"'ɪ,fɛkt","verb":",ɪ'fɛkt"}}
            //var result = JsonConvert.DeserializeObject<KeyValuePair<string, JsonWord>>(json);

            //foreach (var p in result.
            //{
            //    Homonym homonym = new Homonym();
            //    homonym.pos = p.Key.ToString();
            //    homonym.phonentic = p.Value.ToString();
            //}

            return json;
        }

        public string Transcribe_RapidAI()
        {
            List<Parameter> body = new List<Parameter>();

            //body.Add(new DataParameter("ParameterKey1", "ParameterValue1"));
            //body.Add(new DataParameter("ParameterKey2", "ParameterValue2"));

            body.Add(new DataParameter("words", "ParameterValue1"));
            body.Add(new DataParameter("ParameterKey2", "ParameterValue2"));

            try
            {
                Dictionary<string, object> response = RapidApi.Call("APIName", "FunctionName", body.ToArray()).Result;
                object payload;
                if (response.TryGetValue("success", out payload))
                {

                }
                else
                {

                }
            }
            catch (RapidAPIServerException e)
            {

            }
            catch (Exception e)
            {

            }

            return "blah";
        }

        //public string blah()
        //{

        //    var client = new HttpClient();

        //    // Create the HttpContent for the form to be posted.
        //    var requestContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("text", "This is a block of text"),});

        //    // Get the response.
        //    //HttpResponseMessage response = await client.PostAsync("http://api.repustate.com/v2/demokey/score.json", requestContent);
        //    HttpResponseMessage response = client.PostAsync("http://api.repustate.com/v2/demokey/score.json", requestContent);

        //    // Get the response content.
        //    HttpContent responseContent = response.Content;

        //    // Get the stream of the content.
        //    using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
        //    {
        //        // Write the output.
        //        Console.WriteLine(await reader.ReadToEndAsync());
        //    }
        //}


    }
}
