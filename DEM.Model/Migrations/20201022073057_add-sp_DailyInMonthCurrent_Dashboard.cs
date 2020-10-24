using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEM.EF.Migrations
{
    public partial class addsp_DailyInMonthCurrent_Dashboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("25a2c3bd-cef5-42d4-b321-0f446922a63d"));

            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("dba02318-adab-49c7-a342-16f6bd2a04d7"));

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("9f04cdec-4689-4111-af79-afd87f01b036"), "VK", "ADMIN", new DateTime(2020, 10, 22, 14, 30, 57, 218, DateTimeKind.Local).AddTicks(1386), "Vợ", null, null });

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("e199ee23-6a6c-44ec-97b4-bec2337cb0e2"), "CK", "ADMIN", new DateTime(2020, 10, 22, 14, 30, 57, 219, DateTimeKind.Local).AddTicks(5799), "Chồng", null, null });

            var sp = @"
                        IF EXISTS (Select 1 from sysobjects where type = 'P' and category = 0 AND [name] = 'sp_DailyInMonthCurrent_Dashboard') DROP PROC sp_DailyInMonthCurrent_Dashboard
                        GO
                        CREATE PROC sp_DailyInMonthCurrent_Dashboard
                        AS
                        BEGIN
	                        SET NOCOUNT ON;
	                        with expense_tmp (TimeDisplay, [Money]) as
	                        (
	                        select  CONCAT( DAY(PayTime),'/',MONTH(PayTime)) as  TimeDisplay, Money 
	                        from expense
	                        )

	                        select TimeDisplay as Daily, SUM([Money]) [Money]
	                        from expense_tmp
	                        group by TimeDisplay
                        END
                        ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("9f04cdec-4689-4111-af79-afd87f01b036"));

            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("e199ee23-6a6c-44ec-97b4-bec2337cb0e2"));

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("25a2c3bd-cef5-42d4-b321-0f446922a63d"), "VK", "ADMIN", new DateTime(2020, 10, 21, 16, 46, 20, 364, DateTimeKind.Local).AddTicks(7408), "Vợ", null, null });

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("dba02318-adab-49c7-a342-16f6bd2a04d7"), "CK", "ADMIN", new DateTime(2020, 10, 21, 16, 46, 20, 365, DateTimeKind.Local).AddTicks(8366), "Chồng", null, null });
        }
    }
}
