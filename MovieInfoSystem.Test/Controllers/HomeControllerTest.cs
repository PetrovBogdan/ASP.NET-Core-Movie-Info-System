namespace MovieInfoSystem.Test.Controllers
{
    using Xunit;
    
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Controllers;

    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController(null);
            //Act
            var result = homeController.Error();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
