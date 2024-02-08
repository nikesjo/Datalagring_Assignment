using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services;

public class UserService_Tests
{
    private readonly UserContext _userContext =
        new(new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public async Task CreateUserAsync_Should_CreateANewUser_And_ReturnUserDto()
    {
        // Arrange
        IUserRepository userRepository = new UserRepository(_userContext);
        IAuthRepository authRepository = new AuthRepository(_userContext);
        IProfileRepository profileRepository = new ProfileRepository(_userContext);
        IAddressRepository addressRepository = new AddressRepository(_userContext);
        IUserService userService = new UserService(userRepository, authRepository, profileRepository, addressRepository);
        var userRegistrationDto = new UserRegistrationDto
        {
            FirstName = "Test",
            LastName = "Test",
            PhoneNumber = "Test",
            Email = "Test",
            Password = "Test",
            StreetName = "Test",
            PostalCode = "Test",
            City = "Test"
        };

        // Act
        var result = await userService.CreateUserAsync(userRegistrationDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetUsersAsync_Should_GetAllUsers_And_ReturnIEnumerableOfTypeUserDto()
    {
        // Arrange
        IUserRepository userRepository = new UserRepository(_userContext);
        IAuthRepository authRepository = new AuthRepository(_userContext);
        IProfileRepository profileRepository = new ProfileRepository(_userContext);
        IAddressRepository addressRepository = new AddressRepository(_userContext);
        IUserService userService = new UserService(userRepository, authRepository, profileRepository, addressRepository);
        var userRegistrationDto = new UserRegistrationDto
        {
            FirstName = "Test",
            LastName = "Test",
            PhoneNumber = "Test",
            Email = "Test",
            Password = "Test",
            StreetName = "Test",
            PostalCode = "Test",
            City = "Test"
        };
        await userService.CreateUserAsync(userRegistrationDto);

        // Act
        var result = await userService.GetUsersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<UserDto>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetUserAsync_Should_GetOneUser_And_ReturnOneUserDto()
    {
        // Arrange
        IUserRepository userRepository = new UserRepository(_userContext);
        IAuthRepository authRepository = new AuthRepository(_userContext);
        IProfileRepository profileRepository = new ProfileRepository(_userContext);
        IAddressRepository addressRepository = new AddressRepository(_userContext);
        IUserService userService = new UserService(userRepository, authRepository, profileRepository, addressRepository);
        var userRegistrationDto = new UserRegistrationDto
        {
            FirstName = "Test",
            LastName = "Test",
            PhoneNumber = "Test",
            Email = "Test",
            Password = "Test",
            StreetName = "Test",
            PostalCode = "Test",
            City = "Test"
        };
        await userService.CreateUserAsync(userRegistrationDto);

        var userDto = new UserDto
        {
            Id = 1,
            FirstName = userRegistrationDto.FirstName,
            LastName = userRegistrationDto.LastName,
            PhoneNumber = userRegistrationDto.PhoneNumber,
            Email = userRegistrationDto.PhoneNumber,
            Password = userRegistrationDto.Password,
            LastModified = DateTime.Now,
            Addresses = new List<AddressDto>
            {
                new AddressDto
                {
                    StreetName = userRegistrationDto.StreetName,
                    PostalCode = userRegistrationDto.PostalCode,
                    City = userRegistrationDto.City
                }
            }
        };

        // Act
        var result = await userService.GetUserAsync(x => x.Id == userDto.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userDto.Id, result.Id);
    }

    [Fact]
    public async Task UpdateUserAsync_Should_UpdateExistingUser_And_ReturnUpdatedUserDto()
    {
        // Arrange
        IUserRepository userRepository = new UserRepository(_userContext);
        IAuthRepository authRepository = new AuthRepository(_userContext);
        IProfileRepository profileRepository = new ProfileRepository(_userContext);
        IAddressRepository addressRepository = new AddressRepository(_userContext);
        IUserService userService = new UserService(userRepository, authRepository, profileRepository, addressRepository);
        var userRegistrationDto = new UserRegistrationDto
        {
            FirstName = "Test",
            LastName = "Test",
            PhoneNumber = "Test",
            Email = "Test",
            Password = "Test",
            StreetName = "Test",
            PostalCode = "Test",
            City = "Test"
        };
        await userService.CreateUserAsync(userRegistrationDto);

        var userDto = new UserDto
        {
            Id = 1,
            FirstName = userRegistrationDto.FirstName,
            LastName = userRegistrationDto.LastName,
            PhoneNumber = userRegistrationDto.PhoneNumber,
            Email = userRegistrationDto.PhoneNumber,
            Password = userRegistrationDto.Password,
            LastModified = DateTime.Now,
            Addresses = new List<AddressDto>
            {
                new AddressDto
                {
                    StreetName = userRegistrationDto.StreetName,
                    PostalCode = userRegistrationDto.PostalCode,
                    City = userRegistrationDto.City
                }
            }
        };

        // Act
        userDto.FirstName = "NewTest";
        userDto.LastName = "NewTest";
        userDto.PhoneNumber = "NewTest";
        userDto.Email = "NewTest";
        userDto.Password = "NewTest";
        userDto.LastModified = DateTime.Today;
        userDto.Addresses = new List<AddressDto>
        {
            new AddressDto
            {
                StreetName = "NewTest",
                PostalCode = "NewTest",
                City = "NewTest"
            }
        };
        var result = await userService.UpdateUserAsync(userDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userDto.Id, result.Id);
        Assert.Equal("NewTest", result.FirstName);
        Assert.Equal("NewTest", result.LastName);
        Assert.Equal("NewTest", result.PhoneNumber);
        Assert.Equal("NewTest", result.Email);
        Assert.Equal("NewTest", result.Password);
        Assert.Equal(DateTime.Today, result.LastModified);
    }

    [Fact]
    public async Task DeleteUserAsync_Should_RemoveOneUser_And_ReturnTrue()
    {
        // Arrange
        IUserRepository userRepository = new UserRepository(_userContext);
        IAuthRepository authRepository = new AuthRepository(_userContext);
        IProfileRepository profileRepository = new ProfileRepository(_userContext);
        IAddressRepository addressRepository = new AddressRepository(_userContext);
        IUserService userService = new UserService(userRepository, authRepository, profileRepository, addressRepository);
        var userRegistrationDto = new UserRegistrationDto
        {
            FirstName = "Test",
            LastName = "Test",
            PhoneNumber = "Test",
            Email = "Test",
            Password = "Test",
            StreetName = "Test",
            PostalCode = "Test",
            City = "Test"
        };
        await userService.CreateUserAsync(userRegistrationDto);

        var userDto = new UserDto
        {
            Id = 1,
            FirstName = userRegistrationDto.FirstName,
            LastName = userRegistrationDto.LastName,
            PhoneNumber = userRegistrationDto.PhoneNumber,
            Email = userRegistrationDto.PhoneNumber,
            Password = userRegistrationDto.Password,
            LastModified = DateTime.Now,
            Addresses = new List<AddressDto>
            {
                new AddressDto
                {
                    StreetName = userRegistrationDto.StreetName,
                    PostalCode = userRegistrationDto.PostalCode,
                    City = userRegistrationDto.City
                }
            }
        };

        // Act
        var result = await userService.DeleteUserAsync(userDto);

        // Assert
        Assert.True(result);
    }
}
