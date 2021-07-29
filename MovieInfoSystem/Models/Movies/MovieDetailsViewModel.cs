namespace MovieInfoSystem.Models.Movies
{
    using System.Collections.Generic;

    public class MovieDetailsViewModel
    {
        public int Id { get; init; }

        public string Title { get; set; }
  
        public string Image { get; set; }

        public string CreatedOn { get; set; } 

        public string Audio { get; set; }

        public string Summary { get; set; }

        public string Duration { get; set; }

        public int AuthorId { get; set; }

        public ICollection<DirectorListingViewModel> Directors { get; init; }

        public ICollection<MovieGenreViewModel> Genres { get; init; } 

        public ICollection<CountryListingViewModel> Countries { get; init; } 

        public ICollection<ActorListingViewModel> Actors { get; init; }

        //public ICollection<Comment> Comments { get; init; } = new HashSet<Comment>();
    }
}
