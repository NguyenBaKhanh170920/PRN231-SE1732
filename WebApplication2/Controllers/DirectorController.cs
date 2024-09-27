using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly PE_PRN231_Trial_02Context  _context;
        public DirectorController(PE_PRN231_Trial_02Context context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetDirectors/{nationality}/{gender}")]
        public IActionResult Get(string nationality,string gender)
        {
            bool check=false;
            if (gender == "Male")
            {
                check = true;   
            }
            var list=_context.Directors.Where(x=>x.Nationality==nationality && x.Male==check)
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
        [HttpDelete]
        [Route("RemoveStarFromMovie/{movieId}/{starId}")]
        public IActionResult Delete(int movieId,int starId)
        {
            try
            {
                var movie = _context.Movies.Include(x=>x.Stars).FirstOrDefault(x => x.Id == movieId);
                if (movie == null)
                {
                    return NotFound("The requested movie could not be found");
                }
                var star = movie.Stars.FirstOrDefault(x => x.Id == starId);
                if (star == null)
                {
                    return NotFound("The requested actor could not be found in the list of actors of the requested movie");
                }
                movie.Stars.Remove(star);
                _context.SaveChanges();
                return Ok();
            }catch(Exception ex)
            {
                return Conflict();
            }
        }
    }
}
