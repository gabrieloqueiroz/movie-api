﻿using System.ComponentModel.DataAnnotations;

namespace MovieApi.Data.Dtos
{
    public class UpdateMovieDto
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(50, ErrorMessage = "Genus length cannot be greater than 50 characters")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Range(70, 600, ErrorMessage = "Time must have between 70 and 600 minutes.")]
        public int Time { get; set; }
    }
}
