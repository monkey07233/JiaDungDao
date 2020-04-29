using Microsoft.EntityFrameworkCore.Migrations;

namespace JiaDungDao.Migrations
{
    public partial class Menu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "m_imgUrl",
                table: "Menu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "m_imgUrl",
                table: "Menu");
        }
    }
}
