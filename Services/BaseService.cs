namespace NCD.Services
{
    public class BaseService
    {
        public static string BaseUrl = "http://125.63.74.18:8081/nirmyaapi/api/";
        public static HttpClient Client { get; set; } = new HttpClient()
        {
            MaxResponseContentBufferSize = 9999999,
        };
    }
}
