using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Q1.DTO;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly SPRING_24_B1Context _context;
        public SkillController(SPRING_24_B1Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetSkills")]
        [EnableQuery]
        public IActionResult Get()
        {
            var skill = _context.Skills.Select(s => new SkillDTO
            {
                SkillId = s.SkillId,
                SkillName = s.SkillName,
                Description = s.Description,
                NumberOfEmployees = s.EmployeeSkills.Count
            });
            return Ok(skill);
        }

        [HttpGet]
        [Route("GetSkill/{SkillId}")]
        public IActionResult ListSkill(int SkillId)
        {
            var skill = _context.Skills
                .Where(s => s.SkillId == SkillId)
                .Select(x => new
                {
                    skillId = x.SkillId,
                    skillName = x.SkillName,
                    description = x.Description,
                    employees = x.EmployeeSkills.Select(e => new
                    {
                        employeeId = e.EmployeeId,
                        employeeName = e.Employee.Name,
                        department = e.Employee.Department.DepartmentName,
                        proficiencyLevel = e.ProficiencyLevel,
                        acquiredDate = e.AcquiredDate
                    })
                }).FirstOrDefault();
            if (skill == null)
            {
                return NotFound();
            }
            return Ok(skill);
        }
    }
}
