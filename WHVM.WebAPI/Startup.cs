using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WHVM.Database.Models;

namespace WHVM.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddDbContext<HomeVideoDBContext>(options =>
            {
                options.UseLazyLoadingProxies(false);
#if DEBUG
                options.UseSqlite("Data Source=HomeVideoDB.db");
#else
            options.UseSqlServer(connectionString);
#endif
            });
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            string mediaStoragePath;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                mediaStoragePath = Directory.CreateDirectory(Path.Join(Directory.GetParent(Directory.GetCurrentDirectory()).FullName,
                    "MediaStorage_Dev")).FullName;
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
                mediaStoragePath = Directory.CreateDirectory(Path.GetFullPath(Configuration.GetValue<string>("SiteVariables:MediaStorePath"))).FullName;
            }

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(mediaStoragePath),
                RequestPath = "/mediastorage",
                EnableDirectoryBrowsing = true
            });

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}