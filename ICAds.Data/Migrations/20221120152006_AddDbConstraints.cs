using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICAds.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDbConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrganizationId",
                table: "Users",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TemplatesMetadata",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationId",
                table: "TemplatesMetadata",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TemplatesMetadata",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublic",
                table: "TemplatesMetadata",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "IntegrationId",
                table: "TemplatesMetadata",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TemplatesMetadata",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TemplatesMetadata",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateMetadataId",
                table: "Templates",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Templates",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Organizations",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Organizations",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Integrations",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationId",
                table: "Integrations",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Integrations",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AccessToken",
                table: "Integrations",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Integrations",
                type: "character(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrganizationId",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TemplatesMetadata",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationId",
                table: "TemplatesMetadata",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TemplatesMetadata",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublic",
                table: "TemplatesMetadata",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "IntegrationId",
                table: "TemplatesMetadata",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TemplatesMetadata",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TemplatesMetadata",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "TemplateMetadataId",
                table: "Templates",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Templates",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Organizations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Organizations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Integrations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationId",
                table: "Integrations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Integrations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "AccessToken",
                table: "Integrations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Integrations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(36)",
                oldFixedLength: true,
                oldMaxLength: 36);
        }
    }
}
