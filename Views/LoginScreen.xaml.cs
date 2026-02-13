namespace MedicalCenter.Views;

public partial class LoginScreen : ContentPage
{
	public LoginScreen()
	{
		InitializeComponent();

        BindingContext = new MedicalCenter.ViewModels.LoginViewModel();
    }
}