using NCD.DTO;
using Newtonsoft.Json;
using System.Text;

namespace NCD.Services
{
    public class RegistrationService : BaseService
    {
        private const string RegistrationPath = "Registration";
        
        /// <summary>
        /// Get all registrations
        /// </summary>
        /// <returns>List of registration items</returns>
        public static async Task<List<RegistrationListItem>> GetRegistrationsAsync(RegistrationFilterDTO filterdata)
        {
            List<RegistrationListItem> registrations = new();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(filterdata);
                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await Client.PostAsync(BaseUrl + RegistrationPath + $"/GetRegistration", stringContent);
                if (response.IsSuccessStatusCode)
                {
                    string dataResult = await response.Content.ReadAsStringAsync();
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponseDTO>(dataResult);
                    
                    if (serviceResponse != null && serviceResponse.Result != null)
                    {
                        registrations = JsonConvert.DeserializeObject<List<RegistrationListItem>>(serviceResponse.Result.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
            }
            return registrations ?? new List<RegistrationListItem>();
        }
        
        /// <summary>
        /// Search registrations by query string
        /// </summary>
        /// <param name="searchQuery">Search query</param>
        /// <returns>Filtered list of registration items</returns>
        public static async Task<List<RegistrationListItem>> SearchRegistrationsAsync(string searchQuery)
        {
            List<RegistrationListItem> registrations = new();
            try
            {
                HttpResponseMessage response = await Client.GetAsync(BaseUrl + RegistrationPath + $"/Search?query={searchQuery}");
                if (response.IsSuccessStatusCode)
                {
                    string dataResult = await response.Content.ReadAsStringAsync();
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponseDTO>(dataResult);
                    
                    if (serviceResponse != null && serviceResponse.Result != null)
                    {
                        registrations = JsonConvert.DeserializeObject<List<RegistrationListItem>>(serviceResponse.Result.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
            }
            return registrations ?? new List<RegistrationListItem>();
        }
        
        /// <summary>
        /// Get a registration by ID
        /// </summary>
        /// <param name="id">Registration ID</param>
        /// <returns>Registration details</returns>
        public static async Task<RegistrationListItem> GetRegistrationByIdAsync(int id)
        {
            RegistrationListItem registration = null;
            try
            {
                HttpResponseMessage response = await Client.GetAsync(BaseUrl + RegistrationPath + $"/GetRegistrationById?Id={id}");
                if (response.IsSuccessStatusCode)
                {
                    string dataResult = await response.Content.ReadAsStringAsync();
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponseDTO>(dataResult);
                    
                    if (serviceResponse != null && serviceResponse.Result != null)
                    {
                        registration = JsonConvert.DeserializeObject<RegistrationListItem>(serviceResponse.Result.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
            }
            return registration ?? new RegistrationListItem();
        }
    }
} 