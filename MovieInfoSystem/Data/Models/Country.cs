namespace MyWebProjectDb.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(56)]
        public string Name { get; set; }

        public IEnumerable<Movie> Movies { get; init; } = new HashSet<Movie>();
    }
}
