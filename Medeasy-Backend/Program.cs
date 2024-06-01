
using Domain.Entities;
using Infrastructure.Context;
using Medeasy_Backend.DatabaseSeedings;
using Medeasy_Backend.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMedeasyBackendExtensions(builder.Configuration);
// builder.Services.ConfigureDBContext(builder.Configuration);

builder.Services.AddDbContext<MedeasyDbContext>();

builder.Services.ConfigureIdentity(builder.Configuration);

builder.Services.AddMedeasyBackendAuthentication(builder.Configuration);

builder.Services.AddMedeasyBackendAuthorization(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    try
    {
        var context = service.GetRequiredService<MedeasyDbContext>();
        context.Database.EnsureCreated();

        await ServiceSeedings.InitService(context);
    }
    catch (Exception ex)
    {
        var logger = service.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

