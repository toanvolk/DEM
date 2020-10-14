using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DEM.App;
using DEM.App.Implements;
using DEM.EF;
using DEM.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DEM.Web
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
            // Add the Kendo UI services to the services container.
            services.AddKendo();
            // Auto Mapper Profile
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CategoryProfileMapping());
                cfg.AddProfile(new ExpenseProfileMapping());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Add Db Context
            services.AddDbContext<DEMContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DEMConnection")));

            // Register UnitOfWork
            services.AddScoped<IUnitOfWorkMedia, UnitOfWorkMedia>();

            //Dependency
            services.AddScoped<IRootCategoryService, RootCategoryService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IExpenseService, ExpenseService>();

            services.AddControllersWithViews();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            // Add Logfile
            loggerFactory.AddFile(Configuration.GetSection("Logging:LogPath").Value);

            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
