using AutoMapper;
using CodingChallengeAPI.DTO;
using CodingChallengeAPI.Entities;

namespace CodingChallengeAPI.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
