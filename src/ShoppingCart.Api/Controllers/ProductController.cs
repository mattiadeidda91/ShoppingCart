using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using ShoppingCart.Abstractions.Dapper.IRepository;
using ShoppingCart.Abstractions.Models.Dtos;

namespace ShoppingCart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepositoryAsync;
        private readonly IMapper mapper;
        private readonly ILogger<UserController> logger;

        public ProductController(IProductRepository productRepositoryAsync, IMapper mapper, ILogger<UserController> logger)
        {
            this.productRepositoryAsync = productRepositoryAsync ?? throw new ArgumentNullException(nameof(productRepositoryAsync));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var users = await productRepositoryAsync.GetAllAsync();

            if (users.Any())
                return Ok(mapper.Map<IEnumerable<ProductDto>>(users));
            else
                return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var user = await productRepositoryAsync.GetByIdAsync(id);

            if(user != null)
                return Ok(mapper.Map<ProductDto>(user));
            else
                return NotFound();
        }

        [HttpGet("get-users-and-products")]
        public async Task<IActionResult> GetUsersAndProducts()
        {
            (var users, var products) = await productRepositoryAsync.GetUserAndProductsAsync();

            var usersDto = mapper.Map<IEnumerable<UserDto>>(users);
            var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);

            if (usersDto.Any() || productsDto.Any())
            {
                var result = new { Users = usersDto, Products = productsDto };
                return Ok(result);
            }
            else
                return NotFound();
        }

        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductByUserId(int userId)
        {
            var users = await productRepositoryAsync.GetProductByUserIdAsync(userId);

            if (users.Any())
            {
                var usersDto = mapper.Map<IEnumerable<ProductDto>>(users);

                return Ok(usersDto);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddProduct([FromBody] ProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);

            var newUserId = await productRepositoryAsync.InsertAsync(product);

            if (newUserId > 0)
            {
                var route = Url.Action("GetProductById", new { id = newUserId });
                return Created(route ?? "/GetProductById", newUserId);
            }    
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<ProductDto>> UpdateUser([FromQuery] int id, [FromBody] ProductDto userDto)
        {
            var user = mapper.Map<Product>(userDto);
            user.Id = id;

            var updateResult = await productRepositoryAsync.UpdateAsync(user);

            if (updateResult > 0)
            {
                return Ok(userDto);
            }
            else
                return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteUser([FromQuery] int id)
        {
            var result = await productRepositoryAsync.DeleteAsync(id);

            if (result > 0)
            {
                return Ok(true);
            }
            else
                return BadRequest();
        }
    }
}