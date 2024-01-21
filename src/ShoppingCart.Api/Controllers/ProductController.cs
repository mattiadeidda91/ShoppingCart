using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Abstractions.Dapper.Entities;
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
        private readonly ILogger<ProductController> logger;

        public ProductController(IProductRepository productRepositoryAsync, IMapper mapper, ILogger<ProductController> logger)
        {
            this.productRepositoryAsync = productRepositoryAsync ?? throw new ArgumentNullException(nameof(productRepositoryAsync));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await productRepositoryAsync.GetAllAsync();

            if (products.Any())
                return Ok(mapper.Map<IEnumerable<ProductDto>>(products));
            else
                return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await productRepositoryAsync.GetByIdAsync(id);

            if(product != null)
                return Ok(mapper.Map<ProductDto>(product));
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
            var products = await productRepositoryAsync.GetProductByUserIdAsync(userId);

            if (products.Any())
            {
                var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);

                return Ok(productsDto);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddProduct([FromBody] ProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);

            var newProductId = await productRepositoryAsync.InsertAsync(product);

            if (newProductId > 0)
            {
                var route = Url.Action("GetProductById", new { id = newProductId });
                return Created(route ?? "/GetProductById", newProductId);
            }    
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<ProductDto>> UpdateProduct([FromQuery] int id, [FromBody] ProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);
            product.Id = id;

            var updateResult = await productRepositoryAsync.UpdateAsync(product);

            if (updateResult > 0)
            {
                return Ok(productDto);
            }
            else
                return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteProduct([FromQuery] int id)
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