using Microsoft.VisualStudio.TestTools.UnitTesting;
using IpaTranscriber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IpaTranscriber.Tests
{
    [TestClass()]
    public class IpaTranscriberTests
    {
        //[TestMethod()]
        //public void TranscribeTest()
        //{
        //    IpaTranscriber ipa = new IpaTranscriber();

        //    //string result = ipa.Transcribe("effect", "noun");
        //    string result = ipa.Transcribe("effect");
        //    Assert.AreEqual(result, "'ɪ,fɛkt");
        //}

        //public void TranscribePhraseTest_old()
        //{
        //    IpaTranscriber ipa = new IpaTranscriber();

        //    var regex = new Regex(@"\b[\s,\.-:;]*");
        //    var phrase = "I am a student";
        //    var words = regex.Split(phrase).Where(x => !string.IsNullOrEmpty(x));

        //    string phrase_ipa = "";

        //    foreach (var word in words)
        //    {
        //        //string result = ipa.Transcribe("effect", "noun");
        //        phrase_ipa += ipa.Transcribe(word) + " ";
        //    }

        //    //Assert.AreEqual(result, "'ɪ,fɛkt");
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void TranscribePhraseTest()
        {
            IpaTranscriber ipa = new IpaTranscriber();
            var phrase = "I am a student";
            var expected = "/aɪ æm eɪ 'studənt/";
            string phrase_ipa = ipa.TranscribePhrase(phrase).Trim();
            Assert.AreEqual(phrase_ipa, expected);
        }

        //public void TranscribePhraseTest()
        //{
        //    IpaTranscriber ipa = new IpaTranscriber();

        //    string result = ipa.Transcribe("effect", "noun");
        //    Assert.Fail();
        //}

    }
}