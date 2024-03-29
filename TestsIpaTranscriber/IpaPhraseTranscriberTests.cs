﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class IpaPhraseTranscriberTests
    {
       [TestMethod()]
        public void TranscribePhraseTest1()
        {
            IpaTranscriber ipa = new IpaTranscriber();

            var phrase = "I am a computer science student";

            var expected = "/aɪ æm eɪ kəm'pjutər 'saɪəns 'studənt/";
            string phrase_ipa = ipa.TranscribePhrase(phrase).Trim();
            Assert.AreEqual(phrase_ipa, expected);
        }

        [TestMethod()]
        public void TranscribePhraseTest2()
        {
            IpaTranscriber ipa = new IpaTranscriber();

            var phrase = "You are nice.";

            var expected = "/ju ɑr nis/";
            string phrase_ipa = ipa.TranscribePhrase(phrase).Trim();
            Assert.AreEqual(phrase_ipa, expected);
        }

        [TestMethod()]
        public void TranscribePhraseTest3()
        {
            IpaTranscriber ipa = new IpaTranscriber();
            //var phrase = "I am a student";

            var phrase = "You are a computational linguist.";

            var expected = "/ju ɑr eɪ ,kɑmpju'teɪʃʌnʌl 'lɪŋgwɪst/";
            string phrase_ipa = ipa.TranscribePhrase(phrase).Trim();
            Assert.AreEqual(phrase_ipa, expected);
        }

        [TestMethod()]
        public void TranscribePhraseTest4()
        {
            IpaTranscriber ipa = new IpaTranscriber();
            //var phrase = "I am a student";

            var phrase = "I am studying linguistics";

            var expected = "/aɪ æm 'stʌdiɪŋ lɪŋ'gwɪstɪks/";
            string phrase_ipa = ipa.TranscribePhrase(phrase).Trim();
            Assert.AreEqual(phrase_ipa, expected);
        }

        [TestMethod()]
        public void TranscribePhraseTest5()
        {
            IpaTranscriber ipa = new IpaTranscriber();
            //var phrase = "I am a student";

            var phrase = "Click on the microphone to transcribe your speech into the International Phonetic Alphabet (IPA)";

            var expected = "/aɪ æm 'stʌdiɪŋ lɪŋ'gwɪstɪks/";
            string phrase_ipa = ipa.TranscribePhrase(phrase).Trim();
            Assert.AreEqual(phrase_ipa, expected);
        }

        [TestMethod()]
        public void TranscribePhraseTest_OOV()
        {
            IpaTranscriber ipa = new IpaTranscriber();
            var phrase = "I have placed information vital to the survival of the Rebellion into the memory systems of this R2 unit.";
            var expected = "/aɪ hæv pleɪst ,ɪnfər'meɪʃən 'vaɪtəl tu ðʌ sər'vaɪvəl ʌv ðʌ rɪ'bɛljən ɪn'tu ðʌ 'mɛməri 'sɪstəm ʌv ðɪs <OOV> 'junɪt/";
            string phrase_ipa = ipa.TranscribePhrase(phrase).Trim();
            Assert.AreEqual(phrase_ipa, expected);
        }

        [TestMethod()]
        public void TranscribeWordTest_POS()
        {
            IpaTranscriber ipa = new IpaTranscriber();

            var input = "effect";

            var expected = "ju";
            //string output_ipa = ipa.TranscribePhrase(input).Trim();
            string output_ipa = ipa.Transcribe(input).Trim();
            Assert.AreEqual(output_ipa, expected);
        }

        //public void TranscribePhraseTest()
        //{
        //    IpaTranscriber ipa = new IpaTranscriber();

        //    string result = ipa.Transcribe("effect", "noun");
        //    Assert.Fail();
        //}

    }
}