using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WHVM.Database.Models;

namespace WHVM.Database
{
    public class HomeVideoDBContextFactory : IDesignTimeDbContextFactory<HomeVideoDBContext>
    {
        public HomeVideoDBContext CreateDbContext(string[] args)
        {
            // ReSharper disable once UnusedVariable
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
#if DEBUG
                .AddJsonFile("appsettings.Development.json", true)
#endif
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<HomeVideoDBContext>();

            optionsBuilder.UseLazyLoadingProxies();
#if DEBUG

            optionsBuilder.UseSqlite("Data Source=HomeVideoDB.db");
#else
            string connectionString = configuration.GetConnectionString("DatabaseConnection");
            optionsBuilder.UseSqlServer(connectionString);
#endif
            return new HomeVideoDBContext(optionsBuilder.Options);
        }
    }
}