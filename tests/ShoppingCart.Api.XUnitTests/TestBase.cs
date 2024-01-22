using AutoMapper;
using ShoppingCart.Api.Mappers;

namespace ShoppingCart.Api.XUnitTests
{
    public class TestBase
    {
        protected readonly IMapper mapper;

        public TestBase() 
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping>();
            });

            mapper = new Mapper(configurationProvider);
        }
    }
}
