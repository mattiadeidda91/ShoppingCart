using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using System.Text.Json;

namespace ShoppingCart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserController> logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<User?> GetUser([FromQuery] int id)
        {
            var user = userRepository.GetById(id);

            return user;
        }

        [HttpPost]
        public ActionResult<int> AddUser([FromBody] User user)
        {
            var id = userRepository.Insert(user);

            return Ok(id);
        }

        [HttpPut]
        public ActionResult<User> UpdateUser([FromBody] User user)
        {
            var id = userRepository.Update(user);

            return Ok(user);
        }

        [HttpDelete]
        public ActionResult<bool> DeleteUser([FromQuery] int id)
        {
            userRepository.Delete(id);

            return Ok(true);
        }
    }
}