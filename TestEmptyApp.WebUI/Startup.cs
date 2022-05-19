using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TestEmptyApp.WebUI.Models.DataContext;

namespace TestEmptyApp.WebUI
{
    public class Startup
    {
        readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRouting(cfg =>
            {
                cfg.LowercaseUrls = true;
            });
            services.AddDbContext<BankDbContext>(cfg =>
            {
                cfg.UseSqlServer(configuration.GetConnectionString("cString"));
            });
            //services.AddIdentity<BankUser,BankRole>().AddEntityFrameworkStores<DbContext>();
            //services.AddScoped<SignInManager<BankUser>>();
            services.AddAuthentication();
            services.AddAuthorization();
            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.Cookie.Name = "BankApp";
                cfg.Cookie.HttpOnly = true;
                cfg.ExpireTimeSpan = new TimeSpan(0, 5, 0);
                cfg.LoginPath = "signin.html";
                //cfg.AccessDeniedPath = "access";

            });
            services.Configure<IdentityOptions>(cfg =>
            {
                //cfg.User.AllowedUserNameCharacters = "abcde";
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 1;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
