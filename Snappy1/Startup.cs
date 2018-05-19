using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Snappy1.Data;
using Snappy1.Models;
using Snappy1.Services;

namespace Snappy1
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
            services.AddDbContext<online_resEntities>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "1965719240423945";
                facebookOptions.AppSecret ="d3fe66051bb2247e28d76ce7a87a096a";
            });
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

           

            services.AddMvc();
        }

        private static async Task EnsureRoleManager(RoleManager<IdentityRole> roleManager)
        {
            string[] arr = new string[] { "Manager", "Customer" };

            for(int i=0;i<arr.Length;i++)
            {
                var alreadyExists = await roleManager.RoleExistsAsync(arr[i]);
                if (!alreadyExists)
                {
                    await roleManager.CreateAsync(new IdentityRole((arr[i])));
                }

            }
           // var alreadyExists = await roleManager.RoleExistsAsync("Manager");
            //if (alreadyExists)
            //{
            //    return;
            //}
            //await roleManager.CreateAsync(new IdentityRole(("Manager")));

            //var CustomerRoleExists = await roleManager.RoleExistsAsync("Customer");
            //if(CustomerRoleExists)
            //{
            //    return;
            //}
            //await roleManager.CreateAsync(new IdentityRole(("Customer")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
                EnsureRoleManager(roleManager).Wait();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
