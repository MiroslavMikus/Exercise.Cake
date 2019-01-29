using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.Cake.Test
{
    [TestClass]
    public class ExampleTest
    {
        [TestMethod]
        public void TestSum()
        {
            var givenCalc = new Calculator();

            var actual = givenCalc.Sum(3, 6);

            actual.Should().Be(9);
        }

        [TestMethod]
        public void TestSubstract()
        {
            var givenCalc = new Calculator();

            var actual = givenCalc.Substract(3, 6);

            actual.Should().Be(-3);
        }
    }
}
