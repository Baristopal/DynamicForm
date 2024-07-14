using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Field");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Field",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "Required",
                table: "Field",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Required",
                table: "Field");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Field",
                newName: "LastName");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Field",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Field",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
