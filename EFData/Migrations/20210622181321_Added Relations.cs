using Microsoft.EntityFrameworkCore.Migrations;

namespace EFData.Migrations
{
    public partial class AddedRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Customers_CustomerID",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Customers_CustomerID1",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Lodgings_LodgingID",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Animals_AnimalID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Animals_AnimalID",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Animals_CustomerID",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_CustomerID1",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "AdoptedBy",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "CustomerID1",
                table: "Animals");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Volunteers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalID",
                table: "Treatments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalID",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdoptedByID",
                table: "Animals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InterestedAnimal",
                columns: table => new
                {
                    AnimalID = table.Column<int>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestedAnimal", x => new { x.AnimalID, x.CustomerID });
                    table.ForeignKey(
                        name: "FK_InterestedAnimal_Animals_AnimalID",
                        column: x => x.AnimalID,
                        principalTable: "Animals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestedAnimal_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_EmailAddress",
                table: "Volunteers",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_EmailAddress",
                table: "Customers",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_AdoptedByID",
                table: "Animals",
                column: "AdoptedByID");

            migrationBuilder.CreateIndex(
                name: "IX_InterestedAnimal_CustomerID",
                table: "InterestedAnimal",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Customers_AdoptedByID",
                table: "Animals",
                column: "AdoptedByID",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Lodgings_LodgingID",
                table: "Animals",
                column: "LodgingID",
                principalTable: "Lodgings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Animals_AnimalID",
                table: "Comments",
                column: "AnimalID",
                principalTable: "Animals",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Animals_AnimalID",
                table: "Treatments",
                column: "AnimalID",
                principalTable: "Animals",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Customers_AdoptedByID",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Lodgings_LodgingID",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Animals_AnimalID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Animals_AnimalID",
                table: "Treatments");

            migrationBuilder.DropTable(
                name: "InterestedAnimal");

            migrationBuilder.DropIndex(
                name: "IX_Volunteers_EmailAddress",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_EmailAddress",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Animals_AdoptedByID",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "AdoptedByID",
                table: "Animals");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Volunteers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "AnimalID",
                table: "Treatments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "AnimalID",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "AdoptedBy",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerID1",
                table: "Animals",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Lodgings_LodgingID",
                table: "Animals",
                column: "LodgingID",
                principalTable: "Lodgings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Animals_AnimalID",
                table: "Comments",
                column: "AnimalID",
                principalTable: "Animals",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Animals_AnimalID",
                table: "Treatments",
                column: "AnimalID",
                principalTable: "Animals",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
