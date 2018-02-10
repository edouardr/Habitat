namespace Sitecore.Foundation.Multisite.Providers
{
    using System.Collections.Generic;
    using Sitecore.Data.Items;
    using Sitecore.Foundation.Multisite.Models;

    public interface ISiteDefinitionsProvider
    {
        IEnumerable<SiteDefinition> SiteDefinitions { get; }
        SiteDefinition GetContextSiteDefinition(Item item);
    }
}
