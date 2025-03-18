using NCD.Views;

namespace NCD
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(Dashboard), typeof(Dashboard));
            Routing.RegisterRoute(nameof(Registration), typeof(Registration));
            Routing.RegisterRoute(nameof(RegistrationList), typeof(RegistrationList));

            // Register other pages here
        }

        public void ShowAuthenticatedContent()
        {
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            //AuthenticatedContent.IsVisible = true;
            //LoginPage.IsVisible = false;
            DashboardItem.IsVisible = true;
            RegistrationItem.IsVisible = true;
            RegistrationListItem.IsVisible = true;
        }
        
    }
}
