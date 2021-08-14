namespace MovieInfoSystem.Services.Authors
{
    using System.Linq;
    using MovieInfoSystem.Data;

    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext data;

        public AuthorService(ApplicationDbContext data)
            => this.data = data;

        public bool IsAuthor(string userId)
            => this.data
                .Authors
                .Any(x => x.UserId == userId);
    }
}
