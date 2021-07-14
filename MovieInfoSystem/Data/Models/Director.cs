namespace MyWebProjectDb.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Director
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        public ICollection<Movie> Movies { get; init; } = new HashSet<Movie>();
    }
}
