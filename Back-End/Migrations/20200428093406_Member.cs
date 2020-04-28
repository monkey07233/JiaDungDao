using Microsoft.EntityFrameworkCore.Migrations;

namespace JiaDungDao.Migrations
{
    public partial class Member : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isValid",
                table: "Member",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "isValid",
                table: "Member",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
