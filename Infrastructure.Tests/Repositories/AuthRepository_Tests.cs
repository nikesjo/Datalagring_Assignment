using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class AuthRepository_Tests
{
    private readonly UserContext _userContext =
        new(new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public async Task CreateAsync_Should_CreateANewAuthEntity_And_ReturnAuthEntity()
    {
        // Arrange
        IAuthRepository authRepository = new AuthRepository(_userContext);
        var authEntity = new AuthEntity
        {
            UserId = 1,
            Email = "email@domain.com",
            Password = "password"
        };

        // Act
        var result = await authRepository.CreateAsync(authEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.UserId);
    }

    [Fact]
    public async Task GetAsync_Should_GetAll_And_ReturnIEnumerableOfTypeAuthEntity()
    {
        // Arrange
        IAuthRepository authRepository = new AuthRepository(_userContext);
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        var authEntity = new AuthEntity
        {
            UserId = userEntity.Id,
            Email = "email@domain.com",
            Password = "password"
        };
        await authRepository.CreateAsync(authEntity);

        // Act
        var result = await authRepository.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<AuthEntity>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAsync_Should_GetOneAuth_And_ReturnOneAuth()
    {
        // Arrange
        IAuthRepository authRepository = new AuthRepository(_userContext);
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        var authEntity = new AuthEntity
        {
            UserId = userEntity.Id,
            Email = "email@domain.com",
            Password = "password"
        };
        await authRepository.CreateAsync(authEntity);

        // Act
        var result = await authRepository.GetAsync(x => x.UserId == authEntity.UserId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(authEntity.UserId, result.UserId);
    }

    [Fact]
    public async Task ExistsAsync_Should_CheckIfAuthEntityExists_And_ReturnFound()
    {
        // Arrange
        IAuthRepository authRepository = new AuthRepository(_userContext);
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        var authEntity = new AuthEntity
        {
            UserId = userEntity.Id,
            Email = "email@domain.com",
            Password = "password"
        };
        await authRepository.CreateAsync(authEntity);

        // Act
        var result = await authRepository.ExistsAsync(x => x.UserId == authEntity.UserId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingAuthEntity_And_ReturnUpdatedAuthEntity()
    {
        // Arrange
        IAuthRepository authRepository = new AuthRepository(_userContext);
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        var authEntity = new AuthEntity
        {
            UserId = userEntity.Id,
            Email = "email@domain.com",
            Password = "password"
        };
        await authRepository.CreateAsync(authEntity);

        // Act
        authEntity.Email = "NewEmail@domain.com";
        authEntity.Password = "NewPassword";
        var result = await authRepository.UpdateAsync(x => x.UserId == authEntity.UserId, authEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(authEntity.UserId, result.UserId);
        Assert.Equal("NewEmail@domain.com", result.Email);
        Assert.Equal("NewPassword", result.Password);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneAuth_And_ReturnTrue()
    {
        // Arrange
        IAuthRepository authRepository = new AuthRepository(_userContext);
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        var authEntity = new AuthEntity
        {
            UserId = userEntity.Id,
            Email = "email@domain.com",
            Password = "password"
        };
        await authRepository.CreateAsync(authEntity);

        // Act
        var result = await authRepository.DeleteAsync(x => x.UserId == authEntity.UserId);

        // Assert
        Assert.True(result);
    }
}
