using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWorkout.Web.Data.Migrations
{
    public partial class AddWorkoutDayTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkoutDayId",
                table: "Exercises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WorkoutDays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(nullable: false),
                    MuscleGroup = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutDays", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutDayId",
                table: "Exercises",
                column: "WorkoutDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_WorkoutDays_WorkoutDayId",
                table: "Exercises",
                column: "WorkoutDayId",
                principalTable: "WorkoutDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_WorkoutDays_WorkoutDayId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "WorkoutDays");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_WorkoutDayId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "WorkoutDayId",
                table: "Exercises");
        }
    }
}
