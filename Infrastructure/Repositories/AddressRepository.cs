using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class AddressRepository(UserContext userContext) : Repo<AddressEntity, UserContext>(userContext), IAddressRepository
{
    private readonly UserContext _userContext = userContext;

    public override async Task<IEnumerable<AddressEntity>> GetAsync()
    {
        try
        {
            var entities = await _userContext.Addresses
                .Include(i => i.Profiles)
                .ToListAsync();

            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch { }

        return null!;
    }

    public override async Task<AddressEntity> GetAsync(Expression<Func<AddressEntity, bool>> expression)
    {
        try
        {
            var entity = await _userContext.Addresses
                .Include(i => i.Profiles)
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
