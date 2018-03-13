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
    public class IpaWordTranscriberTests
    {
        [TestMethod()]
        public void TranscribeWordTest1()
        {
            IpaTranscriber ipa = new IpaTranscriber();

            var input = "you";
            var expected = "ju";
            string output_ipa = ipa.Transcribe(input).Trim();
            Assert.AreEqual(output_ipa, expected);
        }

        [TestMethod()]
        public void TranscribeWordTest2()
        {
            IpaTranscriber ipa = new IpaTranscriber();

            var input = "are";
            var expected = "ɑr";
            string output_ipa = ipa.Transcribe(input).Trim();
            Assert.AreEqual(output_ipa, expected);
        }

        [TestMethod()]
        public void TranscribeWordTest3()
        {
            IpaTranscriber ipa = new IpaTranscriber();

            var input = "kbapmftrkl";
            var expected = "<OOV>";
            string output_ipa = ipa.Transcribe(input).Trim();
            Assert.AreEqual(output_ipa, expected);
        }


        [TestMethod()]
        public void TranscribeWordOovTest1()
        {
            IpaTranscriber ipa = new IpaTranscriber();
            var phrase = "stormtrooper";
            var expected = "/<OOV>/";
            string phrase_ipa = ipa.Transcribe(phrase).Trim();
            Assert.AreEqual(phrase_ipa, expected);
        }

        [TestMethod()]
        public void TranscribeWordOovTest2()
        {
            IpaTranscriber ipa = new IpaTranscriber();
            var phrase = "happened";
            var expected = "/<OOV>/";
            string phrase_ipa = ipa.Transcribe(phrase).Trim();
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