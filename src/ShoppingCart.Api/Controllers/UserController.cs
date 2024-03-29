using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using ShoppingCart.Abstractions.Models.Dtos;

namespace ShoppingCart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepositoryAsync;
        private readonly IMapper mapper;
        private readonly ILogger<UserController> logger;

        public UserController(IUserRepository userRepositoryAsync, IMapper mapper, ILogger<UserController> logger)
        {
            this.userRepositoryAsync = userRepositoryAsync ?? throw new ArgumentNullException(nameof(userRepositoryAsync));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await userRepositoryAsync.GetAllAsync();

            if (users.Any())
                return Ok(mapper.Map<IEnumerable<UserDto>>(users));
            else
                return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await userRepositoryAsync.GetByIdAsync(id);

            if(user != null)
                return Ok(mapper.Map<UserDto>(user));
            else
                return NotFound();
        }

        [HttpGet("get-users-and-products")]
        public async Task<IActionResult> GetUsersAndProducts()
        {
            (var users, var products) = await userRepositoryAsync.GetUserAndProductsAsync();

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

        [HttpGet("Product/{productId}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByProductId(int productId)
        {
            var users = await userRepositoryAsync.GetUsersByProductIdAsync(productId);

            if (users.Any())
            {
                var usersDto = mapper.Map<IEnumerable<UserDto>>(users);

                return Ok(usersDto);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddUser([FromBody] UserDto userDto)
        {
            var user = mapper.Map<User>(userDto);

            var newUserId = await userRepositoryAsync.InsertAsync(user);

            if (newUserId > 0)
            {
                var route = Url.Action("GetUserById", new { id = newUserId });
                return Created(route ?? "/GetUserById", newUserId);
            }    
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUser([FromQuery] int id, [FromBody] UserDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            user.Id = id;

            var updateResult = await userRepositoryAsync.UpdateAsync(user);

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
            var result = await userRepositoryAsync.DeleteAsync(id);

            if (result > 0)
            {
                return Ok(true);
            }
            else
                return BadRequest();
        }
    }
}