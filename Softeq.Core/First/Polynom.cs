using System;
using System.Collections.Generic;
using System.Linq;

namespace Softeq.Core.First
{
    public class Polynom
    {
        private readonly List<Func<double, double>> _members;

        public Polynom(params double[] odds)
        {
            if (odds.Length > 6)
            {
                throw new Exception("max polynom degree is five");
            }

            _members = new List<Func<double, double>>();

            for (int i = 0; i < odds.Length; i++)
            {
                _members.Add(MakeFx(odds[i], odds.Length - 1 - i));
            }

        }

        public double Calculate(double x) => _members.Select(m => m(x)).Sum();

        public Func<double, double> WithPrecision(int precision) => x => Math.Round(Calculate(x), precision);

        private Func<double, double> MakeFx(double odd, int degree)
            => x => odd * Math.Pow(x, degree);
    }
}
