namespace MovieInfoSystem.Models.Movies
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class AddCountryFormModel
    {
        [Required]
        [StringLength(CountryNameMaxLength, MinimumLength = CountryNameMinLength)]
        public string Name { get; set; }

    }
}
