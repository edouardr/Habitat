namespace Sitecore.Foundation.Multisite.Services
{
    public interface IFieldSourceConfigurationService
    {
        
        string GetSiteFieldSourceConfigurationName(string fieldSourceLocationValue);
        bool IsSiteFieldSourceLocation(string fieldSourceLocationValue);
    }
}