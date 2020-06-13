using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWorkout.Web.Data.Migrations
{
    public partial class CreateRelationExerciseRepeats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Repeats",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseId",
                table: "Repeats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Repeats_ExerciseId",
                table: "Repeats",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repeats_Exercises_ExerciseId",
                table: "Repeats",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repeats_Exercises_ExerciseId",
                table: "Repeats");

            migrationBuilder.DropIndex(
                name: "IX_Repeats_ExerciseId",
                table: "Repeats");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "Repeats");

            migrationBuilder.AddColumn<int>(
                name: "Repeats",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
