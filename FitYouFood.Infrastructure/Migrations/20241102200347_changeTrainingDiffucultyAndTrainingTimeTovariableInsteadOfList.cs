using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitYouFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeTrainingDiffucultyAndTrainingTimeTovariableInsteadOfList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a95d201-dca8-49e3-a4c4-668b6bdbcd28");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca6c9936-3424-42da-ac2f-3d8489abd16e");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WhenTrained",
                table: "Trainings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Difficulty",
                table: "Trainings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5076d909-23a6-42bc-b2b4-c0682861d28f", null, "Admin", "ADMIN" },
                    { "cf14a341-59bf-41fa-b930-28597a2d5971", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5076d909-23a6-42bc-b2b4-c0682861d28f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf14a341-59bf-41fa-b930-28597a2d5971");

            migrationBuilder.AlterColumn<string>(
                name: "WhenTrained",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Difficulty",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6a95d201-dca8-49e3-a4c4-668b6bdbcd28", null, "Admin", "ADMIN" },
                    { "ca6c9936-3424-42da-ac2f-3d8489abd16e", null, "User", "USER" }
                });
        }
    }
}
