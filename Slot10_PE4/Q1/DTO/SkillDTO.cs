using System.ComponentModel.DataAnnotations;

namespace Q1.DTO
{
    public class SkillDTO
    {
        //skillId = s.SkillId,
        //        skillName = s.SkillName,
        //        description = s.Description,
        //        numberOfEmployees = s.EmployeeSkills.Count
        public SkillDTO()
        {

        }
        public SkillDTO(int skillId, string skillName, string? description, int numberOfEmployees)
        {
            SkillId = skillId;
            SkillName = skillName;
            Description = description;
            NumberOfEmployees = numberOfEmployees;
        }
        [Key]
        public int SkillId { get; set; }
        public string SkillName { get; set; } = null!;
        public string? Description { get; set; }
        public int NumberOfEmployees { get; set; }
    }
}
