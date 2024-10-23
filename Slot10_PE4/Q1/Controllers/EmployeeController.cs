using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly SPRING_24_B1Context _context;
        public EmployeeController(SPRING_24_B1Context context)
        {
            _context = context;
        }

        [HttpDelete]
        [Route("Delete/{EmployeeId}")]
        public IActionResult DeleteEmployee(int EmployeeId)
        {
            try
            {
                var employee = _context.Employees
                .Where(e => e.EmployeeId == EmployeeId)
                .Include(e => e.EmployeeSkills)
                .Include(e => e.EmployeeProjects)
                .Include(e => e.Departments)
                .FirstOrDefault();
                if (employee == null)
                {
                    return NotFound("No employee has id " + EmployeeId);
                }
                else
                {
                    var department = _context.Departments
                        .Where(d => d.ManagerId == EmployeeId);
                    foreach (var d in department)
                    {
                        d.ManagerId = null;
                    }
                    int numberOfProjects = employee.EmployeeProjects.Count;
                    int numberOfSkills = employee.EmployeeSkills.Count;
                    int numberOfDepartments = employee.Departments.Count;
                    _context.EmployeeProjects.RemoveRange(employee.EmployeeProjects);
                    _context.EmployeeSkills.RemoveRange(employee.EmployeeSkills);
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        numberOfProjects = numberOfProjects,
                        numberOfSkills = numberOfSkills,
                        numberOfDepartments = numberOfDepartments
                    });
                }
            }
            catch (Exception)
            {
                return Conflict();
            }
        }

    }
}
