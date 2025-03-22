using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using NCD.ViewModels;
using NirmayaMobileApp.DTO;
using NirmayaMobileApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NirmayaMobileApp.ViewModels
{
    public partial class BreastCancerScreeningViewModel : BaseViewModel
    {
        private readonly IBreastCancerScreeningService _screeningService;
        private int _currentScreeningId;

        [ObservableProperty]
        private string personName;

        [ObservableProperty]
        private string fatherHusbandName;

        [ObservableProperty]
        private string telephoneNo;

        [ObservableProperty]
        private string address;

        [ObservableProperty]
        private string age;

        [ObservableProperty]
        private DateTime dateOfScreening = DateTime.Today;

        [ObservableProperty]
        private string examinationBy = "Self";

        [ObservableProperty]
        private DateTime dateOfLastMenstrualPeriod = DateTime.Today;

        [ObservableProperty]
        private string ageAtFirstMenstruation;

        [ObservableProperty]
        private string numberOfPregnancies;

        [ObservableProperty]
        private string numberOfBirths;

        [ObservableProperty]
        private string ageAtFirstChildbirth;

        [ObservableProperty]
        private string breastfeedingHistory;

        [ObservableProperty]
        private string menopausalStatus;

        [ObservableProperty]
        private string historyOfBreastLumps;

        [ObservableProperty]
        private string previousBreastBiopsies;

        [ObservableProperty]
        private string useOfHormonalContraceptives;

        [ObservableProperty]
        private string hormoneReplacementTherapy;

        [ObservableProperty]
        private bool isAgeRiskFactor;

        [ObservableProperty]
        private bool isFamilyHistoryRiskFactor;

        [ObservableProperty]
        private bool isGeneticMutationsRiskFactor;

        [ObservableProperty]
        private bool isPreviousRadiationTherapyRiskFactor;

        [ObservableProperty]
        private bool isObesityRiskFactor;

        [ObservableProperty]
        private bool isOtherRiskFactor;

        [ObservableProperty]
        private string alcoholConsumption;

        [ObservableProperty]
        private string smoking;

        public List<string> YesNoOptions { get; } = new List<string> { "--Select--", "Yes", "No" };
        public List<string> BreastfeedingHistoryOptions { get; } = new List<string> { "--Select--", "Never", "Past", "Current" };
        public List<string> MenopausalStatusOptions { get; } = new List<string> { "--Select--", "Pre-menopausal", "Peri-menopausal", "Post-menopausal" };

        public BreastCancerScreeningViewModel(IBreastCancerScreeningService screeningService)
        {
            _screeningService = screeningService;
            Title = "Breast Cancer Screening";
            
            // Initialize with sample data
            PersonName = "Harshali munde";
            FatherHusbandName = "Pravin";
            TelephoneNo = "9325590309";
            Address = "Chalisgaon";
            Age = "19";
        }

        [RelayCommand]
        private async Task SubmitScreeningAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(DateOfScreening.ToString()) || string.IsNullOrWhiteSpace(ExaminationBy))
                {
                    await Shell.Current.DisplayAlert("Error", "Please fill in all required fields", "OK");
                    return;
                }

                // Create or update a screening DTO
                var screeningDto = new BreastCancerScreeningDTO
                {
                    Id = _currentScreeningId,
                    PersonName = PersonName,
                    FatherHusbandName = FatherHusbandName,
                    TelephoneNo = TelephoneNo,
                    Address = Address,
                    Age = Age,
                    DateOfScreening = DateOfScreening,
                    ExaminationBy = ExaminationBy,
                    DateOfLastMenstrualPeriod = DateOfLastMenstrualPeriod,
                    AgeAtFirstMenstruation = AgeAtFirstMenstruation,
                    NumberOfPregnancies = NumberOfPregnancies,
                    NumberOfBirths = NumberOfBirths,
                    AgeAtFirstChildbirth = AgeAtFirstChildbirth,
                    BreastfeedingHistory = BreastfeedingHistory,
                    MenopausalStatus = MenopausalStatus,
                    HistoryOfBreastLumps = HistoryOfBreastLumps,
                    PreviousBreastBiopsies = PreviousBreastBiopsies,
                    UseOfHormonalContraceptives = UseOfHormonalContraceptives,
                    HormoneReplacementTherapy = HormoneReplacementTherapy,
                    IsAgeRiskFactor = IsAgeRiskFactor,
                    IsFamilyHistoryRiskFactor = IsFamilyHistoryRiskFactor,
                    IsGeneticMutationsRiskFactor = IsGeneticMutationsRiskFactor,
                    IsPreviousRadiationTherapyRiskFactor = IsPreviousRadiationTherapyRiskFactor,
                    IsObesityRiskFactor = IsObesityRiskFactor,
                    IsOtherRiskFactor = IsOtherRiskFactor,
                    AlcoholConsumption = AlcoholConsumption,
                    Smoking = Smoking
                };

                if (_currentScreeningId > 0)
                {
                    // Update existing screening
                    await _screeningService.UpdateScreeningAsync(screeningDto);
                }
                else
                {
                    // Create new screening
                    var result = await _screeningService.CreateScreeningAsync(screeningDto);
                    _currentScreeningId = result.Id;
                }

                // Show success message
                await Shell.Current.DisplayAlert("Success", "Breast cancer screening data saved successfully", "OK");
                
                // Navigate back or to another page
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task LoadScreeningAsync(int id)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var screening = await _screeningService.GetScreeningByIdAsync(id);
                if (screening != null)
                {
                    _currentScreeningId = screening.Id;
                    PersonName = screening.PersonName;
                    FatherHusbandName = screening.FatherHusbandName;
                    TelephoneNo = screening.TelephoneNo;
                    Address = screening.Address;
                    Age = screening.Age;
                    DateOfScreening = screening.DateOfScreening;
                    ExaminationBy = screening.ExaminationBy;
                    DateOfLastMenstrualPeriod = screening.DateOfLastMenstrualPeriod ?? DateTime.Today;
                    AgeAtFirstMenstruation = screening.AgeAtFirstMenstruation;
                    NumberOfPregnancies = screening.NumberOfPregnancies;
                    NumberOfBirths = screening.NumberOfBirths;
                    AgeAtFirstChildbirth = screening.AgeAtFirstChildbirth;
                    BreastfeedingHistory = screening.BreastfeedingHistory;
                    MenopausalStatus = screening.MenopausalStatus;
                    HistoryOfBreastLumps = screening.HistoryOfBreastLumps;
                    PreviousBreastBiopsies = screening.PreviousBreastBiopsies;
                    UseOfHormonalContraceptives = screening.UseOfHormonalContraceptives;
                    HormoneReplacementTherapy = screening.HormoneReplacementTherapy;
                    IsAgeRiskFactor = screening.IsAgeRiskFactor;
                    IsFamilyHistoryRiskFactor = screening.IsFamilyHistoryRiskFactor;
                    IsGeneticMutationsRiskFactor = screening.IsGeneticMutationsRiskFactor;
                    IsPreviousRadiationTherapyRiskFactor = screening.IsPreviousRadiationTherapyRiskFactor;
                    IsObesityRiskFactor = screening.IsObesityRiskFactor;
                    IsOtherRiskFactor = screening.IsOtherRiskFactor;
                    AlcoholConsumption = screening.AlcoholConsumption;
                    Smoking = screening.Smoking;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while loading the screening: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task ShowDateOfScreeningPickerAsync()
        {
            try
            {
                var page = Application.Current?.MainPage;
                if (page == null) return;

                // Use a DatePicker dialog or custom implementation
                var date = await page.DisplayPromptAsync("Select Date", "Enter Date of Screening (dd-MM-yyyy):", 
                    initialValue: DateOfScreening.ToString("dd-MM-yyyy"));
                
                if (date != null && DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var result))
                {
                    DateOfScreening = result;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to show date picker: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task ShowLMPPickerAsync()
        {
            try
            {
                var page = Application.Current?.MainPage;
                if (page == null) return;

                // Use a DatePicker dialog or custom implementation
                var date = await page.DisplayPromptAsync("Select Date", "Enter Date of Last Menstrual Period (dd-MM-yyyy):", 
                    initialValue: DateOfLastMenstrualPeriod.ToString("dd-MM-yyyy"));
                
                if (date != null && DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var result))
                {
                    DateOfLastMenstrualPeriod = result;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to show date picker: {ex.Message}", "OK");
            }
        }
    }
} 