using MedicalCenter.ViewModels.Claims;

namespace MedicalCenter.Views.Claims;

public partial class ClaimExportScreen : ContentPage
{
	public ClaimExportScreen()
	{
		InitializeComponent();

        BindingContext = new ClaimExportViewModel();
    }
}