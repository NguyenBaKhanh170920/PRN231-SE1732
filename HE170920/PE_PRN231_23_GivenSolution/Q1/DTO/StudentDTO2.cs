namespace Q1.DTO
{
    public class StudentDTO2
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Gender { get; set; }
        public string Dob { get; set; }
        public string LecturerName { get; set; } = null!;
        public int Age { get; set; }
        public List<string> Classes {  get; set; }
        public List<string> Subjects { get; set; }
        public double CPA{ get; set; }

    }
}
