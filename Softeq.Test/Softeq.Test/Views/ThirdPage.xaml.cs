
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Softeq.Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThirdPage : ContentPage
    {


        public ThirdPage()
        {
            InitializeComponent();

            BindingContext = new ViewModels.ThirdViewModel(DependencyService.Get<Core.Third.MMCreator>());
        }
    }
}
