using System.Linq;

namespace Softeq.Core.Second
{
    public class MileageCalculator
    {
        private int _n;
        private int _m;
        private int[] _spares;

        protected MileageCalculator(int n, int m, int[] array)
        {
            _n = n;
            _m = m;
            _spares = array;
        }

        public static MileageCalculator Create(int n, int m, int[] array) => new MileageCalculator(n, m, array);

        public double Calc()
        {
            return _m / _spares.Select(s => 1.0 / s).Sum();
        }
    }
}
