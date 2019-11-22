using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportShop.Models;

namespace SportShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IManufacturerRepository, EFManufacturerRepository>();
            services.AddTransient<ICategoryRepository, EFCategoryRepository>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseDeveloperExceptionPage();

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{controller=Manufacturer}/{action=List}/{id?}");
                routes.MapRoute(
                   name: null,
                   template: "{controller=Product}/{action=List}/{id?}" 
                    );
                routes.MapRoute(
                   name: null,
                   template: "{controller=Admin}/{action=Index}"
                    );
                routes.MapRoute(
                name:null,
                template:"{controller=Admin}/{action=Edit}/{id?}"
                    );
            }

            );
            SeedData.EnsurePopulated(app);


        }
    }
}
