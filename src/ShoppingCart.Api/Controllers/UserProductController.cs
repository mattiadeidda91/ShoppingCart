using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.IRepository;

namespace ShoppingCart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProductController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<UserProductController> logger;
        private readonly IUserProductRepository userProductRepository;

        public UserProductController(IUserProductRepository userProductRepository, IMapper mapper, ILogger<UserProductController> logger)
        {
            this.userProductRepository = userProductRepository ?? throw new ArgumentNullException(nameof(userProductRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProduct>>> Get()
        {
            return Ok(await userProductRepository.GetAsync());
        }

        [HttpGet("User/{id}")]
        public async Task<ActionResult<User>> GetByUserId(int id)
        {
            return Ok(await userProductRepository.GetByUserIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<object>> Insert(int userId, int productId)
        {
           return Ok(await userProductRepository.InsertAsync(userId,productId));
        }

        [HttpDelete("User/{userId}/Product/{productId}")]
        public async Task<ActionResult<object>> Delete(int userId, int productId)
        {
            return Ok(await userProductRepository.DeleteAsync(userId, productId));
        }
    }
}
