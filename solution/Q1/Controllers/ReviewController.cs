using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.DTO;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly PE_PRN_24SumB1Context _dbContext;

        public ReviewController(PE_PRN_24SumB1Context dbContext)
        {
            _dbContext = dbContext;
        }
        [Authorize]
        [HttpGet("User/{id}")]
        public IActionResult getReview(int id)
        {
            var reviews = _dbContext.Reviews
                .Include(r => r.Course)
                .Where(r => r.UserId == id)
                .Select(r => new ReviewDto
                {
                    CourseId = r.CourseId,
                    Title = r.Course.Title,
                    ReviewText = r.ReviewText,
                    ReviewDate = r.ReviewDate,
                    Rating = r.Rating,
                })
                .ToList();

            return Ok(reviews);
        }
    }
}
