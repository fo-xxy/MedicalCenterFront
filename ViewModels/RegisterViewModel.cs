using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalCenter.Services;
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
                // 1. Validaciones básicas
                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Role))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
                    return;
                }

                IsBusy = true;

                var apiService = new AuthService(); 
                var exito = await apiService.RegisterAsync(Email, Password, Role);

                IsBusy = false;

                // 3. Respuesta al usuario
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



