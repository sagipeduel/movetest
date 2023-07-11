using movetest.Models;

namespace movetest.BAL
{
    public interface IUserService
    {
        Task<bool> CreateUser(User user);
        Task<bool> DeleteUser(int userId);
        Task<User> GetUser(int userId);
        Task<List<User>> GetUsers(int page);
        Task<bool> UpdateUser(int userId, User user);
    }
}