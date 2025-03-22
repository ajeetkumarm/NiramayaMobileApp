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

        [ObservableProperty]
        private ObservableCollection<DropdownDTO> genders = new();

        [ObservableProperty]
        private ObservableCollection<DropdownDTO> districts = new();

        [ObservableProperty]
        private ObservableCollection<DropdownDTO> blocks = new();

        [ObservableProperty]
        private ObservableCollection<DropdownDTO> villages = new();

        [ObservableProperty]
        private ObservableCollection<DropdownDTO> screeningStatus = new();

        [ObservableProperty]
        private ObservableCollection<DropdownDTO> tbScreeining = new();

        [ObservableProperty]
        private ObservableCollection<DropdownDTO> marital = new();

        [ObservableProperty]
        private ObservableCollection<DropdownDTO> keyVulPopulation = new();

        [ObservableProperty]
        private ObservableCollection<DropdownDTO> occupations = new();

        [ObservableProperty]
        private ObservableCollection<DropdownDTO> yesNoOptions = new();

        [ObservableProperty]
        private DropdownDTO defaultDropdown;

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

        [ObservableProperty]
        private DropdownDTO selectedDistrict;

        [ObservableProperty]
        private DropdownDTO selectedBlock;

        [ObservableProperty]
        private DropdownDTO selectedVillage;

        [ObservableProperty]
        private DropdownDTO selectedGender;

        [ObservableProperty]
        private DropdownDTO selectedMaritalStatus;

        [ObservableProperty]
        private DropdownDTO selectedKeyVulPopulation;

        [ObservableProperty]
        private DropdownDTO selectedOccupation;

        public RegistrationViewModel()
        {
            // Initialize registration object
            Registration = new Registration();
            // Registration.CreatedBy =  

            DefaultDropdown = new DropdownDTO
            {
                Value = "0",
                Text = "Select"
            };

            // Initialize empty collections to prevent null reference
            yesNoOptions = new ObservableCollection<DropdownDTO>();
            genders = new ObservableCollection<DropdownDTO>();
            districts = new ObservableCollection<DropdownDTO>();
            blocks = new ObservableCollection<DropdownDTO>();
            villages = new ObservableCollection<DropdownDTO>();
            marital = new ObservableCollection<DropdownDTO>();
            keyVulPopulation = new ObservableCollection<DropdownDTO>();
            occupations = new ObservableCollection<DropdownDTO>();

            // Set default values for all dropdown selections
            SelectedDistrict = DefaultDropdown;
            SelectedBlock = DefaultDropdown;
            SelectedVillage = DefaultDropdown;
            SelectedGender = DefaultDropdown;
            SelectedMaritalStatus = DefaultDropdown;
            SelectedKeyVulPopulation = DefaultDropdown;
            SelectedOccupation = DefaultDropdown;

            // Load data immediately but don't block constructor
            InitializeAsync();
        }

        // Add this method to handle async initialization
        private async void InitializeAsync()
        {
            try
            {
                await LoadDefaultData();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to initialize: " + ex.Message, "OK");
            }
        }

        // Modify LoadDefaultData to add debug info
        private async Task LoadDefaultData()
        {
            try
            {
                var stateId = await AppHelper.GetStateId();
                
                // Load default data for dropdown lists
                var defaultData = await RegistrationService.GetDefaultDataAsync(stateId);
                
                if (defaultData != null)
                {
                    // The ObservableProperty pattern will handle the proper notifications
                    YesNoOptions = new ObservableCollection<DropdownDTO>(defaultData.YesNoOptions ?? new List<DropdownDTO>());
                    Genders = new ObservableCollection<DropdownDTO>(defaultData.Genders ?? new List<DropdownDTO>());
                    Districts = new ObservableCollection<DropdownDTO>(defaultData.Districts ?? new List<DropdownDTO>());
                    Marital = new ObservableCollection<DropdownDTO>(defaultData.Marital ?? new List<DropdownDTO>());
                    KeyVulPopulation = new ObservableCollection<DropdownDTO>(defaultData.KeyVulPopulation ?? new List<DropdownDTO>());
                    Occupations = new ObservableCollection<DropdownDTO>(defaultData.Occupation ?? new List<DropdownDTO>());
                    
                    if (defaultData.Blocks?.Any() == true)
                        Blocks = new ObservableCollection<DropdownDTO>(defaultData.Blocks);
                    
                    if (defaultData.Villages?.Any() == true)
                        Villages = new ObservableCollection<DropdownDTO>(defaultData.Villages);
                    
                    // Set default values
                    SelectedDistrict = DefaultDropdown;
                    SelectedBlock = DefaultDropdown;
                    SelectedVillage = DefaultDropdown;
                    SelectedGender = DefaultDropdown;
                    SelectedMaritalStatus = DefaultDropdown;
                    SelectedKeyVulPopulation = DefaultDropdown;
                    SelectedOccupation = DefaultDropdown;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No data returned from API", "OK");
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
                    var _createdBy = await AppHelper.GetUserId();

                    Registration.CreatedBy = _createdBy.ToString();
                    Registration.StateId = await AppHelper.GetStateId();
                    Registration.QueryType = Registration.Id==0 ? "A" : "U";
                    Registration.EnrollmentId = Guid.NewGuid().ToString();
                    Registration.TBEnrollmentid = Guid.NewGuid().ToString();
                    Registration.ScreeningStatus = "Screened";
                    

                    // Registration object is already populated with the form data
                    var response = await RegistrationService.SaveRegistration(Registration);
                    if (response.Success)
                    {
                        await Shell.Current.DisplayAlert("Success", response.Message, "OK");
                        ResetForm();
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", response.Message, "OK");
                    }
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

            // Reset all dropdown selections
            SelectedDistrict = DefaultDropdown;
            SelectedBlock = DefaultDropdown;
            SelectedVillage = DefaultDropdown;
            SelectedGender = DefaultDropdown;
            SelectedMaritalStatus = DefaultDropdown;
            SelectedKeyVulPopulation = DefaultDropdown;
            SelectedOccupation = DefaultDropdown;

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



        partial void OnSelectedDistrictChanged(DropdownDTO value)
        {
            if (value != null)
            {
                Registration.BlockId = 0;
                Registration.VillageId = 0;
                Registration.DistrictId = Convert.ToInt32( value.Value);
                LoadBlocksForDistrict(Registration.DistrictId);
               
            }
        }
        partial void OnSelectedBlockChanged(DropdownDTO value)
        {
            if (value != null)
            {
                Registration.VillageId = 0;
                villages.Clear();
                Registration.BlockId = Convert.ToInt32(value.Value);
                LoadVillagesForBlock(Registration.BlockId);
                
            }
        }

        partial void OnSelectedVillageChanged(DropdownDTO value)
        {
            if (value != null && !string.IsNullOrEmpty(value.Value))
            {
                Registration.VillageId = Convert.ToInt32(value.Value);
            }
        }

        partial void OnSelectedGenderChanged(DropdownDTO value)
        {
            if (value != null)
            {
                Registration.Gender = value.Text;
            }
        }

        partial void OnSelectedMaritalStatusChanged(DropdownDTO value)
        {
            if (value != null)
            {
                Registration.MaritalStatus = value.Text;
            }
        }

        partial void OnSelectedKeyVulPopulationChanged(DropdownDTO value)
        {
            if (value != null)
            {
                Registration.KeyVulPopulation = value.Text;
            }
        }

        partial void OnSelectedOccupationChanged(DropdownDTO value)
        {
            if (value != null)
            {
                Registration.Occupation = value.Text;
            }
        }

        private async void LoadBlocksForDistrict(int districtId)
        {
            try
            {
                var blocksList = await RegistrationService.GetBlocksByDistrictAsync(districtId);
                Blocks = new ObservableCollection<DropdownDTO>(blocksList ?? new List<DropdownDTO>());
                SelectedBlock = DefaultDropdown;
                Villages = new ObservableCollection<DropdownDTO>(); // Clear villages
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
                var villagesList = await RegistrationService.GetVillagesByBlockAsync(blockId);
                Villages = new ObservableCollection<DropdownDTO>(villagesList ?? new List<DropdownDTO>());
                SelectedVillage = DefaultDropdown;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to load villages: " + ex.Message, "OK");
            }
        }

        // Add this method and call it where needed
        private void LogDebug(string message)
        {
            System.Diagnostics.Debug.WriteLine($"Registration: {message}");
            // You could also log to a file or other output
        }
    }
} 