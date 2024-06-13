using System.Linq.Expressions;
using Application.Dto;
using Infrastructure.Repository;

namespace Application.Utils;

public static class SearchUtil
{

    public static Expression<Func<T, bool>> BuildSearchExpression<T>(string searchTerm, params string[] propertyNames)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

        var propertyChecks = propertyNames
            .Select(propertyName =>
                Expression.Call(
                    Expression.PropertyOrField(parameter, propertyName),
                    containsMethod,
                    Expression.Constant(searchTerm)));

        var body = propertyChecks.Aggregate<Expression, Expression>(null, (current, propertyCheck) =>
            current == null ? propertyCheck : Expression.OrElse(current, propertyCheck));

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    public static IList<T> FetchByPagination<T>(
        IBaseRepository<T> repository,
        string[] searchFields,
        PaginationDto dto,
        Expression<Func<T, object>> orderBy
    ) where T : class
    {
        if (dto.searchTerm == null && dto.pageNumber == null)
            throw new Exception("either search or provide page numner");

        var query = dto.searchTerm != null ?
            repository.GetByCondition(
                SearchUtil.BuildSearchExpression<T>(
                    dto.searchTerm, searchFields
                )) : repository.GetAll();


        if (dto.pageNumber != null && dto.pageNumber != null)
        {
            var skip = dto.pageNumber != 0 ? (dto.pageNumber - 1) * dto.pageSize : dto.pageSize;
            query = query
                        .Skip((int)skip!)
                        .Take((int)dto.pageSize!);
        }

        return [.. query.OrderBy(orderBy)];
    }


}