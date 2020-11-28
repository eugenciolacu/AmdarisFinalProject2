using AmdarisInternship.API.Infrastructure.Extensions;
using AmdarisInternship.Domain.Auth;
using AmdarisInternship.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using AutoMapper;
using AmdarisInternship.API.Mappings;
using AmdarisInternship.Infrastructure.Repositories.Interfaces;
using AmdarisInternship.Infrastructure.Repositories.Implementations;
using AmdarisInternship.API.Services.Interfaces;
using AmdarisInternship.API.Services.Implementations;

namespace AmdarisInternship
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
            services.AddControllers();

            services.AddDbContext<AppDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>();

            var authOptions = services.ConfigureAuthOptions(Configuration);
            services.AddJwtAuthentication(authOptions);
            services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });

            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new ModuleMappingProfile());
            });

            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IModuleRepository, ModuleRepository>();

            services.AddScoped<IModuleModuleGradingsService, ModuleModuleGradingsService>();

            services.AddScoped<IAccountService, AccountService>();

            ConfigureSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(configurePolicy => configurePolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "Swagger Demo API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Amdaris Internship API",
                Description = "Amdaris Internship lesson calendar",
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);
            });
        }
    }
}
