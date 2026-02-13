using MedicalCenter.ViewModels.Claims;

namespace MedicalCenter.Views.Claims;

public partial class ClaimImportScreen : ContentPage
{
	public ClaimImportScreen()
	{
		InitializeComponent();

        BindingContext = new ClaimImportViewModel();
    }
}