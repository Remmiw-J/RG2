using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signum.Engine;
using Signum.Engine.Basics;
using Signum.Engine.Chart;
using Signum.Engine.Maps;
using Signum.Engine.Migrations;
using Signum.Engine.Operations;
using Signum.Engine.Translation;
using Signum.Entities;
using Signum.Entities.Authorization;
using Signum.Entities.Basics;
using Signum.Entities.Files;
using Signum.Entities.Mailing;
using Signum.Entities.SMS;
using Signum.Entities.Word;
using Signum.Services;
using Signum.Utilities;
using RG2.Entities;
using Signum.Entities.Workflow;
using Signum.Engine.UserAssets;
using System.IO;
using Signum.Entities.MachineLearning;
using Signum.Entities.UserAssets;
using Signum.Engine.Authorization;
using Signum.Engine.MachineLearning;
using Signum.Engine.DynamicQuery;

namespace RG2.Terminal
{
    public static class RG2Migrations
    {
        public static void CSharpMigrations(bool autoRun)
        {
            Schema.Current.Initialize();

            OperationLogic.AllowSaveGlobally = true;

            new CSharpMigrationRunner
            {
                CreateRoles,
                CreateSystemUser,
                CreateCultureInfo,
                EmployeeLoader.LoadRegions,
                EmployeeLoader.LoadTerritories,
                EmployeeLoader.LoadEmployees,
                ProductLoader.LoadSuppliers,
                ProductLoader.LoadCategories,
                ProductLoader.LoadProducts,
                CustomerLoader.LoadCompanies,
                CustomerLoader.LoadPersons,
                OrderLoader.LoadShippers,
                OrderLoader.LoadOrders,
                EmployeeLoader.CreateUsers,
                OrderLoader.UpdateOrdersDate,
                ImportWordReportTemplateForOrder,
                ImportUserAssets,
                ImportSpanishInstanceTranslations,
                InitialAuthRulesImport,
            }.Run(autoRun);
        } //CSharpMigrations

        internal static void InitialAuthRulesImport()
        {
            AuthLogic.AutomaticImportAuthRules();
        }

        internal static void CreateRoles()
        {
            using (Transaction tr = new Transaction())
            {
                RoleEntity su = new RoleEntity() { Name = "Super user", MergeStrategy = MergeStrategy.Intersection }.Save();
                RoleEntity u = new RoleEntity() { Name = "User", MergeStrategy = MergeStrategy.Union }.Save();

                RoleEntity au = new RoleEntity()
                {
                    Name = "Advanced user",
                    Roles = new MList<Lite<RoleEntity>> { u.ToLite() },
                    MergeStrategy = MergeStrategy.Union
                }.Save();

                RoleEntity an = new RoleEntity() { Name = "Anonymous", MergeStrategy = MergeStrategy.Union }.Save();
                
                tr.Commit();
            }
        }

        internal static void CreateSystemUser()
        {
            using (OperationLogic.AllowSave<UserEntity>())
            using (Transaction tr = new Transaction())
            {
                UserEntity system = new UserEntity
                {
                    UserName = "System",
                    PasswordHash = Security.EncodePassword("System"),
                    Role = Database.Query<RoleEntity>().Where(r => r.Name == "Super user").SingleEx().ToLite(),
                    State = UserState.Saved,
                }.Save();

                UserEntity anonymous = new UserEntity
                {
                    UserName = "Anonymous",
                    PasswordHash = Security.EncodePassword("Anonymous"),
                    Role = Database.Query<RoleEntity>().Where(r => r.Name == "Anonymous").SingleEx().ToLite(),
                    State = UserState.Saved,
                }.Save(); //Anonymous

                tr.Commit();
            }
        } //CreateSystemUser

        public static void CreateCultureInfo()
        {
            using (Transaction tr = new Transaction())
            {
                var en = new CultureInfoEntity(CultureInfo.GetCultureInfo("en")).Save();
                var es = new CultureInfoEntity(CultureInfo.GetCultureInfo("es")).Save();

                new ApplicationConfigurationEntity
                {
                    Environment = "Development",
                    DatabaseName = "RG2",
                    AuthTokens = new AuthTokenConfigurationEmbedded
                    {
                    },
                    WebAuthn = new WebAuthnConfigurationEmbedded
                    {
                        ServerName = "RG2"
                    }, //Auth
                    Email = new EmailConfigurationEmbedded
                    {
                        SendEmails = true,
                        DefaultCulture = en,
                        UrlLeft = "http://localhost/RG2"
                    },
                    EmailSender = new EmailSenderConfigurationEntity
                    {
                        Name = "localhost",
                        SMTP = new SmtpEmbedded
                        {
                            Network = new SmtpNetworkDeliveryEmbedded
                            {
                                Host = "localhost"
                            }
                        }
                    }, //Email
                    Workflow = new WorkflowConfigurationEmbedded
                    {
                    }, //Workflow
                    Translation = new TranslationConfigurationEmbedded
                    {
                        AzureCognitiveServicesAPIKey = null,
                        DeepLAPIKey = null,
                    },
                    Folders = new FoldersConfigurationEmbedded
                    {
                        ExceptionsFolder = @"c:/RG2/Exceptions",
                        OperationLogFolder = @"c:/RG2/OperationLog",
                        ViewLogFolder = @"c:/RG2/ViewLog",
                        EmailMessageFolder = @"c:/RG2/EmailMessage",
                    }
                }.Save();

                tr.Commit();
            }

        }

        public static void ImportSpanishInstanceTranslations()
        {
            TranslatedInstanceLogic.ImportExcelFile("../../../InstanceTranslations/Category.es.View.xlsx");
            TranslatedInstanceLogic.ImportExcelFile("../../../InstanceTranslations/Dashboard.es.View.xlsx");
            TranslatedInstanceLogic.ImportExcelFile("../../../InstanceTranslations/Toolbar.es.View.xlsx");
            TranslatedInstanceLogic.ImportExcelFile("../../../InstanceTranslations/ToolbarMenu.es.View.xlsx");
            TranslatedInstanceLogic.ImportExcelFile("../../../InstanceTranslations/UserChart.es.View.xlsx");
            TranslatedInstanceLogic.ImportExcelFile("../../../InstanceTranslations/UserQuery.es.View.xlsx");
        }

        public static void ImportWordReportTemplateForOrder()
        {
            new WordTemplateEntity
            {
                Name = "Order template",
                Query = QueryLogic.GetQueryEntity(typeof(OrderEntity)),
                Culture = CultureInfo.GetCultureInfo("en").ToCultureInfoEntity(),
                Template = new FileEntity("../../../WordTemplates/Order.docx").ToLiteFat(),
                FileName = "Order.docx"
            }.Save();
        }

        public static void ImportUserAssets()
        {
            var bytes = File.ReadAllBytes("../../../UserAssets.xml");
            var preview = UserAssetsImporter.Preview(bytes);
            using (UserHolder.UserSession(AuthLogic.SystemUser!))
                UserAssetsImporter.Import(bytes, preview);
        }
    }
}
