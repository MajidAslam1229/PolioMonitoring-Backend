using Microsoft.EntityFrameworkCore.Migrations;

namespace PolioMonitoringSystem.Migrations
{
    public partial class RecordStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RecordStatus",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "AspNetUsers");
        }
    }
}
