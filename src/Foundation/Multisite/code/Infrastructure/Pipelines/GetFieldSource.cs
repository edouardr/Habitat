namespace Sitecore.Foundation.Multisite.Infrastructure.Pipelines
{
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.Multisite.Providers;
    using Sitecore.Foundation.Multisite.Services;
    using Sitecore.Pipelines.GetLookupSourceItems;

    public class GetFieldSource
    {
        private readonly IFieldSourceProvider provider;
        private readonly IFieldSourceConfigurationService fieldSourceConfigurationService;

        public GetFieldSource(IFieldSourceProvider provider,
            IFieldSourceConfigurationService fieldSourceConfigurationService)
        {
            this.provider = provider;
            this.fieldSourceConfigurationService = fieldSourceConfigurationService;
        }

        public void Process(GetLookupSourceItemsArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            
            if (!args.Item.Paths.FullPath.StartsWith("/sitecore/content"))
            {
                return;
            }

            this.ResolveSource(args);
        }

        private void ResolveSource(GetLookupSourceItemsArgs args)
        {
            var contextItem = args.Item;

            if (!this.fieldSourceConfigurationService.IsSiteFieldSourceLocation(args.Source))
            {
                return;
            }


            var name = this.fieldSourceConfigurationService.GetSiteFieldSourceConfigurationName(args.Source);
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            args.Source = this.provider.GetFieldSource(contextItem, name);
        }
    }
}