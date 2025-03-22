using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NCD.Services;
using System.ComponentModel.DataAnnotations;
using NCD.DTO;
using Newtonsoft.Json;

namespace NCD.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasEmailError))]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string email;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasPasswordError))]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string password;

        [ObservableProperty]
        private string emailError;

        [ObservableProperty]
        private string passwordError;

        [ObservableProperty]
        private bool isLoading;

        public bool HasEmailError => !string.IsNullOrEmpty(EmailError);
        public bool HasPasswordError => !string.IsNullOrEmpty(PasswordError);
        public bool IsNotLoading => !IsLoading;

        private readonly INavigationService _navigationService;

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private bool ValidateInput()
        {
            var isValid = true;

            // Email validation
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "Email is required";
                isValid = false;
            }
            //else if (!new EmailAddressAttribute().IsValid(Email))
            //{
            //    EmailError = "Invalid email format";
            //    isValid = false;
            //}
            else
            {
                EmailError = string.Empty;
            }

            // Password validation
            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordError = "Password is required";
                isValid = false;
            }
            else if (Password.Length < 6)
            {
                PasswordError = "Password must be at least 6 characters";
                isValid = false;
            }
            else
            {
                PasswordError = string.Empty;
            }

            return isValid;
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task LoginAsync()
        {
            if (!ValidateInput())
                return;

            try
            {
                IsLoading = true;
                
                var loginRequest = new LoginRequestDTO
                {
                    Email = Email,
                    Password = Password
                };

                var response = await LoginService.LoginValidate(loginRequest);

                if (response.Success && response.Result != null)
                {
                    var userLoginValidate = JsonConvert.DeserializeObject<UserLoginValidateDTO>(response.Result.ToString());
                    await SecureStorage.Default.SetAsync("auth_token", userLoginValidate.Token);
                    await SecureStorage.Default.SetAsync("user_id", userLoginValidate.UserCode.ToString());
                    await SecureStorage.Default.SetAsync("user_Category", userLoginValidate.UserCategory.ToString());
                    await SecureStorage.Default.SetAsync("user_name", userLoginValidate.FirstName +" "+ userLoginValidate.LastName);
                    await SecureStorage.Default.SetAsync("user_email", userLoginValidate.Email);
                    await SecureStorage.Default.SetAsync("state_Id", userLoginValidate.StateCode.ToString());
                    await _navigationService.NavigateToMainPageAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", response.Message ?? "Login failed", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "An unexpected error occurred", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Password);
        }


        [RelayCommand]
        private async Task ForgotPasswordAsync()
        {
            await _navigationService.NavigateToAsync("ForgotPassword");
        }
    }
}
