namespace Q2.DTO
{
    public class EmployeeDTO
    {
        public EmployeeDTO()
        {

        }

        public EmployeeDTO(int employeeId, string name)
        {
            EmployeeId = employeeId;
            Name = name;
        }

        public int EmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public int? DepartmentId { get; set; }
        public string? Position { get; set; }
        public DateTime? HireDate { get; set; }
    }
}
