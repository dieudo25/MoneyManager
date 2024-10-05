using Microsoft.AspNetCore.Identity;
using UserService.Domain.Models;

namespace UserService.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task DeleteUserAsync(Guid userId);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetUserByIdAsync(Guid userId);
        Task<User> GetUserByEmailAsync(string email);
        Task UpdateUserAsync(User user);
    }
}