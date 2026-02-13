namespace MedicalCenter.Views;

public partial class LoginScreen : ContentPage
{
	public LoginScreen()
	{
        InitializeComponent();
        BindingContext = new ViewModels.LoginViewModel();

        NavigationPage.SetHasNavigationBar(this, false);
    }
}