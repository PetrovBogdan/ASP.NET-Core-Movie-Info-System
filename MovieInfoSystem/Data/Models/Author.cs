namespace MovieInfoSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();


    }
}
