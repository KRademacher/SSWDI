using Microsoft.EntityFrameworkCore.Migrations;

namespace EFData.Migrations
{
    public partial class UpdatedAnimalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Animals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Animals");
        }
    }
}
