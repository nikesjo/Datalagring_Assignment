using Infrastructure.Entities;

namespace Infrastructure.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime LastModified {  get; set; }

    public List<AddressDto> Addresses { get; set; } = new List<AddressDto>();


    public static implicit operator UserDto(UserEntity entity)
    {
        var userDto = new UserDto
        {
            Id = entity.Id,
            LastModified = DateTime.Now,
            FirstName = entity.Profile.FirstName,
            LastName = entity.Profile.LastName,
            PhoneNumber = entity.Profile.PhoneNumber,
            Email = entity.Auth.Email
        };

        foreach (var addressEntity in entity.Profile.Addresses)
        {
            var addressDto = new AddressDto
            {
                StreetName = addressEntity.StreetName,
                PostalCode = addressEntity.PostalCode,
                City = addressEntity.City
            };
            userDto.Addresses.Add(addressDto);
        }

        return userDto;
    }
}
