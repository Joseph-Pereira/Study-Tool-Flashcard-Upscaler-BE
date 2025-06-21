using Microsoft.AspNetCore.Mvc;
using StudyToolFlashcardUpscaler.Models.Dtos;
using StudyToolFlashcardUpscaler.Api.Services;
using System.Collections.Generic;

namespace StudyToolFlashcardUpscaler.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;

        public LoginController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users from the system.
        /// </summary>
        /// <returns>List of UserDto objects.</returns>
        [HttpGet("all-users")]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            try
            {

                var users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("get-user")]
        public ActionResult<UserDto> GetUser([FromQuery] string username, [FromQuery] string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Username and password are required.");
            }

            var user = _userService.GetUser(username, password);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpPost("create-user")]
        public ActionResult<UserDto> CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is required.");
            }

            var createdUser = _userService.CreateUser(userDto);
            return CreatedAtAction(nameof(GetAllUsers), new { id = createdUser.Id }, createdUser);
        }

    }
}
