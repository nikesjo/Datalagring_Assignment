using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class UserContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<AuthEntity> Authentications { get; set; }
    public DbSet<ProfileEntity> Profiles { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
}
