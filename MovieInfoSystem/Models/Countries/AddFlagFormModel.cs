namespace MovieInfoSystem.Models.Countries
{
    using System.ComponentModel.DataAnnotations;

    public class AddFlagFormModel
    {
        [Url]
        [Required]
        [Display(Name ="Flag URL")]
        public string FlagUrl { get; set; }
    }
}
