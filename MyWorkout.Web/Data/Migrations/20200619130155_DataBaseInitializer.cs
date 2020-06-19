using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWorkout.Web.Data.Migrations
{
    public partial class DataBaseInitializer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "TestPlan" });

            migrationBuilder.InsertData(
                table: "WorkoutDays",
                columns: new[] { "Id", "DayOfWeek", "MuscleGroup", "PlanId" },
                values: new object[] { 1, 1, 2, 1 });

            migrationBuilder.InsertData(
                table: "WorkoutDays",
                columns: new[] { "Id", "DayOfWeek", "MuscleGroup", "PlanId" },
                values: new object[] { 2, 5, 1, 1 });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Name", "RestTime", "WorkoutDayId" },
                values: new object[] { 1, "Push-ups", new TimeSpan(0, 0, 0, 30, 0), 1 });

            migrationBuilder.InsertData(
                table: "Repeats",
                columns: new[] { "Id", "Count", "ExerciseId", "Number" },
                values: new object[] { 1, 5, 1, 1 });

            migrationBuilder.InsertData(
                table: "Repeats",
                columns: new[] { "Id", "Count", "ExerciseId", "Number" },
                values: new object[] { 2, 10, 1, 2 });

            migrationBuilder.InsertData(
                table: "Repeats",
                columns: new[] { "Id", "Count", "ExerciseId", "Number" },
                values: new object[] { 3, 15, 1, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Repeats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Repeats",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Repeats",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WorkoutDays",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkoutDays",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
