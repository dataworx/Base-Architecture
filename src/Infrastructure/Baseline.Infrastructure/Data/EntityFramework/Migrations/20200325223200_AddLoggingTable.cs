using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Baseline.Infrastructure.Data.EntityFramework.Migrations
{
    public partial class AddLoggingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: false),
                    MessageTemplate = table.Column<string>(maxLength: 500, nullable: false),
                    Level = table.Column<string>(maxLength: 50, nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    ApplicationName = table.Column<string>(maxLength: 100, nullable: false),
                    MachineName = table.Column<string>(maxLength: 100, nullable: false),
                    Exception = table.Column<string>(nullable: true),
                    LogEvent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEntries");
        }
    }
}
