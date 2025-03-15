namespace NCD.Services
{
    public interface IToolbarService
    {
        void AddLogoutButton(ContentPage page);
    }

    public class ToolbarService : IToolbarService
    {
        private readonly INavigationService _navigationService;

        public ToolbarService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public void AddLogoutButton(ContentPage page)
        {
            page.ToolbarItems.Add(new ToolbarItem
            {
                Text = "Logout",
                IconImageSource = "logout_icon.png", // Replace with your icon path
                Command = new Command(async () => await OnLogoutClicked(page))
            });
        }

        private async Task OnLogoutClicked(ContentPage page)
        {
            bool confirmLogout = await page.DisplayAlert("Confirm Logout", "Are you sure you want to log out?", "Yes", "No");
            if (confirmLogout)
            {
                var navigationStack = Shell.Current.Navigation.NavigationStack.ToList();
                foreach (var navPage in navigationStack)
                {
                    if (navPage != null)
                    {
                        Shell.Current.Navigation.RemovePage(navPage);
                    }
                }
                await _navigationService.NavigateToLoginPageAsync();
            }
        }
    }
}
