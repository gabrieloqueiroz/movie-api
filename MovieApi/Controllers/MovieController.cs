using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;

namespace MovieApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{

    private static List<Movie> movies = new List<Movie>();

    [HttpPost]
    public void insertMovie([FromBody] Movie movie)
    {
        movies.Add(movie);
        Console.WriteLine(movie.Title);
    }

}
