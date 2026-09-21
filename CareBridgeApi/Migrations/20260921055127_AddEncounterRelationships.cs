using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareBridgeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Encounters_PatientId",
                table: "Encounters",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_ProviderId",
                table: "Encounters",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encounters_Patients_PatientId",
                table: "Encounters",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Encounters_Providers_ProviderId",
                table: "Encounters",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encounters_Patients_PatientId",
                table: "Encounters");

            migrationBuilder.DropForeignKey(
                name: "FK_Encounters_Providers_ProviderId",
                table: "Encounters");

            migrationBuilder.DropIndex(
                name: "IX_Encounters_PatientId",
                table: "Encounters");

            migrationBuilder.DropIndex(
                name: "IX_Encounters_ProviderId",
                table: "Encounters");
        }
    }
}
