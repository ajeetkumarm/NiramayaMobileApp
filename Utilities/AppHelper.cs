
namespace Kadam.Utilities
{
    public static class AppHelper
    {
        public static async Task<string?> GetToken()
        {
            var token = await SecureStorage.GetAsync("auth_token");
            return token ?? string.Empty;
        }
        public static async Task<int> GetUserId()
        {
            var userId = await SecureStorage.GetAsync("user_id");
            return userId != null ? int.Parse(userId) : 0;
        }
    }
}
