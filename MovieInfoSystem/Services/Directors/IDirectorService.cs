namespace MovieInfoSystem.Services.Directors
{
    using MovieInfoSystem.Services.Directors.Models;

    public interface IDirectorService
    {
        public AllDirectorsServiceModel All(int currentPage,
            string searchTerm);

        public DirectorDetailsServiceModel Details(int id);

        public bool AddDetails(string country,
            string biography,
            string picture,
            int id);
    }
}
