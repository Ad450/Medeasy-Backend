using System.Linq.Expressions;
using Infrastructure.MeadeasyDbContext;

namespace Infrastructure.Repository;

public interface IBaseRepository<T>
{
    public Task Save(T entity);
    public Task<T> GetById(Guid id);

    public IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate);
    public IQueryable<T> GetAll();
    public Task Delete(T entity);
    public Task Update();

    public MedeasyContext GetContext();
}