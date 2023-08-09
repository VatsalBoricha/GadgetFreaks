using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadgetFreaks.Data.Migrations
{
    public partial class InitializeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Gadgets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Gadgets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
