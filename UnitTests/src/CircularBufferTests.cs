using FF12RNGHelper.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CircularBufferTests
    {
        [TestMethod]
        public void TestCircularBufferWraps()
        {
            CircularBuffer<int> buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);

            Assert.AreEqual(4, buffer[0]);
            Assert.AreEqual(2, buffer[1]);
            Assert.AreEqual(3, buffer[2]);
        }

        [TestMethod]
        public void TestCircularBufferSetsIndexes()
        {
            CircularBuffer<int> buffer = new CircularBuffer<int>(3)
            {
                [0] = 3,
                [1] = 2,
                [2] = 1
            };

            Assert.AreEqual(3, buffer[0]);
            Assert.AreEqual(2, buffer[1]);
            Assert.AreEqual(1, buffer[2]);
        }

        [TestMethod]
        public void TestCircularBufferDeepClones()
        {
            CircularBuffer<int> original = new CircularBuffer<int>(3);
            original.Add(1);
            original.Add(2);

            CircularBuffer<int> clone = original.DeepClone();
            original.Add(3);
            clone.Add(4);

            Assert.AreEqual(original[0], clone[0]);
            Assert.AreEqual(original[1], clone[1]);
            Assert.AreNotEqual(original[2], clone[2]);
        }

        [TestMethod]
        public void TestCircularBufferHandlesNegativeIndexes()
        {
            CircularBuffer<int> buffer = new CircularBuffer<int>(3)
            {
                [-3] = 1,
                [-2] = 2,
                [-1] = 3
            };

            Assert.AreEqual(1, buffer[0]);
            Assert.AreEqual(2, buffer[1]);
            Assert.AreEqual(3, buffer[2]);
        }

        [TestMethod]
        public void TestCircularBufferWrapsBackwards()
        {
            CircularBuffer<int> buffer = new CircularBuffer<int>(3)
            {
                [-1] = 3,
                [-2] = 2,
                [-3] = 1,
                [-4] = 0
            };

            Assert.AreEqual(1, buffer[0]);
            Assert.AreEqual(2, buffer[1]);
            Assert.AreEqual(0, buffer[2]);
        }
    }
}
