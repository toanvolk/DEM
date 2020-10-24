using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEM.EF.Migrations
{
    public partial class modifyspexpenserealandintendedtime1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("729467aa-dfed-4d31-bf08-40f077e23178"));

            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("e75294aa-d9fe-4d0d-b40d-3918dc75c2ab"));

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("e472a16a-0542-4a62-aca5-a25733661f24"), "VK", "ADMIN", new DateTime(2020, 10, 23, 17, 49, 52, 94, DateTimeKind.Local).AddTicks(3148), "Vợ", null, null });

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("90c5345b-a16c-49b7-a2e5-a781d5c82e81"), "CK", "ADMIN", new DateTime(2020, 10, 23, 17, 49, 52, 95, DateTimeKind.Local).AddTicks(2956), "Chồng", null, null });

            var sp = @"IF EXISTS (Select 1 from sysobjects where type = 'P' and category = 0 AND [name] = 'sp_ExpenseRealAndIntended') DROP PROC sp_ExpenseRealAndIntended
                    GO
                    CREATE PROC sp_ExpenseRealAndIntended
                    AS
                    BEGIN
                    SET NOCOUNT ON;	         
                    declare @RootCategory varchar(10) = 'EXPENSE'
                    declare @IntendedId varchar(50)

                    select top 1 @IntendedId = Id from Intended order by CreatedDate desc

                    select m.[Description], ca.[Name], d.[Money] as MoneyIntended, SUM(ex.[Money]) as MoneyExpense
                    from Intended m with(nolock)
	                    inner join IntendedDetail d with(nolock) on m.Id = d.IntendedId 
	                    inner join Category ca with(nolock) on d.CategoryId = ca.Id
	                    left join Expense ex with(nolock) on ca.Id = ex.CategoryId and ex.PayTime between m.FromDate and m.ToDate
                    where m.RootCategory = @RootCategory 
		                    and m.Id = @IntendedId		
                    group by m.[Description], ca.[Name], d.[Money]

                    END
                    ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("90c5345b-a16c-49b7-a2e5-a781d5c82e81"));

            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("e472a16a-0542-4a62-aca5-a25733661f24"));

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("e75294aa-d9fe-4d0d-b40d-3918dc75c2ab"), "VK", "ADMIN", new DateTime(2020, 10, 23, 17, 16, 35, 975, DateTimeKind.Local).AddTicks(3763), "Vợ", null, null });

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("729467aa-dfed-4d31-bf08-40f077e23178"), "CK", "ADMIN", new DateTime(2020, 10, 23, 17, 16, 35, 976, DateTimeKind.Local).AddTicks(4573), "Chồng", null, null });
        }
    }
}
