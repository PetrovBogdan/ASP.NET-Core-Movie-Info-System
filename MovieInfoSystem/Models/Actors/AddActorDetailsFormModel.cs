namespace MovieInfoSystem.Models.Actors
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class AddActorDetailsFormModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Country name")]
        [StringLength(CountryNameMaxLength, MinimumLength = CountryNameMinLength)]
        public string CountryName { get; set; }

        [Required]
        [StringLength(BiographyMaxLength, MinimumLength = BiographyMinLength)]
        public string Biography { get; set; }

        [Required]
        public string Picture { get; set; }
    }
}
