using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class ProfileEntity
{
    [Key]
    [ForeignKey(nameof(UserEntity))]
    public int UserId { get; set; }
    public virtual UserEntity User { get; set; } = null!;

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<AddressEntity> Addresses { get; set; } = new List<AddressEntity>();
}
