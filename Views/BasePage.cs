namespace Kadam.Views
{
    public partial class BasePage : ContentPage
    {
        public BasePage()
        {
            // Add the toolbar item for logout
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Logout",
                IconImageSource = "logout_icon.png", // Replace with your icon path
                Command = new Command(OnLogoutClicked)
            });
        }

        private async void OnLogoutClicked()
        {
            // Handle logout logic here
            await DisplayAlert("Logout", "You have logged out.", "OK");
            // Navigate to login page or perform other actions
        }
    }
}