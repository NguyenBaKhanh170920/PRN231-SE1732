using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Q1.Models;

namespace Pe3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly PE_PRN_Fall22B1Context _context;
        public DirectorController(PE_PRN_Fall22B1Context context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("getAllDirectors")]
        public IActionResult Get()
        {
            var list = _context.Directors.ToList();
            return Ok(list);
        }
        [HttpGet]
        [Route("GetDirectors/{nationality}/{gender}")]
        public IActionResult Get(string nationality, string gender)
        {
            var list = _context.Directors.Where(x => x.Nationality == nationality)
                .Select(x => new
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Gender = x.Male ? "Male" : "Female",
                    dob = x.Dob,
                    dobString = x.Dob.ToShortDateString(),
                    nationality = x.Nationality,
                    description = x.Description
                })
                .ToList();
            return Ok(list);
        }
        [HttpGet]
        [Route("GetDirector/{id}")]
        public IActionResult GetId(int id)
        {
            var drt = _context.Directors.Where(x => x.Id == id)
                .Select(x => new
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Gender = x.Male ? "Male" : "Female",
                    dob = x.Dob,
                    dobString = x.Dob.ToShortDateString(),
                    nationality = x.Nationality,
                    description = x.Description,
                    movie = x.Movies.Select(n => new
                    {
                        id = n.Id,
                        title = n.Title,
                        ReleaseDate = n.ReleaseDate,
                        ReleaseYear = n.ReleaseDate.Value.Year,
                        Description = n.Description,
                        Language = n.Language,
                        ProducerId = n.ProducerId,
                        ProducerName = n.Producer.Name,
                        DirectorId = n.DirectorId,
                        DirectorName = n.Director.FullName,
                        genres = new List<Genre>(),
                        stars = new List<Star>(),
                    })
                }).FirstOrDefault();
            return Ok(drt);
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(DirectorAddDTO drt)
        {
            try
            {
                Director newDrt = new Director()
                {
                    FullName = drt.FullName,
                    Male = drt.Male,
                    Dob = drt.Dob,
                    Nationality = drt.Nationality,
                    Description = drt.Description,
                };
                _context.Directors.Add(newDrt);
                _context.SaveChanges();
                return Ok("1");
            }
            catch (Exception ex)
            {
                return Conflict("There is an error while adding.");
            }
        }
        public class DirectorAddDTO
        {
            public string FullName { get; set; } = null!;
            public bool Male { get; set; }
            public DateTime Dob { get; set; }
            public string Nationality { get; set; } = null!;
            public string Description { get; set; } = null!;
        }
    }
}
