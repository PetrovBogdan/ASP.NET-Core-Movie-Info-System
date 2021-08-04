namespace MovieInfoSystem.Models.Movies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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

        public bool IsCreator { get; set; }

        public ICollection<MovieDirectorsViewModel> Directors { get; init; }

        [Display(Name ="Genre")]
        public ICollection<MovieGenreViewModel> Genres { get; init; } 

        public ICollection<MovieCountriesViewMOdel> Countries { get; init; } 

        public ICollection<MovieActorsViewModel> Actors { get; init; }

        //TODO: Add functionallity for adding comments to the movie. 
        //public ICollection<Comment> Comments { get; init; } = new HashSet<Comment>();
    }
}
