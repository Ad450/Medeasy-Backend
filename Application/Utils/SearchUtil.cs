using System.Linq.Expressions;

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

}