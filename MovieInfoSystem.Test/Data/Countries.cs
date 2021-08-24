namespace MovieInfoSystem.Test.Data
{
    using System.Linq;
    using System.Collections.Generic;

    using MovieInfoSystem.Data.Models;

    public class Countries
    {
        public static IEnumerable<Country> GetTenCountries
       => Enumerable.Range(0, 10).Select(x => new Country());
    }
}
