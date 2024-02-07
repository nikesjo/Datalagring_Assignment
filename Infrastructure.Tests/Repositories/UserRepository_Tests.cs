using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class UserRepository_Tests
{
    private readonly UserContext _userContext = 
        new(new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);
    [Fact]
    public async Task CreateAsync_Should_CreateANewUserEntity_And_ReturnUserEntity()
    {
        // Arrange
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity 
        { 
          Created = DateTime.Now,
          LastModified = DateTime.Now,
          IsEnabled = false
        };

        // Act
        var result = await userRepository.CreateAsync(userEntity);

        // Assert
        Assert.NotNull( result );
        Assert.Equal( 1, result.Id );
    }

    [Fact]
    public async Task GetAsync_Should_GetAll_And_ReturnIEnumerableOfTypeUserEntity()
    {
        // Arrange
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        // Act
        var result = await userRepository.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<UserEntity>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAsync_Should_GetOneUser_And_ReturnOneUser()
    {
        // Arrange
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        // Act
        var result = await userRepository.GetAsync(x => x.Id == userEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userEntity.Id, result.Id );
    }

    [Fact]
    public async Task ExistsAsync_Should_CheckIfUserExists_And_ReturnFound()
    {
        // Arrange
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        // Act
        var result = await userRepository.ExistsAsync(x => x.Id == userEntity.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingUserEntity_And_ReturnUpdatedUserEntity()
    {
        // Arrange
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        userEntity = await userRepository.CreateAsync(userEntity);

        // Act
        userEntity.LastModified = DateTime.Today;
        userEntity.IsEnabled = true;
        var result = await userRepository.UpdateAsync(x => x.Id == userEntity.Id, userEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userEntity.Id, result.Id);
        Assert.Equal(DateTime.Today, result.LastModified);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneUser_And_ReturnTrue()
    {
        // Arrange
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        // Act
        var result = await userRepository.DeleteAsync(x => x.Id == userEntity.Id);

        // Assert
        Assert.True(result);
    }
}
