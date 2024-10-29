using Q1.Models;

namespace Q1.DTO
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? InstructorId { get; set; }
        public string? InstructorName { get; set; }
        public List<CategoryDto>? Categories { get; set; }
    }

    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
