namespace MovieInfoSystem.Models.Movies
{
    using System.Collections.Generic;

    public class MovieListingViewModel
    {
        public int Id { get; init; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Audio { get; set; }

        public string Summary { get; set; }

        public ICollection<MovieActorsViewModel> Actors { get; init; }

        public ICollection<MovieDirectorsViewModel> Directors { get; init; }

        public ICollection<MovieCountriesViewMOdel> Countries { get; init; }


    }
}
