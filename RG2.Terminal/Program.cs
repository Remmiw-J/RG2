﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Signum.Engine;
using Signum.Engine.Maps;
using Signum.Utilities;
using RG2.Logic;
using RG2.Entities;
using Signum.Services;
using Signum.Entities;
using Signum.Engine.Authorization;
using Signum.Entities.Reflection;
using Signum.Engine.Chart;
using Signum.Engine.Operations;
using Signum.Entities.Translation;
using System.Globalization;
using Signum.Entities.Mailing;
using Signum.Entities.Files;
using Signum.Entities.SMS;
using Signum.Entities.Basics;
using Signum.Engine.Translation;
using Signum.Engine.Help;
using Signum.Entities.Word;
using Signum.Engine.Basics;
using Signum.Engine.Migrations;
using Signum.Entities.Authorization;
using Signum.Engine.CodeGeneration;
using Signum.Entities.MachineLearning;
using Signum.Engine.MachineLearning;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using Signum.Engine.Cache;
using Signum.Engine.SchemaInfoTables;

namespace RG2.Terminal
{
    class Program
    {
        public static IConfigurationRoot ConfigRoot = null!;

        static int Main(string[] args)
        {
            try
            {
                using (AuthLogic.Disable())
                using (CultureInfoUtils.ChangeCulture("en"))
                using (CultureInfoUtils.ChangeCultureUI("en"))
                {
                    ConfigRoot = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                        .AddUserSecrets<Program>().Build();
                    
                    Starter.Start(ConfigRoot.GetConnectionString("ConnectionString"), ConfigRoot.GetValue<bool>("IsPostgres"));

                    Console.WriteLine("..:: Welcome to RG2 Loading Application ::..");
                    Console.WriteLine($"{Connector.Current.GetType().Name} (Database: {Connector.Current.DatabaseName()})");
                    Console.WriteLine();

                    if (args.Any())
                    {
                        switch (args.First().ToLower().Trim('-', '/'))
                        {
                            case "sql": SqlMigrationRunner.SqlMigrations(true); return 0;
                            case "csharp": RG2Migrations.CSharpMigrations(true); return 0;
                            case "load": Load(args.Skip(1).ToArray()); return 0;
                            default:
                            {
                                SafeConsole.WriteLineColor(ConsoleColor.Red, "Unkwnown command " + args.First());
                                Console.WriteLine("Examples:");
                                Console.WriteLine("   sql: SQL Migrations");
                                Console.WriteLine("   csharp: C# Migrations");
                                Console.WriteLine("   load 1-4,7: Load processes 1 to 4 and 7");
                                return -1;
                            }
                        }
                    } //if(args.Any())

                    while (true)
                    {
                        Action? action = new ConsoleSwitch<string, Action>
                        {
                            {"N", NewDatabase},
                            {"G", CodeGenerator.GenerateCodeConsole },
                            {"SQL", SqlMigrationRunner.SqlMigrations},
                            {"CS", () => RG2Migrations.CSharpMigrations(false), "C# Migrations"},
                            {"S", Synchronize},
                            {"L", () => Load(null), "Load"},
                        }.Choose();

                        if (action == null)
                            return 0;

                        action();
                    }
                }
            }
            catch (Exception e)
            {
                SafeConsole.WriteColor(ConsoleColor.DarkRed, e.GetType().Name + ": ");
                SafeConsole.WriteLineColor(ConsoleColor.Red, e.Message);
                SafeConsole.WriteLineColor(ConsoleColor.DarkRed, e.StackTrace!.Indent(4));
                return -1;
            }
        }

        private static void Load(string[]? args)
        {
            Schema.Current.Initialize();

            OperationLogic.AllowSaveGlobally = true;

            while (true)
            {
                var actions = new ConsoleSwitch<string, Action>
                {
                    {"AR", AuthLogic.ImportExportAuthRules},
                    {"HL", HelpXml.ImportExportHelp},
                    {"CT", TranslationLogic.CopyTranslations},
                    {"SO", ShowOrder},
                }.ChooseMultipleWithDescription(args);

                if (actions == null)
                    return;

                foreach (var acc in actions)
                {
                    MigrationLogic.ExecuteLoadProcess(acc.Value, acc.Description);
                }
            }
        }

        public static void NewDatabase()
        {
            var databaseName = Connector.Current.DatabaseName();
            if (Database.View<SysTables>().Any())
            {
                SafeConsole.WriteLineColor(ConsoleColor.Red, $"Are you sure you want to delete all the data in the database '{databaseName}'?");
                Console.Write($"Confirm by writing the name of the database:");
                string val = Console.ReadLine()!;
                if (val.ToLower() != databaseName.ToLower())
                {
                    Console.WriteLine($"Wrong name. No changes where made");
                    Console.WriteLine();
                    return;
                }
            }

            Console.Write("Creating new database...");
            Administrator.TotalGeneration();
            Console.WriteLine("Done.");
        }

        static void Synchronize()
        {
            Console.WriteLine("Check and Modify the synchronization script before");
            Console.WriteLine("executing it in SQL Server Management Studio: ");
            Console.WriteLine();

            SqlPreCommand? command = Administrator.TotalSynchronizeScript();
            if (command == null)
            {
                SafeConsole.WriteLineColor(ConsoleColor.Green, "Already synchronized!");
                return;
            }

            command.OpenSqlFileRetry();
        }

        static void ShowOrder()
        {
            var query = Database.Query<OrderEntity>()
              .Where(a => a.Details.Any(l => l.Discount != 0))
              .OrderByDescending(a => a.TotalPrice);

            OrderEntity order = query.First();
        }//ShowOrder
    }
}
