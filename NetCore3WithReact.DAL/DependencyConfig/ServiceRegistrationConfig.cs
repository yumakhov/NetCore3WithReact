using Microsoft.Extensions.DependencyInjection;

namespace NetCore3WithReact.DAL.DependencyConfig
{
    public static class ServiceRegistrationConfig
    {
        public static void RegisterTypes(IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });
        }
    }
}
