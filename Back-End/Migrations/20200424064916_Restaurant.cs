using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JiaDungDao.Migrations
{
    public partial class Restaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    m_name = table.Column<string>(maxLength: 50, nullable: false),
                    m_email = table.Column<string>(maxLength: 250, nullable: false),
                    m_account = table.Column<string>(maxLength: 50, nullable: false),
                    m_password = table.Column<string>(nullable: false),
                    m_birthday = table.Column<DateTime>(nullable: false),
                    m_address = table.Column<string>(nullable: false),
                    m_role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantID = table.Column<int>(nullable: false),
                    m_item = table.Column<string>(nullable: true),
                    m_type = table.Column<string>(nullable: true),
                    m_price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuID);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    RestaurantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    r_name = table.Column<string>(nullable: false),
                    r_address = table.Column<string>(nullable: true),
                    r_tel = table.Column<string>(maxLength: 10, nullable: true),
                    m_account = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.RestaurantID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Restaurant");
        }
    }
}
