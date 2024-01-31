using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class Repo_Tests<TContext> where TContext : DbContext, new()
{
    //private readonly TContext _context = new(new DbContextOptionsBuilder<TContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

}
