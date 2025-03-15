namespace NCD.Services
{
    public class BaseService
    {
        public static string BaseUrl = "http://119.82.79.46:903/api/";
        public static HttpClient Client { get; set; } = new HttpClient()
        {
            MaxResponseContentBufferSize = 9999999,
        };
    }
}
