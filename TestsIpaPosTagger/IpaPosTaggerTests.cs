using Microsoft.VisualStudio.TestTools.UnitTesting;
using IpaPosTagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpaPosTagger.Tests
{
    [TestClass()]
    public class IpaPosTaggerTests
    {
        [TestMethod()]
        public void TagPosTest()
        {
            IpaPosTagger tagger = new IpaPosTagger();
            tagger.TagPos("blah");
            Assert.Fail();
        }
    }
}