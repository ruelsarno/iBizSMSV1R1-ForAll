using Microsoft.EntityFrameworkCore.Migrations;

namespace iBizSMSV1R1.Migrations
{
    public partial class billstopay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "paymentupdate",
                table: "BillToPay");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "paymentupdate",
                table: "BillToPay",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
