namespace MovieInfoSystem.Models.Index
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalMovies { get; set; }

        public int TotalUsers { get; set; }

        public int TotalActors { get; set; }

        public int TotalDirectors { get; set; }

        public List<MovieIndexViewModel> Movies { get; init; }
    }
}
