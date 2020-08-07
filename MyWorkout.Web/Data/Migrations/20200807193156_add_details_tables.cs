using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWorkout.Web.Data.Migrations
{
    public partial class add_details_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanDetails_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutDayDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    PlanDetailsId = table.Column<int>(nullable: true),
                    WorkoutDayId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutDayDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutDayDetails_PlanDetails_PlanDetailsId",
                        column: x => x.PlanDetailsId,
                        principalTable: "PlanDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkoutDayDetails_WorkoutDays_WorkoutDayId",
                        column: x => x.WorkoutDayId,
                        principalTable: "WorkoutDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(nullable: true),
                    WorkoutDayDetailsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseDetails_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExerciseDetails_WorkoutDayDetails_WorkoutDayDetailsId",
                        column: x => x.WorkoutDayDetailsId,
                        principalTable: "WorkoutDayDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepeatDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OverriddenCount = table.Column<int>(nullable: false),
                    RepeatId = table.Column<int>(nullable: true),
                    ExerciseDetailsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepeatDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepeatDetails_ExerciseDetails_ExerciseDetailsId",
                        column: x => x.ExerciseDetailsId,
                        principalTable: "ExerciseDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepeatDetails_Repeats_RepeatId",
                        column: x => x.RepeatId,
                        principalTable: "Repeats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDetails_ExerciseId",
                table: "ExerciseDetails",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDetails_WorkoutDayDetailsId",
                table: "ExerciseDetails",
                column: "WorkoutDayDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanDetails_PlanId",
                table: "PlanDetails",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RepeatDetails_ExerciseDetailsId",
                table: "RepeatDetails",
                column: "ExerciseDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_RepeatDetails_RepeatId",
                table: "RepeatDetails",
                column: "RepeatId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDayDetails_PlanDetailsId",
                table: "WorkoutDayDetails",
                column: "PlanDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDayDetails_WorkoutDayId",
                table: "WorkoutDayDetails",
                column: "WorkoutDayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepeatDetails");

            migrationBuilder.DropTable(
                name: "ExerciseDetails");

            migrationBuilder.DropTable(
                name: "WorkoutDayDetails");

            migrationBuilder.DropTable(
                name: "PlanDetails");
        }
    }
}
