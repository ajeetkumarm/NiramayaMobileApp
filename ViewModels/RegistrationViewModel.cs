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

namespace NCD.ViewModels
{
    public partial class RegistrationViewModel : INotifyPropertyChanged
    {


        private string _name;
        private string _fatherHusbandName;
        private string _age;
        private string _selectedGender;
        private string _selectedMaritalStatus;
        private string _telephoneNo;
        private string _address;
        private string _selectedKeyPopulationType;
        private string _selectedOccupation;

        // Error messages for validation
        private Dictionary<string, string> _errors = new Dictionary<string, string>();

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    ValidateName();
                    OnPropertyChanged();
                }
            }
        }

        public string FatherHusbandName
        {
            get => _fatherHusbandName;
            set
            {
                if (_fatherHusbandName != value)
                {
                    _fatherHusbandName = value;
                    ValidateFatherHusbandName();
                    OnPropertyChanged();
                }
            }
        }

        public string Age
        {
            get => _age;
            set
            {
                if (_age != value)
                {
                    _age = value;
                    ValidateAge();
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedGender
        {
            get => _selectedGender;
            set
            {
                if (_selectedGender != value)
                {
                    _selectedGender = value;
                    ValidateGender();
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedMaritalStatus
        {
            get => _selectedMaritalStatus;
            set
            {
                if (_selectedMaritalStatus != value)
                {
                    _selectedMaritalStatus = value;
                    ValidateMaritalStatus();
                    OnPropertyChanged();
                }
            }
        }

        public string TelephoneNo
        {
            get => _telephoneNo;
            set
            {
                if (_telephoneNo != value)
                {
                    _telephoneNo = value;
                    ValidateTelephoneNo();
                    OnPropertyChanged();
                }
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedKeyPopulationType
        {
            get => _selectedKeyPopulationType;
            set
            {
                if (_selectedKeyPopulationType != value)
                {
                    _selectedKeyPopulationType = value;
                    ValidateKeyPopulationType();
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedOccupation
        {
            get => _selectedOccupation;
            set
            {
                if (_selectedOccupation != value)
                {
                    _selectedOccupation = value;
                    OnPropertyChanged();
                }
            }
        }

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
        public ICommand SubmitCommand { get; }
        public ICommand ResetCommand { get; }

        // Error properties
        public string NameError => GetError(nameof(Name));
        public string FatherHusbandNameError => GetError(nameof(FatherHusbandName));
        public string AgeError => GetError(nameof(Age));
        public string GenderError => GetError(nameof(SelectedGender));
        public string MaritalStatusError => GetError(nameof(SelectedMaritalStatus));
        public string TelephoneNoError => GetError(nameof(TelephoneNo));
        public string KeyPopulationTypeError => GetError(nameof(SelectedKeyPopulationType));

        public RegistrationViewModel()
        {
            // Initialize commands
            SubmitCommand = new Command(OnSubmit, CanSubmit);
            ResetCommand = new Command(OnReset);
        }

        private bool CanSubmit()
        {
            // Check if all required fields are filled and valid
            bool isValid = !string.IsNullOrWhiteSpace(Name) &&
                           !string.IsNullOrWhiteSpace(FatherHusbandName) &&
                           !string.IsNullOrWhiteSpace(Age) &&
                           !string.IsNullOrWhiteSpace(SelectedGender) &&
                           !string.IsNullOrWhiteSpace(SelectedMaritalStatus) &&
                           !string.IsNullOrWhiteSpace(TelephoneNo) &&
                           !string.IsNullOrWhiteSpace(SelectedKeyPopulationType) &&
                           _errors.Count == 0;

            return isValid;
        }

        private async void OnSubmit()
        {
            if (CanSubmit())
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
                // This is where you would implement your data persistence logic

                await Application.Current.MainPage.DisplayAlert("Success", "Registration submitted successfully", "OK");
                OnReset();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill all required fields correctly", "OK");
            }
        }

        private void OnReset()
        {
            // Reset all form fields
            Name = string.Empty;
            FatherHusbandName = string.Empty;
            Age = string.Empty;
            SelectedGender = null;
            SelectedMaritalStatus = null;
            TelephoneNo = string.Empty;
            Address = string.Empty;
            SelectedKeyPopulationType = null;
            SelectedOccupation = null;

            // Clear all errors
            _errors.Clear();

            // Notify UI of all property changes
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(FatherHusbandName));
            OnPropertyChanged(nameof(Age));
            OnPropertyChanged(nameof(SelectedGender));
            OnPropertyChanged(nameof(SelectedMaritalStatus));
            OnPropertyChanged(nameof(TelephoneNo));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(SelectedKeyPopulationType));
            OnPropertyChanged(nameof(SelectedOccupation));

            OnPropertyChanged(nameof(NameError));
            OnPropertyChanged(nameof(FatherHusbandNameError));
            OnPropertyChanged(nameof(AgeError));
            OnPropertyChanged(nameof(GenderError));
            OnPropertyChanged(nameof(MaritalStatusError));
            OnPropertyChanged(nameof(TelephoneNoError));
            OnPropertyChanged(nameof(KeyPopulationTypeError));
        }

        // Validation methods
        private void ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                SetError(nameof(Name), "Name is required");
            }
            else
            {
                ClearError(nameof(Name));
            }
            OnPropertyChanged(nameof(NameError));
        }

        private void ValidateFatherHusbandName()
        {
            if (string.IsNullOrWhiteSpace(FatherHusbandName))
            {
                SetError(nameof(FatherHusbandName), "Father/Husband Name is required");
            }
            else
            {
                ClearError(nameof(FatherHusbandName));
            }
            OnPropertyChanged(nameof(FatherHusbandNameError));
        }

        private void ValidateAge()
        {
            if (string.IsNullOrWhiteSpace(Age))
            {
                SetError(nameof(Age), "Age is required");
            }
            else if (!int.TryParse(Age, out int ageValue) || ageValue <= 0 || ageValue > 120)
            {
                SetError(nameof(Age), "Age must be a valid number between 1-120");
            }
            else
            {
                ClearError(nameof(Age));
            }
            OnPropertyChanged(nameof(AgeError));
        }

        private void ValidateGender()
        {
            if (string.IsNullOrWhiteSpace(SelectedGender))
            {
                SetError(nameof(SelectedGender), "Gender is required");
            }
            else
            {
                ClearError(nameof(SelectedGender));
            }
            OnPropertyChanged(nameof(GenderError));
        }

        private void ValidateMaritalStatus()
        {
            if (string.IsNullOrWhiteSpace(SelectedMaritalStatus))
            {
                SetError(nameof(SelectedMaritalStatus), "Marital Status is required");
            }
            else
            {
                ClearError(nameof(SelectedMaritalStatus));
            }
            OnPropertyChanged(nameof(MaritalStatusError));
        }

        private void ValidateTelephoneNo()
        {
            if (string.IsNullOrWhiteSpace(TelephoneNo))
            {
                SetError(nameof(TelephoneNo), "Telephone Number is required");
            }
            else if (!IsValidPhoneNumber(TelephoneNo))
            {
                SetError(nameof(TelephoneNo), "Please enter a valid phone number");
            }
            else
            {
                ClearError(nameof(TelephoneNo));
            }
            OnPropertyChanged(nameof(TelephoneNoError));
        }

        private void ValidateKeyPopulationType()
        {
            if (string.IsNullOrWhiteSpace(SelectedKeyPopulationType))
            {
                SetError(nameof(SelectedKeyPopulationType), "Key Population Type is required");
            }
            else
            {
                ClearError(nameof(SelectedKeyPopulationType));
            }
            OnPropertyChanged(nameof(KeyPopulationTypeError));
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Simple validation - can be enhanced based on specific requirements
            return !string.IsNullOrWhiteSpace(phoneNumber) &&
                   phoneNumber.Length >= 6 &&
                   phoneNumber.All(c => char.IsDigit(c) || c == '+' || c == '-' || c == ' ');
        }

        // Error handling methods
        private void SetError(string propertyName, string error)
        {
            _errors[propertyName] = error;
            (SubmitCommand as Command)?.ChangeCanExecute();
        }

        private void ClearError(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                (SubmitCommand as Command)?.ChangeCanExecute();
            }
        }

        private string GetError(string propertyName)
        {
            return _errors.TryGetValue(propertyName, out string error) ? error : string.Empty;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 