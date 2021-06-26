using Microsoft.EntityFrameworkCore.Migrations;

namespace EFData.Migrations
{
    public partial class Addedcustomeranimalrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdopteeName",
                table: "Animals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdopteeName",
                table: "Animals");
        }
    }
}
