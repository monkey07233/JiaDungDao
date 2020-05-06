using Microsoft.EntityFrameworkCore.Migrations;

namespace JiaDungDao.Migrations
{
    public partial class addm_imgUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "m_imgUrl",
                table: "Member",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "m_imgUrl",
                table: "Member");
        }
    }
}
