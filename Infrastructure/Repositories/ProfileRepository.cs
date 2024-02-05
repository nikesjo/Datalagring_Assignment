using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProfileRepository(UserContext userContext) : Repo<ProfileEntity, UserContext>(userContext), IProfileRepository
{
    private readonly UserContext _userContext = userContext;

    public override async Task<IEnumerable<ProfileEntity>> GetAsync()
    {
        try
        {
            var entities = await _userContext.Profiles
                .Include(i => i.User).ThenInclude(i => i.Auth)
                .Include(i => i.Addresses)
                .ToListAsync();

            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }

    public override async Task<ProfileEntity> GetAsync(Expression<Func<ProfileEntity, bool>> expression)
    {
        try
        {
            var entity = await _userContext.Profiles
                .Include(i => i.User).ThenInclude(i => i.Auth)
                .Include(i => i.Addresses)
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
