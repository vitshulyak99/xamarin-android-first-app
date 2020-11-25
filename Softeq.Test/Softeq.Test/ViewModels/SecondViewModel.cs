using Plugin.FilePicker;
using Softeq.Core.Second;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace Softeq.Test.ViewModels
{
    class SecondViewModel : BaseViewModel
    {
        private string _result = string.Empty;
        private string _fileName = string.Empty;

        private int _n;
        private int _m;
        private IEnumerable<int> _spares;

        public SecondViewModel()
        {
            Calculate = new Command(CalculateCommand);
        }

        public string FileName => _fileName;

        public Command Calculate { get; }

        public string Result => _result;

        public async void OnFilePick(object sender, EventArgs e)
        {
            var file = await CrossFilePicker.Current.PickFile();

            if (file != null)
            {
                SetProperty(ref _fileName, file.FileName, nameof(FileName));
                try
                {
                    using (StreamReader reader = new StreamReader(file.GetStream()))
                    {
                        var p = reader.ReadLine().Trim().Split(' ').Select(int.Parse).ToArray();

                        (_n, _m) = (p[0], p[1]);

                        var vs = new List<int>(_n);

                        for (int i = 0; i < _n; i++)
                        {
                            vs.Add(int.Parse(reader.ReadLine()));
                        }

                        _spares = vs;
                    }
                }
                catch (Exception ex)
                {
                    MakeMessage(ex.Message);
                }
            }
        }
        private void CalculateCommand()
        {
            try
            {
                var calculator = MileageCalculator.Create(_n, _m, _spares.ToArray());

                var res = Math.Round(calculator.Calc(), 3);

                SetProperty(ref _result, res.ToString(), nameof(Result));
            }
            catch (Exception e)
            {
                MakeMessage(e.Message);
            }
        }

    }
}
