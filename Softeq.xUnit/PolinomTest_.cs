using Softeq.Core.First;
using System;
using Xunit;

namespace Softeq.xUnit
{
    public class PolynomTest_
    {

        [Theory]
        [InlineData(1, 1, 1, 1, 1, 1)]
        [InlineData(2, 1, 2, 1, 5, 1)]
        [InlineData(2.5, 12, 32, 51, 5, 10)]
        [InlineData(3.2, 11, -2, 1, 55, 1)]

        public void Test(
            double x,
            double a,
            double b,
            double c,
            double d,
            double e
            )
        {
            double pow(double x, double y) => Math.Pow(x, y);
            double testP(double x) => a * pow(x, 4) + b * pow(x, 3) + c * pow(x, 2) + d * pow(x, 1) + e * pow(x, 0);
            var polynom = new Polynom(a, b, c, d, e);

            var result1 = testP(x);
            var result2 = polynom.Calculate(x);


            Assert.Equal(result1, result2);
        }
    }
}
