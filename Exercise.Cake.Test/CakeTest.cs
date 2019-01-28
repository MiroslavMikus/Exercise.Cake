using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.Cake.Test
{
    [TestClass]
    public class CakeTest
    {
        [TestMethod]
        public void TestSum()
        {
            Assert.AreEqual(5, 2 + 3);
        }

        [TestMethod]
        public void TestSubstraction()
        {
            Assert.AreEqual(-1, 2 - 3);
        }
    }
}
