using Softeq.Core.Third;
using System;
using Xunit;

namespace Softeq.xUnit
{
    public class MaxMultiplicationTest_
    {
        [Theory]
        [InlineData(3, 4, 12, 81)]
        [InlineData(4, 3, 12, 81)]
        [InlineData(3, 5, 13, 75)]
        [InlineData(3, 5, 30, 59049)]

        public void Test1(int a, int b, int n, double actual)
        {
            var maxMulti = new MaxMultiplication(a, b, n);

            var expected = maxMulti.Calculate();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(8, 7, 13)]
        public void Test2(int a, int b, int n)
        {
            var maxMulti = new MaxMultiplication(a, b, n);

            Assert.Throws<Exception>(() => maxMulti.Calculate());
        }
    }
}
