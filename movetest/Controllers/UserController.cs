using Microsoft.AspNetCore.Mvc;
using movetest.BAL;
using Microsoft.AspNetCore.Authorization;
using movetest.Models;

namespace movetest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("getUsers/{pageId}", Name = "GetUsers")]
        public async Task<IActionResult> GetUsers([FromRoute] int pageId)
        {
            var users = await _userService.GetUsers(pageId);
            return Ok(users);
        }

        [HttpGet("getUser/{userId}", Name = "GetUser")]
        public async Task<IActionResult> GetUser([FromRoute] int userId)
        {
            var user = await _userService.GetUser(userId);
            return Ok(user);
        }

        [HttpPut("createUser", Name = "CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var isSuccess = await _userService.CreateUser(user);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("updateUser/{userId}", Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] User user)
        {
            var isSuccess = await _userService.UpdateUser(userId, user);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("deleteUser/{userId}", Name = "DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            var isSuccess = await _userService.DeleteUser(userId);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
