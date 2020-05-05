using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoppingCart.Repositories;
using ShoppingCart.Models;
using Microsoft.AspNetCore.Identity;
using ShoppingCart.Middleware;

namespace ShoppingCart
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddDbContext<ProductContext>(options => 
                     options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ShoppingCart;Trusted_Connection=True;MultipleActiveResultSets=true"));

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(12000);
            });

            services.AddDefaultIdentity<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            })
             .AddEntityFrameworkStores<ProductContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ProductContext productContext)
        {
            //productContext.Database.EnsureDeleted();   
            productContext.Database.EnsureCreated();  

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseDeveloperExceptionPage();
            app.UseNodeModules(env.ContentRootPath);

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "ProductRoute",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Product", action = "Index" },
                    constraints: new { id = "[0-9]+" });
            });
        }
    }
}
