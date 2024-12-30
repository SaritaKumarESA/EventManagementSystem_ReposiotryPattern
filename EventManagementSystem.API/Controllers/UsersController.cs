using EventManagementSystem.API.DTOs;
using EventManagementSystem.API.Services;
using EventManagementSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null) {  
                return new BadRequestObjectResult("User object is null");
            }
            var user = await _userService.CreateUser(userDto);
            return new OkObjectResult(user);
        }
    }
}
