using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NirmayaMobileApp.DTO;
using NirmayaMobileApp.Services.Interfaces;

namespace NirmayaMobileApp.Services
{
    public class BreastCancerScreeningService : IBreastCancerScreeningService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _localStorageKey = "breast_cancer_screenings";

        public BreastCancerScreeningService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "https://api.example.com/api/breastcancerscreenings"; // Replace with your actual API endpoint
        }

        public async Task<List<BreastCancerScreeningDTO>> GetAllScreeningsAsync()
        {
            try
            {
                // Try to get data from API first
                var response = await _httpClient.GetAsync(_baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<BreastCancerScreeningDTO>>(content);
                }

                // Fallback to local storage
                return await GetFromLocalStorageAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting screenings: {ex.Message}");
                // Fallback to local storage
                return await GetFromLocalStorageAsync();
            }
        }

        public async Task<BreastCancerScreeningDTO> GetScreeningByIdAsync(int id)
        {
            try
            {
                // Try to get data from API first
                var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<BreastCancerScreeningDTO>(content);
                }

                // Fallback to local storage
                var screenings = await GetFromLocalStorageAsync();
                return screenings.Find(s => s.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting screening by id: {ex.Message}");
                // Fallback to local storage
                var screenings = await GetFromLocalStorageAsync();
                return screenings.Find(s => s.Id == id);
            }
        }

        public async Task<BreastCancerScreeningDTO> CreateScreeningAsync(BreastCancerScreeningDTO screening)
        {
            screening.CreatedAt = DateTime.Now;
            
            try
            {
                // Try to save to API first
                var json = JsonConvert.SerializeObject(screening);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_baseUrl, content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<BreastCancerScreeningDTO>(responseContent);
                }

                // Fallback to local storage
                return await SaveToLocalStorageAsync(screening);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating screening: {ex.Message}");
                // Fallback to local storage
                return await SaveToLocalStorageAsync(screening);
            }
        }

        public async Task<BreastCancerScreeningDTO> UpdateScreeningAsync(BreastCancerScreeningDTO screening)
        {
            screening.UpdatedAt = DateTime.Now;
            
            try
            {
                // Try to update on API first
                var json = JsonConvert.SerializeObject(screening);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseUrl}/{screening.Id}", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<BreastCancerScreeningDTO>(responseContent);
                }

                // Fallback to local storage
                return await UpdateInLocalStorageAsync(screening);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating screening: {ex.Message}");
                // Fallback to local storage
                return await UpdateInLocalStorageAsync(screening);
            }
        }

        public async Task<bool> DeleteScreeningAsync(int id)
        {
            try
            {
                // Try to delete from API first
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                // Fallback to local storage
                return await DeleteFromLocalStorageAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting screening: {ex.Message}");
                // Fallback to local storage
                return await DeleteFromLocalStorageAsync(id);
            }
        }

        // Local storage methods
        private async Task<List<BreastCancerScreeningDTO>> GetFromLocalStorageAsync()
        {
            try
            {
                var screeningsJson = await SecureStorage.GetAsync(_localStorageKey);
                if (string.IsNullOrEmpty(screeningsJson))
                {
                    return new List<BreastCancerScreeningDTO>();
                }
                return JsonConvert.DeserializeObject<List<BreastCancerScreeningDTO>>(screeningsJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting from local storage: {ex.Message}");
                return new List<BreastCancerScreeningDTO>();
            }
        }

        private async Task<BreastCancerScreeningDTO> SaveToLocalStorageAsync(BreastCancerScreeningDTO screening)
        {
            var screenings = await GetFromLocalStorageAsync();
            
            // Generate a new ID if not set
            if (screening.Id <= 0)
            {
                var maxId = screenings.Count > 0 ? screenings.Max(s => s.Id) : 0;
                screening.Id = maxId + 1;
            }
            
            screenings.Add(screening);
            
            var json = JsonConvert.SerializeObject(screenings);
            await SecureStorage.SetAsync(_localStorageKey, json);
            
            return screening;
        }

        private async Task<BreastCancerScreeningDTO> UpdateInLocalStorageAsync(BreastCancerScreeningDTO screening)
        {
            var screenings = await GetFromLocalStorageAsync();
            var index = screenings.FindIndex(s => s.Id == screening.Id);
            
            if (index >= 0)
            {
                screenings[index] = screening;
                var json = JsonConvert.SerializeObject(screenings);
                await SecureStorage.SetAsync(_localStorageKey, json);
            }
            
            return screening;
        }

        private async Task<bool> DeleteFromLocalStorageAsync(int id)
        {
            var screenings = await GetFromLocalStorageAsync();
            var removed = screenings.RemoveAll(s => s.Id == id) > 0;
            
            if (removed)
            {
                var json = JsonConvert.SerializeObject(screenings);
                await SecureStorage.SetAsync(_localStorageKey, json);
            }
            
            return removed;
        }
    }
} 