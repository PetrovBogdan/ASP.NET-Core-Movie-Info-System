namespace MovieInfoSystem.Test.Data
{
    using System.Linq;
    using System.Collections.Generic;

    using MovieInfoSystem.Data.Models;
  
    public static class Actors
    {
        public static IEnumerable<Actor> GetTenActors
        => Enumerable.Range(0, 10).Select(x => new Actor());
    }
}
