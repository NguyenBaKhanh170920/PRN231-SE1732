using Q1.Models;

namespace Q1.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? UserEmail { get; set; }

       public List<UserCourseDto> Courses { get; set; }
    }

    public class UserCourseDto
    {
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public DateTime? EnrolledDate { get; set; }
    }

}
