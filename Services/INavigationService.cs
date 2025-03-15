using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCD.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync(string route);
        Task NavigateToAsync(string route, IDictionary<string, object> parameters);
        Task GoBackAsync();
        Task NavigateToMainPageAsync();
        Task NavigateToLoginPageAsync(); // Add this method
    }

    // Services/NavigationService.cs
    public class NavigationService : INavigationService
    {
        public async Task NavigateToAsync(string route)
        {
            await Shell.Current.GoToAsync(route);
        }

        public async Task NavigateToAsync(string route, IDictionary<string, object> parameters)
        {
            await Shell.Current.GoToAsync(route, parameters);
        }

        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task NavigateToMainPageAsync()
        {
            if (Shell.Current is AppShell appShell)
            {
                appShell.ShowAuthenticatedContent();
            }
            await Shell.Current.GoToAsync("//Dashboard");
        }
        public async Task NavigateToLoginPageAsync() // Implement this method
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
