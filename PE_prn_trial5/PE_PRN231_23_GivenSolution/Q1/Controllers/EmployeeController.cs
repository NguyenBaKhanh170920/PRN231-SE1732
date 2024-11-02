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
    public class EmployeeController : ControllerBase
    {
        private readonly  PE_PRN_23SumContext _context;
        public EmployeeController(PE_PRN_23SumContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Index")]
        [EnableQuery]
        public IActionResult Get()
        {

           var listEmp = _context.Employees.Select( e => new EmployeeDTO
           {
               employeeId = e.EmployeeId,
               lastName = e.LastName,
               firstName = e.FirstName,
               fullName = e.FirstName +" "+e.LastName,  
               title = e.Title,
               titeleOfcourtesy = e.TitleOfCourtesy,
               birthDate = e.BirthDate,
               birthDateStr = e.BirthDate.Value.ToString("MM/dd/yyyy"),
               reportsTo = e.ReportsToNavigation != null
                ? e.ReportsToNavigation.FirstName + " " + e.ReportsToNavigation.LastName
                : "No one"

           });
            return Ok(listEmp);
        }
        [HttpGet]
        [Route("List/{minyear}/{maxyear}")]
        public IActionResult List(int minyear, int maxyear)
        {
            var listEmp = _context.Employees.Where(e=> e.BirthDate.Value.Year >= minyear && e.BirthDate.Value.Year <= maxyear).Select(e => new
            {
                employeeId = e.EmployeeId,
                lastname = e.LastName,
                firstname = e.FirstName,
                fullname = e.FirstName + " " + e.LastName,
                title = e.Title,
                titeleOfcourtesy = e.TitleOfCourtesy,
                birthDate = e.BirthDate,
                birthDateStr = e.BirthDate.Value.ToString("MM/dd/yyyy"),
                reportsTo = e.ReportsToNavigation != null
               ? e.ReportsToNavigation.FirstName + " " + e.ReportsToNavigation.LastName
               : "No one"

            });
            return Ok(listEmp);
         
        }
        [HttpDelete]
        [Route("Delete/{employeeid}")]
        public IActionResult Delete(int employeeid) { 

            var empDelete = _context.Employees.Include(e => e.Orders).ThenInclude(e => e.OrderDetails).FirstOrDefault(e => e.EmployeeId == employeeid);
            if( empDelete == null)
            {
                return  NotFound("The requested employee could not be found.");
            } else
            {
                var empReportsTo = _context.Employees.Where(e => e.ReportsTo == employeeid);
                if (empReportsTo.Any()) {
                    foreach (var item in empReportsTo)
                    {
                        item.ReportsTo = null;
                    }

                }
                _context.OrderDetails.RemoveRange(empDelete.Orders.SelectMany(o => o.OrderDetails));
                _context.Orders.RemoveRange(empDelete.Orders);
                _context.Employees.Remove(empDelete);
                //_context.Remove(empDelete);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
