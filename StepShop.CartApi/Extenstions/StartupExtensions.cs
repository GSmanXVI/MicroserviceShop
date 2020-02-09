using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StepShop.CartApi.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepShop.CartApi.Extenstions
{
    public static class StartupExtensions
    {
        public static void AddMongoClient(this IServiceCollection services, Action<MongoDbCartOptions> options)
        {
            MongoDbCartOptions cartOptions = null;
            options?.Invoke(cartOptions);

            services.AddSingleton(x =>
            {
                return new MongoClient(cartOptions.ConnectionString);
            });

            services.Configure<MongoDbCartOptions>(options);
        }
    }
}
