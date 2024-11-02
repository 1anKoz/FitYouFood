using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitYouFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class correctExerciseDataModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "994f3cee-4b9d-4c8c-b361-61c877e2bb9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7d02394-90ff-48c4-8f44-5239e6a2ec64");

            migrationBuilder.DropColumn(
                name: "WorkoutTime",
                table: "ExerciseDatas");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WhenExercised",
                table: "ExerciseDatas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "ExerciseDatas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HowMuchMoreRepsAbleToDo",
                table: "ExerciseDatas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ExerciseDatas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "126b1e1d-379c-41dc-a8c0-0b4f32548158", null, "User", "USER" },
                    { "5a1d0f2c-7544-44a4-83c4-fc0f447b155f", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "126b1e1d-379c-41dc-a8c0-0b4f32548158");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a1d0f2c-7544-44a4-83c4-fc0f447b155f");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "ExerciseDatas");

            migrationBuilder.DropColumn(
                name: "HowMuchMoreRepsAbleToDo",
                table: "ExerciseDatas");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ExerciseDatas");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WhenExercised",
                table: "ExerciseDatas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkoutTime",
                table: "ExerciseDatas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "994f3cee-4b9d-4c8c-b361-61c877e2bb9e", null, "Admin", "ADMIN" },
                    { "c7d02394-90ff-48c4-8f44-5239e6a2ec64", null, "User", "USER" }
                });
        }
    }
}
