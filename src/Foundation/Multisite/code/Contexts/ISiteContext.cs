namespace Sitecore.Foundation.Multisite.Contexts
{
    using Sitecore.Data.Items;
    using Sitecore.Foundation.Multisite.Models;

    public interface ISiteContext
    {
        SiteDefinition GetSiteDefinition([NotNull] Item item);
    }
}