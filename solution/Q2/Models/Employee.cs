namespace Q2.Models
{
    public class Employee
    {
        public Employee()
        {

        }


        public int employeeId { get; set; }
        public string name { get; set; } = null!;
        public string gender { get; set; }
        public DateTime? dob { get; set; }
        public string? position { get; set; }
        public bool? isFulltime { get; set; }
    }
}
