using FF12RNGHelper.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ComboTests
    {

        [TestMethod]
        public void IndexOfPreviousComboRng_Correct()
        {
            Assert.IsTrue(
                Combo.IndexOfPreviousComboRng.Equals(5));
        }

        [TestMethod]
        public void RngConsumedForAttack_Correct()
        {
            Assert.IsTrue(
                Combo.RngConsumedForAttack.Equals(10));
        }

        [TestMethod]
        public void IsSuccessful_True()
        {
            Assert.IsTrue(Combo.IsSucessful(0));
        }

        [TestMethod]
        public void IsSuccessful_False()
        {
            Assert.IsFalse(Combo.IsSucessful(99));
        }

        [TestMethod]
        public void IsSuccessful_True_EdgeCase()
        {
            Assert.IsTrue(Combo.IsSucessful(2));
        }

        [TestMethod]
        public void IsSuccessful_False_EdgeCase()
        {
            Assert.IsFalse(Combo.IsSucessful(3));
        }
    }
}
