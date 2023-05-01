using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;

namespace MovieApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{

    private static List<Movie> movies = new List<Movie>();
    private static int id = 0;

    [HttpPost]
    public IActionResult InsertMovie([FromBody] Movie movie)
    {
        movie.Id= id++;
        movies.Add(movie);
        
        return CreatedAtAction(
            nameof(GetMovieById),
            new { id = movie.Id },
            movie);
    }

    [HttpGet]
    public IEnumerable<Movie> GetMovie(
        [FromQuery] int skip = 0, 
        [FromQuery] int take = 10) //Adicionando o " = x " determinamos um valor default caso não seja passado
    {
        return movies.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
       var film = movies.FirstOrDefault(movie => movie.Id.Equals(id));

        if (film == null) return NotFound();

        return Ok(film);
    }
}
