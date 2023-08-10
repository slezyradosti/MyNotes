using Domain.Repositories.EFInitial;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using webapi.Extensions;

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
                //authorization policy
            });
            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 5001;
            });

            services.AddApplicationServices(_configuration);
            // then add here identitypolicy and applicaton policy
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
        {
            //app.UseMiddleware<ExceptionMiddleware>();
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
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();
            
            //authentication and authorization

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/error", () => Results.Problem());
                endpoints.MapControllers();
                endpoints.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
