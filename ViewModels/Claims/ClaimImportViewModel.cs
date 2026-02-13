using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalCenter.Services.Claims;
using System;
using System.Collections.Generic;
using System.Text;


namespace MedicalCenter.ViewModels.Claims
{
    public partial class ClaimImportViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanImport))]
        private string _fileName = "Seleccione el archivo.";

        [ObservableProperty]
        private string _filePath;

        [ObservableProperty]
        private bool _isBusy;

        public bool IsNotBusy => !IsBusy;
        public bool CanImport => !string.IsNullOrEmpty(FilePath) && !IsBusy;

        public ClaimImportViewModel()
        {
        }
        [RelayCommand]
        private async Task PickFile()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Seleccione un archivo"
                });

                if (result != null)
                {
                    FileName = result.FileName;
                    FilePath = result.FullPath;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task Upload()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                await Shell.Current.DisplayAlert("Atención", "Por favor, seleccione un archivo primero.", "OK");
                return;
            }

            IsBusy = true; 
            try
            {
                var claimsService = new ClaimsService();

                bool success = await claimsService.ImportClaimsAsync(FilePath);

                if (success)
                {
                    await Shell.Current.DisplayAlert("Completado", "Las reclamaciones se importaron con éxito.", "OK");
                    FileName = "Seleccione el archivo.";
                    FilePath = string.Empty;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Hubo un problema al subir el archivo al servidor.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task Help()
        {
            await Shell.Current.DisplayAlert("Ayuda", "El archivo debe tener las columnas: Paciente, Monto y Fecha.", "Entendido");
        }
    }
}
