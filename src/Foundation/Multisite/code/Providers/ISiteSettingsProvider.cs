namespace Sitecore.Foundation.Multisite.Providers
{
    using Sitecore.Data;
    using Sitecore.Data.Items;

    public interface ISiteSettingsProvider
    {
        Item GetSetting(Item contextItem, ID settingFolderTemplateId, string settingsType, string setting);
    }
}