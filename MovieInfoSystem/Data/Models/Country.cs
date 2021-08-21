namespace MovieInfoSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;
    public class Country
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CountryNameMaxLength)]
        public string Name { get; set; }

        public string FlagUrl { get; set; }

        public ICollection<Director> Directors { get; set; } = new HashSet<Director>();

        public ICollection<Actor> Actors { get; init; } = new HashSet<Actor>();

        public ICollection<CountryMovie> Movies { get; init; } = new HashSet<CountryMovie>();

    }
}
