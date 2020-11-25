using Plugin.FilePicker;
using Softeq.Core.Fourth;
using System;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace Softeq.Test.ViewModels
{
    class FourthViewModel : BaseViewModel
    {
        private int _n;
        private int _m;
        private string _result;
        private string _fileName;

        public FourthViewModel()
        {
            Calculate = new Command(() =>
            {
                try
                {
                    var result = MouseLine.Create(_n, _m).FindWithMathFunc();
                    SetProperty(ref _result, result.ToString(), nameof(Result));

                }
                catch (Exception e)
                {
                    MakeMessage(e.Message);
                }
            });
        }

        public int N => _n;

        public int M => _m;

        public string Result => _result;

        public string FileName => _fileName;

        public async void OnFilePick(object sender, EventArgs eventArgs)
        {
            var file = await CrossFilePicker.Current.PickFile();

            if (file != null)
            {
                SetProperty(ref _fileName, file.FileName, nameof(FileName));

                try
                {
                    string line = string.Empty;
                    using (StreamReader reader = new StreamReader(file.GetStream()))
                    {
                        line = reader.ReadLine().Trim();
                    }

                    var nm = line.Split(' ').Select(int.Parse).ToArray();

                    SetProperty(ref _n, nm[0], nameof(N));
                    SetProperty(ref _m, nm[1], nameof(M));

                }
                catch (FormatException)
                {
                    MakeMessage("File contains invalid data");
                }
                catch (Exception e)
                {
                    MakeMessage(e.Message);
                }
            }
        }

        public Command Calculate { get; }
    }
}
