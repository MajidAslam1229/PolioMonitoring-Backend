using Microsoft.EntityFrameworkCore.Migrations;

namespace PolioMonitoringSystem.Migrations
{
    public partial class AddFormIdinAspNetRoleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormId",
                table: "AspNetRoles");
        }
    }
}
