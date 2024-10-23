using Q1.Models;
using System.ComponentModel.DataAnnotations;

namespace Q1.DTO
{
    public class MovieDTO
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; } = null!;
        public int? year { get; set; }
        public string? description { get; set; }
        public int directorId { get; set; }
        public string directorName { get; set; }
        public  List<string> movieStars { get; set; }
       
    }
}
