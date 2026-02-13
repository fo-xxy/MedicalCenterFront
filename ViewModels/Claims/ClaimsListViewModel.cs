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
    public partial class ClaimsListViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Claim> claims;

        [ObservableProperty]
        private bool isRefreshing;

        [ObservableProperty]
        private bool isLoading;

        private bool _isAscending = true;
        private string _lastSortColumn = string.Empty;


        public ClaimsListViewModel()
        {
            Claims = new ObservableCollection<Claim>();
        }

        [RelayCommand]
        private async Task LoadClaims()
        {
            try
            {
                IsRefreshing = true;

                if (Claims.Count == 0) IsLoading = true;

                var claimsService = new ClaimsService();
                var listaDesdeApi = await claimsService.GetClaimsAsync();

                if (listaDesdeApi != null)
                {
                    Claims.Clear();
                    foreach (var item in listaDesdeApi)
                    {
                        Claims.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error de conexión", "OK");
            }
            finally
            {
                IsRefreshing = false;
                IsLoading = false; 
            }
        }

        [RelayCommand]
        private void Sort(string column)
        {
            if (Claims == null || !Claims.Any()) return;

            if (_lastSortColumn == column)
                _isAscending = !_isAscending;
            else
                _isAscending = true;

            _lastSortColumn = column;

            List<Claim> sorted;

            switch (column)
            {
                case "Id":
                    sorted = _isAscending
            ? Claims.OrderBy(c => int.TryParse(c.ClaimNumber, out int n) ? n : int.MaxValue).ToList()
            : Claims.OrderByDescending(c => int.TryParse(c.ClaimNumber, out int n) ? n : int.MinValue).ToList();
                    break;
                case "Patient":
                    sorted = _isAscending
                        ? Claims.OrderBy(c => c.PatientName).ToList()
                        : Claims.OrderByDescending(c => c.PatientName).ToList();
                    break;
                case "Amount":
                    sorted = _isAscending
                        ? Claims.OrderBy(c => c.Amount).ToList()
                        : Claims.OrderByDescending(c => c.Amount).ToList();
                    break;
                default:
                    return;
            }

            var temp = new List<Claim>(sorted);
            Claims.Clear();
            foreach (var item in temp) Claims.Add(item);
        }
    }
}
