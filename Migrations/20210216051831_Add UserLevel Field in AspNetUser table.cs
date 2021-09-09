using Microsoft.EntityFrameworkCore.Migrations;

namespace PolioMonitoringSystem.Migrations
{
    public partial class AddUserLevelFieldinAspNetUsertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserLVL",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserLVL",
                table: "AspNetUsers");
        }
    }
}
