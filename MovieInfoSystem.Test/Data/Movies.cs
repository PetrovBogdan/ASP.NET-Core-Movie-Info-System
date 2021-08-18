namespace MovieInfoSystem.Test.Data
{
    using System.Linq;
    using System.Collections.Generic;

    using MovieInfoSystem.Data.Models;
    using MovieInfoSystem.Models.Movies;

    public static class Movies
    {
        public static IEnumerable<Movie> GetTenMovies
            => Enumerable.Range(0, 10).Select(x => new Movie());

    }
}
