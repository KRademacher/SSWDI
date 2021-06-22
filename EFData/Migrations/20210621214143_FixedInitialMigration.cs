using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFData.Migrations
{
    public partial class FixedInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lodgings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    LodgingType = table.Column<int>(nullable: false),
                    AnimalType = table.Column<int>(nullable: false),
                    MaxCapacity = table.Column<int>(nullable: false),
                    CurrentCapacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lodgings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    EstimatedAge = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    AnimalType = table.Column<int>(nullable: false),
                    Breed = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Picture = table.Column<string>(nullable: true),
                    DateOfArrival = table.Column<DateTime>(nullable: false),
                    DateOfAdoption = table.Column<DateTime>(nullable: false),
                    DateOfPassing = table.Column<DateTime>(nullable: false),
                    IsNeutered = table.Column<bool>(nullable: false),
                    IsChildFriendly = table.Column<int>(nullable: false),
                    LeavingReason = table.Column<string>(nullable: false),
                    Adoptable = table.Column<bool>(nullable: false),
                    AdoptedBy = table.Column<string>(nullable: true),
                    LodgingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Animals_Lodgings_LodgingID",
                        column: x => x.LodgingID,
                        principalTable: "Lodgings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Author = table.Column<string>(nullable: false),
                    AnimalID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comments_Animals_AnimalID",
                        column: x => x.AnimalID,
                        principalTable: "Animals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreatmentType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Cost = table.Column<double>(nullable: false),
                    MinimumAge = table.Column<int>(nullable: false),
                    PerformedBy = table.Column<string>(nullable: false),
                    PerformDate = table.Column<DateTime>(nullable: false),
                    AnimalID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Treatments_Animals_AnimalID",
                        column: x => x.AnimalID,
                        principalTable: "Animals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_LodgingID",
                table: "Animals",
                column: "LodgingID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AnimalID",
                table: "Comments",
                column: "AnimalID");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_AnimalID",
                table: "Treatments",
                column: "AnimalID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Lodgings");
        }
    }
}
