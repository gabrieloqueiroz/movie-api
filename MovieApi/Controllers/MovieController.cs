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
    public void InsertMovie([FromBody] Movie movie)
    {
        movie.Id= id++;
        movies.Add(movie);
        Console.WriteLine(movie.Title);
    }

    [HttpGet]
    public IEnumerable<Movie> GetMovie()
    {
        return movies;
    }

    [HttpGet("{id}")]
    public Movie? GetMovieById(int id)
    {
        return movies.FirstOrDefault(movie => movie.Id.Equals(id));
    }

}
