using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veltis.Workspace.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Sprint22DynamicFormEngine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "workspace",
                table: "form_definitions",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                schema: "workspace",
                table: "form_definitions",
                type: "character varying(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                schema: "workspace",
                table: "form_definitions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SchemaJson",
                schema: "workspace",
                table: "form_definitions",
                type: "text",
                nullable: false,
                defaultValue: "{}");

            migrationBuilder.AddColumn<string>(
                name: "UiSchemaJson",
                schema: "workspace",
                table: "form_definitions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValidationJson",
                schema: "workspace",
                table: "form_definitions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "form_submissions",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: true),
                    ValuesJson = table.Column<string>(type: "text", nullable: false),
                    ValidationJson = table.Column<string>(type: "text", nullable: true),
                    IsPreview = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_form_submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_form_submissions_form_definitions_FormDefinitionId",
                        column: x => x.FormDefinitionId,
                        principalSchema: "workspace",
                        principalTable: "form_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_form_definitions_TenantId_Name_Version",
                schema: "workspace",
                table: "form_definitions",
                columns: new[] { "TenantId", "Name", "Version" });

            migrationBuilder.CreateIndex(
                name: "IX_form_submissions_FormDefinitionId",
                schema: "workspace",
                table: "form_submissions",
                column: "FormDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_form_submissions_TenantId_FormDefinitionId",
                schema: "workspace",
                table: "form_submissions",
                columns: new[] { "TenantId", "FormDefinitionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "form_submissions",
                schema: "workspace");

            migrationBuilder.DropIndex(
                name: "IX_form_definitions_TenantId_Name_Version",
                schema: "workspace",
                table: "form_definitions");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "workspace",
                table: "form_definitions");

            migrationBuilder.DropColumn(
                name: "Icon",
                schema: "workspace",
                table: "form_definitions");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                schema: "workspace",
                table: "form_definitions");

            migrationBuilder.DropColumn(
                name: "SchemaJson",
                schema: "workspace",
                table: "form_definitions");

            migrationBuilder.DropColumn(
                name: "UiSchemaJson",
                schema: "workspace",
                table: "form_definitions");

            migrationBuilder.DropColumn(
                name: "ValidationJson",
                schema: "workspace",
                table: "form_definitions");
        }
    }
}
