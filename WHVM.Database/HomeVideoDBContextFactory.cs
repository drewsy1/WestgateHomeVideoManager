using System;
using System.IO;
using System.Runtime.InteropServices;
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
                .AddJsonFile("appsettings.Development.json", optional: true)
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