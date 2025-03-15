using NCD.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCD.Services
{
    public class StudentService : BaseService
    {
        private const string StudentPath = "student";

        public static async Task<ServiceResponseDTO> GetInstitutionsByUserId(int userId, string token)
        {
            ServiceResponseDTO _responseResult = new();
            try
            {
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await Client.GetAsync($"{BaseUrl}{StudentPath}/institutions/{userId}");
                
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    _responseResult.Success = false;
                    _responseResult.Message = "Token expired. Please login again.";
                    _responseResult.StatusCode = 401;
                    return _responseResult;
                }

                string dataResult = await response.Content.ReadAsStringAsync();
                _responseResult.Result = JsonConvert.DeserializeObject<List<AppInstitutionDTO>>(dataResult) ?? new List<AppInstitutionDTO>();
                _responseResult.Success = response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                _responseResult.Success = false;
                _responseResult.Message = ex.Message;
                _responseResult.StatusCode = 500;
            }
            return _responseResult;
        }

    public static async Task<ServiceResponseDTO> SaveStudent(Student student, string token)
    {
        ServiceResponseDTO _responseResult = new();
        try
        {
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            
            var jsonContent = JsonConvert.SerializeObject(student);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await Client.PostAsync($"{BaseUrl}{StudentPath}", stringContent);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _responseResult.Success = false;
                _responseResult.Message = "Token expired. Please login again.";
                _responseResult.StatusCode = 401;
                return _responseResult;
            }

            string dataResult = await response.Content.ReadAsStringAsync();
            _responseResult = JsonConvert.DeserializeObject<ServiceResponseDTO>(dataResult);
            
        }
        catch (Exception ex)
        {
            _responseResult.Success = false;
            _responseResult.Message = ex.Message;
            _responseResult.StatusCode = 500;
        }
        return _responseResult;
    }
    }
}
