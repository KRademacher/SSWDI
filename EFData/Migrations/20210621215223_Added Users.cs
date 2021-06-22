using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFData.Migrations
{
    public partial class AddedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Animals",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerID1",
                table: "Animals",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    UserRole = table.Column<int>(nullable: false),
                    RegistrationNumber = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<int>(nullable: false),
                    HouseNumberAddition = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    UserRole = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_CustomerID",
                table: "Animals",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_CustomerID1",
                table: "Animals",
                column: "CustomerID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Customers_CustomerID",
                table: "Animals",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Customers_CustomerID1",
                table: "Animals",
                column: "CustomerID1",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Customers_CustomerID",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Customers_CustomerID1",
                table: "Animals");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Animals_CustomerID",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_CustomerID1",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "CustomerID1",
                table: "Animals");
        }
    }
}
