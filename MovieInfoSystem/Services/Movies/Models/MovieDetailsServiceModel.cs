namespace MovieInfoSystem.Services.Movies.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class MovieDetailsServiceModel
    {
        public int Id { get; init; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string CreatedOn { get; set; }

        public string Audio { get; set; }

        public string Summary { get; set; }

        public int Duration { get; set; }

        public int AuthorId { get; set; }

        public bool IsCreator { get; set; }

        [StringLength(CommentMaxLength, MinimumLength = CommentMinLength)]
        public string Comment { get; set; }

        public ICollection<MovieDirectorsServiceModel> Directors { get; init; }

        [Display(Name = "Genre")]
        public ICollection<MovieGenreServiceModel> Genres { get; init; }

        public ICollection<MovieCountriesServiceModel> Countries { get; init; }

        public ICollection<MovieActorsServiceModel> Actors { get; init; }

        public ICollection<MovieCommentsServiceModel> Comments { get; init; }
    }
}
