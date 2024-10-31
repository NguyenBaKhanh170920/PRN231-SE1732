using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetMovies()
        {
            var ls = _context.Movies.Select(x => new
            {
                id = x.Id,
                title = x.Title,
                year = x.Year,
                description = x.Description,
                directorId = x.DirectorId,
                directorName = x.Director.Name,
                moviesStars = x.MovieStars.Select(s => s.Star.Name).ToList()
            });
            return Ok(ls);
        }

        [HttpGet]
        [Route("GetAllMoviesByStar/{star}")]
        public IActionResult GetAllMoviesByStar(string star)
        {
            var LS = _context.Movies.Select(x => new
            {
                id = x.Id,
                title = x.Title,
                year = x.Year,
                description = x.Description,
                directorId = x.DirectorId,
                directorName = x.Director.Name,
                moviesStars = x.MovieStars.Select(s => s.Star.Name).ToList()
            }).Where(r => r.moviesStars.Contains(star)).ToList();
            return Ok(LS);
        }
    }
}
