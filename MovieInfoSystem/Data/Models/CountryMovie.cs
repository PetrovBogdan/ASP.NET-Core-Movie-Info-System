namespace MovieInfoSystem.Data.Models
{
    public class CountryMovie
    {
        public int CountryId { get; set; }

        public Country Country { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }

    }
}
