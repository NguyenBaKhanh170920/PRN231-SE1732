using System.ComponentModel.DataAnnotations;

namespace Q1.DTO
{
    public class EmployeeDTO
    {
        /*  employeeId = e.EmployeeId,
                  lastname = e.LastName,
                  firstname = e.FirstName,
                  fullname = e.FirstName + " " + e.LastName,
                  title = e.Title,
                  titeleOfcourtesy = e.TitleOfCourtesy,
                  birthDate = e.BirthDate,
                  birthDateStr = e.BirthDate.Value.ToString("MM/dd/yyyy"),
                  reportsTo = e.ReportsToNavigation != null
                 ? e.ReportsToNavigation.FirstName + " " + e.ReportsToNavigation.LastName
                 : "No one"*/
        [Key]
         public int employeeId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }    
        public string fullName { get; set; }
        public string title { get; set; }
        public string titeleOfcourtesy { get; set; }
        public DateTime? birthDate { get; set; }
        public string? birthDateStr { get; set; }
        public string reportsTo { get; set; }

    }
}
