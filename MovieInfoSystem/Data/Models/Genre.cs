namespace MyWebProjectDb.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Genre
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Type { get; set; }

        public ICollection<Movie> Movies { get; init; } = new HashSet<Movie>();

    }
}
