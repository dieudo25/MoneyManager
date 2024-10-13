using UserService.Domain.DTO;
using UserService.Domain.Models;

namespace UserService.Domain.Helpers
{
    public static class MappingHelper
    {
        public static UserDto ToUserDto(User user)
        {
            if (user == null)
            {
                throw new NullReferenceException("User object is null, mapping to DTO object failed.");
            }

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }

        // Maps CreateUserDto to User model
        public static User ToUserModel(UserDto createUserDto)
        {
            if (createUserDto == null)
            {
                throw new ArgumentNullException(nameof(createUserDto));
            }

            return new User
            {
                UserName = createUserDto.UserName,
                Email = createUserDto.Email,
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
            };
        }
    }
}
