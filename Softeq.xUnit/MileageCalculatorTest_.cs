using Softeq.Core.Second;
using Xunit;

namespace Softeq.xUnit
{
    public class MileageCalculatorTest_
    {
        [Theory]
        [InlineData(4, 4, 1500, 3000, 3000, 1000, 1000)]
        [InlineData(4, 6, 2000, 2000, 2000, 1000, 1000)]
        public void Test(int n, int m, double actual, params int[] ps)
        {
            var expected = MileageCalculator.Create(n, m, ps).Calc();

            Assert.Equal(expected, actual);
        }
    }
}
