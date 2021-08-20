namespace MovieInfoSystem.Data.Models
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

        [Required]
        public string Image { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public string Audio { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public int Duration { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public string Creator { get; set; }

        public ICollection<DirectorMovie> Directors { get; init; } = new HashSet<DirectorMovie>();

        public ICollection<GenreMovie> Genres { get; init; } = new HashSet<GenreMovie>();

        public ICollection<CountryMovie> Countries { get; init; } = new HashSet<CountryMovie>();

        public ICollection<ActorMovie> Actors { get; init; } = new HashSet<ActorMovie>();

        public ICollection<Comment> Comments { get; init; } = new HashSet<Comment>();

    }
}
