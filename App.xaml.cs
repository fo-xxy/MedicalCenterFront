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
            return new Window(new LoginScreen());
        }
    }
}