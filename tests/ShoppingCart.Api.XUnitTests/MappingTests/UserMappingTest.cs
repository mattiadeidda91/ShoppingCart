using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Models.Dtos;

namespace ShoppingCart.Api.XUnitTests.MappingTests
{
    public class UserMappingTest : TestBase
    {
        [Fact]
        public void UserMapping_Test()
        {
            var user = new User
            {
                Id = 1,
                City = "City",
                Email = "test@test.com",
                Lastname = "lastname",
                Name= "Test",
                Phone = "01199999",
                Products = new List<Product>() 
                {
                    new Product 
                    {
                        Name= "TestProduct",
                        Id= 1,
                        Category = "category",
                        Description = "description",
                        Price = 20.99M
                    }
                }
            };

            var userDto = mapper.Map<UserDto>(user);

            Assert.NotNull(userDto);
            Assert.Equal(userDto.Name, user.Name);
            Assert.Equal(userDto.City, user.City);
            Assert.Equal(userDto.Email, user.Email);
            Assert.Equal(userDto.Lastname, user.Lastname);
            Assert.Equal(userDto.Phone, user.Phone);
        }

        [Fact]
        public void UserDtoMapping_Test()
        {
            var userDto = new UserDto
            {
                City = "City",
                Email = "test@test.com",
                Lastname = "lastname",
                Name = "Test",
                Phone = "01199999"
            };

            var user = mapper.Map<User>(userDto);

            Assert.NotNull(user);
            Assert.Equal(user.Name, userDto.Name);
            Assert.Equal(user.City, userDto.City);
            Assert.Equal(user.Email, userDto.Email);
            Assert.Equal(user.Lastname, userDto.Lastname);
            Assert.Equal(user.Phone, userDto.Phone);
        }
    }
}
