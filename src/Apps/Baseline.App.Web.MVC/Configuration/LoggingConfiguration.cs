using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Exceptions.SqlServer.Destructurers;

namespace Baseline.App.Web.MVC.Configuration
{
    public static class LoggingConfiguration
    {
        public static void Configure(string connectionStringName)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = InitJsonConfig(environment);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("ApplicationName", config.GetValue<string>("Serilog:ApplicationName"))
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers(new [] {new SqlExceptionDestructurer()})
                    .WithDestructurers(new [] {new DbUpdateExceptionDestructurer()})
                )
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
            columnOptions.AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn
                {
                    ColumnName = "ApplicationName"
                },
                new SqlColumn
                {
                    ColumnName = "MachineName"
                }
            };

            config.WriteTo.MSSqlServer(
                connectionString: connectionStringName,
                tableName: "LogEntries",
                columnOptions: columnOptions,
                appConfiguration: configuration);

            return config;
        }
    }
}