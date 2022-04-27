using Microsoft.Extensions.DependencyInjection;

namespace NetCore3WithReact.BusinessLogic.Services
{
    public static class DependenciesRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
