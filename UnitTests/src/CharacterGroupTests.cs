using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF12RNGHelper.Core;

namespace UnitTests
{
    [TestClass]
    public class CharacterGroupTests
    {
        [TestMethod]
        public void TestAddCharacterCharacterCount()
        {
            CharacterGroup group = new CharacterGroup();
            Assert.AreEqual(0, group.CharacterCount());

            Character char1 = TestUtils.GetDefaultCharacter();
            group.AddCharacter(char1);
            Assert.AreEqual(1, group.CharacterCount());

            group.AddCharacter(char1);
            group.AddCharacter(char1);
            group.AddCharacter(char1);
            Assert.AreEqual(4, group.CharacterCount());
        }

        [TestMethod]
        public void TestClearCharacters()
        {
            CharacterGroup group = TestUtils.GetDefaultCharacterGroup();
            group.ClearCharacters();
            Assert.AreEqual(0, group.CharacterCount());
        }

        [TestMethod]
        public void TestGetSetIndex()
        {
            CharacterGroup group = TestUtils.GetDefaultCharacterGroup();
            Assert.AreEqual(0, group.GetIndex());

            group.SetIndex(2);
            Assert.AreEqual(2, group.GetIndex());
        }

        [TestMethod]
        public void TestIncrementIndex()
        {
            CharacterGroup group = TestUtils.GetDefaultCharacterGroup();
            group.IncrimentIndex();
            Assert.AreEqual(1, group.GetIndex());
        }

        [TestMethod]
        public void TestIncrementIndexLoops()
        {
            CharacterGroup group = TestUtils.GetDefaultCharacterGroup();
            group.IncrimentIndex();
            group.IncrimentIndex();
            group.IncrimentIndex();
            Assert.AreEqual(0, group.GetIndex());
        }

        [TestMethod]
        public void TestResetIndex()
        {
            CharacterGroup group = TestUtils.GetDefaultCharacterGroup();
            group.IncrimentIndex();
            group.ResetIndex();
            Assert.AreEqual(0, group.GetIndex());
        }

        [TestMethod]
        public void TestGetHealMin()
        {
            CharacterGroup group = TestUtils.GetComplexCharacterGroup();
            Assert.AreEqual(86, group.HealMin());

            group.IncrimentIndex();
            Assert.AreEqual(130, group.HealMin());

            group.IncrimentIndex();
            Assert.AreEqual(195, group.HealMin());

            group.IncrimentIndex();
            Assert.AreEqual(86, group.HealMin());

            group.IncrimentIndex();
            Assert.AreEqual(130, group.HealMin());

            group.IncrimentIndex();
            Assert.AreEqual(195, group.HealMin());
        }

        [TestMethod]
        public void TestGetHealMax()
        {
            CharacterGroup group = TestUtils.GetComplexCharacterGroup();
            Assert.AreEqual(97, group.HealMax());

            group.IncrimentIndex();
            Assert.AreEqual(146, group.HealMax());

            group.IncrimentIndex();
            Assert.AreEqual(219, group.HealMax());

            group.IncrimentIndex();
            Assert.AreEqual(97, group.HealMax());

            group.IncrimentIndex();
            Assert.AreEqual(146, group.HealMax());

            group.IncrimentIndex();
            Assert.AreEqual(219, group.HealMax());
        }

        [TestMethod]
        public void TestValidateHealValue()
        {
            CharacterGroup group = new CharacterGroup();
            group.AddCharacter(TestUtils.GetDefaultCharacter());
            Assert.IsFalse(group.ValidateHealValue(85));

            Assert.IsTrue(group.ValidateHealValue(86));

            Assert.IsTrue(group.ValidateHealValue(92));

            Assert.IsTrue(group.ValidateHealValue(97));

            Assert.IsFalse(group.ValidateHealValue(98));

        }

        [TestMethod]
        public void TestGetHealValue()
        {
            CharacterGroup group = TestUtils.GetComplexCharacterGroup();
            Assert.AreEqual(91, group.GetHealValue(1372235862));

            Assert.AreEqual(137, group.GetHealValue(1372235862));

            Assert.AreEqual(215, group.GetHealValue(1372235862));

            Assert.AreEqual(91, group.GetHealValue(1372235862));

            Assert.AreEqual(137, group.GetHealValue(1372235862));

            Assert.AreEqual(215, group.GetHealValue(1372235862));
        }

        [TestMethod]
        public void TestPeekHealValue()
        {
            CharacterGroup group = TestUtils.GetComplexCharacterGroup();
            Assert.AreEqual(
                group.PeekHealValue(1372235862),
                group.PeekHealValue(1372235862));
        }

        [TestMethod]
        public void TestEmptyGroupThrowsException()
        {
            CharacterGroup group = new CharacterGroup();
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                delegate
                {
                    group.PeekHealValue(1372235862);
                }
            );

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                delegate
                {
                    group.GetHealValue(1372235862);
                }
            );

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                delegate
                {
                    group.HealMin();
                }
            );

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                delegate
                {
                    group.HealMax();
                }
            );
        }

        [TestMethod]
        public void TestSetIndex_ArgumentOutOfRange()
        {
            CharacterGroup group = TestUtils.GetDefaultCharacterGroup();
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                delegate
                {
                    group.SetIndex(3);
                }
            );
        }
    }
}
