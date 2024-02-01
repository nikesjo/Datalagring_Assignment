using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class UserService(IUserRepository userRepository, IAuthRepository authRepository, IProfileRepository profileRepository, IAddressRepository addressRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAuthRepository _authRepository = authRepository;
    private readonly IProfileRepository _profileRepository = profileRepository;
    private readonly IAddressRepository _addressRepository = addressRepository;

    public async Task<UserDto> CreateUserAsync(UserRegistrationDto userRegistration)
    {
        try
        {
            if (!await _authRepository.ExistsAsync(x => x.Email == userRegistration.Email))
            {
                var userEntity = new UserEntity
                {
                    FirstName = userRegistration.FirstName,
                    LastNsm
                };
            }
        }
        catch { }

        return false;
    }

    public async Task<UserDto> GetUserAsync(Expression<Func<UserEntity, bool>> expression)
    {
        try
        {
            var userEntity = await _userRepository.GetAsync(expression);
            if (userEntity != null)
            {
                var userDto = new UserDto
                {

                }
            }
        }
        catch { }

        return null!;
    }
}
