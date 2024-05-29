using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FM_Api.Migrations
{
    /// <inheritdoc />
    public partial class MyMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trajectorie_Taxi_TaxiId",
                table: "Trajectorie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trajectorie",
                table: "Trajectorie");

            migrationBuilder.RenameTable(
                name: "Trajectorie",
                newName: "Trajectory");

            migrationBuilder.RenameIndex(
                name: "IX_Trajectorie_TaxiId",
                table: "Trajectory",
                newName: "IX_Trajectory_TaxiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trajectory",
                table: "Trajectory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trajectory_Taxi_TaxiId",
                table: "Trajectory",
                column: "TaxiId",
                principalTable: "Taxi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trajectory_Taxi_TaxiId",
                table: "Trajectory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trajectory",
                table: "Trajectory");

            migrationBuilder.RenameTable(
                name: "Trajectory",
                newName: "Trajectorie");

            migrationBuilder.RenameIndex(
                name: "IX_Trajectory_TaxiId",
                table: "Trajectorie",
                newName: "IX_Trajectorie_TaxiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trajectorie",
                table: "Trajectorie",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trajectorie_Taxi_TaxiId",
                table: "Trajectorie",
                column: "TaxiId",
                principalTable: "Taxi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
