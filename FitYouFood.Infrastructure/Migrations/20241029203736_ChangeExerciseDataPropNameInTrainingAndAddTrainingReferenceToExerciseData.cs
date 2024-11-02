using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitYouFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeExerciseDataPropNameInTrainingAndAddTrainingReferenceToExerciseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f478c77-aaae-46a5-98e7-a46ec68f3d99");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd44f95b-02f6-410a-bcc5-eb48bbecbc26");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6a95d201-dca8-49e3-a4c4-668b6bdbcd28", null, "Admin", "ADMIN" },
                    { "ca6c9936-3424-42da-ac2f-3d8489abd16e", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a95d201-dca8-49e3-a4c4-668b6bdbcd28");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca6c9936-3424-42da-ac2f-3d8489abd16e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f478c77-aaae-46a5-98e7-a46ec68f3d99", null, "User", "USER" },
                    { "cd44f95b-02f6-410a-bcc5-eb48bbecbc26", null, "Admin", "ADMIN" }
                });
        }
    }
}
