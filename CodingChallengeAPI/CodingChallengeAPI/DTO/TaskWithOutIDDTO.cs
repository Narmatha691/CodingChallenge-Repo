namespace CodingChallengeAPI.DTO
{
    public class TaskWithOutIDDTO
    {
        public int? UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
