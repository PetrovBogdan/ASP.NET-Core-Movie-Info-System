namespace MovieInfoSystem.Services.Authors
{
    using MovieInfoSystem.Data;
    using System.Linq;

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
