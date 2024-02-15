using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingChallengeAPI.Entities
{
    [Table("Tasks")]
    public class TaskEntity
    {
        [Key]
        public int? TaskId { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? CompletedDate { get; set; }
        [Required]
        public int Completed { get; set; } = 0;
    }
}
