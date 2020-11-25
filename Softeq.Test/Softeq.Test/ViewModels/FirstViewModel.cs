using Plugin.FilePicker;
using Softeq.Core.First;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace Softeq.Test.ViewModels
{
    class FirstViewModel : BaseViewModel
    {
        private readonly PolynomCreator _polynomCreator;

        private string _polynomText = string.Empty;
        private readonly List<double> _members;
        private string _outputDataFileName;
        private string _inputDataFileName;
        private string _textEditor;
        private string _fileInputPath;
        private string _fileOutputPath;


        private FirstViewModel()
        {
            _members = new List<double>(6);
            Add = new Command(AddCommand);
            Clear = new Command(ClearCommand);
            Calculate = new Command(CalculateCommand);


        }

        public FirstViewModel(PolynomCreator polynomCreator) : this()
        {
            _polynomCreator = polynomCreator;
        }

        public Command Add { get; }

        public Command Clear { get; }

        public Command Calculate { get; }
        public string PolynomText { get => _polynomText; }



        public ObservableCollection<double> Output { get; } = new ObservableCollection<double>();

        public string TextEditor
        {
            get => _textEditor;
            set => SetProperty(ref _textEditor, value);
        }

        public string InputDataFileName => _inputDataFileName;
        public string OutputDataFileName => _outputDataFileName;


        public async void OnFilePick(object sender, EventArgs e)
        {
            var file = await CrossFilePicker.Current.PickFile();

            if (file != null)
            {
                var clickedButton = sender as Button;

                if (clickedButton.Text == "InputDataFile")
                {
                    Output.Clear();
                    _fileInputPath = file.FilePath;
                    SetProperty(ref _inputDataFileName, file.FileName, nameof(InputDataFileName));
                }
                else if (clickedButton.Text == "OutputDataFile")
                {
                    if (file.FilePath == _fileInputPath)
                    {
                        MakeMessage("input and output file must not match");
                    }
                    else
                    {
                        _fileOutputPath = file.FilePath;
                        SetProperty(ref _outputDataFileName, file.FileName, nameof(OutputDataFileName));
                    }
                }
            }
        }

        private string MakePolynomText()
        {
            string result = string.Empty;

            for (int i = 0; i < _members.Count; i++)
            {
                var degree = _members.Count - 1 - i;
                var a = _members[i];

                if (a != 0)
                {
                    if (degree == 0)
                    {
                        if (i == 0)
                        {
                            result += a > 0 ? $"{a}" : a.ToString();
                        }
                        else
                        {
                            result += a > 0 ? $"+{a}" : a.ToString();
                        }
                    }
                    else if (i == 0 && degree > 1)
                    {
                        result += a > 0 ? $"{a}x^{degree}" : $"{a}x^{degree}";
                    }
                    else if (i == 0 && degree == 1)
                    {
                        result += a > 0 ? $"{a}x" : $"{a}x";
                    }
                    else
                    {
                        result += a > 0 ? $"+{a}x^{degree}" : $"{a}x^{degree}";
                    }
                }
            }

            return result;
        }


        private void CalculateCommand()
        {
            try
            {
                if (string.IsNullOrEmpty(_fileInputPath))
                {
                    throw new Exception("Choice the input file!");
                }

                var lines = ReadFile(_fileInputPath);
                var polynom = _polynomCreator.Create(_members);

                foreach (var line in lines)
                {
                    if (double.TryParse(line, out double x) && x < 1001 && x > -1001)
                    {
                        Output.Add(polynom.WithPrecision(3)(x));
                    }
                    else
                    {
                        MakeMessage("file contains not valid data");
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(_fileOutputPath))
                {
                    WriteFile(_fileOutputPath, Output.Cast<string>().ToArray());
                    MakeMessage($"Succesfuly write result to file:{_fileOutputPath}");

                }

            }
            catch (Exception e)
            {
                MakeMessage(e.Message);
            }
        }

        private void ClearCommand()
        {

            _members.Clear();
            SetProperty(ref _polynomText, MakePolynomText(), nameof(PolynomText));
        }
        private void AddCommand()
        {

            if (_members.Count <= 5)
            {
                if (double.TryParse(TextEditor, out double val))
                {
                    _members.Add(val);
                    SetProperty(ref _polynomText, MakePolynomText(), nameof(PolynomText));
                    SetProperty(ref _textEditor, string.Empty, nameof(TextEditor));
                }
            }
            else
            {
                MakeMessage("5 max polinom degree");
            }
        }

        private string[] ReadFile(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    var firstLine = reader.ReadLine();

                    if (int.TryParse(firstLine, out int times))
                    {
                        string[] lines = new string[times];

                        for (int i = 0; i < times; i++)
                        {
                            lines[i] = reader.ReadLine();
                        }

                        return lines;
                    }
                    else
                    {
                        throw new System.Exception();
                    }
                }
            }
            else
            {
                return System.Array.Empty<string>();
            }
        }

        private void WriteFile(string path, string[] lines)
        {

            using (StreamWriter writer = new StreamWriter(path))
            {

                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}
