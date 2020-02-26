using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.IO;

namespace Baseline.App.Web.MVC.Configuration
{
    public static class LoggingConfiguration
    {
        public static void Configure(string connectionStringName)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = InitJsonConfig(environment);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .AddSqlServerLogging(connectionStringName, config)
                .CreateLogger();
        }

        private static IConfiguration InitJsonConfig(string environment)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();
        }

        private static LoggerConfiguration AddSqlServerLogging(this LoggerConfiguration config,
            string connectionStringName, IConfiguration configuration)
        {
            var columnOptions = new ColumnOptions();
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.Store.Add(StandardColumn.LogEvent);

            config.WriteTo.MSSqlServer(
                connectionString: connectionStringName,
                tableName: "LogEntries",
                columnOptions: columnOptions,
                appConfiguration: configuration,
                autoCreateSqlTable: true);

            return config;
        }
    }
}