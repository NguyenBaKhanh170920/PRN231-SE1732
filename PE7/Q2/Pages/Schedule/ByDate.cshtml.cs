using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.DTO;
using System.Security.Cryptography;
using System.Text.Json;

namespace Q2.Pages.Schedule
{
    public class ByDateModel : PageModel
    {
        public void OnGet()
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage response = _httpClient.GetAsync("http://localhost:5100/api/TimeSlot/List").Result;
            var timeSlots = response.Content.ReadFromJsonAsync<List<TimeSlotDTO>>().Result;
            ViewData["timeslot"] = timeSlots.ToList();


            HttpResponseMessage response2 = _httpClient.GetAsync("http://localhost:5100/api/Room/List").Result;
            var rooms = response2.Content.ReadFromJsonAsync<List<RoomDTO>>().Result;
            ViewData["room"] = rooms.ToList();
            
            HttpResponseMessage response3 = _httpClient.GetAsync("http://localhost:5100/api/Movie/List").Result;
            var movies = response3.Content.ReadFromJsonAsync<List<MovieDTO>>().Result;
            ViewData["movies"] = movies.ToList();

            ViewData["dateMovie"] = new DateTime(2024, 10, 24).ToString("dd/MM/yyyy");


            HttpResponseMessage response4 = _httpClient.GetAsync("http://localhost:5100/api/Schedule/List/2023-10-21").Result;
            var schedules = response4.Content.ReadFromJsonAsync<List<ScheduleDTO>>().Result;
            ViewData["schedules"] = schedules.ToList();
        }


        public void OnPost(DateTime searchDate)
        {

            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage response = _httpClient.GetAsync("http://localhost:5100/api/TimeSlot/List").Result;
            var timeSlots = response.Content.ReadFromJsonAsync<List<TimeSlotDTO>>().Result;
            ViewData["timeslot"] = timeSlots.ToList();


            HttpResponseMessage response2 = _httpClient.GetAsync("http://localhost:5100/api/Room/List").Result;
            var rooms = response2.Content.ReadFromJsonAsync<List<RoomDTO>>().Result;
            ViewData["room"] = rooms.ToList();

            HttpResponseMessage response3 = _httpClient.GetAsync("http://localhost:5100/api/Movie/List").Result;
            var movies = response3.Content.ReadFromJsonAsync<List<MovieDTO>>().Result;
            ViewData["movies"] = movies.ToList();

            ViewData["dateMovie"] = searchDate.ToString("dd/MM/yyyy");
            var dateAPI = searchDate.ToString("yyyy-MM-dd");
            Console.WriteLine(searchDate);
            Console.WriteLine($"http://localhost:5100/api/Schedule/List/{dateAPI}");



            HttpResponseMessage response4 = _httpClient.GetAsync($"http://localhost:5100/api/Schedule/List/{dateAPI}").Result;
            var schedules = response4.Content.ReadFromJsonAsync<List<ScheduleDTO>>().Result;
            ViewData["schedules"] = schedules.ToList();

        }
    }
}
