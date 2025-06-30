using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using Task_12.Components;

namespace Task_12
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
            services.AddScoped(sp =>
            {
                HttpClient httpClient = new()
                {
                    BaseAddress = new Uri("https://localhost:7089/")
                };

                return new Client("https://localhost:7089/", httpClient);
            });

            services.AddRazorComponents()
                .AddInteractiveServerComponents()
            ;
        }

        private static void ConfigureApp(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);

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