namespace MovieInfoSystem.Services.Authors
{
    public interface IAuthorService
    {
        public bool IsAuthor(string userId);

        public int GetId(string userId);
    }
}
