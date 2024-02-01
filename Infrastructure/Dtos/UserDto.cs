using Infrastructure.Entities;

namespace Infrastructure.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime Created {  get; set; }

    public List<AddressDto> Addresses { get; set; } = new List<AddressDto>();


    public static implicit operator UserDto(UserEntity entity)
    {
        var userDto = new UserDto
        {
            Id = entity.Id,
            Created = entity.Created,
            FirstName = entity.Profile.FirstName,
            LastName = entity.Profile.LastName,
            PhoneNumber = entity.Profile.PhoneNumber,
            Email = entity.Auth.Email
        };
        return userDto;
    }
}
