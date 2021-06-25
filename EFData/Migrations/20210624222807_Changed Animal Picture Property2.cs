using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFData.Migrations
{
    public partial class ChangedAnimalPictureProperty2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Animals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Animals");
        }
    }
}
