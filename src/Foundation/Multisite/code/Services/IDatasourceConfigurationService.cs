namespace Sitecore.Foundation.Multisite.Services
{
    public interface IDatasourceConfigurationService
    {
        string GetSiteDatasourceConfigurationName(string datasourceLocationValue);
        bool IsSiteDatasourceLocation(string datasourceLocationValue);
    }
}