namespace MovieInfoSystem.Test.Controllers
{
    using Xunit;
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using MovieInfoSystem.Controllers;
    using MovieInfoSystem.Models.Authors;
    using MovieInfoSystem.Data.Models;

    public class AuthorsControllerTest
    {
        [Fact]
        public void CreateShoulReturnViewWhenUsersAreAuthorized()
            => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Authors/Create")
                .WithUser())
            .To<AuthorsController>(x => x.Create())
            .Which()
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("Author", "author@author.com")]
        public void PostCreateShouldBeForAuthorizedUsersAndRedirectToActionView(string name,
            string email)
            => MyController<AuthorsController>
            .Instance(controller => controller
                .WithUser())
            .Calling(x => x.Create(new BecomeAuthorFormModel
            {
                Name = name,
                Email = email,
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<Author>(authors => authors
                    .Any(a =>
                         a.Name == name &&
                         a.Email == email &&
                          a.UserId == TestUser.Identifier)))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Add", "Movies");

    }
}
