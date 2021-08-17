namespace MovieInfoSystem.Services.Index
{
    using System.Linq;
    using MovieInfoSystem.Data;
    using MovieInfoSystem.Services.Index.Models;

    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext data;

        public HomeService(ApplicationDbContext data)
            => this.data = data;

        public IndexServiceModel Index()
        {
            var totalMovies = this.data.Movies.Count();
            var totalActors = this.data.Actors.Count();
            var totalDirectors = this.data.Directors.Count();
            var totalUsers = this.data.Users.Count();

            var movies = this.data
                .Movies
                .OrderByDescending(x => x.Id)
                .Select(x => new MovieIndexServiceModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = x.Image,
                })
                .Take(3)
                .ToList();

            return new IndexServiceModel
            {
                TotalMovies = totalMovies,
                TotalActors = totalActors,
                TotalDirectors = totalDirectors,
                TotalUsers = totalUsers,
                Movies = movies,
            };
        }
    }
}
