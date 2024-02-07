using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class ProfileRepository_Tests
{
    private readonly UserContext _userContext =
        new(new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public async Task CreateAsync_Should_CreateANewProfileEntity_And_ReturnProfileEntity()
    {
        // Arrange
        IProfileRepository profileRepository = new ProfileRepository(_userContext);
        var profileEntity = new ProfileEntity
        {
            UserId = 1,
            FirstName = "FirstName",
            LastName = "LastName",
            PhoneNumber = "1234"
        };

        // Act
        var result = await profileRepository.CreateAsync(profileEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.UserId);
    }

    [Fact]
    public async Task GetAsync_Should_GetAll_And_ReturnIEnumerableOfTypeProfileEntity()
    {
        // Arrange
        IProfileRepository profileRepository = new ProfileRepository(_userContext);
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        var profileEntity = new ProfileEntity
        {
            UserId = userEntity.Id,
            FirstName = "FirstName",
            LastName = "LastName",
            PhoneNumber = "1234"
        };
        await profileRepository.CreateAsync(profileEntity);

        // Act
        var result = await profileRepository.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProfileEntity>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAsync_Should_GetOneAuth_And_ReturnOneAuth()
    {
        // Arrange
        IProfileRepository profileRepository = new ProfileRepository(_userContext);
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        var profileEntity = new ProfileEntity
        {
            UserId = userEntity.Id,
            FirstName = "FirstName",
            LastName = "LastName",
            PhoneNumber = "1234"
        };
        await profileRepository.CreateAsync(profileEntity);

        // Act
        var result = await profileRepository.GetAsync(x => x.UserId == profileEntity.UserId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(profileEntity.UserId, result.UserId);
    }

    [Fact]
    public async Task ExistsAsync_Should_CheckIfProfileEntityExists_And_ReturnFound()
    {
        // Arrange
        IProfileRepository profileRepository = new ProfileRepository(_userContext);
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        var profileEntity = new ProfileEntity
        {
            UserId = userEntity.Id,
            FirstName = "FirstName",
            LastName = "LastName",
            PhoneNumber = "1234"
        };
        await profileRepository.CreateAsync(profileEntity);

        // Act
        var result = await profileRepository.ExistsAsync(x => x.UserId == profileEntity.UserId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingProfileEntity_And_ReturnUpdatedProfileEntity()
    {
        // Arrange
        IProfileRepository profileRepository = new ProfileRepository(_userContext);
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        var profileEntity = new ProfileEntity
        {
            UserId = userEntity.Id,
            FirstName = "FirstName",
            LastName = "LastName",
            PhoneNumber = "1234"
        };
        await profileRepository.CreateAsync(profileEntity);

        // Act
        profileEntity.FirstName = "NewFirstName";
        profileEntity.LastName = "NewLastName";
        profileEntity.PhoneNumber = "4321";
        var result = await profileRepository.UpdateAsync(x => x.UserId == profileEntity.UserId, profileEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(profileEntity.UserId, result.UserId);
        Assert.Equal("NewFirstName", result.FirstName);
        Assert.Equal("NewLastName", result.LastName);
        Assert.Equal("4321", result.PhoneNumber);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneProfile_And_ReturnTrue()
    {
        // Arrange
        IProfileRepository profileRepository = new ProfileRepository(_userContext);
        IUserRepository userRepository = new UserRepository(_userContext);
        var userEntity = new UserEntity
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            IsEnabled = false
        };
        await userRepository.CreateAsync(userEntity);

        var profileEntity = new ProfileEntity
        {
            UserId = userEntity.Id,
            FirstName = "FirstName",
            LastName = "LastName",
            PhoneNumber = "1234"
        };
        await profileRepository.CreateAsync(profileEntity);

        // Act
        var result = await profileRepository.DeleteAsync(x => x.UserId == profileEntity.UserId);

        // Assert
        Assert.True(result);
    }
}
