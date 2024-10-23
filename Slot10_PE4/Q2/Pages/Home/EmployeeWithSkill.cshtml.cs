using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.DTO;

namespace Q2.Pages.Home
{
    public class EmployeeWithSkillModel : PageModel
    {
        [BindProperty]
        public List<EmployeeDTO> eList { get; set; } = new List<EmployeeDTO>();
        [BindProperty]
        public List<EmployeeSkillDTO> esList { get; set; } = new List<EmployeeSkillDTO>(); 
        [BindProperty]
        public List<SkillDTO> sList { get; set; } = new List<SkillDTO>();
        public void OnGet()
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage employSkillList = _httpClient.GetAsync("http://localhost:5100/api/EmployeeSkill/List").Result;
            HttpResponseMessage employList = _httpClient.GetAsync("http://localhost:5100/api/Employee/List").Result;
            HttpResponseMessage skillList = _httpClient.GetAsync("http://localhost:5100/api/Skill/List").Result;
            eList = employList.Content.ReadFromJsonAsync<List<EmployeeDTO>>().Result;
            esList = employSkillList.Content.ReadFromJsonAsync<List<EmployeeSkillDTO>>().Result;
            sList = skillList.Content.ReadFromJsonAsync<List<SkillDTO>>().Result;
            var list = (from a in eList
                       join b in esList on a.EmployeeId equals b.EmployeeId
                       join c in sList on b.SkillId equals c.SkillId
                       select new
                       {
                           EmployeeName=a.Name,
                           Pro=b.ProficiencyLevel,
                           Date=b.AcquiredDate,
                       }).ToList();
            ViewData["List"]=list;
        }
    }
}
