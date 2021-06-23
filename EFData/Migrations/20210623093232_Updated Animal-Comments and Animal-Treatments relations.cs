using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFData.Migrations
{
    public partial class UpdatedAnimalCommentsandAnimalTreatmentsrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Animals_AnimalID",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_AnimalID",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "AnimalID",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "PerformDate",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "PerformedBy",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "InterestedAnimal");

            migrationBuilder.CreateTable(
                name: "AnimalTreatment",
                columns: table => new
                {
                    AnimalID = table.Column<int>(nullable: false),
                    TreatmentID = table.Column<int>(nullable: false),
                    PerformedBy = table.Column<string>(nullable: false),
                    PerformDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalTreatment", x => new { x.AnimalID, x.TreatmentID });
                    table.ForeignKey(
                        name: "FK_AnimalTreatment_Animals_AnimalID",
                        column: x => x.AnimalID,
                        principalTable: "Animals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalTreatment_Treatments_TreatmentID",
                        column: x => x.TreatmentID,
                        principalTable: "Treatments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalTreatment_TreatmentID",
                table: "AnimalTreatment",
                column: "TreatmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalTreatment");

            migrationBuilder.AddColumn<int>(
                name: "AnimalID",
                table: "Treatments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PerformDate",
                table: "Treatments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PerformedBy",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "InterestedAnimal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_AnimalID",
                table: "Treatments",
                column: "AnimalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Animals_AnimalID",
                table: "Treatments",
                column: "AnimalID",
                principalTable: "Animals",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
