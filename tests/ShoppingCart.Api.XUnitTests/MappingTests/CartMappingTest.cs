using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Models.Dtos;

namespace ShoppingCart.Api.XUnitTests.MappingTests
{
    public class CartMappingTest : TestBase
    {
        [Fact]
        public void CartMapping_Test()
        {
            var cart = new Cart
            {
                Id = 1,
                User = new User()
                {
                    Id = 1,
                    Name = "Name",
                    Lastname = "Lastname",
                    City = "City",
                    Email = "test@test.com",
                    Phone = "011987534",
                    Products = new List<Product>()
                    {
                        new Product
                        {
                            Id = 1,
                            Name = "Name",
                            Category = "Category",
                            Description = "Description",
                            Price = 19.99M
                        }
                    }
                }
            };

            var cartDto = mapper.Map<CartDto>(cart);

            Assert.NotNull(cartDto);
            Assert.Equal(cartDto.Id, cart.Id);
            Assert.NotNull(cartDto.User);
            Assert.Equal(cartDto.User.Name, cart.User.Name);
            Assert.Equal(cartDto.User.Lastname, cart.User.Lastname);
            Assert.Equal(cartDto.User.City, cart.User.City);
            Assert.Equal(cartDto.User.Email, cart.User.Email);
            Assert.Equal(cartDto.User.Phone, cart.User.Phone);
        }

        [Fact]
        public void CartDtoMapping_Test()
        {
            var cartDto = new CartDto
            {
                Id = 1,
                User = new UserDto()
                {
                    Name = "Name",
                    Lastname = "Lastname",
                    City = "City",
                    Email = "test@test.com",
                    Phone = "011987534"
                }
            };

            var cart = mapper.Map<Cart>(cartDto);

            Assert.NotNull(cart);
            Assert.Equal(cart.Id, cartDto.Id);
            Assert.NotNull(cart.User);
            Assert.Equal(0, cart.User.Id);
            Assert.Equal(cart.User.Name, cartDto.User.Name);
            Assert.Equal(cart.User.Lastname, cartDto.User.Lastname);
            Assert.Equal(cart.User.City, cartDto.User.City);
            Assert.Equal(cart.User.Email, cartDto.User.Email);
            Assert.Equal(cart.User.Phone, cartDto.User.Phone);
            Assert.Null(cart.User.Products);
        }
    }
}
