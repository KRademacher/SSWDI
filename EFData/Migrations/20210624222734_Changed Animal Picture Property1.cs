using Microsoft.EntityFrameworkCore.Migrations;

namespace EFData.Migrations
{
    public partial class ChangedAnimalPictureProperty1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Animals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
