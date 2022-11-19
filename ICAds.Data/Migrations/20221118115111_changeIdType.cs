using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICAds.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldFixedLength: true,
                oldMaxLength: 36);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "uuid",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36);
        }
    }
}
