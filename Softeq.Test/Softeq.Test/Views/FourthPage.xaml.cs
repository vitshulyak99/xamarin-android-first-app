using Softeq.Test.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Softeq.Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FourthPage : ContentPage
    {
        private FourthViewModel _viewmodel;

        public FourthPage()
        {
            InitializeComponent();

            BindingContext = _viewmodel = new FourthViewModel();
            FilePickButton.Clicked += _viewmodel.OnFilePick;
        }
    }
}
