using Microsoft.EntityFrameworkCore.Migrations;

namespace JiaDungDao.Migrations
{
    public partial class Member : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "isValid",
                table: "Member",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isValid",
                table: "Member");
        }
    }
}
