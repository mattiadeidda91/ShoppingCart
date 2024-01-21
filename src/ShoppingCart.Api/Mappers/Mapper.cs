using AutoMapper;
using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Models.Dtos;

namespace ShoppingCart.Api.Mappers
{
    public class Mapper : Profile
    {
        public Mapper() 
        {
            _ = CreateMap<UserDto, User>().ReverseMap();
            _ = CreateMap<ProductDto, Product>().ReverseMap();
            _ = CreateMap<CartDto, Cart>().ReverseMap();
        }
    }
}
