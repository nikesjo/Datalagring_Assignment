using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class UserRepository(UserContext userContext) : Repo<UserEntity, UserContext>(userContext), IUserRepository
{
    private readonly UserContext _userContext = userContext;

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
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

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
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }
}
