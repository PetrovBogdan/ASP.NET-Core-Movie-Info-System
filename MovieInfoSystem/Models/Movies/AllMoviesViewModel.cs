namespace MovieInfoSystem.Models.Movies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllMoviesViewModel
    {
        public const int MoviesPerPage = 4;

        [Display(Name = "Search by title")]
        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalMovies { get; set; }

        public List<MovieListingViewModel> Movies { get; init; }
    }
}
