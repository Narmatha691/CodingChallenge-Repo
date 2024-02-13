using CodingChallengeAPI.Entities;
using CodingChallengeAPI.Model;

namespace CodingChallengeAPI.Services
{
    public interface IUserService
    {
        ResultModel AddUser(User user);
    }
}
