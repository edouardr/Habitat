namespace Sitecore.Foundation.Multisite.Tests
{
    using FluentAssertions;
    using Sitecore.Foundation.Multisite.Services;
    using Sitecore.Foundation.Multisite.Tests.Extensions;
    using Xunit;

    public class DatasourceConfigurationServiceTests
    {
        [Theory]
        [AutoDbData]
        public void GetSiteDatasourceConfigurationName_CorrectSettingsString_ReturnSettingName()
        {
            IDatasourceConfigurationService service = new DatasourceConfigurationService();
            var setting = "media";
            var name = $"site:{setting}";
            var settingName = service.GetSiteDatasourceConfigurationName(name);
            settingName.Should().BeEquivalentTo(setting);
        }

        [Theory]
        [AutoDbData]
        public void GetSiteDatasourceConfigurationName_IncorrectSettings_NullOrEmpty()
        {
            IDatasourceConfigurationService service = new DatasourceConfigurationService();
            var setting = "med.ia";
            var name = $"site:{setting}";
            var settingName = service.GetSiteDatasourceConfigurationName(name);
            settingName.Should().BeNullOrEmpty();
        }
    }
}
