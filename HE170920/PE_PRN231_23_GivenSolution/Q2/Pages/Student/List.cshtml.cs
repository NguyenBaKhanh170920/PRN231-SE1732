using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.DTO;

namespace Q2.Pages.Student
{
    public class ListModel : PageModel
    {
        [BindProperty]
        public List<StudentDTO> elist { get; set; } = new List<StudentDTO>();
        public void OnGet()
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage list = _httpClient.GetAsync("http://localhost:5000/api/Student/List").Result;
            elist = list.Content.ReadFromJsonAsync<List<StudentDTO>>().Result;
        }
        public async Task<IActionResult> OnPostDelete(int did)
        {
            HttpClient _httpClient = new HttpClient();
            var content = new StringContent("");
            HttpResponseMessage response = await _httpClient.PostAsync($"http://localhost:5000/api/Student/Delete/{did}", 
                content);
            return RedirectToAction(nameof(OnGet));
        }
    }
}
