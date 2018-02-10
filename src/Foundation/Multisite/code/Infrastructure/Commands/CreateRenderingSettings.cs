namespace Sitecore.Foundation.Multisite.Infrastructure.Commands
{
    using System.Collections.Specialized;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.DependencyInjection;
    using Sitecore.Foundation.Multisite.Services;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore.Text;
    using Sitecore.Web.UI.Sheer;

    public class CreateRenderingSettings : Command
    {
        private const string DatasourceLocationFieldName = "Datasource Location";

        private readonly IDatasourceConfigurationService datasourceConfigurationService;

        // TODO Service Locator Anti-pattern - Find a proper way to inject dependencies for a commad (usage of resolve="true" doesn't work)
        public CreateRenderingSettings() : this(ServiceLocator.ServiceProvider
            .GetService(typeof(IDatasourceConfigurationService)) as IDatasourceConfigurationService)
        {
        }

        public CreateRenderingSettings(IDatasourceConfigurationService datasourceConfigurationService)
        {
            this.datasourceConfigurationService = datasourceConfigurationService;
        }

        public override void Execute(CommandContext context)
        {
            var parameters = new NameValueCollection();
            var parentId = context.Parameters["parentID"];
            if (string.IsNullOrEmpty(parentId))
            {
                var item = context.Items[0];
                parentId = item.ID.ToString();
            }

            parameters.Add("item", parentId);
            Context.ClientPage.Start(this, "Run", parameters);
        }

        public void Run(ClientPipelineArgs args)
        {
            if (!args.IsPostBack)
            {
                ShowDatasourceSettingsDialog();
                args.WaitForPostBack();
            }
            else
            {
                if (!args.HasResult)
                {
                    return;
                }

                var itemId = ID.Parse(args.Parameters["item"]);
                CreateDatasourceConfigurationItem(itemId, args.Result);
            }
        }

        private void CreateDatasourceConfigurationItem(ID contextItemId, string renderingItemId)
        {
            var contextItem = Context.ContentDatabase.GetItem(contextItemId);
            if (contextItem == null)
            {
                return;
            }

            var renderingItem = Context.ContentDatabase.GetItem(renderingItemId);
            if (renderingItem == null)
            {
                return;
            }

            var datasourceConfigurationName = GetDatasourceConfigurationName(renderingItem);

            contextItem.Add(datasourceConfigurationName, new TemplateID(Templates.DatasourceConfiguration.ID));
        }

        private string GetDatasourceConfigurationName(Item renderingItem)
        {
            var datasourceLocationValue = renderingItem[DatasourceLocationFieldName];
            var datasourceConfigurationName = this.datasourceConfigurationService.GetSiteDatasourceConfigurationName(datasourceLocationValue);
            if (string.IsNullOrEmpty(datasourceConfigurationName))
            {
                datasourceConfigurationName = renderingItem.Name;
            }

            return datasourceConfigurationName;
        }

        private void ShowDatasourceSettingsDialog()
        {
            var urlString = new UrlString(Context.Site.XmlControlPage)
            {
                ["xmlcontrol"] = "DatasourceSettings"
            };
            var dialogOptions = new ModalDialogOptions(urlString.ToString())
            {
                Response = true
            };
            SheerResponse.ShowModalDialog(dialogOptions);
        }
    }
}