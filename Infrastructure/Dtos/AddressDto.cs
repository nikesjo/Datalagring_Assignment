using Infrastructure.Entities;

namespace Infrastructure.Dtos;

public class AddressDto
{
    public int Id { get; set; }
    public string StreetName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public static implicit operator AddressDto(AddressEntity entity)
    {
        var addressDto = new AddressDto
        {
            Id = entity.Id,
            StreetName = entity.StreetName,
            PostalCode = entity.PostalCode,
            City = entity.City
        };
        return addressDto;
    }
}
