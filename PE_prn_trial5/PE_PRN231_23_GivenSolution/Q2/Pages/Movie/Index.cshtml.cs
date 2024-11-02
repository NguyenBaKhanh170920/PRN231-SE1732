using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.DTO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using System.Net.Http;

namespace Q2.Pages.Movie
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<MovieDTO> mvList { get; set; } = new List<MovieDTO>();
        [BindProperty]
        public List<StudioDTO> stList { get; set; } = new List<StudioDTO>();
        [BindProperty]
        public MovieDTO NewMovie { get; set; }
        public void OnGet()
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage response = _httpClient.GetAsync("http://localhost:5000/api/Movie/List/0").Result;
            var employees = response.Content.ReadFromJsonAsync<List<MovieDTO>>().Result;
            mvList = employees.ToList();
            //ViewData["movie"] = employees.ToList();
            Load();
        }
        public void OnGetStudioList(int sid)
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage response = _httpClient.GetAsync($"http://localhost:5000/api/Movie/List/{sid}").Result;
            var employees = response.Content.ReadFromJsonAsync<List<MovieDTO>>().Result;
            mvList = employees.ToList();
            Load();
        }
        public async Task<IActionResult> OnPostAddMovie(int id, string title, int studio)
        {
            var formData = new MultipartFormDataContent
    {
        { new StringContent(id.ToString()), "MovieId" },
        { new StringContent(title), "Title" },
        { new StringContent(DateTime.Now.ToString("o")), "PublishDate" }, // Format DateTime as ISO
        { new StringContent(studio.ToString()), "StudioId" }
    };

            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5000/api/Movie/Add", formData);
            return RedirectToAction(nameof(OnGet));
        }
        public void Load()
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage response2 = _httpClient.GetAsync("http://localhost:5000/api/Studio/List").Result;
            var studioList = response2.Content.ReadFromJsonAsync<List<StudioDTO>>().Result;
            stList = studioList.ToList();
        }
    }
}
