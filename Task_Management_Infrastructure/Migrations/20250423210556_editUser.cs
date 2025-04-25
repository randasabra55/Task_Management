using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Management_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_externalAPITasks_tasksses_TaskId",
                table: "externalAPITasks");

            migrationBuilder.DropIndex(
                name: "IX_externalAPITasks_TaskId",
                table: "externalAPITasks");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "externalAPITasks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "externalAPITasks",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "ExternalTaskId",
                table: "externalAPITasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GoogleAccessToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalTaskId",
                table: "externalAPITasks");

            migrationBuilder.DropColumn(
                name: "GoogleAccessToken",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "externalAPITasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "externalAPITasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_externalAPITasks_TaskId",
                table: "externalAPITasks",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_externalAPITasks_tasksses_TaskId",
                table: "externalAPITasks",
                column: "TaskId",
                principalTable: "tasksses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
