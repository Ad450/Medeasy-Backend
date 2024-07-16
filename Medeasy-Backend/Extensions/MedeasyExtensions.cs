using System.Text;
using Application.Extension;
using Domain.Entities;
using Domain.Enum;
using Infrastructure.Extensions;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Application.Utils;


namespace Medeasy_Backend.Extensions;

public static class MedeasyBackendExtensions
{
    public static IServiceCollection AddMedeasyBackendExtensions(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddInfrastructureDependencies();
        service.AddApplicationExtensions();
        return service;
    }
    public static IServiceCollection ConfigureIdentity(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddIdentity<MedeasyUser, MedeasyRole>(o =>
        {
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 8;
            o.User.RequireUniqueEmail = true;


        })
          .AddEntityFrameworkStores<MedeasyDbContext>();

        return service;

    }

    // public static void ConfigureDBContext(this IServiceCollection service, IConfiguration configuration)
    // {
    //     service.AddDbContext<MedeasyDbContext>(options =>
    //     {
    //         options.UseNpgsql(configuration.GetConnectionString("MedeasyConnectionString") ?? throw new Exception("could not retrieve connection string "));
    //     });
    // }
    public static IServiceCollection AddMedeasyBackendAuthentication(this IServiceCollection service, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JWT").Get<JwtSettings>();

        var signingCredentials = jwtSettings!.SigningCredentials;
        var issuer = jwtSettings!.Issuer;

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
                                Encoding.UTF8.GetBytes(signingCredentials)
                            ),
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = issuer,
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
                   policy.RequireRole(UserRole.Patient.ToString());
               })
             .AddPolicy("Practitioner", policy =>
               {
                   policy.RequireRole(UserRole.Practitioner.ToString());
               })
            .AddPolicy("PatientORPractitioner", policy =>
               {
                   policy.RequireRole(UserRole.Patient.ToString(), UserRole.Practitioner.ToString());
               });
        return service;

    }

    public static IServiceCollection ConfigureSwaggerWithAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                            new string[] { }
                    }
            });
        });
        return services;
    }

}


