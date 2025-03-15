using NCD.ViewModels;

namespace NCD.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Disable the left menu
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Re-enable the left menu when leaving the page
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
    }
}