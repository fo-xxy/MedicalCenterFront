using MedicalCenter.Views;
using MedicalCenter.Views.Claims;
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
            var token = Task.Run(async () => await SecureStorage.Default.GetAsync("auth_token")).Result;

            var shell = new AppShell();

            if (!string.IsNullOrEmpty(token))
            {
       
                Task.Run(async () => await shell.GoToAsync("//ClaimsPage"));
            }
            else
            {
                Task.Run(async () => await shell.GoToAsync("//Login"));
            }

            return new Window(shell); 
        }
    }
}