using Domain.Repositories.EFInitial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using webapi;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHost(args).Build();

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured during migration");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });      
    }
}