using Microsoft.Extensions.Caching.Memory;
using movetest.Models;
using Newtonsoft.Json;

namespace movetest.BAL
{
    public class UserService : IUserService
    {
        private static string _url = "https://reqres.in";
        private IHttpHelper _httpHelper;
        private readonly IMemoryCache _cache;
        public UserService(IHttpHelper httpHelper, IMemoryCache cache)
        {
            _httpHelper = httpHelper;
            _cache = cache;
        }

        public async Task<List<User>> GetUsers(int pageId)
        {
            var users = new Response<List<User>>();
            return await _cache.GetOrCreateAsync($"Page_{pageId}", async entry =>
            {
                var jsonResponse = await _httpHelper.GetAsync($"{_url}/api/users?page={pageId}");
                users = JsonConvert.DeserializeObject<Response<List<User>>>(jsonResponse);
                return users.Data;
            });
        }

        public async Task<User> GetUser(int userId)
        {
            return await _cache.GetOrCreateAsync($"User_{userId}", async entry =>
            {
                var jsonResponse = await _httpHelper.GetAsync($"{_url}/api/users/{userId}");
                var user = JsonConvert.DeserializeObject<Response<User>>(jsonResponse);
                return user.Data;
            });
        }

        public async Task<bool> CreateUser(User user)
        {
            if (_cache.TryGetValue($"User_{user.Id}", out _))
            {
                return false;
            }
            var jsonResponse = await _httpHelper.PostAsync($"{_url}/api/users", JsonConvert.SerializeObject(user));
            _cache.Set($"User_{user.Id}", user);
            return true;
        }

        public async Task<bool> UpdateUser(int userId, User user)
        {
            if (_cache.TryGetValue($"User_{userId}", out _))
            {
                return false;
            }
            var jsonResponse = await _httpHelper.PostAsync($"{_url}/api/users/{userId}", JsonConvert.SerializeObject(user));
            _cache.Set($"User_{user.Id}", user);
            return true;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            if (!_cache.TryGetValue($"User_{userId}", out _))
            {
                return false;
            }
            var jsonResponse = await _httpHelper.DeleteAsync($"{_url}/api/users/{userId}");
            _cache.Remove($"User_{userId}");
            return true;
        }
    }
}
