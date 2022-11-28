using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICAds.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateTemplateConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IntegrationId",
                table: "TemplatesMetadata",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "TemplatesMetadata",
                type: "character(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemplatesMetadata_CreatedByUserId",
                table: "TemplatesMetadata",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemplatesMetadata_Users_CreatedByUserId",
                table: "TemplatesMetadata",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemplatesMetadata_Users_CreatedByUserId",
                table: "TemplatesMetadata");

            migrationBuilder.DropIndex(
                name: "IX_TemplatesMetadata_CreatedByUserId",
                table: "TemplatesMetadata");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "TemplatesMetadata");

            migrationBuilder.AlterColumn<string>(
                name: "IntegrationId",
                table: "TemplatesMetadata",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36,
                oldNullable: true);
        }
    }
}
