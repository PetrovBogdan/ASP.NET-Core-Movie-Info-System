namespace MovieInfoSystem.Models.Authors
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class BecomeAuthorFormModel
    {

        [Required]
        [StringLength(NameMaxLength,MinimumLength =NameMinLength)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
