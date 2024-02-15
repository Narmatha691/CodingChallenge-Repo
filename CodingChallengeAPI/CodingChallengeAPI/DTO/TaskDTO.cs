using CodingChallengeAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CodingChallengeAPI.DTO
{
    public class TaskDTO
    {
        public int? TaskId { get; set; }
        public int? UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime? CompletedDate { get; set; }
        public int? Completed { get; set; }
    }
}
