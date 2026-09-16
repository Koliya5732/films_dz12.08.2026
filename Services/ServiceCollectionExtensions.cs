using Microsoft.Extensions.DependencyInjection;

namespace films_dz12._08._2026.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMovieServices(
            this IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();

            return services;
        }
    }
}