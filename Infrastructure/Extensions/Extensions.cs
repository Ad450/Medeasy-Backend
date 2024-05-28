using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services) =>
        services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));

}