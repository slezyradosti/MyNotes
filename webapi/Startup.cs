using Domain.Repositories.EFInitial;
using IndentityLogic.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using webapi.Extensions;
using webapi.Middleware;

namespace webapi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("MSSQLConnection")));

            services.AddControllers(options =>
            {
                //every controller requires auth
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));

                //cache profiles
                options.CacheProfiles.Add("NoCache",
                    new CacheProfile() { NoStore = true });
                options.CacheProfiles.Add("Any-30",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.Any,
                        Duration = 30
                    });
                options.CacheProfiles.Add("Any-60",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.Any,
                        Duration = 60
                    });
                options.CacheProfiles.Add("Any-180",
                new CacheProfile()
                {
                    Location = ResponseCacheLocation.Any,
                    Duration = 180
                });
                options.CacheProfiles.Add("Any-300",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.Any,
                        Duration = 300
                    });
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 5001;
            });

            services.Configure<Security>(_configuration.GetSection("SecurityKey"));

            services.AddApplicationServices(_configuration);
            services.AddIdentityServices(_configuration);

            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            if (_configuration.GetValue<bool>("UseSwagger"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }
            if (_configuration.GetValue<bool>("UseDeveloperExceptionPage"))
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use((context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        NoCache = true,
                        NoStore = true
                    };
                return next.Invoke(); // nonblocking
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
