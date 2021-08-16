namespace MovieInfoSystem.Services.Movies.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class MovieCommentsServiceModel
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        [Required]
        [StringLength(CommentMaxLength, MinimumLength = CommentMinLength)]
        public string Body { get; set; }

        public string CreatedOn { get; set; }
    }
}
