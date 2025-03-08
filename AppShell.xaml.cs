using Kadam.Views;

namespace Kadam
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
            Routing.RegisterRoute(nameof(StudentRegistration), typeof(StudentRegistration));

            // Register other pages here
        }

        public void ShowAuthenticatedContent()
        {
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            //AuthenticatedContent.IsVisible = true;
            //LoginPage.IsVisible = false;
            DashboardItem.IsVisible = true;
            StudentRegistrationItem.IsVisible = true;
        }
        
    }
}
