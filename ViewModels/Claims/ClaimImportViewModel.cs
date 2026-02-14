using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalCenter.Models;
using MedicalCenter.Services.Claims;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        [ObservableProperty]
        private ObservableCollection<ClaimImport> _importHistory = new();

        public ClaimImportViewModel()
        {
            _ = LoadHistoryAsync();
        }
        [RelayCommand]
        private async Task PickFile()
        {
            try
            {
                var csvFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                { DevicePlatform.Android, new[] { "text/csv", "text/comma-separated-values" } },
                { DevicePlatform.iOS, new[] { "public.comma-separated-values-text" } },
                { DevicePlatform.WinUI, new[] { ".csv" } }
                    });

                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Seleccione el archivo de Claims (Formato CSV)",
                    FileTypes = csvFileType
                });

                if (result != null)
                {
                    var extension = Path.GetExtension(result.FileName).ToLower();

                    if (extension == ".csv")
                    {
                        FileName = result.FileName;
                        FilePath = result.FullPath;
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Archivo inválido",
                            "El sistema requiere un archivo CSV.", "OK");

                        FileName = string.Empty;
                        FilePath = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo acceder al archivo: " + ex.Message, "OK");
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


        [RelayCommand]
        public async Task LoadHistoryAsync()
        {
            try
            {
                var claimsService = new ClaimsService();

                var history = await claimsService.GetClaimsHistoryAsync();

                ImportHistory.Clear();
                foreach (var item in history)
                {
                    ImportHistory.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo cargar el historial", "OK");
            }
        }
    }
}
