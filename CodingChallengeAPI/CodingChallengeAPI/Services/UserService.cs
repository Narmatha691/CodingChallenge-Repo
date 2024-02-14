using AutoMapper;
using CodingChallengeAPI.Database;
using CodingChallengeAPI.Entities;
using CodingChallengeAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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


        public List<User> GetAllUsers()
        {
            try
            {
                return context.Users.ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public User GetUserById(int id)
        {

            try
            {
                return context.Users.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ResultModel EditUser(User newuser)
        {
            try
            {
                User existinguser = context.Users.SingleOrDefault(u => u.UserId == newuser.UserId);

                if (existinguser != null)
                {
                    context.Entry(existinguser).State = EntityState.Detached;
                    context.Users.Update(newuser);
                    context.SaveChanges();
                    return new ResultModel { Success = true, Message = "User edited successfully." };
                }
                else
                {
                    return new ResultModel { Success = false, Message = "User not found." };
                }
            }
            catch (Exception ex)
            {

                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public ResultModel DeleteUser(int userId)
        {
            try
            {
                User post = context.Users.SingleOrDefault(p => p.UserId == userId);

                if (post != null)
                {
                    context.SaveChanges();
                    return new ResultModel { Success = true, Message = "User deleted successfully." };
                }
                else
                {
                    return new ResultModel { Success = false, Message = "User not found." };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }


        public User ValidteUser(string email, string password)
        {
            return context.Users.SingleOrDefault(u => u.UserEmail == email && u.Password == password);
        }

    }
}
