using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Signum.Engine;
using Signum.Engine.Authorization;
using Signum.Engine.Maps;
using Signum.Engine.Operations;
using Signum.Utilities;
using RG2.Logic;
using Xunit;

namespace RG2.Test.Environment
{
    public class EnvironmentTest
    {
        [Fact]
        public void GenerateTestEnvironment()
        {
            var authRules = XDocument.Load(@"..\..\..\RG2.Terminal\AuthRules.xml");

            RG2Environment.Start(includeDynamic: false);

            Administrator.TotalGeneration();

            Schema.Current.Initialize();

            OperationLogic.AllowSaveGlobally = true;

            using (AuthLogic.Disable())
            {
                RG2Environment.LoadBasics();

                AuthLogic.LoadRoles(authRules);
                RG2Environment.LoadEmployees();
                RG2Environment.LoadUsers();
                RG2Environment.LoadProducts();
                RG2Environment.LoadCustomers();
                RG2Environment.LoadShippers();

                AuthLogic.ImportRulesScript(authRules, interactive: false)!.PlainSqlCommand().ExecuteLeaves();
            }

            OperationLogic.AllowSaveGlobally = false;
        }
    }
}
