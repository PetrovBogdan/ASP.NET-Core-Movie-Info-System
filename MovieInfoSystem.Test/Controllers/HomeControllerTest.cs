namespace MovieInfoSystem.Test.Controllers
{

    using Xunit;
    using MovieInfoSystem.Controllers;
    using MyTested.AspNetCore.Mvc;
    using MovieInfoSystem.Services.Index.Models;

    using static Data.Movies;

    public class HomeControllerTest
    {
        [Fact]

        public void IndexShouldReturnViewWithCorrectModelAndData()
                => MyMvc
                     .Pipeline()
                     .ShouldMap("/")
                     .To<HomeController>(x => x.Index())
                     .Which(controler => controler
                         .WithData(GetTenMovies))
                     .ShouldReturn()
                     .View(view => view
                     .WithModelOfType<IndexServiceModel>());

        [Fact]
        public void ErrorShouldReturnView()
            => MyMvc
                 .Pipeline()
                 .ShouldMap("/Home/Error")
                 .To<HomeController>(x => x.Error())
                 .Which()
                 .ShouldReturn()
                 .View();

    }
}
