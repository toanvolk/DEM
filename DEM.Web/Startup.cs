using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DEM.App;
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
            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.AutomaticAuthentication = false;
            //});
            // Add the Kendo UI services to the services container.
            services.AddKendo();
            // Auto Mapper Profile
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CategoryProfileMapping());
                cfg.AddProfile(new ExpenseProfileMapping());
                cfg.AddProfile(new IntendedProfileMapping());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Add Db Context
            services.AddDbContext<DEMContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DEMConnection")));

            // Register DbContext
            services.AddScoped<DEMContext>();
            // Register UnitOfWork
            services.AddScoped<IUnitOfWorkMedia, UnitOfWorkMedia>();
            // Register RepositoryBase
            services.AddScoped<IRepositoryBase, RepositoryBase>();

            // Register BaseService
            services.AddScoped<IBaseService, BaseService>();

            //Dependency
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IRootCategoryService, RootCategoryService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IPayerService, PayerService>();
            services.AddScoped<IIntendedService, IntendedService>();

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
            //Migrate database
            MigrateDatabaseAuto.Migrate(app);

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
