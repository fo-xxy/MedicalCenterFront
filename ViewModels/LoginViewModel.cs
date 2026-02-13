using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliJ.Lang.Annotations;
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
            if (IsBusy) return;

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Atención", "Por favor, complete todos los campos.", "Entendido");
                return;
            }

            try
            {
                IsBusy = true;

                await Task.Delay(1500);

                if (App.Current?.MainPage != null)
                {
                    await App.Current.MainPage.DisplayAlert(
                        "Acceso Concedido",
                        $"Bienvenido al sistema, {Email}",
                        "Continuar");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en Login: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
