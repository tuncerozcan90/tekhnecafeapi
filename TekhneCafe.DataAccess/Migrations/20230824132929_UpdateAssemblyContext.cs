using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TekhneCafe.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssemblyContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Notification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 24, 16, 29, 29, 736, DateTimeKind.Local).AddTicks(4477),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 24, 16, 6, 32, 339, DateTimeKind.Local).AddTicks(4177));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 24, 16, 29, 29, 736, DateTimeKind.Local).AddTicks(1627),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 24, 16, 6, 32, 339, DateTimeKind.Local).AddTicks(828));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Notification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 24, 16, 6, 32, 339, DateTimeKind.Local).AddTicks(4177),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 24, 16, 29, 29, 736, DateTimeKind.Local).AddTicks(4477));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 24, 16, 6, 32, 339, DateTimeKind.Local).AddTicks(828),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 24, 16, 29, 29, 736, DateTimeKind.Local).AddTicks(1627));
        }
    }
}
