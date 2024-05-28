using System.Security.Claims;
using System.Text;
using Application.Extension;
using Domain.Enum;
using Infrastructure.Extensions;
using Infrastructure.MeadeasyDbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Medeasy_Backend.Extensions;

public static class MedeasyBackendExtensions
{
    public static IServiceCollection AddMedeasyBackendExtensions(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddInfrastructureDependencies();
        service.AddApplicationExtensions();

        service.AddEntityFrameworkNpgsql().AddDbContext<MedeasyContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("MedeasyConnectionString"),
            o => o.MigrationsAssembly("Medeasy-Backend")
        ));

        return service;
    }

    public static IServiceCollection AddMedeasyBackendAuthentication(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new()
                        {
                            ValidateIssuer = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration.GetRequiredSection("JWT:SigningCred").Value!)
                            ),
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = configuration.GetRequiredSection("JWT:medeasy").Value!,
                        };
                    }
                );

        return service;
    }


    public static IServiceCollection AddMedeasyBackendAuthorization(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAuthorizationBuilder()
            .AddPolicy("Patient", policy =>
               {
                   policy.RequireClaim(ClaimTypes.Role, UserRole.Patient.ToString());
               })
             .AddPolicy("Practitioner", policy =>
               {
                   policy.RequireClaim(ClaimTypes.Role, UserRole.Practitioner.ToString());
               });

        return service;
    }

}


//TODO
// add domain , application and infrastructure extensions
// Configure database connection
// Add mediator to Service collection
// Add jwt authentication and authorization