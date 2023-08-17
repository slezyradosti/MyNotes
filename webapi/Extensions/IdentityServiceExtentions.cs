using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos.Interfaces;
using Domain.Repositories.Repos;
using IndentityLogic;
using IndentityLogic.Interfaces;

namespace webapi.Extensions
{
    public static class IdentityServiceExtentions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<DataContext>();

            services.AddAuthentication();

            services.AddScoped<ILogin, Login>();

            return services;
        }
    }
}
