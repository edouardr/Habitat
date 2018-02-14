namespace Sitecore.Foundation.Multisite
{
    using Sitecore.Data;

    public class Templates
    {
        public struct Site
        {
            public static ID ID = new ID("{BB85C5C2-9F87-48CE-8012-AF67CF4F765D}");
        }

        public struct DatasourceConfiguration
        {
            public static ID ID = new ID("{C82DC5FF-09EF-4403-96D3-3CAF377B8C5B}");

            public struct Fields
            {
                public static readonly ID DatasourceLocation = new ID("{5FE1CC43-F86C-459C-A379-CD75950D85AF}");
                public const string DatasourceLocationName = @"Datasouce Location";
                public static readonly ID DatasourceTemplate = new ID("{498DD5B6-7DAE-44A7-9213-1D32596AD14F}");
            }
        }

        public struct FieldSourceConfiguration
        {
            public static ID ID = new ID("{3A1059A6-EA04-4033-B20D-72DCF7AFD702}");

            public struct Fields
            {
                public static readonly ID Source = new ID("{0EE3046C-C927-4776-91EF-F9BB0161D274}");
                public const string SourceName = @"Source";
            }
        }

        public struct RenderingSiteSettings
        {
            public static ID ID = new ID("{BCCFEBEA-DCCB-48FE-9570-6503829EC03F}");
        }

        public struct FieldSiteSettings
        {
            public static ID ID = new ID("{BE99420D-A2C5-4664-88D9-D2CB89028790}");
        }

        public struct RenderingOptions
        {
            public static ID ID = new ID("{D1592226-3898-4CE2-B190-090FD5F84A4C}");

            public struct Fields
            {
                public static readonly ID DatasourceLocation = new ID("{B5B27AF1-25EF-405C-87CE-369B3A004016}");
                public static readonly ID DatasourceTemplate = new ID("{1A7C85E5-DC0B-490D-9187-BB1DBCB4C72F}");
            }
        }

        public struct TemplateField
        {
            public static ID ID = new ID("{455A3E98-A627-4B40-8035-E683A0331AC7}");

            public struct Fields
            {
                public static readonly ID Source = new ID("{1EB8AE32-E190-44A6-968D-ED904C794EBF}");
            }
        }
    }
}