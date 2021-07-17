namespace MovieInfoSystem.Models.Movies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddMovieFormModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        public string Audio { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        //public ICollection<DirectorMovie> Directors { get; init; } = new HashSet<DirectorMovie>();
        [Required]
        public ICollection<int> GenreId { get; set; }
        public ICollection<MovieGenreViewModel> Genres { get; set; }

        //public ICollection<CountryMovie> Countries { get; init; } = new HashSet<CountryMovie>();
        public MovieActorViewModel Actor { get; set; }
        public ICollection<MovieActorViewModel> Actors { get; init; }

        //public ICollection<Comment> Comments { get; init; } = new HashSet<Comment>();
    }
}
