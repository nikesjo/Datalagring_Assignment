using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class AuthEntity
{
    [Key]
    public int UserId { get; set; }
    public virtual UserEntity User { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
