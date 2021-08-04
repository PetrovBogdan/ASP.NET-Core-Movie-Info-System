namespace MovieInfoSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Director
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        public string Biography { get; set; }

        public int? CountryId { get; set; }

        public Country Country { get; set; }

        public string Picture { get; set; }

        public ICollection<DirectorMovie> Movies { get; init; } = new HashSet<DirectorMovie>();
    }
}
