
using System.Linq.Expressions;
using Infrastructure.MeadeasyDbContext;


namespace Infrastructure.Repository;

public class BaseRepository<T>(MedeasyContext context) : IBaseRepository<T> where T : class

{
    public async Task Delete(T entity)
    {
        context.Remove<T>(entity);
        await context.SaveChangesAsync();
    }

    public IQueryable<T> GetAll()
    {
        return context.Set<T>();
    }

    public IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>().Where(predicate);
    }

    public async Task<T> GetById(Guid id)
    {
        return await context.Set<T>().FindAsync(id)
        ?? throw new Exception(message: "No match for id");
    }

    public async Task Save(T entity)
    {
        try
        {
            context.Add<T>(entity);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(message: $"error saving entity{entity} at error: {e}");
        }
    }

}