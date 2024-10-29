using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Q1.DTO;
using Q1.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PE_PRN_24SumB1Context _dbContext;

        public UserController(PE_PRN_24SumB1Context dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) { 
            var user = _dbContext.Users
                .Include(u => u.Enrollments)
                .FirstOrDefault(u => u.UserId == id);

            if (user == null) { 
            
                return NoContent();
            }

            List<UserCourseDto> userCourseDtos = new List<UserCourseDto>();

            foreach (var e in user.Enrollments)
            {
                var course = _dbContext.Courses.FirstOrDefault(c => c.CourseId == e.CourseId);
                userCourseDtos.Add(new UserCourseDto
                {
                    CourseId = course.CourseId,
                    Title = course.Title,
                    EnrolledDate = e.EnrolledDate,
                });
            }

            var mappedUser = new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                UserEmail = user.Email,
                Courses = userCourseDtos,
            };

            return Ok(mappedUser);
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var user = _dbContext.Users.FirstOrDefault(
             u => u.Username == userLoginDto.UserName
                && u.Password == userLoginDto.Password
                );

            if (user == null ) { return NoContent(); }

            var claims = new[]
           {
            new Claim(ClaimTypes.Name, userLoginDto.UserName!.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Practical Exam - PRN231 - Summer 2024 - Computing Fundamental Department - FPT University"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "http://fpt.edu.vn",
                audience: "http://localhost:5000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
