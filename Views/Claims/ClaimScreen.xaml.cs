using MedicalCenter.ViewModels.Claims;

namespace MedicalCenter.Views.Claims;

public partial class ClaimScreen : ContentPage
{
	public ClaimScreen()
	{
		InitializeComponent();

        BindingContext = new ClaimsListViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ClaimsListViewModel viewModel)
        {
            ClaimsList.Opacity = 0;

            await viewModel.LoadClaimsCommand.ExecuteAsync(null);

            await ClaimsList.FadeTo(1, 600, Easing.CubicIn);
        }
    }
}