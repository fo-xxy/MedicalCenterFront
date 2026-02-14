using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalCenter.Models;
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

            var loginRequest = new LoginRequest
            {
                Email = Email,
                Password = Password
            };

            var authService = new Services.Auth.AuthApiService();
            var result = await authService.LoginAsync(loginRequest);

            IsBusy = false;

            if (result != null && !string.IsNullOrEmpty(result.Token))
            {
                await SecureStorage.SetAsync("auth_token", result.Token);


                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage = new AppShell();
                });
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
