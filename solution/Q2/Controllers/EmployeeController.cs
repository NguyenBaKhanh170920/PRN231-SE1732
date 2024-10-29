using Microsoft.AspNetCore.Mvc;
using Q2.Models;
using System.Text;
using System.Text.Json;

namespace Q2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient httpClient;
        private Uri baseUrl = new Uri("http://localhost:5100/api/Employee");

        public EmployeeController()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<IActionResult> List()
        {
            List<Employee> employees = new List<Employee>();
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                employees = JsonSerializer.Deserialize<List<Employee>>(jsonResponse);
            }

            ViewData["employees"] = employees;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmp([FromForm] Employee employee)
        {
            string jsonEmployee = JsonSerializer.Serialize(employee);
            HttpContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");

            // Send the JSON to the API
            HttpResponseMessage response = await httpClient.PostAsync(baseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Add successfully";

                return RedirectToAction("List");
            }
            TempData["error"] = "Add failed";
            return RedirectToAction("List");
        }
    }
}
