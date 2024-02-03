using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class UserContext: DbContext
{
    protected UserContext()
    {
    }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<AuthEntity> Authentications { get; set; }
    public virtual DbSet<ProfileEntity> Profiles { get; set; }
    public virtual DbSet<AddressEntity> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthEntity>()
            .HasIndex(x => x.Email)
            .IsUnique();
    }
}
