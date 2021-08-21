namespace MovieInfoSystem.Services.Movies
{
    using System.Collections.Generic;
    using MovieInfoSystem.Services.Movies.Models;
    public interface IMovieService
    {
        public void Create(string title,
            string summary,
            int duration,
            string image,
            string audio,
            int authorId,
            bool userIsAdmin,
            string userId,
            ICollection<int?> genres,
            ICollection<AddActorServiceModel> actors,
            ICollection<AddDirectorServiceModel> directors,
            ICollection<AddCountryServiceModel> countries);

        public void Edit(int id,
            string title,
            string summary,
            int duration,
            string image,
            string audio,
            ICollection<AddActorServiceModel> actors,
            ICollection<AddDirectorServiceModel> directors,
            ICollection<AddCountryServiceModel> countries);

        public MovieDetailsServiceModel Details(int id, string userId);

        public AllMoviesServiceModel All(string searchTerm,
            int currentPage);

        public ICollection<MovieListingServiceModel> Mine(string userId);

        public bool AddComment(int id,
            string comment, 
            string userId);

        public bool Delete(int id);

        public int RemovieDirector(int directorId, string title);

        public int RemoveActor(int actorId, string title);

        public int RemoveCountry(int countryId, string title);

        public ICollection<MovieGenreServiceModel> GetGenres();

        public string GetCreatorId(int userId);

        public MovieServiceModel GetEditDetails(int id);

    }
}
