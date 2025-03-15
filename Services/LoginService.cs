using NCD.DTO;
using Newtonsoft.Json;
using System.Text;

namespace NCD.Services
{
    public class LoginService : BaseService
    {
        private const string LoginPath = "Login";
        public static async Task<ServiceResponseDTO> LoginValidate(LoginRequestDTO LoginInfo)
        {
            ServiceResponseDTO _responseResult = new();
            try
            {
                string jsonData = JsonConvert.SerializeObject(LoginInfo);
                StringContent content = new(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                response = await Client.PostAsync(BaseUrl + LoginPath + "/LoginToken", content);
                string dataResult = await response.Content.ReadAsStringAsync();
                _responseResult = JsonConvert.DeserializeObject<ServiceResponseDTO>(dataResult) ?? new ServiceResponseDTO();
            }
            catch(Exception ex)
            {

            }
            return _responseResult;
        }
    }
}
