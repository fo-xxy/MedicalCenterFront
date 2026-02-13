namespace MedicalCenter.Views;

public partial class RegisterScreen : ContentPage
{
	public RegisterScreen()
	{
		InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, true);
        Title = "";

        BindingContext = new ViewModels.RegisterViewModel();

        NavigationPage.SetHasNavigationBar(this, false);
    }
}