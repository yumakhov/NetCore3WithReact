using NetCore3WithReact.DAL.Services.Data;

namespace NetCore3WithReact.DAL.Services
{
    public interface IFeatureSettingsService
    {
        CacheProvider CacheProvider { get;  }
    }
}
