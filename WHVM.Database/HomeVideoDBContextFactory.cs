using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using WHVM.Database.Models;

namespace WHVM.Database
{
    public class HomeVideoDBContextFactory : IDesignTimeDbContextFactory<HomeVideoDBContext>
    {
        public HomeVideoDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
#if DEBUG
                .AddJsonFile("appsettings.Development.json", optional:true)
#endif
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<HomeVideoDBContext>();
            string connectionString = configuration.GetConnectionString("DatabaseConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new HomeVideoDBContext(optionsBuilder.Options);
        }
    }
}