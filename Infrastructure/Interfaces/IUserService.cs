using Infrastructure.Dtos;
using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(UserRegistrationDto userRegistrationDto);
    Task<bool> DeleteUserAsync(UserDto userDto);
    Task<UserDto> GetUserAsync(Expression<Func<UserEntity, bool>> expression);
    Task<IEnumerable<UserDto>> GetUsersAsync();
    Task<UserDto> UpdateUserAsync(UserDto userDto);
}
