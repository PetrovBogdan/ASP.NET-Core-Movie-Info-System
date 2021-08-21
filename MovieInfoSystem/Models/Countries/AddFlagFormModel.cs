namespace MovieInfoSystem.Models.Countries
{
    using System.ComponentModel.DataAnnotations;

    public class AddFlagFormModel
    {
        [Url]
        [Required]
        public string FlagUrl { get; set; }
    }
}
