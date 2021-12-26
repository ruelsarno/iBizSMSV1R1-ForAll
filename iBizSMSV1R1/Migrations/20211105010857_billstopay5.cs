using Microsoft.EntityFrameworkCore.Migrations;

namespace iBizSMSV1R1.Migrations
{
    public partial class billstopay5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "paymentoffice",
                table: "BillToPay",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "paymenttype",
                table: "BillToPay",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "paymentoffice",
                table: "BillToPay");

            migrationBuilder.DropColumn(
                name: "paymenttype",
                table: "BillToPay");
        }
    }
}
