using Application.Core;
using Application.Notebooks;
using Domain.Repositories.Repos;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic;
using IndentityLogic.Interfaces;
using Microsoft.OpenApi.Models;
 
namespace webapi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });

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

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(List.Handler).Assembly));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            services.AddScoped<INotebookRepository, NotebookRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAcessor>();

            return services;
        }
    }
}
