using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEM.EF.Migrations
{
    public partial class modifystructexpenseaddcolcategoryid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Expense",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Expense");
        }
    }
}
