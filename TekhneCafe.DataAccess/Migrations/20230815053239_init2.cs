using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TekhneCafe.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsValid",
                table: "ProductAttributeProduct",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsValid",
                table: "ProductAttribute",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsValid",
                table: "Product",
                newName: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "ProductAttributeProduct",
                newName: "IsValid");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "ProductAttribute",
                newName: "IsValid");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Product",
                newName: "IsValid");
        }
    }
}
