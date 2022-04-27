using Microsoft.Extensions.DependencyInjection;

namespace NetCore3WithReact.DAL
{
    public static class DependenciesRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });
        }
    }
}
