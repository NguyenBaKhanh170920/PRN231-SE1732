using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.DTO;
using System.Security.Cryptography;

namespace Q2.Pages.Movie
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<MovieDTO> mvList { get; set; }=new List<MovieDTO>();
        [BindProperty]
        public List<StudioDTO> stList { get; set; } = new List<StudioDTO>();
        public void OnGet()
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage response = _httpClient.GetAsync("http://localhost:5000/api/Movie/List/0").Result;
            HttpResponseMessage response2 = _httpClient.GetAsync("http://localhost:5000/api/Studio/List").Result;
            var employees = response.Content.ReadFromJsonAsync<List<MovieDTO>>().Result;
            var studioList = response2.Content.ReadFromJsonAsync<List<StudioDTO>>().Result;
            mvList=employees.ToList();
            stList=studioList.ToList();
            //ViewData["movie"] = employees.ToList();
        }
    }
}
