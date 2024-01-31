namespace Infrastructure.Entities;

public class ProfileEntity
{
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public ICollection<AddressEntity> Addresses { get; set; } = new List<AddressEntity>();
}
