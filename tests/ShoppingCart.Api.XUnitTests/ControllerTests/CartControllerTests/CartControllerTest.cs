using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using ShoppingCart.Api.Controllers;

namespace ShoppingCart.Api.XUnitTests.ControllerTests.CartControllerTests
{
    public class CartControllerTest : TestBase
    {
        private readonly CartController controller;
        private readonly Mock<ICartRepository> cartRepositoryMock;

        public CartControllerTest() : base()
        {
            var loggerMock = new Mock<ILogger<CartController>>();
            cartRepositoryMock = new Mock<ICartRepository>();
            controller = new CartController(cartRepositoryMock.Object, mapper, loggerMock.Object);
        }

        #region GetCarts

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetCarts_Test(bool expectedResult)
        {
            // Arrange
            cartRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedResult ? new List<Cart> { new Cart() } : new List<Cart>());

            // Act
            var result = await controller.GetCarts();

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result.Result);
            else
                Assert.IsType<NotFoundResult>(result.Result);
        }

        #endregion

        #region GetCartById

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GGetCartById_Test(bool expectedResult)
        {
            // Arrange
            cartRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedResult ? new Cart() : null);

            // Act
            var result = await controller.GetCartById(1);

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result.Result);
            else
                Assert.IsType<NotFoundResult>(result.Result);
        }

        #endregion

        #region GetCartByUserId

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetCartByUserId_Test(bool expectedResult)
        {
            // Arrange
            cartRepositoryMock.Setup(repo => repo.GetCartByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResult ? new List<Cart> { new Cart() } : new List<Cart>());

            // Act
            var result = await controller.GetCartByUserId(1);

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result.Result);
            else
                Assert.IsType<NotFoundResult>(result.Result);
        }

        #endregion

        #region AddCart

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task AddCart_Test(bool expectedResult)
        {
            var urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock.Setup(urlHelper => urlHelper.Action(It.IsAny<UrlActionContext>()))
                    .Returns("/api/carts/GetCartById/1");

            controller.Url = urlHelperMock.Object;

            // Arrange
            cartRepositoryMock.Setup(repo => repo.InserCartAsync(It.IsAny<int>())).ReturnsAsync(expectedResult ? 1 : 0);

            // Act
            var result = await controller.AddCart(1);

            // Assert
            if (expectedResult)
                Assert.IsType<CreatedResult>(result.Result);
            else
                Assert.IsType<BadRequestResult>(result.Result);
        }

        #endregion

        #region DeleteCart

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteCart_Test(bool expectedResult)
        {
            // Arrange
            cartRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).ReturnsAsync(expectedResult ? 1 : 0);

            // Act
            var result = await controller.DeleteCart(1);

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result.Result);
            else
                Assert.IsType<BadRequestResult>(result.Result);
        }

        #endregion

    }
}
