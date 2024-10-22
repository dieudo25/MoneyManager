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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            _logger.LogDebug($"Fetch user: {id}");

            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                _logger.LogError($"User '{id}' not found");
                return NotFound();
            }

            _logger.LogInformation($"User '{id}' fetched successfully");

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            _logger.LogDebug($"Fetch user '{email}'");

            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                _logger.LogError($"User '{email}' not found");
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserDto createUserDto)
        {
            _logger.LogDebug($"Request to create user: {@createUserDto}", createUserDto);

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
            
            _logger.LogError($"Failed to create user: {result.Errors}");
            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            _logger.LogDebug("Update user: {@User}", user);

            if (user == null)
            {
                _logger.LogError($"User to update is not found");
                return BadRequest("User to update is not found");
            }

            await _userRepository.UpdateUserAsync(user);

            _logger.LogInformation("User updated successfully");

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            _logger.LogDebug($"Delete user {id}");

            await _userRepository.DeleteUserAsync(id);

            _logger.LogInformation($"User '{id}' deleted successfully");

            return NoContent();
        }
    }
}
