using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class UserEntity
{
    private static DateTime _currentDateTime = DateTime.Now;

    [Key]
    public int Id { get; set; }

    public DateTime Created { get; set; } = _currentDateTime;
    public DateTime LastModified { get; set; } = _currentDateTime;
    public bool IsEnabled { get; set; }

    public AuthEntity Auth { get; set; } = null!;
    public ProfileEntity Profile { get; set; } = null!;


    public static implicit operator UserEntity(UserRegistrationDto userRegistrationDto)
    {
        var userEntity = new UserEntity
        {
            IsEnabled = userRegistrationDto.IsEnabled,
            Profile = new ProfileEntity
            {
                FirstName = userRegistrationDto.FirstName,
                LastName = userRegistrationDto.LastName,
                PhoneNumber = userRegistrationDto.PhoneNumber,
                Addresses = new List<AddressEntity>
                {
                    new AddressEntity
                    {
                        StreetName = userRegistrationDto.StreetName,
                        PostalCode = userRegistrationDto.PostalCode,
                        City = userRegistrationDto.City
                    }
                }
            },
            Auth = new AuthEntity
            {
                Email = userRegistrationDto.Email,
                Password = userRegistrationDto.Password
            }
        };
        return userEntity;
    }
}
