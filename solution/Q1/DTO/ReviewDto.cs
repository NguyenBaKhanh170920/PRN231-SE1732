using Q1.Models;

namespace Q1.DTO
{
    public class ReviewDto
    {
        public int? CourseId { get; set; }
        public string? Title { get; set; }
        public string? ReviewText { get; set; }
        public DateTime? ReviewDate { get; set; }
        public int? Rating { get; set; }
    }
}
