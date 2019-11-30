using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Projekt.Service;
using Projekt.Service.Common;
using Projekt.Service.DAL;

namespace Projekt.MVC
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<VehicleDbContext>(options => options.UseSqlServer(
                                config.GetConnectionString("VehicleDbConnection"),
                                    x => x.MigrationsAssembly("Projekt.Service")));

            services.AddTransient<IVehicleMakeService, VehicleMakeService>();
            services.AddTransient<IVehicleModelService, VehicleModelService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Projekt.MVC.Mappings.MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);


            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error/");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                       name: "default",
                       template: "{controller=Make}/{action=Administration}/{id?}");
            });
        }
    }
}
