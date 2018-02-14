namespace Sitecore.Foundation.Multisite.Infrastructure
{
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Sitecore.Abstractions;
    using Sitecore.DependencyInjection;
    using Sitecore.Foundation.Multisite.Contexts;
    using Sitecore.Foundation.Multisite.Placeholders;
    using Sitecore.Foundation.Multisite.Providers;
    using Sitecore.Foundation.Multisite.Services;
    using Sitecore.Sites;
    using Sitecore.Web;

    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.Replace(ServiceDescriptor
              .Singleton<BasePlaceholderCacheManager, SiteSpecificPlaceholderCacheManager>());

            serviceCollection.AddSingleton<ISiteSettingsProvider, SiteSettingsProvider>();
            serviceCollection.AddSingleton<ISiteDefinitionsProvider, SiteDefinitionsProvider>();
            serviceCollection.AddSingleton<IDatasourceProvider, DatasourceProvider>();
            serviceCollection.AddSingleton<IFieldSourceProvider, FieldSourceProvider>();

            serviceCollection.AddSingleton<IDatasourceConfigurationService, DatasourceConfigurationService>();
            serviceCollection.AddSingleton<IFieldSourceConfigurationService, FieldSourceConfigurationService>();

            serviceCollection.AddTransient<ISiteContext, Contexts.SiteContext>();
            serviceCollection.AddTransient<ISiteDefinitionsProvider, SiteDefinitionsProvider>();

            serviceCollection.AddTransient<IEnumerable<SiteInfo>>(resolver => SiteContextFactory.Sites);
        }
    }
}