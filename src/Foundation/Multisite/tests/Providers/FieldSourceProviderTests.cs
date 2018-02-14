namespace Sitecore.Foundation.Multisite.Tests.Providers
{
    using FluentAssertions;
    using NSubstitute;
    using Ploeh.AutoFixture.Xunit2;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb;
    using Sitecore.Foundation.Multisite.Providers;
    using Sitecore.Foundation.Multisite.Tests.Extensions;
    using Xunit;

    public class FieldSourceProviderTests
    {
        [Theory]
        [AutoDbData]
        public void GetFieldSource_ShouldReturnSourceFromSettingItem([Frozen] ISiteSettingsProvider siteSettingsProvider, [Greedy] FieldSourceProvider provider, string name, Item contextItem, Db db, string settingItemName, Item item, DbItem sourceRoot)
        {
            var settingId = ID.NewID;
            var expectedQuery = "query:/sitecore/content/*/Global/FAQ//*[@@templateid='{BFDC1F27-3D2D-495F-89A3-0951F882420B}']";
            db.Add(new DbItem(settingItemName, settingId, Templates.FieldSourceConfiguration.ID)
            {
                new DbField(Templates.FieldSourceConfiguration.Fields.Source)
                {
                    {"en", expectedQuery},
                }
            });

            db.Add(sourceRoot);
            
            var settingItem = db.GetItem(settingId);
            siteSettingsProvider.GetSetting(Arg.Any<Item>(), Arg.Any<ID>(), Arg.Any<string>(), Arg.Any<string>()).Returns(settingItem);
            var sources = provider.GetFieldSource(item, name);
            sources.Should().NotBeNull();
            sources.Should().Contain(expectedQuery);
        }
    }
}