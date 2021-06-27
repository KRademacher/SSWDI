using DomainServices.Repositories;
using DomainServices.Services;
using EFData;
using Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Services;
using System;
using System.Globalization;

namespace WebService
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

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireVolunteer", policy => policy.RequireRole("Volunteer"));
                options.AddPolicy("RequireCustomer", policy => policy.RequireRole("Customer"));
                options.AddPolicy("RequireVolunteerOrCustomer", policy => policy.RequireRole("Volunteer", "Customer"));
            });

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddMvc();
            services.AddSwaggerGen();

            // Repositories Dependency Injections
            services.AddTransient<IAnimalRepository, EFAnimalRepository>();
            services.AddTransient<ILodgingRepository, EFLodgingRepository>();
            services.AddTransient<IInterestedAnimalRepository, EFInterestedAnimalRepository>();
            services.AddTransient<IUserRepository, EFUserRepository>();

            // Services Dependency Injections
            services.AddTransient<IAnimalService, AnimalService>();
            services.AddTransient<ILodgingService, LodgingService>();

            services.AddLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Animal Shelter API");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            var supportedCultures = new[]
            {
                new CultureInfo("nl-NL")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("nl-NL"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                FallBackToParentCultures = false,
                FallBackToParentUICultures = false
            });
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("nl-NL");
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("nl-NL");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}