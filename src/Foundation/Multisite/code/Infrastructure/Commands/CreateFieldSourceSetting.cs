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

    public class CreateFieldSourceSetting: Command
    {
        private readonly IFieldSourceConfigurationService fieldSourceConfigurationService;

        // TODO Service Locator Anti-pattern - Find a proper way to inject dependencies for a command (usage of resolve="true" doesn't work)
        public CreateFieldSourceSetting() : this(ServiceLocator.ServiceProvider
            .GetService(typeof(IFieldSourceConfigurationService)) as IFieldSourceConfigurationService)
        {
        }

        public CreateFieldSourceSetting(IFieldSourceConfigurationService fieldSourceConfigurationService)
        {
            this.fieldSourceConfigurationService = fieldSourceConfigurationService;
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

            var datasourceConfigurationName = GetFieldourceConfigurationName(renderingItem);

            contextItem.Add(datasourceConfigurationName, new TemplateID(Templates.FieldSourceConfiguration.ID));
        }

        private string GetFieldourceConfigurationName(Item renderingItem)
        {
            var fieldsourceLocationValue = renderingItem[Templates.FieldSourceConfiguration.Fields.Source];
            var fieldsourceConfigurationName = this.fieldSourceConfigurationService.GetSiteFieldSourceConfigurationName(fieldsourceLocationValue);
            if (string.IsNullOrEmpty(fieldsourceConfigurationName))
            {
                fieldsourceConfigurationName = renderingItem.Name;
            }

            return fieldsourceConfigurationName;
        }

        private void ShowDatasourceSettingsDialog()
        {
            var urlString = new UrlString(Context.Site.XmlControlPage)
            {
                ["xmlcontrol"] = "FieldsourceSettings"
            };
            var dialogOptions = new ModalDialogOptions(urlString.ToString())
            {
                Response = true
            };
            SheerResponse.ShowModalDialog(dialogOptions);
        }
    }
}