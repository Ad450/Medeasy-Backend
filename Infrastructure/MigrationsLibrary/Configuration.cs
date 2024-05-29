// using Infrastructure.MeadeasyDbContext;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;
// using Microsoft.Extensions.Configuration;

// namespace MigrationLibrary.Configuration;

// // ReSharper disable once UnusedType.Global
// public class DatabaseDesignTimeDbContextFactory
//     : IDesignTimeDbContextFactory<MedeasyContext>
// {

//     private readonly IConfiguration _configuration;

//     public DatabaseDesignTimeDbContextFactory()
//     {
//         // Default constructor
//     }

//     public DatabaseDesignTimeDbContextFactory(IConfiguration configuration)
//     {
//         _configuration = configuration;
//     }

//     public MedeasyContext CreateDbContext(string[] args)
//     {
//         var builder = new DbContextOptionsBuilder<MedeasyContext>();
//         builder.UseNpgsql(_configuration.GetConnectionString("MedeasyConnectionString")
//         );
//         return new MedeasyContext(builder.Options);
//     }

// }