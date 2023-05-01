using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
    public IEnumerable<ReadMovieDto> GetMovie(
        [FromQuery] int skip = 0, 
        [FromQuery] int take = 10) //Adicionando o " = x " determinamos um valor default caso não seja passado
    {
        return _mapper.Map<List<ReadMovieDto>>(_movieContext.Movies.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
       var movie = _movieContext
            .Movies
            .FirstOrDefault(movie => 
                        movie.Id.Equals(id));

        if (movie == null) return NotFound();
        return Ok(_mapper.Map<ReadMovieDto>(movie));
    }

    [HttpPut("{id}")]
    public IActionResult updateMovie(int id, UpdateMovieDto movieDto)
    {
        var movie = _movieContext.Movies.FirstOrDefault(movies => 
                    movies.Id.Equals(id));

        if (movie == null) return NotFound();
        _mapper.Map(movieDto, movie);
        _movieContext.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult updatePartialMovie(int id, JsonPatchDocument<UpdateMovieDto> patch)
    {
        var movie = _movieContext.Movies.FirstOrDefault(movies =>
                    movies.Id.Equals(id));

        if (movie == null) return NotFound();
        var movieToUpdate = _mapper.Map<UpdateMovieDto>(movie);

        patch.ApplyTo(movieToUpdate, ModelState);

        if (!TryValidateModel(movieToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(movieToUpdate, movie);
        _movieContext.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult deleteMovie(int id)
    {
        var movie = _movieContext.Movies.FirstOrDefault(movies =>
                    movies.Id.Equals(id));

        if (movie == null) return NotFound();

        _movieContext.Remove(movie);
        _movieContext.SaveChanges();
        return NoContent();
    }
}
