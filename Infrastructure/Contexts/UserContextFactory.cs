using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Contexts;

public class UserContextFactory : IDesignTimeDbContextFactory<UserContext>
{
    public UserContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Education\datalagring\assignment\Datalagring_Assignment\Infrastructure\Data\user_database_cf.mdf;Integrated Security=True;Connect Timeout=30");

        return new UserContext(optionsBuilder.Options);
    }
}
