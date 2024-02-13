using AutoMapper;
using CodingChallengeAPI.Database;
using CodingChallengeAPI.Entities;
using CodingChallengeAPI.Model;

namespace CodingChallengeAPI.Services
{
    public class UserService : IUserService
    {

        private readonly MyContext context;
        private readonly IMapper _mapper;

        public UserService(MyContext context, IMapper mapper)
        {
            this.context = context;
            this._mapper = mapper;

        }
        public ResultModel AddUser(User user)
        {
            try
            {
                if (context.Users.Any(u => u.UserEmail == user.UserEmail))
                {
                    return new ResultModel { Success = false, Message = "User with the same email already exists." };
                }
                context.Users.Add(user);
                context.SaveChanges();

                return new ResultModel { Success = true, Message = "User added successfully." };
            }
            catch (Exception ex)
            {
                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

    }
}
