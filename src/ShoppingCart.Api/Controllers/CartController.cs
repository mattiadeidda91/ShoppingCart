using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using ShoppingCart.Abstractions.Models.Dtos;

namespace ShoppingCart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository cartRepositoryAsync;
        private readonly IMapper mapper;
        private readonly ILogger<CartController> logger;

        public CartController(ICartRepository cartRepositoryAsync, IMapper mapper, ILogger<CartController> logger)
        {
            this.cartRepositoryAsync = cartRepositoryAsync ?? throw new ArgumentNullException(nameof(cartRepositoryAsync));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetCarts()
        {
            var carts = await cartRepositoryAsync.GetAllAsync();

            if (carts.Any())
                return Ok(mapper.Map<IEnumerable<CartDto>>(carts));
            else
                return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetCartById(int id)
        {
            var cart = await cartRepositoryAsync.GetByIdAsync(id);

            if(cart != null)
                return Ok(mapper.Map<CartDto>(cart));
            else
                return NotFound();
        }

        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetCartByUserId(int userId)
        {
            var cart = await cartRepositoryAsync.GetCartByUserIdAsync(userId);

            if (cart != null)
            {
                var cartDto = mapper.Map<IEnumerable<CartDto>>(cart);

                return Ok(cartDto);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddCart([FromBody] CartDto cartDto)
        {
            var cart = mapper.Map<Cart>(cartDto);

            var newCartId = await cartRepositoryAsync.InsertAsync(cart);

            if (newCartId > 0)
            {
                var route = Url.Action("GetCartById", new { id = newCartId });
                return Created(route ?? "/GetCartById", newCartId);
            }    
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<CartDto>> UpdateCart([FromQuery] int id, [FromBody] CartDto cartDto)
        {
            var cart = mapper.Map<Cart>(cartDto);
            cart.Id = id;

            var updateResult = await cartRepositoryAsync.UpdateAsync(cart);

            if (updateResult > 0)
            {
                return Ok(cartDto);
            }
            else
                return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCart([FromQuery] int id)
        {
            var result = await cartRepositoryAsync.DeleteAsync(id);

            if (result > 0)
            {
                return Ok(true);
            }
            else
                return BadRequest();
        }
    }
}