using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyGuide.Persistence.Migrations
{
    public partial class GameAddRules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rules",
                table: "Games",
                type: "TEXT",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rules",
                table: "Games");
        }
    }
}
