using MedicalCenter.Views;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalCenter
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var navigationPage = new NavigationPage(new MedicalCenter.Views.LoginScreen());

            return new Window(navigationPage);
        }
    }
}