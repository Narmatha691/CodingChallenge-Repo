using CodingChallengeAPI.Entities;
using CodingChallengeAPI.Model;

namespace CodingChallengeAPI.Services
{
    public interface IUserService
    {
        ResultModel AddUser(User user);
        User ValidteUser(string email, string password);
        List<User> GetAllUsers();
        User GetUserById(int id);
        ResultModel EditUser(User newuser);
        ResultModel DeleteUser(int userId);
    }
}
