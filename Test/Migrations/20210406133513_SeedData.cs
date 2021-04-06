using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

SET IDENTITY_INSERT [dbo].[Payment] ON 

INSERT INTO [dbo].[Payment] ([TransactionId], [Amonnt], [CurrencyCode], [TransactionDate], [Status]) VALUES (N'Inv00001', CAST(200.00 AS Decimal(18, 2)), N'USD', N'2019-01-23 13:45:10', N'Done')
INSERT INTO [dbo].[Payment] ([TransactionId], [Amonnt], [CurrencyCode], [TransactionDate], [Status]) VALUES (N'Inv00002', CAST(10000.00 AS Decimal(18, 2)), N'EUR', N'2019-01-24 16:09:15', N'Rejected')
INSERT INTO [dbo].[Payment] ([TransactionId], [Amonnt], [CurrencyCode], [TransactionDate], [Status]) VALUES (N'Invoice0000001', CAST(1000.00 AS Decimal(18, 2)), N'USD', N'2019-02-20 12:33:16', N'Approved')
INSERT INTO [dbo].[Payment] ([TransactionId], [Amonnt], [CurrencyCode], [TransactionDate], [Status]) VALUES (N'Invoice0000002', CAST(300.00 AS Decimal(18, 2)), N'USD', N'2019-02-21 02:04:59', N'Failed')

SET IDENTITY_INSERT [dbo].[Payment] OFF

");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
