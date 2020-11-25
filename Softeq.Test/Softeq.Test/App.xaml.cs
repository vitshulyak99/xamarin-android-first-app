using Softeq.Core.First;
using Softeq.Core.Third;
using Xamarin.Forms;

namespace Softeq.Test
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<PolynomCreator>();
            DependencyService.Register<MMCreator>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
