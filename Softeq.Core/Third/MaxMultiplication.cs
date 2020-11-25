
using System;

namespace Softeq.Core.Third
{
    public class MaxMultiplication
    {
        private readonly double _a;
        private readonly double _b;
        private readonly double _n;

        public MaxMultiplication(double a, double b, double n)
        {
            if (1 < a && a < 10)
                _a = a;
            else throw new Exception("invalid value A");

            if (1 < b && b < 10)
                _b = b;
            else throw new Exception("invalid value B");

            if (10 < n && n < 100)
                _n = n;
            else throw new Exception("invalid value N");
        }


        public double Calculate()
        {
            (int maxRepeatA, int maxRepeatB) = (CheckRepeats(_a, GetMaxRepeat(_a, _n), _b), CheckRepeats(_b, GetMaxRepeat(_b, _n), _a));

            (int repA, int repB) = (maxRepeatA, (int)((_n - (maxRepeatA * _a)) / _b));
            (int repB2, int repA2) = (maxRepeatB, (int)((_n - (maxRepeatB * _b)) / _a));

            if (repA < 0 || repB2 < 0 || repB < 0 || repA2 < 0)
            {
                throw new Exception("N не является суммой чисел A и B");
            }

            double resultOne = repA * _a + repB * _b == _n ? Math.Pow(_a, repA) * Math.Pow(_b, repB) : 0;
            double resultTwo = repA2 * _a + repB2 * _b == _n ? Math.Pow(_a, repA2) * Math.Pow(_b, repB2) : 0;

            return resultOne < resultTwo ? resultTwo : resultOne;
        }

        private int GetMaxRepeat(double a, double n)
        {
            return (int)(n / a);
        }

        private int CheckRepeats(double x, int repeatX, double y)
        {
            while ((_n - x * repeatX) % y != 0)
            {
                repeatX--;
            }

            return repeatX;
        }
    }
}
