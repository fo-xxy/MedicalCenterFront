using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalCenter.Services.Claims;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCenter.ViewModels.Claims
{
    public partial class ClaimExportViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        public bool IsNotBusy => !IsBusy;

        public ClaimExportViewModel()
        {
        }

        [RelayCommand]
        private async Task Export()
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {

                var claimsService = new ClaimsService();

                var filePath = await claimsService.ExportClaimsAsync();

                if (!string.IsNullOrEmpty(filePath))
                {
                  
                    await Launcher.Default.OpenAsync(new OpenFileRequest
                    {
                        Title = "Reporte de Reclamaciones",
                        File = new ReadOnlyFile(filePath)
                    });
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo obtener el archivo desde el servidor.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Ocurrió un problema: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}