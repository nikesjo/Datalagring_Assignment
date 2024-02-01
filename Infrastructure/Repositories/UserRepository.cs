using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class UserRepository(UserContext userContext) : Repo<UserEntity, UserContext>(userContext), IUserRepository
{
    private readonly UserContext _userContext = userContext;

    public override async Task<UserEntity> CreateAsync(UserEntity entity)
    {
        try
        {
            _userContext.Users
                .Include(i => i.Profile).ThenInclude(i => i.Addresses)
                .Include(i => i.Auth)
                .Add(entity);
            await _userContext.SaveChangesAsync();

            return entity;
        }
        catch { }

        return null!;
    }

    public override async Task<IEnumerable<UserEntity>> GetAsync()
    {
        try
        {
            var entities = await _userContext.Users
                .Include(i => i.Profile).ThenInclude(i => i.Addresses)
                .Include(i => i.Auth)
                .ToListAsync();

            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch { }

        return null!;
    }

    public override async Task<UserEntity> GetAsync(Expression<Func<UserEntity, bool>> expression)
    {
        try
        {
            var entity = await _userContext.Users
                .Include(i => i.Profile).ThenInclude(i => i.Addresses)
                .Include(i => i.Auth)
                .FirstOrDefaultAsync(expression);

            if (entity != null)
            {
                return entity;
            }
        }
        catch { }

        return null!;
    }
}
