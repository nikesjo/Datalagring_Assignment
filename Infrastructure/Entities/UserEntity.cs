using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }

    public DateTime Created {  get; set; }
    public DateTime LastModified { get; set; }
    public bool IsEnabled { get; set; }

    public AuthEntity Auth { get; set; } = null!;
    public ProfileEntity Profile { get; set; } = null!;
}
