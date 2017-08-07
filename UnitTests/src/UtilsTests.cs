using FF12RNGHelper.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void TestRandToPercent()
        {
            Assert.AreEqual(0, Utils.RandToPercent(0));
            Assert.AreEqual(20, Utils.RandToPercent(20));
            Assert.AreEqual(99, Utils.RandToPercent(99));

            Assert.AreEqual(0, Utils.RandToPercent(100));
            Assert.AreEqual(1, Utils.RandToPercent(101));

            Assert.AreEqual(62, Utils.RandToPercent(1372235862)); 
        }
    }
}
