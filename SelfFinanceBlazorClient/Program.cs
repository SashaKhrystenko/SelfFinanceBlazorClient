using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SelfFinanceBlazorClient.Components;
using SelfFinanceBlazorClient.Interfaces;
using SelfFinanceBlazorClient.Providers.Network;
using SelfFinanceBlazorClient.Providers.Network.Settings;
using System.IO;

namespace SelfFinanceBlazorClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            ConfigureService(builder.Services);

            WebApplication app = builder.Build();

            ConfigureApp(app);

            app.Run();
        }

        private static void ConfigureService(IServiceCollection services)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build()
            ;

            services.AddSingleton<INetworkProvider, NetworkProvider>();

            services.AddSingleton<SelfFinanceNetworkSettings>(settings =>
            {
                return configuration.GetSection("NetworkSettings:SelfFinance").Get<SelfFinanceNetworkSettings>();
            });

            services.AddRazorComponents()
                .AddInteractiveServerComponents()
            ;

            services.AddBlazorBootstrap();
        }

        private static void ConfigureApp(WebApplication app)
        {
            app.UseExceptionHandler("/Error500", createScopeForErrors: true);

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error500", createScopeForErrors: true);

                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
            ;
        }
    }
}