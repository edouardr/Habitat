namespace Sitecore.Foundation.Multisite.Services
{
    using System.Text.RegularExpressions;

    public class DatasourceConfigurationService: IDatasourceConfigurationService
    {
        public const string SiteDatasourcePrefix = "site:";
        public const string SiteDatasourceMatchPattern = @"^" + SiteDatasourcePrefix + @"(\w*)$";

        public string GetSiteDatasourceConfigurationName(string datasourceLocationValue)
        {
            var match = Regex.Match(datasourceLocationValue, SiteDatasourceMatchPattern);
            return !match.Success ? null : match.Groups[1].Value;
        }

        public bool IsSiteDatasourceLocation(string datasourceLocationValue)
        {
            var match = Regex.Match(datasourceLocationValue, SiteDatasourceMatchPattern);
            return match.Success;
        }

    }
}