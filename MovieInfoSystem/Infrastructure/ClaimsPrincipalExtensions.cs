namespace MovieInfoSystem.Infrastructure
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier) == null ?
                                                            null :
                                                            user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
