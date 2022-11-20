using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICAds.Data.Migrations
{
    /// <inheritdoc />
    public partial class templateIntegrationrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccesToken",
                table: "Integrations",
                newName: "AccessToken");

            migrationBuilder.CreateTable(
                name: "TemplatesMetadata",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    OrganizationId = table.Column<string>(type: "text", nullable: false),
                    IntegrationId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplatesMetadata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplatesMetadata_Integrations_IntegrationId",
                        column: x => x.IntegrationId,
                        principalTable: "Integrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplatesMetadata_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TemplateJSON = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    TemplateMetadataId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_TemplatesMetadata_TemplateMetadataId",
                        column: x => x.TemplateMetadataId,
                        principalTable: "TemplatesMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Templates_TemplateMetadataId",
                table: "Templates",
                column: "TemplateMetadataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemplatesMetadata_IntegrationId",
                table: "TemplatesMetadata",
                column: "IntegrationId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplatesMetadata_OrganizationId",
                table: "TemplatesMetadata",
                column: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "TemplatesMetadata");

            migrationBuilder.RenameColumn(
                name: "AccessToken",
                table: "Integrations",
                newName: "AccesToken");
        }
    }
}
