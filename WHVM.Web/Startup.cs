using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using WHVM.Database.Models;

namespace WHVM.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Startup.Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<HomeVideoDBContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            services.AddDirectoryBrowser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            string mediaStoragePath;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                mediaStoragePath = Path.Join(Directory.GetParent(Directory.GetCurrentDirectory()).FullName,
                    "MediaStorage_Dev");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                mediaStoragePath = Path.GetFullPath(Configuration.GetValue<string>("SiteVariables:MediaStorePath"));
            }

            app.UseStaticFiles();

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(mediaStoragePath),
                RequestPath = "/mediastorage",
                EnableDirectoryBrowsing = true
            });

            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new {controller = "Home", action = "Index"});
            });
        }
    }
}
