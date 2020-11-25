

using System.Linq;

namespace Softeq.Core.First
{
    public class PolynomCreator
    {
        public Polynom Create(System.Collections.Generic.IEnumerable<double> odds) => new Polynom(odds.ToArray());

        public Polynom Create(params double[] odds) => new Polynom(odds);
    }
}
