
using System.ComponentModel.DataAnnotations;

namespace CodingChallengeAPI.DTO
{
    public class UserDTO
    {
        public string? Name { get; set; }
        public string? UserEmail { get; set; }
        public string? Password { get; set; }
    }
}
