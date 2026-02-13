using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalCenter.Models;
using MedicalCenter.Services;
using MedicalCenter.Services.Auth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace MedicalCenter.ViewModels
{

        public partial class RegisterViewModel : ObservableObject
        {
            [ObservableProperty]
            private string email;

            [ObservableProperty]
            private string password;

            [ObservableProperty]
            private string role;

            [ObservableProperty]
            [NotifyPropertyChangedFor(nameof(IsNotBusy))]
            private bool isBusy;

            public bool IsNotBusy => !IsBusy;

            [RelayCommand]
            private async Task Register()
            {
                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Role))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
                    return;
                }

                IsBusy = true;


            var registerRequest = new RegisterRequest
            {
                Email = Email,
                Password = Password,
                Role = Role
            };



            var apiService = new AuthApiService(); 
                var exito = await apiService.RegisterAsync(registerRequest);

                IsBusy = false;

                if (exito)
                {
                    await App.Current.MainPage.DisplayAlert("¡Éxito!", "Usuario creado correctamente", "OK");

                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No se pudo crear la cuenta. Verifica los datos o la conexión.", "Reintentar");
                }
            }

            [RelayCommand]
            private async Task GoBack()
            {
                await App.Current.MainPage.Navigation.PopAsync();
            }
        }
    } 



