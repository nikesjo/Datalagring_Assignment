using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using System.Linq.Expressions;
using System.Diagnostics;

namespace Infrastructure.Services;

public class UserService(IUserRepository userRepository, IAuthRepository authRepository, IProfileRepository profileRepository, IAddressRepository addressRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAuthRepository _authRepository = authRepository;
    private readonly IProfileRepository _profileRepository = profileRepository;
    private readonly IAddressRepository _addressRepository = addressRepository;

    public async Task<UserDto> CreateUserAsync(UserRegistrationDto userRegistrationDto)
    {
        try
        {
            if (!await _authRepository.ExistsAsync(x => x.Email == userRegistrationDto.Email))
            {
                var userEntity = await _userRepository.CreateAsync(userRegistrationDto);

                if (userEntity != null)
                    return userEntity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }

    public async Task<UserDto> GetUserAsync(Expression<Func<UserEntity, bool>> expression)
    {
        try
        {
            var userEntity = await _userRepository.GetAsync(expression);
            if (userEntity != null)
            {
                return userEntity;
                //var userDto = new UserDto
                //{
                //    Id = userEntity.Id,
                //    Email = userEntity.Auth.Email,
                //    FirstName = userEntity.Profile.FirstName,
                //    LastName = userEntity.Profile.LastName,
                //    PhoneNumber = userEntity.Profile.PhoneNumber,
                //};
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        var users = new List<UserDto>();
        try
        {
            var userEntities = await _userRepository.GetAsync();
            foreach ( var userEntity in userEntities)
            {
                users.Add(userEntity);
            }
            return users;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }

    public async Task<UserDto> UpdateUserAsync(UserDto userDto)
    {
        try
        {
            var user = await _userRepository.GetAsync(x => x.Id == userDto.Id);
            if (user != null)
            {
                var updatedUser = await _userRepository.UpdateAsync(x => x.Id == user.Id, user);
                if (updatedUser != null)
                {
                    var updatedUserDto = new UserDto();

                    return updatedUserDto;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<bool> DeleteUserAsync(UserDto userDto)
    {
        try
        {
            var userEntity = await _userRepository.GetAsync(x => x.Auth.Email == userDto.Email);
            if (userEntity != null)
            {
                await _userRepository.DeleteAsync(x => x.Id == userEntity.Id);
                await _authRepository.DeleteAsync(x => x.UserId == userEntity.Id);
                await _profileRepository.DeleteAsync(x => x.UserId == userEntity.Id);
                
                foreach (var addressDto in userEntity.Profile.Addresses)
                {
                    await _addressRepository.DeleteAsync(x => x.Id == addressDto.Id);
                }

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return false;
    }
}
