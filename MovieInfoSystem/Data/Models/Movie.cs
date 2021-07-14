namespace MyWebProjectDb.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Movie
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Title { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [Required]
        public string Image { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public string Audio { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        public ICollection<Director> Directors { get; init; } = new HashSet<Director>();

        public ICollection<Genre> Genres { get; init; } = new HashSet<Genre>();

        public ICollection<Country> Countries { get; init; } = new HashSet<Country>();

        public ICollection<Actor> Actors { get; init; } = new HashSet<Actor>();

        public IEnumerable<Comment> Comments { get; init; } = new HashSet<Comment>();

    }
}
