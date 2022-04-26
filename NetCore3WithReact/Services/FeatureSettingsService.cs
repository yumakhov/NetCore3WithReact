using Microsoft.Extensions.Configuration;
using NetCore3WithReact.DAL.Services;

namespace NetCore3WithReact.Services
{
    public class FeatureSettingsService : IFeatureSettingsService
    {
        private readonly IConfigurationSection _featureSettingsSection;

        public FeatureSettingsService()
        {
            _featureSettingsSection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FeatureSettings");
        }

        public bool IsCacheEnabled => bool.Parse(_featureSettingsSection["IsCacheEnabled"]);
    }
}
