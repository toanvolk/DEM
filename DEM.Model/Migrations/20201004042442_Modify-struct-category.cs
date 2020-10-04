using Microsoft.EntityFrameworkCore.Migrations;

namespace DEM.EF.Migrations
{
    public partial class Modifystructcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "NotUse",
                table: "Category",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Category",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Category");

            migrationBuilder.AlterColumn<bool>(
                name: "NotUse",
                table: "Category",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
