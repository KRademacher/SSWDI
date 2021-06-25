using Microsoft.EntityFrameworkCore.Migrations;

namespace EFData.Migrations
{
    public partial class AddedDbContextforInterestedAnimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestedAnimal_Animals_AnimalID",
                table: "InterestedAnimal");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestedAnimal_Customers_CustomerID",
                table: "InterestedAnimal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterestedAnimal",
                table: "InterestedAnimal");

            migrationBuilder.RenameTable(
                name: "InterestedAnimal",
                newName: "InterestedAnimals");

            migrationBuilder.RenameIndex(
                name: "IX_InterestedAnimal_CustomerID",
                table: "InterestedAnimals",
                newName: "IX_InterestedAnimals_CustomerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterestedAnimals",
                table: "InterestedAnimals",
                columns: new[] { "AnimalID", "CustomerID" });

            migrationBuilder.AddForeignKey(
                name: "FK_InterestedAnimals_Animals_AnimalID",
                table: "InterestedAnimals",
                column: "AnimalID",
                principalTable: "Animals",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestedAnimals_Customers_CustomerID",
                table: "InterestedAnimals",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestedAnimals_Animals_AnimalID",
                table: "InterestedAnimals");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestedAnimals_Customers_CustomerID",
                table: "InterestedAnimals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterestedAnimals",
                table: "InterestedAnimals");

            migrationBuilder.RenameTable(
                name: "InterestedAnimals",
                newName: "InterestedAnimal");

            migrationBuilder.RenameIndex(
                name: "IX_InterestedAnimals_CustomerID",
                table: "InterestedAnimal",
                newName: "IX_InterestedAnimal_CustomerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterestedAnimal",
                table: "InterestedAnimal",
                columns: new[] { "AnimalID", "CustomerID" });

            migrationBuilder.AddForeignKey(
                name: "FK_InterestedAnimal_Animals_AnimalID",
                table: "InterestedAnimal",
                column: "AnimalID",
                principalTable: "Animals",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestedAnimal_Customers_CustomerID",
                table: "InterestedAnimal",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
