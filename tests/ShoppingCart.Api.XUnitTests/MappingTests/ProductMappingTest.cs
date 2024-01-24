using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Models.Dtos;

namespace ShoppingCart.Api.XUnitTests.MappingTests
{
    public class ProductMappingTest : TestBase
    {
        [Fact]
        public void ProductMapping_Test()
        {
            var product = new Product
            {
                Id= 1,
                Name = "Product",
                Description= "Description",
                Price = 10.99M,
                Category = "Category",
            };

            var productDto = mapper.Map<ProductDto>(product);

            Assert.NotNull(productDto);
            Assert.Equal(productDto.Name, product.Name);
            Assert.Equal(productDto.Description, product.Description);
            Assert.Equal(productDto.Category, product.Category);
            Assert.Equal(productDto.Price, product.Price);
        }

        [Fact]
        public void ProductDtoMapping_Test()
        {
            var productDto = new ProductDto
            {
                Name = "Product",
                Description = "Description",
                Price = 10.99M,
                Category = "Category"
            };

            var product = mapper.Map<Product>(productDto);

            Assert.NotNull(product);
            Assert.Equal(product.Name, productDto.Name);
            Assert.Equal(product.Description, productDto.Description);
            Assert.Equal(product.Category, productDto.Category);
            Assert.Equal(product.Price, productDto.Price);
        }
    }
}
