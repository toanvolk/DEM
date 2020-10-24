using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEM.EF.Migrations
{
    public partial class modifyspdailyinmonth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("8299dfd3-2228-4f8c-a311-c26c213199bc"), "VK", "ADMIN", new DateTime(2020, 10, 23, 10, 6, 4, 321, DateTimeKind.Local).AddTicks(6988), "Vợ", null, null });

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("12601c62-30ee-410c-a418-7283cc0434e3"), "CK", "ADMIN", new DateTime(2020, 10, 23, 10, 6, 4, 323, DateTimeKind.Local).AddTicks(2163), "Chồng", null, null });

            var sp = @"
                       IF EXISTS (Select 1 from sysobjects where type = 'P' and category = 0 AND [name] = 'sp_DailyInMonthCurrent_Dashboard') DROP PROC sp_DailyInMonthCurrent_Dashboard
                        GO
                        CREATE PROC sp_DailyInMonthCurrent_Dashboard
                        AS
                        BEGIN
                        SET NOCOUNT ON;	                       
                        declare @now date = getdate();
                        declare @last date = dateadd(MONTH, -1, @now) ;
                        declare @tableDaily table(Daily date, TimeDisplay varchar(10))

                        while(@last <= @now)
                        begin
	                        insert into @tableDaily (Daily, TimeDisplay) values (@last,CONCAT( DAY(@last),'/',MONTH(@last)))
	                        set @last = dateadd(day,1, @last)
                        end;

                        with expense_tmp (Daily, TimeDisplay, [Money], [Type]) as
                        (
                        select m.Daily, m.TimeDisplay,  ex.[Money], ca.[Type]
                        from  @tableDaily m 
	                        left join expense ex with(nolock) on m.TimeDisplay = CONCAT(DAY(ex.PayTime),'/',MONTH(ex.PayTime))
	                        left join Category ca with(nolock) on ex.CategoryId = ca.Id

                        )

                        select Daily,TimeDisplay,[EXPENSE],[REVENUE],[SAVING]
                        from expense_tmp
                        pivot (SUM([Money]) FOR [Type] IN ([EXPENSE],[REVENUE],[SAVING])) AS PivotedExpense
                        order by Daily

                        END 
                    ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("12601c62-30ee-410c-a418-7283cc0434e3"));

            migrationBuilder.DeleteData(
                table: "Payer",
                keyColumn: "Id",
                keyValue: new Guid("8299dfd3-2228-4f8c-a311-c26c213199bc"));

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("9f04cdec-4689-4111-af79-afd87f01b036"), "VK", "ADMIN", new DateTime(2020, 10, 22, 14, 30, 57, 218, DateTimeKind.Local).AddTicks(1386), "Vợ", null, null });

            migrationBuilder.InsertData(
                table: "Payer",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("e199ee23-6a6c-44ec-97b4-bec2337cb0e2"), "CK", "ADMIN", new DateTime(2020, 10, 22, 14, 30, 57, 219, DateTimeKind.Local).AddTicks(5799), "Chồng", null, null });
        }
    }
}
