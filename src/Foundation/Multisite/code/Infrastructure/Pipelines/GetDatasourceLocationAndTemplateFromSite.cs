namespace Sitecore.Foundation.Multisite.Infrastructure.Pipelines
{
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.Multisite.Providers;
    using Sitecore.Foundation.Multisite.Services;
    using Sitecore.Pipelines.GetRenderingDatasource;

    public class GetDatasourceLocationAndTemplateFromSite
    {
        private readonly IDatasourceProvider provider;
        private readonly IDatasourceConfigurationService datasourceConfigurationService;

        public GetDatasourceLocationAndTemplateFromSite(IDatasourceProvider provider,
            IDatasourceConfigurationService datasourceConfigurationService)
        {
            this.provider = provider;
            this.datasourceConfigurationService = datasourceConfigurationService;
        }

        public void Process(GetRenderingDatasourceArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            var datasource = args.RenderingItem[Templates.RenderingOptions.Fields.DatasourceLocation];
            if (!this.datasourceConfigurationService.IsSiteDatasourceLocation(datasource))
            {
                return;
            }

            this.ResolveDatasource(args);
            this.ResolveDatasourceTemplate(args);
        }

        protected virtual void ResolveDatasource(GetRenderingDatasourceArgs args)
        {
            var contextItem = args.ContentDatabase.GetItem(args.ContextItemPath);
            var source = args.RenderingItem[Templates.RenderingOptions.Fields.DatasourceLocation];
            var name = this.datasourceConfigurationService.GetSiteDatasourceConfigurationName(source);
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            var datasourceLocations = this.provider.GetDatasourceLocations(contextItem, name);
            args.DatasourceRoots.AddRange(datasourceLocations);
        }

        protected virtual void ResolveDatasourceTemplate(GetRenderingDatasourceArgs args)
        {
            var contextItem = args.ContentDatabase.GetItem(args.ContextItemPath);
            var datasourceLocation = args.RenderingItem[Templates.RenderingOptions.Fields.DatasourceLocation];
            var name = this.datasourceConfigurationService.GetSiteDatasourceConfigurationName(datasourceLocation);
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            args.Prototype = this.provider.GetDatasourceTemplate(contextItem, name);
        }
    }
}