using MovieInfoSystem.Services.Actors.Models;

namespace MovieInfoSystem.Services.Actors
{
    public interface IActorService
    {
        public AllActorsServiceModel All(int currentPage,
            string searchTerm);

        public ActorDetailsServiceModel Details(int id);

        public bool AddDetails(string country,
            string biography,
            string picture,
            int id);
    }
}
