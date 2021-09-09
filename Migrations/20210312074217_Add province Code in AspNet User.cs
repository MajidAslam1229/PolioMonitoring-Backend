using Microsoft.EntityFrameworkCore.Migrations;

namespace PolioMonitoringSystem.Migrations
{
    public partial class AddprovinceCodeinAspNetUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProvinceCode",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProvinceCode",
                table: "AspNetUsers");
        }
    }
}
