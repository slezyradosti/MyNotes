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
            services.AddScoped<INotebookRepository, NotebookRepository>();

            return services;
        }
    }
}
