using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Q1.DTO;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DUONGPV14_PRNContext _context;
        public StudentController(DUONGPV14_PRNContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("List")]
        [EnableQuery]
        public IActionResult Get()
        {
            var stu = _context.Students.Select(x => new StudentDTO
            {
                Id = x.Id,
                FullName = x.FullName,
                Gender = x.Male ? "Male" : "FeMale",
                Dob = x.Dob.ToShortDateString(),
                LecturerName = x.Lecture.FullName,
                Age = DateTime.Now.Year - x.Dob.Year,
            }).ToList();
            return Ok(stu);
        }
        [HttpGet]
        [Route("Detail")]
        [EnableQuery]
        public IActionResult Get2([FromQuery] int id)
        {
            var student = _context.Students
                .Where(s => s.Id == id)
                .Include(s => s.Lecture)
                .Include(s => s.Classes)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                .Select(s => new StudentDTO2
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Gender = s.Male ? "Male" : "Female",
                    Dob = s.Dob.ToShortDateString(),
                    LecturerName = s.Lecture.FullName,
                    Age = DateTime.Now.Year - s.Dob.Year,
                    Classes = s.Classes.Select(c => c.ClassName).ToList(),
                    Subjects = s.StudentSubjects.Select(ss =>  ss.Subject.Subject1 ).ToList(),
                    CPA = s.StudentSubjects.Any()
                        ? s.StudentSubjects.Average(ss => ss.Grade)
                        : 0.0
                })
                .FirstOrDefault();
            return Ok(student);
        }
        [HttpPost]
        [Route("Delete/{id}")]
        public IActionResult Post(int id)
        {
            try
            {
                var student = _context.Students
                    .Include(s => s.StudentSubjects)
                    .Include(s => s.Classes)
                    .FirstOrDefault(s => s.Id == id);

                if (student == null)
                {
                    return NotFound();
                }
                _context.StudentSubjects.RemoveRange(student.StudentSubjects);
                foreach (var cls in student.Classes)
                {
                    cls.Students.Remove(student);
                }
                _context.Students.Remove(student);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Conflict(ex);
            }
        }


    }
}
