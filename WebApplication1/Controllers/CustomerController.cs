using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly PRN_Sum22_B1Context _context;
        public CustomerController(PRN_Sum22_B1Context context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("delete/{CustomerId}")]
        public IActionResult Delete(string CustomerId)
        {
            try
            {
                var cus = _context.Customers.Include(x=>x.Orders).ThenInclude(x=>x.OrderDetails)
                    .FirstOrDefault(x => x.CustomerId == CustomerId);
                if (cus == null)
                {
                    return NotFound("No customer");
                }
                var rt = _context.Customers;
                _context.Customers.Remove(cus);
                _context.SaveChanges();
                return Ok(new
                {
                    CustomerDeletedCount=1,
                    orderDeletedCount=cus.Orders.Count(),
                    orderDetailDeletedCount=cus.Orders.SelectMany(x=>x.OrderDetails).Count(),
                });
            }catch (Exception ex)
            {
                return Conflict("There was an unknown error when performing data deletion");
            }
        }
    }
}
