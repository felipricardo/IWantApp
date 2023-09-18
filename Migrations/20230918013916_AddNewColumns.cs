using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IWantApp.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EditeOn",
                table: "Products",
                newName: "EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditeBy",
                table: "Products",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                table: "Products",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditeOn",
                table: "Categories",
                newName: "EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditeBy",
                table: "Categories",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                table: "Categories",
                newName: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EditedOn",
                table: "Products",
                newName: "EditeOn");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Products",
                newName: "EditeBy");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Products",
                newName: "CreateBy");

            migrationBuilder.RenameColumn(
                name: "EditedOn",
                table: "Categories",
                newName: "EditeOn");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Categories",
                newName: "EditeBy");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Categories",
                newName: "CreateBy");
        }
    }
}
