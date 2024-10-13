using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Domain.Models;
using UserService.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace UserService.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(UserManager<User> userManager, ILogger<UserRepository> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                _logger.LogInformation($"User {userId} deleted successfully.");
            }
            else
            {
                _logger.LogWarning($"User {userId} not found. Delete failed.");
                throw new NullReferenceException($"User {{{userId}}} not found. Delete failed");
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }
    }
}
