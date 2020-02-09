using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazor.Extensions.Storage;
using StepShop.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace StepShop.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddStorage();

            builder.Services.AddScoped<TokenStorage>();
            builder.Services.AddScoped<AccountService>();

            builder.Services.AddScoped<TokenAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<TokenAuthenticationStateProvider>());

            builder.Services.AddAuthorizationCore();
            builder.Services.AddOptions();

            await builder.Build().RunAsync();
        }
    }
}
