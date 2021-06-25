using AutoMapper;
using Core.DomainModel;
using DomainServices.Repositories;
using DomainServices.Services;
using EFData;
using HttpData;
using Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adoption
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:AnimalShelter:ConnectionString"])
                    .EnableSensitiveDataLogging());

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:Identity:ConnectionString"])
                    .EnableSensitiveDataLogging());

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireVolunteer", policy => policy.RequireRole("Volunteer"));
                options.AddPolicy("RequireCustomer", policy => policy.RequireRole("Customer"));
                options.AddPolicy("RequireVolunteerOrCustomer", policy => policy.RequireRole("Volunteer", "Customer"));
            });

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddMvc();
            services.AddSession();

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IAnimalRepository, HttpAnimalRepository>();
            services.AddScoped<IUserRepository, EFUserRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<PasswordHasher<ApplicationUser>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, ApplicationUser>();
                cfg.CreateMap<Volunteer, ApplicationUser>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
