
namespace NCD.Utilities
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
        public static async Task<int> GetUserCategory()
        {
            var userCategory = await SecureStorage.GetAsync("user_Category");
            return userCategory != null ? int.Parse(userCategory) : 0;
        }
        public static async Task<int> GetStateId()
        {
            var userId = await SecureStorage.GetAsync("state_Id");
            return userId != null ? int.Parse(userId) : 0;
        }
    }
}
