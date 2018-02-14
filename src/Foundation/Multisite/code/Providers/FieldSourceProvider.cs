namespace Sitecore.Foundation.Multisite.Providers
{
    using Sitecore.Data.Items;

    public class FieldSourceProvider : IFieldSourceProvider
    {
        public const string FieldsourceSettingsName = "Field sources";

        private readonly ISiteSettingsProvider siteSettingsProvider;

        public FieldSourceProvider(ISiteSettingsProvider siteSettingsProvider)
        {
            this.siteSettingsProvider = siteSettingsProvider;
        }

        public string GetFieldSource(Item contextItem, string settingName)
        {
            var settingItem = this.siteSettingsProvider.GetSetting(contextItem, Templates.FieldSiteSettings.ID, FieldsourceSettingsName, settingName);
            return settingItem?[Templates.FieldSourceConfiguration.Fields.Source];
        }
    }
}