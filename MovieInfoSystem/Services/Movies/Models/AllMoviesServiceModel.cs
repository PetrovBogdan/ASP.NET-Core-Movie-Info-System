namespace MovieInfoSystem.Services.Movies.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllMoviesServiceModel
    {
        public const int MoviesPerPage = 4;

        [Display(Name = "Search by title")]
        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalMovies { get; set; }

        public List<MovieListingServiceModel> Movies { get; init; }
    }
}
