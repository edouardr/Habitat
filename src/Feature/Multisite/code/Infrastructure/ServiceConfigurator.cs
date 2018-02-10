namespace Sitecore.Feature.Multisite.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;
    using Sitecore.Feature.Multisite.Repositories;

    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISiteConfigurationRepository, SiteConfigurationRepository>();
        }
    }
}
