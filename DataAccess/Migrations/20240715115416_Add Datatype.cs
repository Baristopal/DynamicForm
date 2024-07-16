using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDatatype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataType",
                table: "Field",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Field_FormId",
                table: "Field",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Field_Forms_FormId",
                table: "Field",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Field_Forms_FormId",
                table: "Field");

            migrationBuilder.DropIndex(
                name: "IX_Field_FormId",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "Field");
        }
    }
}
