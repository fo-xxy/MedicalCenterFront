using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCenter.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))] 
        private bool isBusy;

        public bool IsNotBusy => !IsBusy;

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Llene todos los campos", "OK");
                return;
            }

            IsBusy = true;

            var authService = new Services.AuthService();
            var result = await authService.LoginAsync(Email, Password);

            IsBusy = false;

            if (result != null && !string.IsNullOrEmpty(result.Token))
            {
                await SecureStorage.SetAsync("auth_token", result.Token);

           
                string nombreUsuario = result.User?.FullName ?? Email;

                await App.Current.MainPage.DisplayAlert("Bienvenido", $"Hola, {nombreUsuario}", "Entrar");

                //App.Current.MainPage = new NavigationPage(new Views.DashboardPage());


            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", result?.Message ?? "Credenciales incorrectas", "Intentar de nuevo");
            }
        }

        [RelayCommand]
        private async Task Register()
        {
            if (App.Current?.MainPage?.Navigation != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new Views.RegisterScreen());
            }
        }
    }
}
