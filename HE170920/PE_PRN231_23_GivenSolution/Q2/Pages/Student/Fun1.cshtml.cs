using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.DTO;

namespace Q2.Pages.Student
{
    public class Fun1Model : PageModel
    {
        public void OnGet(int? did)
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage response = _httpClient.GetAsync($"http://localhost:5000/api/Student/Detail?id={did}").Result;
            var employees = response.Content.ReadFromJsonAsync<StudentDTO>().Result;
            ViewData["API"] = employees;
        }
    }
}
