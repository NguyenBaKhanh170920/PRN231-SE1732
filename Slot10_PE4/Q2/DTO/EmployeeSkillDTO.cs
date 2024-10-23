namespace Q2.DTO
{
    public class EmployeeSkillDTO
    {
        public int EmployeeId { get; set; }
        public int SkillId { get; set; }
        public string? ProficiencyLevel { get; set; }
        public DateTime? AcquiredDate { get; set; }

        public EmployeeSkillDTO(int employeeId, int skillId, string? proficiencyLevel, DateTime? acquiredDate)
        {
            EmployeeId = employeeId;
            SkillId = skillId;
            ProficiencyLevel = proficiencyLevel;
            AcquiredDate = acquiredDate;
        }
    }
}
