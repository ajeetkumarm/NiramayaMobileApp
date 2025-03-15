using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using NCD.Services;
using NCD.DTO;
using Newtonsoft.Json;
using NCD.Utilities;

namespace NCD.ViewModels
{
    public partial class RegistrationViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isLoading;

        

        public bool IsNotLoading => !IsLoading;

        public RegistrationViewModel()
        {
            DefaultInstitution = new AppInstitutionDTO 
            { 
                Id = 0, 
                InstitutionName = "Select Institution" 
            };

            

            LoadInstitutions();
            InitializeCollections();
        }

        private void InitializeCollections()
        {
            
            // Add more reasons as needed
        }

        
        

        
        [RelayCommand]
        private async Task Save()
        {
            try
            {
                
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