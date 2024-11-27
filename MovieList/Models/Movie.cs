using System;
using System.ComponentModel.DataAnnotations; //Using the directive

namespace MovieList.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a year.")]
        [Range(1889, 2999, ErrorMessage = "Year must be after 1889.")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Please enter a rating.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? Rating { get; set; }

        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Please enter a genre.")]
        public string GenreId { get; set; }

        // Added Additional field as mentioned in the requirements
        [Required(ErrorMessage = "Please enter the release date.")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        // Added Additional field as mentioned in the requirements
        [Required(ErrorMessage = "Please enter the director's name.")]
        public string Director { get; set; }

        // Added Additional field as mentioned in the requirements
        [Required(ErrorMessage = "Please enter the duration in minutes.")]
        [Range(1, 1440, ErrorMessage = "Duration must be between 1 and 1440 minutes.")]
        public int Duration { get; set; }

        public string Slug => Name?.Replace(' ', '-').ToLower() + '-' + Year?.ToString();
    }
}
