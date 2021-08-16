namespace MovieInfoSystem.Services.Movies.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class AddCountryServiceModel
    {
        [Required]
        [StringLength(CountryNameMaxLength, MinimumLength = CountryNameMinLength)]
        public string Name { get; set; }
    }
}
