using ServicesContracts;
using TournamentCore.Repositories;
using TournamentData.Repositories;
using TournamentServices;
using TournamentServices.Mapping;

namespace TournamentApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServiceLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IGameService, GameService>();
            services.AddTransient<IMapper, Mapper>();
            services.AddLazy<ITournamentService>();
            services.AddLazy<IGameService>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITournamentRepo, TournamentRepo>();
            services.AddScoped<IGameRepo, GameRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddLazy<IGameRepo>();
            services.AddLazy<ITournamentRepo>();
        }
    }
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLazy<TService>(this IServiceCollection services) where TService : class
        {
            return services.AddScoped(provider => new Lazy<TService>(() => provider.GetRequiredService<TService>()));
        }
    }
}
