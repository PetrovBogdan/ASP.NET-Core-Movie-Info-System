namespace MovieInfoSystem.Services.Authors
{
    public interface IAuthorService
    {
        public void Create(string name, 
            string email, 
            string userId);

        public bool IsAuthor(string userId);

        public int GetId(string userId);
    }
}
