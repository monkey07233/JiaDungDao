using Microsoft.EntityFrameworkCore.Migrations;

namespace JiaDungDao.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "Order",
                newName: "OrderId");

            migrationBuilder.AddColumn<string>(
                name: "r_name",
                table: "OrderTitle",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "r_name",
                table: "OrderTitle");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Order",
                newName: "OrderID");
        }
    }
}
