using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEM.EF.Migrations
{
    public partial class additioncolrootCategoryintointended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("53bf30a5-7def-4fcd-b970-d7e18dbebe71"));

            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("90fa8fdc-f6bb-4ab8-84af-687af96e2bc7"));

            migrationBuilder.AddColumn<string>(
                name: "RootCategory",
                table: "Intended",
                maxLength: 50,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("25a2c3bd-cef5-42d4-b321-0f446922a63d"), "VK", "ADMIN", new DateTime(2020, 10, 21, 16, 46, 20, 364, DateTimeKind.Local).AddTicks(7408), "Vợ", null, null });

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("dba02318-adab-49c7-a342-16f6bd2a04d7"), "CK", "ADMIN", new DateTime(2020, 10, 21, 16, 46, 20, 365, DateTimeKind.Local).AddTicks(8366), "Chồng", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("25a2c3bd-cef5-42d4-b321-0f446922a63d"));

            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("dba02318-adab-49c7-a342-16f6bd2a04d7"));

            migrationBuilder.DropColumn(
                name: "RootCategory",
                table: "Intended");

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("90fa8fdc-f6bb-4ab8-84af-687af96e2bc7"), "VK", "ADMIN", new DateTime(2020, 10, 20, 15, 11, 57, 753, DateTimeKind.Local).AddTicks(3033), "Vợ", null, null });

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("53bf30a5-7def-4fcd-b970-d7e18dbebe71"), "CK", "ADMIN", new DateTime(2020, 10, 20, 15, 11, 57, 754, DateTimeKind.Local).AddTicks(9608), "Chồng", null, null });
        }
    }
}
