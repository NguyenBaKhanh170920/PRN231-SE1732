using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.DTO;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly PE_PRN_24SumB1Context _dbContext;

        public CourseController(PE_PRN_24SumB1Context dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var courses = _dbContext.Courses
                .Include(c => c.Categories)
                .Include(c => c.Instructor)
                .ToList();

            var mappedCourses = courses.Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Description = c.Description,
                InstructorId = c.InstructorId,
                InstructorName = c.Instructor.Name,
                Categories = c.Categories.Select(c =>
                new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                }).ToList()
            });

            return Ok(mappedCourses);
        }

        [HttpDelete("{cid}/{uid}")]
        public IActionResult Delete(int cid, int uid)
        {

            var enrollment = _dbContext.Enrollments
                .FirstOrDefault(e => e.UserId == uid && e.CourseId == cid);

            if (enrollment == null)
            {
                return NoContent();
            }

            try
            {
                _dbContext.Enrollments.Remove(enrollment);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Conflict(ex);

            }

        }

    }
}
