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
            if (userEntities != null)
            {
                foreach (var userEntity in userEntities)
                {
                    users.Add(userEntity);
                }
                return users;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }

    public async Task<UserDto> UpdateUserAsync(UserDto userDto)
    {
        try
        {
            var userEntity = await _userRepository.GetAsync(x => x.Id == userDto.Id);
            if (userEntity != null)
            {
                userEntity.LastModified = userDto.LastModified;
                userEntity.Profile.FirstName = userDto.FirstName;
                userEntity.Profile.LastName = userDto.LastName;
                userEntity.Profile.PhoneNumber = userDto.PhoneNumber;
                userEntity.Auth.Email = userDto.Email;
                userEntity.Auth.Password = userDto.Password;

                userEntity.Profile.Addresses.Clear();
                foreach (var addressDto in userDto.Addresses)
                {
                    var addressEntity = new AddressEntity
                    {
                        StreetName = addressDto.StreetName,
                        PostalCode = addressDto.PostalCode,
                        City = addressDto.City
                    };
                    userEntity.Profile.Addresses.Add(addressEntity);
                }

                await _userRepository.UpdateAsync(x => x.Id == userDto.Id, userEntity);

                return userDto;
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
