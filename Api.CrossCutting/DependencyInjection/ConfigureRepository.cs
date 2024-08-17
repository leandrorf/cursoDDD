using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repositories;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository( this IServiceCollection services )
        {
            services.AddScoped( typeof( IRepository<> ), typeof( BaseRepository<> ) );
            services.AddScoped<IUserRepository, UserImplementation>( );

            var connectionString = "Server=localhost;Port=3306;Database=dbAPI2;Uid=root;Pwd=5326";
            //var connectionString = "Server=.\\LRFDEV;Initial Catalog=dbapi;TrustServerCertificate=true;User ID=sa;Password=1";

            services.AddDbContext<MyContext>(
                //options => options.UseSqlServer( connectionString )
                options => options.UseMySql( connectionString, ServerVersion.AutoDetect( connectionString ) )
            );
        }
    }
}