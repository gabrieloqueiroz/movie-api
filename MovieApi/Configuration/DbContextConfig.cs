using Microsoft.EntityFrameworkCore;
using MovieApi.Data;

namespace MovieApi.Configuration
{
    public static class DbContextConfig
    {

        public static IServiceCollection AddDbContextConfig(this IServiceCollection services, ConfigurationManager configuration)
        {

            var movieConn = configuration.GetConnectionString("MovieConnection");

            services.AddDbContext<MovieContext>(opts =>
                        opts.UseMySql(
                            movieConn,
                           ServerVersion.AutoDetect(movieConn)
                        ));


            return services;
        }
    }
}
