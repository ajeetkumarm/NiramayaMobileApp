using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Kadam.Services;
using Kadam.DTO;
using Newtonsoft.Json;
using Kadam.Utilities;

namespace Kadam.ViewModels
{
    public partial class StudentRegistrationViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool isPersonalTabVisible = false;

        [ObservableProperty]
        private bool isEducationTabVisible = false;

        [ObservableProperty]
        private bool isFamilyTabVisible = false;

        [ObservableProperty]
        private Color personalTabColor = Color.FromArgb("#00BCD4");

        [ObservableProperty]
        private Color educationTabColor = Color.FromArgb("#00BCD4");

        [ObservableProperty]
        private Color familyTabColor = Color.FromArgb("#00BCD4");

        [ObservableProperty]
        private Student student;

        [ObservableProperty]
        private AppInstitutionDTO defaultInstitution;

        public ObservableCollection<string> GenderOptions { get; } = new()
        {
            "Male", "Female", "Other"
        };

        public ObservableCollection<AppInstitutionDTO> Institutions { get; } = new();
        public ObservableCollection<string> Sections { get; } = new();
        public ObservableCollection<AppGradeSectionDTO> Grades { get; } = new();
        public ObservableCollection<string> ChildStatusOptions { get; } = new();
        public ObservableCollection<string> StayDurationOptions { get; } = new();
        public ObservableCollection<string> Reasons { get; } = new();

        [ObservableProperty]
        private AppInstitutionDTO selectedInstitution;

        [ObservableProperty]
        private AppGradeSectionDTO selectedGrade = new();

        public bool IsNotLoading => !IsLoading;

        public StudentRegistrationViewModel()
        {
            DefaultInstitution = new AppInstitutionDTO 
            { 
                Id = 0, 
                InstitutionName = "Select Institution" 
            };

            Student = new Student
            {
                DateOfBirth = DateTime.Now,
                CreatedBy = 0,
                IsDeleted = false
            };

            Institutions.Add(DefaultInstitution);
            SelectedInstitution = DefaultInstitution;

            LoadInstitutions();
            InitializeCollections();
        }

        private void InitializeCollections()
        {
            // Initialize your collections here
            Grades.Clear();
            
            // Add more grades as needed

            ChildStatusOptions.Clear();
            ChildStatusOptions.Add("Never Enrolled");
            ChildStatusOptions.Add("Dropped Out");
            // Add more status options as needed

            StayDurationOptions.Clear();
            StayDurationOptions.Add("Less than 6 months");
            StayDurationOptions.Add("6 months to 1 year");
            StayDurationOptions.Add("More than 1 year");
            // Add more duration options as needed

            Reasons.Clear();
            Reasons.Add("Financial");
            Reasons.Add("Migration");
            Reasons.Add("Other");
            // Add more reasons as needed
        }

        partial void OnSelectedInstitutionChanged(AppInstitutionDTO value)
        {
            if (value != null && value != DefaultInstitution)
            {
                Student.InstitutionId = value.Id;
                Grades.Clear();

                foreach (var grade in value.GradeSections)
                {
                    Grades.Add(grade);
                }
                System.Diagnostics.Debug.WriteLine($"Selected Institution: {value.InstitutionName}, ID: {value.Id}");
            }
            else
            {
                Grades.Clear();
                Student.InstitutionId = 0;
            }
        }

        partial void OnSelectedGradeChanged(AppGradeSectionDTO value)
        {
            if (value != null)
            {
                Student.GradeId = value.Id;
                // Clear and populate sections for the selected grade
                Sections.Clear();

                var sections = value.Sections.Split(',');

                foreach (var section in sections)
                {
                    Sections.Add(section);
                }
                //System.Diagnostics.Debug.WriteLine($"Selected Grade: {value.Grade}, Available Sections: {Sections.Count}");
            }
        }
        

        [RelayCommand]
        private void SwitchToPersonalTab()
        {
            IsPersonalTabVisible = true;
            IsEducationTabVisible = false;
            IsFamilyTabVisible = false;
            PersonalTabColor = Color.FromArgb("#00ACC1");
            EducationTabColor = Color.FromArgb("#00BCD4");
            FamilyTabColor = Color.FromArgb("#00BCD4");
        }

        [RelayCommand]
        private void SwitchToEducationTab()
        {
            IsPersonalTabVisible = false;
            IsEducationTabVisible = true;
            IsFamilyTabVisible = false;
            PersonalTabColor = Color.FromArgb("#00BCD4");
            EducationTabColor = Color.FromArgb("#00ACC1");
            FamilyTabColor = Color.FromArgb("#00BCD4");
        }

        [RelayCommand]
        private void SwitchToFamilyTab()
        {
            IsPersonalTabVisible = false;
            IsEducationTabVisible = false;
            IsFamilyTabVisible = true;
            PersonalTabColor = Color.FromArgb("#00BCD4");
            EducationTabColor = Color.FromArgb("#00BCD4");
            FamilyTabColor = Color.FromArgb("#00ACC1");
        }

        private async Task LoadInstitutions()
        {
            try
            {
                IsLoading = true;
                string token = await AppHelper.GetToken() ?? "";
                int userId = await AppHelper.GetUserId();

                if (string.IsNullOrEmpty(token) || userId == 0)
                {
                    await Shell.Current.DisplayAlert("Error", "Please login again", "OK");
                    return;
                }

                var response = await StudentService.GetInstitutionsByUserId(userId, token);
                
                if (response.Success)
                {
                    var institutionsList = (List<AppInstitutionDTO>)response.Result;
                    if (institutionsList != null)
                    {
                        Institutions.Clear();
                        Institutions.Add(DefaultInstitution);
                        foreach (var institution in institutionsList)
                        {
                            Institutions.Add(institution);
                        }
                        SelectedInstitution = DefaultInstitution;
                    }
                }
                else if (response.StatusCode == 401)
                {
                    await Shell.Current.DisplayAlert("Session Expired", "Please login again", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", response.Message, "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            try
            {
                if (!ValidateStudent()) return;

                IsLoading = true;
                string token = await AppHelper.GetToken() ?? "";
                int userId = await AppHelper.GetUserId();

                if (string.IsNullOrEmpty(token) || userId == 0)
                {
                    await Shell.Current.DisplayAlert("Error", "Please login again", "OK");
                    return;
                }

                var response = await StudentService.SaveStudent(Student, token);

                if (response.Success)   
                {
                    await Shell.Current.DisplayAlert("Success", "Student data saved successfully", "OK");
                    await Shell.Current.Navigation.PopAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", response.Message, "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool ValidateStudent()
        {
            if (string.IsNullOrEmpty(Student.FirstName))
            {
                Shell.Current.DisplayAlert("Validation Error", "First Name is required", "OK");
                return false;
            }

            if (SelectedInstitution == DefaultInstitution || SelectedInstitution.Id == 0)
            {
                Shell.Current.DisplayAlert("Validation Error", "Please select an institution", "OK");
                return false;
            }

            // Add more validation as needed
            return true;
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        partial void OnIsLoadingChanged(bool value)
        {
            OnPropertyChanged(nameof(IsNotLoading));
        }
    }
} 