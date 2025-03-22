using NCD.DTO;
using Newtonsoft.Json;
using Nirmaya.DTO;
using System.Text;

namespace NCD.Services
{
    public class RegistrationService : BaseService
    {
        private const string RegistrationPath = "Registration";

        public static async Task<RegistrationDefaultDataDTO> GetDefaultDataAsync(int stateId)
        {
            RegistrationDefaultDataDTO registrations = new();
            try
            {
                HttpResponseMessage response = await Client.GetAsync(BaseUrl + RegistrationPath + $"/RegistrationDefaultData/{stateId}");
                if (response.IsSuccessStatusCode)
                {
                    string dataResult = await response.Content.ReadAsStringAsync();
                    if(!string.IsNullOrEmpty(dataResult))
                    {
                        registrations = JsonConvert.DeserializeObject<RegistrationDefaultDataDTO>(dataResult) ?? new RegistrationDefaultDataDTO();

                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
            }
            return registrations;
        }


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
                        var resultString = serviceResponse.Result.ToString();
                        if (!string.IsNullOrEmpty(resultString))
                        {
                            registrations = JsonConvert.DeserializeObject<List<RegistrationListItem>>(resultString);
                        }
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
                        var resultString = serviceResponse.Result.ToString();
                        if (!string.IsNullOrEmpty(resultString))
                        {
                            registrations = JsonConvert.DeserializeObject<List<RegistrationListItem>>(resultString);
                        }
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
                        var resultString = serviceResponse.Result.ToString();
                        if (!string.IsNullOrEmpty(resultString))
                        {
                            registration = JsonConvert.DeserializeObject<RegistrationListItem>(resultString);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
            }
            return registration ?? new RegistrationListItem();
        }

        /// <summary>
        /// Get blocks for a specific district
        /// </summary>
        /// <param name="districtId">District ID</param>
        /// <returns>List of blocks in the specified district</returns>
        public static async Task<List<DropdownDTO>> GetBlocksByDistrictAsync(int districtId)
        {
            List<DropdownDTO> blocks = new();
            try
            {
                HttpResponseMessage response = await Client.GetAsync(BaseUrl + RegistrationPath + $"/GetBlocksByDistrict?districtId={districtId}");
                if (response.IsSuccessStatusCode)
                {
                    string dataResult = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(dataResult))
                    {
                        blocks = JsonConvert.DeserializeObject<List<DropdownDTO>>(dataResult) ?? new List<DropdownDTO>();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
            }
            return blocks ?? new List<DropdownDTO>();
        }

        /// <summary>
        /// Get villages for a specific block
        /// </summary>
        /// <param name="blockId">Block ID</param>
        /// <returns>List of villages in the specified block</returns>
        public static async Task<List<DropdownDTO>> GetVillagesByBlockAsync(int blockId)
        {
            List<DropdownDTO> villages = new();
            try
            {
                HttpResponseMessage response = await Client.GetAsync(BaseUrl + RegistrationPath + $"/GetVillagesByBlock?blockId={blockId}");
                if (response.IsSuccessStatusCode)
                {
                    string dataResult = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(dataResult))
                    {
                        villages = JsonConvert.DeserializeObject<List<DropdownDTO>>(dataResult) ?? new List<DropdownDTO>();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
            }
            return villages ?? new List<DropdownDTO>();
        }
    }
} 