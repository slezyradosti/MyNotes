using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class deleteBehaviorChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Pages_PageId",
                table: "Notes",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Units_UnitId",
                table: "Pages",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Notebooks_NotebookId",
                table: "Units",
                column: "NotebookId",
                principalTable: "Notebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
    }
}
