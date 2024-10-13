using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Domain.Models;
using UserService.Domain.Interfaces;
using UserService.Domain.DTO;
using UserService.Domain.Helpers;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, UserManager<User> userManager, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            _logger.LogInformation("Fetch All users");

            var users = await _userRepository.GetAllAsync();

            _logger.LogInformation($"Number of users: {users.Count()}");

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserById(Guid userId)
        {
            _logger.LogDebug($"Fetch user {{{userId}}}");

            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            _logger.LogDebug($"Fetch user by email");

            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserDto createUserDto)
        {
            _logger.LogDebug("Request to create user");

            if (createUserDto == null || string.IsNullOrWhiteSpace(createUserDto.Password))
            {
                return BadRequest("User object or password is null.");
            }

            var user = MappingHelper.ToUserModel(createUserDto);

            var result = await _userRepository.CreateUserAsync(user, createUserDto.Password);

            if (result.Succeeded)
            {
                var createdUserDto = MappingHelper.ToUserDto(user);
                return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, user);
            }
            
            _logger.LogError("Failed to create user: {Errors}", result.Errors);
            return BadRequest(result.Errors);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            _logger.LogDebug("Update user: {@User}", user);

            if (user == null)
            {
                _logger.LogError($"User to update is null");
                return BadRequest("User object is null or ID mismatch.");
            }

            await _userRepository.UpdateUserAsync(user);

            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            _logger.LogDebug($"Delete user {userId}");

            await _userRepository.DeleteUserAsync(userId);

            return NoContent();
        }
    }
}
