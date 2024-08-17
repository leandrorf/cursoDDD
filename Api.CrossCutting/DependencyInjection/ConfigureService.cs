using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureService
    {
        public static void ConfigureDependenciesService( this IServiceCollection services )
        {
            services.AddTransient<IUserService, UserService>( );
            services.AddTransient<ILoginService, LoginService>( );
        }
    }
}