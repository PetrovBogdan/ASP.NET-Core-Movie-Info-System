namespace MovieInfoSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Body { get; set; }

        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;

        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

    }
}
