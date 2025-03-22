using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using NCD.DTO;
using Nirmaya.DTO;
using NCD.Services;
using NCD.Utilities;

namespace NCD.ViewModels
{
    public partial class RegistrationViewModel : ObservableObject
    {
        [ObservableProperty]
        private Registration registration;

        // Error messages for validation
        private Dictionary<string, string> _errors = new Dictionary<string, string>();

        public ObservableCollection<DropdownDTO> Genders { get; set; } = [];
        public ObservableCollection<DropdownDTO> Districts { get; set; } = [];
        public ObservableCollection<DropdownDTO> Blocks { get; set; } = [];
        public ObservableCollection<DropdownDTO> Villages { get; set; } = [];
        public ObservableCollection<DropdownDTO> ScreeningStatus { get; set; } = [];
        public ObservableCollection<DropdownDTO> TBScreeining { get; set; } = [];

        public ObservableCollection<DropdownDTO> Marital { get; set; }  = [];

        public ObservableCollection<DropdownDTO> KeyVulPopulation { get; set; } = [];

        public ObservableCollection<DropdownDTO> Occupations { get; set; } = [];

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

        [ObservableProperty]
        private string districtError;

        [ObservableProperty]
        private string blockError;

        [ObservableProperty]
        private string villageError;

        public RegistrationViewModel()
        {
            // Initialize registration object
            Registration = new Registration();
            Task task = LoadDefaultData();
        }
        private async Task LoadDefaultData()
        {
            try
            {
                var stateId = await AppHelper.GetStateId();
                // Load default data for dropdown lists
                var defaultData = await RegistrationService.GetDefaultDataAsync(stateId);
                if (defaultData != null)
                {
                    Genders = new ObservableCollection<DropdownDTO>(defaultData.Genders);
                    Districts = new ObservableCollection<DropdownDTO>(defaultData.Districts);
                    Blocks = new ObservableCollection<DropdownDTO>(defaultData.Blocks);
                    Villages = new ObservableCollection<DropdownDTO>(defaultData.Villages);
                    ScreeningStatus = new ObservableCollection<DropdownDTO>(defaultData.ScreeningStatus);
                    TBScreeining = new ObservableCollection<DropdownDTO>(defaultData.TBScreeining);
                    Marital = new ObservableCollection<DropdownDTO>(defaultData.Marital);
                    KeyVulPopulation = new ObservableCollection<DropdownDTO>(defaultData.KeyVulPopulation);
                    Occupations = new ObservableCollection<DropdownDTO>(defaultData.Occupation);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to load default data: " + ex.Message, "OK");
            }
        }


        [RelayCommand]
        private async Task Submit()
        {
            if (ValidateForm())
            {
                try
                {
                    // Registration object is already populated with the form data
                    await SaveRegistrationData(Registration);
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
            DistrictError = string.Empty;
            BlockError = string.Empty;
            VillageError = string.Empty;

            // Validate Name
            if (string.IsNullOrWhiteSpace(Registration.NameofthePerson))
            {
                NameError = "Name is required";
                isValid = false;
            }

            // Validate Father/Husband Name
            if (string.IsNullOrWhiteSpace(Registration.FatherName))
            {
                FatherHusbandNameError = "Father/Husband Name is required";
                isValid = false;
            }

            // Validate Age
            if (Registration.Age <= 0)
            {
                AgeError = "Age is required and must be positive";
                isValid = false;
            }

            // Validate Gender
            if (string.IsNullOrWhiteSpace(Registration.Gender))
            {
                GenderError = "Gender is required";
                isValid = false;
            }

            // Validate Marital Status
            if (string.IsNullOrWhiteSpace(Registration.MaritalStatus))
            {
                MaritalStatusError = "Marital Status is required";
                isValid = false;
            }

            // Validate Telephone
            if (string.IsNullOrWhiteSpace(Registration.TelephoneNo))
            {
                TelephoneNoError = "Telephone number is required";
                isValid = false;
            }

            // Validate Key Population Type
            if (string.IsNullOrWhiteSpace(Registration.KeyVulPopulation))
            {
                KeyPopulationTypeError = "Key Population Type is required";
                isValid = false;
            }

            // Validate District
            if (Registration.DistrictId==0)
            {
                DistrictError = "District is required";
                isValid = false;
            }

            // Validate Block
            if (Registration.BlockId == 0)
            {
                BlockError = "Block is required";
                isValid = false;
            }

            // Validate Village
            if (Registration.VillageId ==0)
            {
                VillageError = "Village is required";
                isValid = false;
            }

            return isValid;
        }

        private void ResetForm()
        {
            // Reset registration object with a new instance
            Registration = new Registration();

            // Reset all error messages
            NameError = string.Empty;
            FatherHusbandNameError = string.Empty;
            AgeError = string.Empty;
            GenderError = string.Empty;
            MaritalStatusError = string.Empty;
            TelephoneNoError = string.Empty;
            KeyPopulationTypeError = string.Empty;
            DistrictError = string.Empty;
            BlockError = string.Empty;
            VillageError = string.Empty;
        }

        private async Task SaveRegistrationData(Registration person)
        {
            // Implement your data saving logic here
            // This could be an API call or local database operation
            await Task.Delay(1000); // Simulated delay
        }

        [RelayCommand]
        private void DistrictChanged()
        {
            if (Registration.DistrictId>0)
            {
                LoadBlocksForDistrict(Registration.DistrictId);
                
                Registration.BlockId = 0;
                Registration.VillageId = 0;
                
                Villages.Clear();
            }
        }

        [RelayCommand]
        private void BlockChanged()
        {
            if (Registration.BlockId>0)
            {
                LoadVillagesForBlock(Registration.BlockId);
                
                Registration.VillageId = 0;
            }
        }

        private async void LoadBlocksForDistrict(int districtId)
        {
            try
            {
                var blocks = await RegistrationService.GetBlocksByDistrictAsync(districtId);
                Blocks = new ObservableCollection<DropdownDTO>(blocks);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to load blocks: " + ex.Message, "OK");
            }
        }

        private async void LoadVillagesForBlock(int blockId)
        {
            try
            {
                var villages = await RegistrationService.GetVillagesByBlockAsync(blockId);
                Villages = new ObservableCollection<DropdownDTO>(villages);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to load villages: " + ex.Message, "OK");
            }
        }
    }
} 