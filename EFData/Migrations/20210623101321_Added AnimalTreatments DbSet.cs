using Microsoft.EntityFrameworkCore.Migrations;

namespace EFData.Migrations
{
    public partial class AddedAnimalTreatmentsDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalTreatment_Animals_AnimalID",
                table: "AnimalTreatment");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalTreatment_Treatments_TreatmentID",
                table: "AnimalTreatment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalTreatment",
                table: "AnimalTreatment");

            migrationBuilder.RenameTable(
                name: "AnimalTreatment",
                newName: "AnimalTreatments");

            migrationBuilder.RenameIndex(
                name: "IX_AnimalTreatment_TreatmentID",
                table: "AnimalTreatments",
                newName: "IX_AnimalTreatments_TreatmentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalTreatments",
                table: "AnimalTreatments",
                columns: new[] { "AnimalID", "TreatmentID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalTreatments_Animals_AnimalID",
                table: "AnimalTreatments",
                column: "AnimalID",
                principalTable: "Animals",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalTreatments_Treatments_TreatmentID",
                table: "AnimalTreatments",
                column: "TreatmentID",
                principalTable: "Treatments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalTreatments_Animals_AnimalID",
                table: "AnimalTreatments");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalTreatments_Treatments_TreatmentID",
                table: "AnimalTreatments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalTreatments",
                table: "AnimalTreatments");

            migrationBuilder.RenameTable(
                name: "AnimalTreatments",
                newName: "AnimalTreatment");

            migrationBuilder.RenameIndex(
                name: "IX_AnimalTreatments_TreatmentID",
                table: "AnimalTreatment",
                newName: "IX_AnimalTreatment_TreatmentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalTreatment",
                table: "AnimalTreatment",
                columns: new[] { "AnimalID", "TreatmentID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalTreatment_Animals_AnimalID",
                table: "AnimalTreatment",
                column: "AnimalID",
                principalTable: "Animals",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalTreatment_Treatments_TreatmentID",
                table: "AnimalTreatment",
                column: "TreatmentID",
                principalTable: "Treatments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
