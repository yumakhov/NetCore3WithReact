using Microsoft.Extensions.Configuration;
using NetCore3WithReact.DAL.Services;
using NetCore3WithReact.DAL.Services.Data;
using System;

namespace NetCore3WithReact.Services
{
    public class FeatureSettingsService : IFeatureSettingsService
    {
        private readonly IConfigurationSection _featureSettingsSection;

        public FeatureSettingsService()
        {
            _featureSettingsSection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FeatureSettings");
        }

        public CacheProvider CacheProvider => Enum.TryParse<CacheProvider>(_featureSettingsSection["cacheProvider"], true, out var cacheProvider) 
            ? cacheProvider 
            : CacheProvider.None;
    }
}
