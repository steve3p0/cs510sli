using Microsoft.VisualStudio.TestTools.UnitTesting;
using IpaTranscriber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpaTranscriber.Tests
{
    [TestClass()]
    public class IpaTranscriberTests
    {
        [TestMethod()]
        public void TranscribeTest()
        {
            IpaTranscriber ipa = new IpaTranscriber();

            string result = ipa.Transcribe("test");
            Assert.Fail();
        }
    }
}