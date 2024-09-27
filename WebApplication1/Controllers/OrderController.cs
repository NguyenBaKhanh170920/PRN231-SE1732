using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private PRN_Sum22_B1Context _context;
        public OrderController(PRN_Sum22_B1Context context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetOrderByDate/{From}/{To}")]
        public IActionResult Get(DateTime From, DateTime To)
        {
            //PRN_Sum22_B1Context _context=new PRN_Sum22_B1Context();
            var list = _context.Orders.Where(x => x.OrderDate >= From && x.OrderDate <= To).
                Select(x => new
                {
                    OrderId = x.OrderId,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.CompanyName,
                    EmployeeId = x.EmployeeId,
                    EmployeName = x.Employee.FirstName + x.Employee.LastName,
                    EmplyeeDepartmentId = x.Employee.DepartmentId,
                    EmplyeeDepartmentName = x.Employee.Department.DepartmentName,
                    OrderDate = x.OrderDate,
                    RequiredDate = x.RequiredDate,
                    ShippedDate = x.ShippedDate,
                    Freight = x.Freight,
                    ShipName = x.ShipName,
                    ShipAddress = x.ShipAddress,
                    ShipCity = x.ShipCity,
                    ShipRegion = x.ShipRegion,
                    ShipPostalCode = x.ShipPostalCode,
                    ShipCountry = x.ShipCountry,
                })
                .ToList();
            return Ok(list);
        }
        [HttpGet]
        [Route("getallorder")]
        public IActionResult Get()
        {
            var list=_context.Orders
                .Select(x => new
                {
                    OrderId = x.OrderId,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.CompanyName,
                    EmployeeId = x.EmployeeId,
                    EmployeName = x.Employee.FirstName + x.Employee.LastName,
                    EmplyeeDepartmentId = x.Employee.DepartmentId,
                    EmplyeeDepartmentName = x.Employee.Department.DepartmentName,
                    OrderDate = x.OrderDate,
                    RequiredDate = x.RequiredDate,
                    ShippedDate = x.ShippedDate,
                    Freight = x.Freight,
                    ShipName = x.ShipName,
                    ShipAddress = x.ShipAddress,
                    ShipCity = x.ShipCity,
                    ShipRegion = x.ShipRegion,
                    ShipPostalCode = x.ShipPostalCode,
                    ShipCountry = x.ShipCountry,

                })
                .ToList();
            return Ok(list);
        }
    }
}
