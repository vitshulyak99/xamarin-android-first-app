using Softeq.Test.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Softeq.Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondPage : ContentPage
    {
        private SecondViewModel _viewModel;

        public SecondPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SecondViewModel();
            FilePickButton.Clicked += _viewModel.OnFilePick;

        }
    }
}
