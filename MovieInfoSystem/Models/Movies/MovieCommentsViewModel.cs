namespace MovieInfoSystem.Models.Movies
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class MovieCommentsViewModel
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        [Required]
        [StringLength(CommentMaxLength, MinimumLength = CommentMinLength)]
        public string Body { get; set; }

        public string  CreatedOn { get; set; }
    }
}
