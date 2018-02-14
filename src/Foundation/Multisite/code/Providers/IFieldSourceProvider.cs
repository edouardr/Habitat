namespace Sitecore.Foundation.Multisite.Providers
{
    using Sitecore.Data.Items;

    public interface IFieldSourceProvider
    {
        string GetFieldSource(Item contextItem, string settingName);
    }
}