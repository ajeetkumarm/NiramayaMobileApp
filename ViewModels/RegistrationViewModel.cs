using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using NCD.Services;
using NCD.DTO;
using Newtonsoft.Json;
using NCD.Utilities;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NCD.ViewModels
{
    public partial class RegistrationViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string fatherHusbandName;

        [ObservableProperty]
        private string age;

        [ObservableProperty]
        private string selectedGender;

        [ObservableProperty]
        private string selectedMaritalStatus;

        [ObservableProperty]
        private string telephoneNo;

        [ObservableProperty]
        private string address;

        [ObservableProperty]
        private string selectedKeyPopulationType;

        [ObservableProperty]
        private string selectedOccupation;

        // Error messages for validation
        private Dictionary<string, string> _errors = new Dictionary<string, string>();

        // Collection properties for dropdown lists
        public ObservableCollection<string> Genders { get; } = new ObservableCollection<string>
        {
            "Male", "Female", "Other"
        };

        public ObservableCollection<string> MaritalStatuses { get; } = new ObservableCollection<string>
        {
            "Single", "Married", "Divorced", "Widowed"
        };

        public ObservableCollection<string> KeyPopulationTypes { get; } = new ObservableCollection<string>
        {
            "Children", "Elderly", "Women", "Disabled", "Migrants", "Other"
        };

        public ObservableCollection<string> Occupations { get; } = new ObservableCollection<string>
        {
            "Student", "Unemployed", "Self-employed", "Government Employee", "Private Sector", "Farmer", "Other"
        };

        // Commands
        //public ICommand SubmitCommand { get; }
        //public ICommand ResetCommand { get; }

        // Error properties
        [ObservableProperty]
        private string nameError;

        [ObservableProperty]
        private string fatherHusbandNameError;

        [ObservableProperty]
        private string ageError;

        [ObservableProperty]
        private string genderError;

        [ObservableProperty]
        private string maritalStatusError;

        [ObservableProperty]
        private string telephoneNoError;

        [ObservableProperty]
        private string keyPopulationTypeError;

        public RegistrationViewModel()
        {
            // Initialize commands
           // SubmitCommand = new Command(OnSubmit, CanSubmit);
            //ResetCommand = new Command(OnReset);
        }

        //private bool CanSubmit()
        //{
        //    // Check if all required fields are filled and valid
        //    bool isValid = !string.IsNullOrWhiteSpace(Name) &&
        //                   !string.IsNullOrWhiteSpace(FatherHusbandName) &&
        //                   !string.IsNullOrWhiteSpace(Age) &&
        //                   !string.IsNullOrWhiteSpace(SelectedGender) &&
        //                   !string.IsNullOrWhiteSpace(SelectedMaritalStatus) &&
        //                   !string.IsNullOrWhiteSpace(TelephoneNo) &&
        //                   !string.IsNullOrWhiteSpace(SelectedKeyPopulationType) &&
        //                   _errors.Count == 0;

        //    return isValid;
        //}

        [RelayCommand]
        private async Task Submit()
        {
            if (ValidateForm())
            {
                try
                {
                    // Create model from viewmodel properties
                    var person = new Person
                    {
                        Name = Name,
                        FatherHusbandName = FatherHusbandName,
                        Age = int.Parse(Age),
                        Gender = SelectedGender,
                        MaritalStatus = SelectedMaritalStatus,
                        TelephoneNo = TelephoneNo,
                        Address = Address,
                        KeyPopulationType = SelectedKeyPopulationType,
                        Occupation = SelectedOccupation
                    };

                    // Save the data or send to API
                    await SaveRegistrationData(person);
                    await Shell.Current.DisplayAlert("Success", "Registration submitted successfully!", "OK");
                    ResetForm();
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", "Failed to submit registration: " + ex.Message, "OK");
                }
            }
        }

        [RelayCommand]
        private void Reset()
        {
            ResetForm();
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            // Reset all error messages
            NameError = string.Empty;
            FatherHusbandNameError = string.Empty;
            AgeError = string.Empty;
            GenderError = string.Empty;
            MaritalStatusError = string.Empty;
            TelephoneNoError = string.Empty;
            KeyPopulationTypeError = string.Empty;

            // Validate Name
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameError = "Name is required";
                isValid = false;
            }

            // Validate Father/Husband Name
            if (string.IsNullOrWhiteSpace(FatherHusbandName))
            {
                FatherHusbandNameError = "Father/Husband Name is required";
                isValid = false;
            }

            // Validate Age
            if (string.IsNullOrWhiteSpace(Age))
            {
                AgeError = "Age is required";
                isValid = false;
            }
            else if (!int.TryParse(Age, out int ageValue) || ageValue <= 0)
            {
                AgeError = "Please enter a valid age";
                isValid = false;
            }

            // Validate Gender
            if (string.IsNullOrWhiteSpace(SelectedGender))
            {
                GenderError = "Gender is required";
                isValid = false;
            }

            // Validate Marital Status
            if (string.IsNullOrWhiteSpace(SelectedMaritalStatus))
            {
                MaritalStatusError = "Marital Status is required";
                isValid = false;
            }

            // Validate Telephone
            if (string.IsNullOrWhiteSpace(TelephoneNo))
            {
                TelephoneNoError = "Telephone number is required";
                isValid = false;
            }

            // Validate Key Population Type
            if (string.IsNullOrWhiteSpace(SelectedKeyPopulationType))
            {
                KeyPopulationTypeError = "Key Population Type is required";
                isValid = false;
            }

            return isValid;
        }

        private void ResetForm()
        {
            // Reset all fields
            Name = string.Empty;
            FatherHusbandName = string.Empty;
            Age = string.Empty;
            SelectedGender = null;
            SelectedMaritalStatus = null;
            TelephoneNo = string.Empty;
            Address = string.Empty;
            SelectedKeyPopulationType = null;
            SelectedOccupation = null;

            // Reset all error messages
            NameError = string.Empty;
            FatherHusbandNameError = string.Empty;
            AgeError = string.Empty;
            GenderError = string.Empty;
            MaritalStatusError = string.Empty;
            TelephoneNoError = string.Empty;
            KeyPopulationTypeError = string.Empty;
        }

        private async Task SaveRegistrationData(Person person)
        {
            // Implement your data saving logic here
            // This could be an API call or local database operation
            await Task.Delay(1000); // Simulated delay
        }

        // Validation methods
        //private void ValidateName()
        //{
        //    if (string.IsNullOrWhiteSpace(Name))
        //    {
        //        SetError(nameof(Name), "Name is required");
        //    }
        //    else
        //    {
        //        ClearError(nameof(Name));
        //    }
        //    OnPropertyChanged(nameof(NameError));
        //}

        //private void ValidateFatherHusbandName()
        //{
        //    if (string.IsNullOrWhiteSpace(FatherHusbandName))
        //    {
        //        SetError(nameof(FatherHusbandName), "Father/Husband Name is required");
        //    }
        //    else
        //    {
        //        ClearError(nameof(FatherHusbandName));
        //    }
        //    OnPropertyChanged(nameof(FatherHusbandNameError));
        //}

        //private void ValidateAge()
        //{
        //    if (string.IsNullOrWhiteSpace(Age))
        //    {
        //        SetError(nameof(Age), "Age is required");
        //    }
        //    else if (!int.TryParse(Age, out int ageValue) || ageValue <= 0 || ageValue > 120)
        //    {
        //        SetError(nameof(Age), "Age must be a valid number between 1-120");
        //    }
        //    else
        //    {
        //        ClearError(nameof(Age));
        //    }
        //    OnPropertyChanged(nameof(AgeError));
        //}

        //private void ValidateGender()
        //{
        //    if (string.IsNullOrWhiteSpace(SelectedGender))
        //    {
        //        SetError(nameof(SelectedGender), "Gender is required");
        //    }
        //    else
        //    {
        //        ClearError(nameof(SelectedGender));
        //    }
        //    OnPropertyChanged(nameof(GenderError));
        //}

        //private void ValidateMaritalStatus()
        //{
        //    if (string.IsNullOrWhiteSpace(SelectedMaritalStatus))
        //    {
        //        SetError(nameof(SelectedMaritalStatus), "Marital Status is required");
        //    }
        //    else
        //    {
        //        ClearError(nameof(SelectedMaritalStatus));
        //    }
        //    OnPropertyChanged(nameof(MaritalStatusError));
        //}

        //private void ValidateTelephoneNo()
        //{
        //    if (string.IsNullOrWhiteSpace(TelephoneNo))
        //    {
        //        SetError(nameof(TelephoneNo), "Telephone Number is required");
        //    }
        //    else if (!IsValidPhoneNumber(TelephoneNo))
        //    {
        //        SetError(nameof(TelephoneNo), "Please enter a valid phone number");
        //    }
        //    else
        //    {
        //        ClearError(nameof(TelephoneNo));
        //    }
        //    OnPropertyChanged(nameof(TelephoneNoError));
        //}

        //private void ValidateKeyPopulationType()
        //{
        //    if (string.IsNullOrWhiteSpace(SelectedKeyPopulationType))
        //    {
        //        SetError(nameof(SelectedKeyPopulationType), "Key Population Type is required");
        //    }
        //    else
        //    {
        //        ClearError(nameof(SelectedKeyPopulationType));
        //    }
        //    OnPropertyChanged(nameof(KeyPopulationTypeError));
        //}

        //private bool IsValidPhoneNumber(string phoneNumber)
        //{
        //    // Simple validation - can be enhanced based on specific requirements
        //    return !string.IsNullOrWhiteSpace(phoneNumber) &&
        //           phoneNumber.Length >= 6 &&
        //           phoneNumber.All(c => char.IsDigit(c) || c == '+' || c == '-' || c == ' ');
        //}

        // Error handling methods
        //private void SetError(string propertyName, string error)
        //{
        //    _errors[propertyName] = error;
        //    //(SubmitCommand as Command)?.ChangeCanExecute();
        //}

        //private void ClearError(string propertyName)
        //{
        //    if (_errors.ContainsKey(propertyName))
        //    {
        //        _errors.Remove(propertyName);
        //       // (SubmitCommand as Command)?.ChangeCanExecute();
        //    }
        //}

        //private string GetError(string propertyName)
        //{
        //    return _errors.TryGetValue(propertyName, out string error) ? error : string.Empty;
        //}

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 