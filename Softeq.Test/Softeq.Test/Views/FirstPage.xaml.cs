
using Softeq.Core.First;
using Softeq.Test.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Softeq.Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPage : ContentPage
    {
        private readonly FirstViewModel _viewModel;

        public FirstPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new FirstViewModel(DependencyService.Get<PolynomCreator>());
            PickInputFileButton.Clicked += _viewModel.OnFilePick;
            PickOutputFileButton.Clicked += _viewModel.OnFilePick;
        }
    }
}
