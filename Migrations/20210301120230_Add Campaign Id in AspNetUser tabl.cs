using Microsoft.EntityFrameworkCore.Migrations;

namespace PolioMonitoringSystem.Migrations
{
    public partial class AddCampaignIdinAspNetUsertabl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "AspNetUsers");
        }
    }
}
