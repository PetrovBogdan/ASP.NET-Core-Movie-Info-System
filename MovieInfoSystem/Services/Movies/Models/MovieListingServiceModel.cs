namespace MovieInfoSystem.Services.Movies.Models
{
    using System.Collections.Generic;
        
    public class MovieListingServiceModel
    {
        public int Id { get; init; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Audio { get; set; }

        public string Summary { get; set; }

        public ICollection<MovieActorsServiceModel> Actors { get; init; }

        public ICollection<MovieDirectorsServiceModel> Directors { get; init; }

        public ICollection<MovieCountriesServiceModel> Countries { get; init; }
    }
}
