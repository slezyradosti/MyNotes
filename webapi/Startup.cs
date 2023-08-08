using Domain.Repositories.EFInitial;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers(options =>
            {
                //authorization policy
            });
            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 5001;
            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });
            // then add here identitypolicy and applicaton policy
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
        {
            //app.UseMiddleware<ExceptionMiddleware>();

            if (webHostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseRouting();
            
            //authentication and authorization

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
