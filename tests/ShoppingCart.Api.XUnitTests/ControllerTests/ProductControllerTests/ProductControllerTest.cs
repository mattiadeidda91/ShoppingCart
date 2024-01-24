using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using ShoppingCart.Abstractions.Dapper.IRepository;
using ShoppingCart.Abstractions.Models.Dtos;
using ShoppingCart.Api.Controllers;

namespace ShoppingCart.Api.XUnitTests.ControllerTests.ProductControllerTests
{
    public class ProductControllerTest : TestBase
    {
        private readonly ProductController controller;
        private readonly Mock<IProductRepository> productRepositoryMock;

        public ProductControllerTest() : base()
        {
            var loggerMock = new Mock<ILogger<ProductController>>();
            productRepositoryMock = new Mock<IProductRepository>();
            controller = new ProductController(productRepositoryMock.Object, mapper, loggerMock.Object);
        }

        #region GetProducts

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetProducts_Test(bool expectedResult)
        {
            // Arrange
            productRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedResult ? new List<Product> { new Product() } : new List<Product>());

            // Act
            var result = await controller.GetProducts();

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result.Result);
            else
                Assert.IsType<NotFoundResult>(result.Result);
        }

        #endregion

        #region GetProductById

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetProductById_Test(bool expectedResult)
        {
            // Arrange
            productRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedResult ? new Product() : null);

            // Act
            var result = await controller.GetProductById(1);

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
            productRepositoryMock.Setup(repo => repo.GetUserAndProductsAsync())
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

        #region GetProductByUserId

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetProductByUserId_Test(bool expectedResult)
        {
            // Arrange
            productRepositoryMock.Setup(repo => repo.GetProductByUserIdAsync(It.IsAny<int>())).ReturnsAsync(expectedResult
                ? new List<Product> { new Product() }
                : new List<Product>());

            // Act
            var result = await controller.GetProductByUserId(1);

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result.Result);
            else
                Assert.IsType<NotFoundResult>(result.Result);
        }

        #endregion

        #region AddProduct

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task AddProduct_Test(bool expectedResult)
        {
            var urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock.Setup(urlHelper => urlHelper.Action(It.IsAny<UrlActionContext>()))
                    .Returns("/api/products/GetProductById/1");

            controller.Url = urlHelperMock.Object;

            // Arrange
            productRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Product>())).ReturnsAsync(expectedResult ? 1 : 0);

            // Act
            var result = await controller.AddProduct(It.IsAny<ProductDto>());

            // Assert
            if (expectedResult)
                Assert.IsType<CreatedResult>(result.Result);
            else
                Assert.IsType<BadRequestResult>(result.Result);
        }

        #endregion

        #region UpdateProduct

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateProduct_Test(bool expectedResult)
        {
            // Arrange
            productRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(expectedResult ? 1 : 0);

            // Act
            var result = await controller.UpdateProduct(It.IsAny<int>(), new ProductDto());

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result.Result);
            else
                Assert.IsType<BadRequestResult>(result.Result);
        }

        #endregion

        #region DeleteProduct

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteProduct_Test(bool expectedResult)
        {
            // Arrange
            productRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).ReturnsAsync(expectedResult ? 1 : 0);

            // Act
            var result = await controller.DeleteProduct(1);

            // Assert
            if (expectedResult)
                Assert.IsType<OkObjectResult>(result.Result);
            else
                Assert.IsType<BadRequestResult>(result.Result);
        }

        #endregion

    }

}
