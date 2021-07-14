namespace MyWebProjectDb.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Actor
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        public IEnumerable<Movie> Movies { get; init; } = new HashSet<Movie>();
    }
}
