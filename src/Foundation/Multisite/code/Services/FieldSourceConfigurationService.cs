namespace Sitecore.Foundation.Multisite.Services
{
    using System.Text.RegularExpressions;

    public class FieldSourceConfigurationService : IFieldSourceConfigurationService
    {
        public const string SiteFieldsourcePrefix = "site:";
        public const string SiteFieldsourceMatchPattern = @"^" + SiteFieldsourcePrefix + @"(\w*)$";

        public string GetSiteFieldSourceConfigurationName(string fieldSourceLocationValue)
        {
            var match = Regex.Match(fieldSourceLocationValue, SiteFieldsourceMatchPattern);
            return !match.Success ? null : match.Groups[1].Value;
        }

        public bool IsSiteFieldSourceLocation(string fieldSourceLocationValue)
        {
            var match = Regex.Match(fieldSourceLocationValue, SiteFieldsourceMatchPattern);
            return match.Success;
        }
    }
}