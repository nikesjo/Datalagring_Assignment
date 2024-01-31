using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class AuthRepository(UserContext userContext) : Repo<AuthEntity, UserContext>(userContext), IAuthRepository
{
    private readonly UserContext _userContext = userContext;

    public override async Task<IEnumerable<AuthEntity>> GetAsync()
    {
        try
        {
            var entities = await _userContext.Authentications
                .Include(i => i.User).ThenInclude(i => i.Profile)
                .ToListAsync();

            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch { }

        return null!;
    }

    public override async Task<AuthEntity> GetAsync(Expression<Func<AuthEntity, bool>> expression)
    {
        try
        {
            var entity = await _userContext.Authentications
                .Include(i => i.User).ThenInclude(i => i.Profile)
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
