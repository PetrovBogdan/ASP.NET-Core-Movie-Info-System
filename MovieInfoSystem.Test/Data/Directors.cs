namespace MovieInfoSystem.Test.Data
{
    using System.Linq;
    using System.Collections.Generic;

    using MovieInfoSystem.Data.Models;
    class Directors
    {
        public static IEnumerable<Director> GetTenDirectors
        => Enumerable.Range(0, 10).Select(x => new Director());
    }
}
