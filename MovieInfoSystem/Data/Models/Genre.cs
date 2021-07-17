namespace MovieInfoSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;
    public class Genre
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(GenreTypeMaxLength)]
        public string Type { get; set; }

        public ICollection<GenreMovie> Movies { get; init; } = new HashSet<GenreMovie>();

    }
}
