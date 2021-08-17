namespace MovieInfoSystem.Services.Index.Models
{
    using System.Collections.Generic;

    public class IndexServiceModel
    {
        public int TotalMovies { get; set; }

        public int TotalUsers { get; set; }

        public int TotalActors { get; set; }

        public int TotalDirectors { get; set; }

        public List<MovieIndexServiceModel> Movies { get; init; }
    }
}
