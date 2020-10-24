using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEM.EF.Migrations
{
    public partial class addsp_ExpenseStatistical : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("469aae12-7fa6-4a7c-89eb-56c81b9db369"), "VK", "ADMIN", new DateTime(2020, 10, 24, 11, 17, 3, 454, DateTimeKind.Local).AddTicks(3160), "Vợ", null, null });

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("c549d6af-68da-4235-93da-3c878f7348fd"), "CK", "ADMIN", new DateTime(2020, 10, 24, 11, 17, 3, 455, DateTimeKind.Local).AddTicks(911), "Chồng", null, null });

            var sp = @"IF EXISTS (Select 1 from sysobjects where type = 'P' and category = 0 AND [name] = 'sp_ExpenseStatistical') DROP PROC sp_ExpenseStatistical
GO
CREATE PROC sp_ExpenseStatistical
@formDate date,
@toDate date
AS
BEGIN
SET NOCOUNT ON;	         
declare @RootCategory varchar(10) = 'EXPENSE';
declare @VK_Code varchar(10) = 'VK';
declare @CK_Code varchar(10) = 'CK';

select ca.[Name], sum(ex.[Money]) [Money] into #expenseTotal
from Expense ex with(nolock)
	inner join Category ca with(nolock) on ex.CategoryId = ca.Id
where ca.[Type] = @RootCategory 
	and ex.PayTime between @formDate and @toDate
group by ca.[Name]

select expenseMax.[Name] ExpenseMaxName, expenseMax.[Money] ExpenseMaxMoney, expenseTotal.Money_Total, expense_VK.[Money_VK], expense_CK.Money_CK
from (
--Khoản chi tiêu nhiều nhất
	(select top 1 [Name], [Money] from #expenseTotal order by [Money] desc) as expenseMax
	full join
	--Tổng chi phí
	(select  SUM([Money]) as [Money_Total]  from #expenseTotal) as expenseTotal on 1=1
	full join
	--Vợ chi
	(select sum(ex.[Money]) [Money_VK]
	from Expense ex with(nolock)
		inner join Category ca with(nolock) on ex.CategoryId = ca.Id
		inner join Payer pa with(nolock) on ex.Payer = pa.Code and ex.Payer = @VK_Code
	where ca.[Type] = @RootCategory 
		and ex.PayTime between @formDate and @toDate
		)  as expense_VK on 1=1
	full join	
	--Chồng chi
	(select sum(ex.[Money]) [Money_CK]
	from Expense ex with(nolock)
		inner join Category ca with(nolock) on ex.CategoryId = ca.Id
		inner join Payer pa with(nolock) on ex.Payer = pa.Code and ex.Payer = @CK_Code
	where ca.[Type] = @RootCategory 
		and ex.PayTime between @formDate and @toDate
		) as expense_CK on 1=1
)
drop table #expenseTotal
END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("469aae12-7fa6-4a7c-89eb-56c81b9db369"));

            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("c549d6af-68da-4235-93da-3c878f7348fd"));

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("e472a16a-0542-4a62-aca5-a25733661f24"), "VK", "ADMIN", new DateTime(2020, 10, 23, 17, 49, 52, 94, DateTimeKind.Local).AddTicks(3148), "Vợ", null, null });

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("90c5345b-a16c-49b7-a2e5-a781d5c82e81"), "CK", "ADMIN", new DateTime(2020, 10, 23, 17, 49, 52, 95, DateTimeKind.Local).AddTicks(2956), "Chồng", null, null });
        }
    }
}
