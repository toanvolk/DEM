using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEM.EF.Migrations
{
    public partial class addbusinessintended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("6bf79644-5fda-4833-a5e2-9230be9ff17d"));

            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("a8f4a5bf-5adb-4ae7-936c-511c4ebc4ee1"));

            migrationBuilder.CreateTable(
                name: "Intended",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 20, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 20, nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intended", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntendedDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IntendedId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    Money = table.Column<decimal>(type: "decimal(12, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntendedDetail", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("90fa8fdc-f6bb-4ab8-84af-687af96e2bc7"), "VK", "ADMIN", new DateTime(2020, 10, 20, 15, 11, 57, 753, DateTimeKind.Local).AddTicks(3033), "Vợ", null, null });

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("53bf30a5-7def-4fcd-b970-d7e18dbebe71"), "CK", "ADMIN", new DateTime(2020, 10, 20, 15, 11, 57, 754, DateTimeKind.Local).AddTicks(9608), "Chồng", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intended");

            migrationBuilder.DropTable(
                name: "IntendedDetail");

            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("53bf30a5-7def-4fcd-b970-d7e18dbebe71"));

            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("90fa8fdc-f6bb-4ab8-84af-687af96e2bc7"));

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("a8f4a5bf-5adb-4ae7-936c-511c4ebc4ee1"), "VK", "ADMIN", new DateTime(2020, 10, 15, 14, 23, 32, 307, DateTimeKind.Local).AddTicks(4552), "Vợ", null, null });

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("6bf79644-5fda-4833-a5e2-9230be9ff17d"), "CK", "ADMIN", new DateTime(2020, 10, 15, 14, 23, 32, 308, DateTimeKind.Local).AddTicks(5411), "Chồng", null, null });
        }
    }
}
