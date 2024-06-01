
using System.Formats.Asn1;
using System.Linq.Expressions;
using System.Reflection;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Update.Internal;


namespace Infrastructure.Repository;

public class BaseRepository<T>(MedeasyDbContext context) : IBaseRepository<T> where T : class

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

    public async Task<T?> GetById(Guid id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public MedeasyDbContext GetContext()
    {
        return context;
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

    // public async Task Update(T entity) {

    //     ICollection<SetPropertyCalls<object>> setPropertyCalls = [];
    //     Type type= typeof(T);
    //     var properties = type.GetProperties();

    //     foreach (PropertyInfo property in properties) {
    //         setPropertyCalls.Add(e)
    //     }
    //     var Id = entity.GetType().GetProperty("Id")?.GetValue(entity);
    //     var result = await GetById((Guid)Id!);

    // } 

    public async Task Update()
    {
        await context.SaveChangesAsync();
    }
}