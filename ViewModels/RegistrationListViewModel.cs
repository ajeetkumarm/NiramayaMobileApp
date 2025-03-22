using System.Collections.ObjectModel;
using System.Windows.Input;
using NCD.DTO;
using NCD.Services;
using NCD.Utilities;
using NCD.Views;

namespace NCD.ViewModels
{
    public class RegistrationListViewModel : BaseViewModel
    {
        private ObservableCollection<RegistrationListItem> _registrations;
        private string _searchQuery;
        private bool _isRefreshing;
        
        public ObservableCollection<RegistrationListItem> Registrations
        {
            get => _registrations;
            set => SetProperty(ref _registrations, value);
        }
        
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (SetProperty(ref _searchQuery, value))
                {
                    FilterRegistrations();
                }
            }
        }
        
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }
        
        public ICommand RefreshCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ViewDetailsCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand ScreenCommand { get; }
        
        public RegistrationListViewModel()
        {
            Title = "Registration List";
            Registrations = new ObservableCollection<RegistrationListItem>();
            
            RefreshCommand = new Command(async () => await LoadRegistrationsAsync());
            SearchCommand = new Command(async () => await SearchRegistrationsAsync());
            ViewDetailsCommand = new Command<RegistrationListItem>(OnViewDetails);
            EditCommand = new Command<RegistrationListItem>(OnEdit);
            ScreenCommand = new Command<RegistrationListItem>(OnScreen);
            
            // Load registrations initially
            Task.Run(async () => await LoadRegistrationsAsync());
        }
        
        private async Task LoadRegistrationsAsync()
        {
            try
            {
                IsRefreshing = true;
                IsBusy = true;

                int currentUserId = await AppHelper.GetUserId();
                DateTime _fromDate = DateTime.Now.AddDays(-30);
                DateTime _toDate = DateTime.Today.AddDays(1);

                int UserCategory = await AppHelper.GetUserCategory();
                string queryType = "A";

                if (currentUserId == 1 || UserCategory == 1)
                {
                    queryType = "A";

                }
                else if (UserCategory == 7)
                {
                    queryType = "F";
                }
                else
                {
                    queryType = "T";
                }

                RegistrationFilterDTO filterdata = new RegistrationFilterDTO
                {
                    QueryType = queryType,
                    CreatedBy = currentUserId.ToString(),
                    FromDate = _fromDate,
                    ToDate = _toDate
                };

                // Call the service to get registrations
                var registrations = await RegistrationService.GetRegistrationsAsync(filterdata);
                
                // If API call fails or returns no data, use sample data for demonstration
                if (registrations == null || !registrations.Any())
                {
                    registrations = GetSampleData();
                }
                
                Registrations = new ObservableCollection<RegistrationListItem>(registrations);
            }
            catch (Exception ex)
            {
                // Handle error
                Registrations = new ObservableCollection<RegistrationListItem>(GetSampleData());
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
        
        private async Task SearchRegistrationsAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                await LoadRegistrationsAsync();
                return;
            }
            
            try
            {
                IsBusy = true;
                
                // Call the service to search registrations
                var searchResults = await RegistrationService.SearchRegistrationsAsync(SearchQuery);
                
                // If API call fails or returns no data, filter local data
                if (searchResults == null || !searchResults.Any())
                {
                    FilterRegistrationsLocally();
                }
                else
                {
                    Registrations = new ObservableCollection<RegistrationListItem>(searchResults);
                }
            }
            catch (Exception ex)
            {
                // Handle error
                FilterRegistrationsLocally();
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        private void FilterRegistrations()
        {
            // This method is triggered when SearchQuery changes
            // Implement debounce if needed for better UX
        }
        
        private void FilterRegistrationsLocally()
        {
            // Filter the current collection if API search fails
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                return;
            }
            
            var query = SearchQuery.ToLowerInvariant();
            var filteredItems = Registrations.Where(r => 
                r.Name?.ToLowerInvariant().Contains(query) == true || 
                r.FatherName?.ToLowerInvariant().Contains(query) == true || 
                r.EnrollmentId?.ToLowerInvariant().Contains(query) == true).ToList();
                
            Registrations = new ObservableCollection<RegistrationListItem>(filteredItems);
        }
        
        private List<RegistrationListItem> GetSampleData()
        {
            // Sample data for demonstration
            return new List<RegistrationListItem>
            {
                new RegistrationListItem 
                { 
                    Id = 1, 
                    Project = "Niramaya - CSN", 
                    State = "Maharashtra", 
                    Month = "February", 
                    Village = "Adul Bk", 
                    EnrollmentId = "JJ8$2?0HJHJFAIBIF$98970$2?778$541$15$455$2AAFBHF", 
                    Name = "Shital Ghodake", 
                    FatherName = "Suneel", 
                    Age = 22 
                },
                new RegistrationListItem 
                { 
                    Id = 2, 
                    Project = "Niramaya - CSN", 
                    State = "Maharashtra", 
                    Month = "January", 
                    Village = "Pune", 
                    EnrollmentId = "KK7$1?0HJHJFAIBIF$75680$2?768$541$15$455$2AAFBHF", 
                    Name = "Rahul Sharma", 
                    FatherName = "Rajesh", 
                    Age = 28 
                },
                new RegistrationListItem 
                { 
                    Id = 3, 
                    Project = "Niramaya - CSN", 
                    State = "Maharashtra", 
                    Month = "March", 
                    Village = "Nagpur", 
                    EnrollmentId = "LL9$3?0HJHJFAIBIF$85680$2?768$541$15$455$2AAFBHF", 
                    Name = "Priya Patel", 
                    FatherName = "Mahesh", 
                    Age = 32 
                }
            };
        }
        
        private void OnViewDetails(RegistrationListItem registration)
        {
            // Navigate to details page
            // Shell.Current.GoToAsync($"{nameof(RegistrationDetailPage)}?id={registration.Id}");
        }
        
        private void OnEdit(RegistrationListItem registration)
        {
            // Navigate to edit page
            // Shell.Current.GoToAsync($"{nameof(Registration)}?id={registration.Id}");
        }
        
        private void OnScreen(RegistrationListItem registration)
        {
            // Navigate to screening page
            // Shell.Current.GoToAsync($"{nameof(ScreeningPage)}?id={registration.Id}");
        }
    }
} 