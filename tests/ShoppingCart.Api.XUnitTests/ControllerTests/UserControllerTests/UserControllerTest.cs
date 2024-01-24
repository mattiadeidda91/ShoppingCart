using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using ShoppingCart.Abstractions.Models.Dtos;
using ShoppingCart.Api.Controllers;

namespace ShoppingCart.Api.XUnitTests.ControllerTests.UserControllerTests
{
    public class UserControllerTest : TestBase
    {
        private readonly UserController controller;
        private readonly Mock<IUserRepository> userRepositoryMock;

        public UserControllerTest() : base()
        {
            var loggerMock = new Mock<ILogger<UserController>>();
            userRepositoryMock = new Mock<IUserRepository>();
            controller = new UserController(userRepositoryMock.Object, mapper, loggerMock.Object);
        }

        #region GetUsers

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetUsers_Test(bool expectedResult)
        {
            // Arrange
            userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedResult ? new List<User> { new User() } : new List<User>());

            // Act
            var result = await controller.GetUsers();

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result.Result);
            else
                Assert.IsType<NotFoundResult>(result.Result);
        }

        #endregion

        #region GetUserById

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetUserById_Test(bool expectedResult)
        {
            // Arrange
            userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedResult ? new User() : null);

            // Act
            var result = await controller.GetUserById(1);

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result.Result);
            else
                Assert.IsType<NotFoundResult>(result.Result);
        }

        #endregion

        #region GetUsersAndProducts

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetUsersAndProducts_Test(bool expectedResult)
        {
            // Arrange
            userRepositoryMock.Setup(repo => repo.GetUserAndProductsAsync())
                .ReturnsAsync(expectedResult ?
                    (new List<User> { new User() }, new List<Product> { new Product() }) :
                    (new List<User>(), new List<Product>()));

            // Act
            var result = await controller.GetUsersAndProducts();

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result);
            else
                Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region GetUsersByProductId

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetUsersByProductId_Test(bool expectedResult)
        {
            // Arrange
            userRepositoryMock.Setup(repo => repo.GetUsersByProductIdAsync(It.IsAny<int>())).ReturnsAsync(expectedResult
                ? new List<User> { new User() }
                : new List<User>());

            // Act
            var result = await controller.GetUsersByProductId(1);

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result.Result);
            else
                Assert.IsType<NotFoundResult>(result.Result);
        }

        #endregion

        #region AddUser

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task AddUser_Test(bool expectedResult)
        {
            var urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock.Setup(urlHelper => urlHelper.Action(It.IsAny<UrlActionContext>()))
                    .Returns("/api/users/GetUserById/1");

            controller.Url = urlHelperMock.Object;

            // Arrange
            userRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<User>())).ReturnsAsync(expectedResult ? 1 : 0);

            // Act
            var result = await controller.AddUser(It.IsAny<UserDto>());

            // Assert
            if (expectedResult)
                Assert.IsType<CreatedResult>(result.Result);
            else
                Assert.IsType<BadRequestResult>(result.Result);
        }

        #endregion

        #region UpdateUser

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateUser_Test(bool expectedResult)
        {
            // Arrange
            userRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<User>())).ReturnsAsync(expectedResult ? 1 : 0);

            // Act
            var result = await controller.UpdateUser(It.IsAny<int>(), new UserDto());

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result.Result);
            else
                Assert.IsType<BadRequestResult>(result.Result);
        }

        #endregion

        #region DeleteUser

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteUser_Test(bool expectedResult)
        {
            // Arrange
            userRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).ReturnsAsync(expectedResult ? 1 : 0);

            // Act
            var result = await controller.DeleteUser(1);

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result.Result);
            else
                Assert.IsType<BadRequestResult>(result.Result);
        }

        #endregion

    }

}
