using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WHVM.Database.Models;

namespace WHVM.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string homeVideoDBLocation = Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "HomeVideoDB.db");
            services.AddDbContext<HomeVideoDBContext>(options =>
            {
                options.UseLazyLoadingProxies(false);
#if DEBUG
                options.UseSqlite("Data Source=" + homeVideoDBLocation);
#else
                options.UseSqlServer(connectionString);
#endif
            });
            var mvcCoreBuilder = services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            ;

            mvcCoreBuilder
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}