using Domain.Repositories.Repos;
using Domain.Repositories.Repos.Interfaces;
using webapi.Controllers;

namespace webapi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(cfg =>
                {
                    cfg.WithOrigins(configuration["AllowedOrigins"]);
                    cfg.AllowAnyHeader();
                    cfg.AllowAnyMethod();
                });
                options.AddPolicy(name: "AnyOrigin", cfg =>
                {
                    cfg.AllowAnyOrigin();
                    cfg.AllowAnyHeader();
                    cfg.AllowAnyMethod();
                });
            });

            services.AddScoped<INotebookRepository, NotebookRepository>();

            return services;
        }
    }
}
