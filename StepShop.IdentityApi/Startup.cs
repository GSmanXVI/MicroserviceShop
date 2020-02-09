using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StepShop.IdentityApi.Data;
using StepShop.IdentityApi.Models;
using StepShop.IdentityApi.Options;
using StepShop.IdentityApi.Services;
using FluentValidation.AspNetCore;
using StepShop.Shared.Extensions;
using System.Reflection;

namespace StepShop.IdentityApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AccountsDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Accounts"));
            });

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<AccountsDbContext>();

            services.AddMvc()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddSwagger("AuthApi", "v1");

            services.AddJwtAuthentication();

            services.Configure<TokenGeneratorOptions>(options =>
            {
                options.Secret = Configuration["Secret"];
                options.AccessExpiration = TimeSpan.FromMinutes(15);
                options.RefreshExpiration = TimeSpan.FromDays(30);
            });

            services.AddScoped<TokenGenerator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
