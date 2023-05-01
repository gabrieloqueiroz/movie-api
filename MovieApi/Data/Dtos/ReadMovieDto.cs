using System.ComponentModel.DataAnnotations;

namespace MovieApi.Data.Dtos;

public class ReadMovieDto
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Time { get; set; }
    public DateTime appointmentTime { get; set; } = DateTime.Now;
}
