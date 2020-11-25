using System;
using Xunit;

namespace Softeq.xUnit
{
    public class MouseLineTest_
    {
        [Theory]
        [InlineData(2, 2, 8)]
        [InlineData(1, 3, 7)]
        public void Test1(int n, int m, int actial)
        {
            var mouseLine = new Softeq.Core.Fourth.MouseLine(n, m);

            var expected = mouseLine.FindWithMathFunc();
            var expected2 = mouseLine.FindWithPhysicalSort();

            Assert.Equal(expected, actial);
            Assert.Equal(expected2, actial);
        }

        [Theory]
        [InlineData(-1, 2)]
        [InlineData(1001, 3)]
        [InlineData(1, -1)]
        [InlineData(1, 1001)]
        public void Test2(int n, int m)
        {
            Assert.Throws<Exception>(() => { new Softeq.Core.Fourth.MouseLine(n, m); });
        }
    }
}
