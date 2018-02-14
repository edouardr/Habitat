namespace Sitecore.Foundation.Multisite.Providers
{
    using System.Linq;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Foundation.Multisite.Contexts;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;

    public class SiteSettingsProvider : ISiteSettingsProvider
    {
        private readonly ISiteContext siteContext;

        public SiteSettingsProvider(ISiteContext siteContext)
        {
            this.siteContext = siteContext;
        }

        public static string SettingsRootName => Settings.GetSetting("Foundation.Multisite.SettingsRootName", "settings");

        public virtual Item GetSetting(Item contextItem, ID settingFolderTemplateId, string settingsName, string setting)
        {
            var settingsRootItem = this.GetSettingsRoot(contextItem, settingFolderTemplateId, settingsName);
            var settingItem = settingsRootItem?.Children.FirstOrDefault(i => i.Key.Equals(setting.ToLower()));
            return settingItem;
        }

        private Item GetSettingsRoot(Item contextItem, ID settingFolderTemplateId, string settingsName)
        {
            var currentDefinition = this.siteContext.GetSiteDefinition(contextItem);
            if (currentDefinition?.Item == null)
            {
                return null;
            }

            var definitionItem = currentDefinition.Item;
            var settingsFolder = definitionItem.Children[SettingsRootName];
            var settingsRootItem = settingsFolder?.Children
                .FirstOrDefault(i => i.IsDerived(settingFolderTemplateId) && 
                                     i.Key.Equals(settingsName.ToLower()));
            return settingsRootItem;
        }
    }
}