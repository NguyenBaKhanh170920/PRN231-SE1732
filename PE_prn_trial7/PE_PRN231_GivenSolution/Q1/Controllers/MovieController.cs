using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Q1.DTO;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly PE_PRN_24SumB5Context _context;
        public MovieController(PE_PRN_24SumB5Context context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetMovies")]
        [EnableQuery]
        public IActionResult Get()
        {
            var movie = _context.Movies.Select(x => new MovieDTO
            {
                id = x.Id,
                title = x.Title,
                year = x.Year,
                description = x.Description,
                directorId = x.DirectorId,
                directorName = x.Director.Name,
                movieStars = x.MovieStars.Select(y => y.Star.Name).ToList(),
            });
            return Ok(movie);
        }
        [HttpGet]
        [Route("GetAllMovieByStar/{star}")]
        public IActionResult et(string star)
        {
            var movie = _context.Movies.Where(x => x.MovieStars.Select(y => y.Star.Name).Contains(star)).Select(z => new
            {
                id = z.Id,
                title = z.Title,
                year = z.Year,
                description = z.Description,
                directorId = z.DirectorId,
                directorName = z.Director.Name,
                movieStars = z.MovieStars.Select(y => y.Star.Name).ToList(),
            }).ToList();
            return Ok(movie);
        }
    }
}
