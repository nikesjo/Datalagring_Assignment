using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AuthEntity
{
    [Key]
    [ForeignKey(nameof(UserEntity))]
    public int UserId { get; set; }
    public virtual UserEntity User { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
