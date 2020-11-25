using Softeq.Core.Third;
using System;
using Xamarin.Forms;

namespace Softeq.Test.ViewModels
{
    class ThirdViewModel : BaseViewModel
    {
        private readonly MMCreator _creator;

        private int _a;
        private int _b;
        private int _n;
        private double _result;

        private ThirdViewModel()
        {
            Calculate = new Command(CalculateCommand);
        }

        public ThirdViewModel(MMCreator creator) : this()
        {
            _creator = creator;
        }

        public double Result => _result;

        public int A { get => _a; set => SetProperty(ref _a, value, nameof(A)); }

        public int B { get => _b; set => SetProperty(ref _b, value, nameof(B)); }

        public int N { get => _n; set => SetProperty(ref _n, value, nameof(N)); }

        public Command Calculate { get; }

        private void CalculateCommand()
        {
            if (_a < 1 || _b < 1 || _n < 1)
            {
                MakeMessage("Fill in the fields with numbers greater than 0");
                return;
            }

            try
            {
                var mm = _creator.Create(_a, _b, _n);
                var result = mm.Calculate();

                SetProperty(ref _result, result, nameof(Result));
            }
            catch (Exception e)
            {
                MakeMessage(e.Message);
            }

        }
    }
}
