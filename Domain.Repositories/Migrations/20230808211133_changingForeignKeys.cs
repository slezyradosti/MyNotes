using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class changingForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Pages_Id",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Units_Id",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Notebooks_Id",
                table: "Units");

            migrationBuilder.AddColumn<Guid>(
                name: "NotebookId",
                table: "Units",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "Pages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PageId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Units_NotebookId",
                table: "Units",
                column: "NotebookId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_UnitId",
                table: "Pages",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_PageId",
                table: "Notes",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Pages_PageId",
                table: "Notes",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Units_UnitId",
                table: "Pages",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Notebooks_NotebookId",
                table: "Units",
                column: "NotebookId",
                principalTable: "Notebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Pages_PageId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Units_UnitId",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Notebooks_NotebookId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_NotebookId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Pages_UnitId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Notes_PageId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "NotebookId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Pages_Id",
                table: "Notes",
                column: "Id",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Units_Id",
                table: "Pages",
                column: "Id",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Notebooks_Id",
                table: "Units",
                column: "Id",
                principalTable: "Notebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
