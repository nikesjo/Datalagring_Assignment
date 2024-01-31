using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public abstract class Repo<TEntity> where TEntity : class
{
    private readonly UserContext _userContext;

    protected Repo(UserContext userContext)
    {
        _userContext = userContext;
    }
}
