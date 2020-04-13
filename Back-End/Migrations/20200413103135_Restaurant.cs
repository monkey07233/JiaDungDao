using Microsoft.EntityFrameworkCore.Migrations;

namespace JiaDungDao.Migrations
{
    public partial class Restaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Restaurant");

            migrationBuilder.AddColumn<string>(
                name: "r_address",
                table: "Restaurant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "r_name",
                table: "Restaurant",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "r_tel",
                table: "Restaurant",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "r_address",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "r_name",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "r_tel",
                table: "Restaurant");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Restaurant",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
