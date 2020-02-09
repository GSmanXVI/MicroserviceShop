using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StepShop.CartApi.BackgroundServices;
using StepShop.CartApi.Extenstions;
using StepShop.Shared.Extensions;

namespace StepShop.CartApi
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
            services.AddControllers();
            services.AddSwagger("CartApi", "v1");
            services.AddJwtAuthentication();
            services.AddHostedService<CartBackgroundService>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMongoClient(options => 
            {
                options.ConnectionString = Configuration.GetConnectionString("Cart");
                options.DatabaseName = Configuration["MongoOptions:DatabaseName"];
                options.CartCollectionName = Configuration["MongoOptions:CartCollectionName"];
            });
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
                endpoints.MapControllers();
            });
        }
    }
}
