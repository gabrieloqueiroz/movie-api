using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Data;
using MovieApi.Data.Dtos;
using MovieApi.Models;

namespace MovieApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{

    private MovieContext _movieContext;
    private IMapper _mapper;

    public MovieController(MovieContext movieContext, IMapper mapper)
    {
        _movieContext = movieContext;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult InsertMovie([FromBody] CreateMovieDto movieDto)
    {
        Movie movie = _mapper.Map<Movie>(movieDto);
        _movieContext.Movies.Add(movie);
        _movieContext.SaveChanges();
        
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
        return _movieContext.Movies.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
       var film = _movieContext
            .Movies
            .FirstOrDefault(movie => 
                        movie.Id.Equals(id));

        if (film == null) return NotFound();

        return Ok(film);
    }
}
