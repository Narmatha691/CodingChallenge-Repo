using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CodingChallengeAPI.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int? UserId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Role { get; set; }
        [Required]
        public string? UserEmail { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
