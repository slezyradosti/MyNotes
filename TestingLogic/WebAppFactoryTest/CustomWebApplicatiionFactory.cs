using Domain.Repositories.EFInitial;
using IndentityLogic.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using webapi;

namespace TestingLogic.WebAppFactoryTest
{
    public class CustomWebApplicatiionFactory : WebApplicationFactory<Startup>
    {
        public UserManager<ApplicationUser> UserManager { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var connection = new SqliteConnection("Data Source=:memory:");
                connection.Open(); // ! important

                // Remove the app's DataContext registration
                services.RemoveAll(typeof(DbContextOptions<DataContext>));

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseSqlite(connection);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DataContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicatiionFactory>>();

                    db.Database.EnsureCreated();

                    //try
                    //{
                    //    Utilities.InitializeDbForTests(db);
                    //}
                    //catch (Exception ex)
                    //{
                    //    logger.LogError(ex, "An error occurred seeding the " +
                    //        "database with test messages. Error: {Message}", ex.Message);
                    //}
                }

                UserManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
            });
        }
    }
}