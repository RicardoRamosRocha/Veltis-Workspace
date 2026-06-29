using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Veltis.Workspace.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Sprint1FunctionalFoundation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfessionId",
                schema: "workspace",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "professions",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Icon = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Color = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    Slug = table.Column<string>(type: "character varying(140)", maxLength: 140, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "workspaces",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workspaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workspaces_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "workspace",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_professions",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_professions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_professions_professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalSchema: "workspace",
                        principalTable: "professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_professions_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "workspace",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "workspace",
                table: "professions",
                columns: new[] { "Id", "Active", "Color", "CreatedAt", "DeletedAt", "Description", "Icon", "Name", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), true, "#0f766e", new DateTime(2026, 6, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, "Perfil profissional generico.", "user", "Perfil Base", "Perfil Base", null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), true, "#2563eb", new DateTime(2026, 6, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, "Perfil de gestao generico.", "settings", "Gestor", "Gestor", null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), true, "#7c3aed", new DateTime(2026, 6, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, "Profissional de Analistaia.", "chart", "Analista", "Analista", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_ProfessionId",
                schema: "workspace",
                table: "users",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_professions_Slug",
                schema: "workspace",
                table: "professions",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_professions_ProfessionId",
                schema: "workspace",
                table: "user_professions",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_user_professions_UserId_ProfessionId",
                schema: "workspace",
                table: "user_professions",
                columns: new[] { "UserId", "ProfessionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_workspaces_UserId",
                schema: "workspace",
                table: "workspaces",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_professions_ProfessionId",
                schema: "workspace",
                table: "users",
                column: "ProfessionId",
                principalSchema: "workspace",
                principalTable: "professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_professions_ProfessionId",
                schema: "workspace",
                table: "users");

            migrationBuilder.DropTable(
                name: "user_professions",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "workspaces",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "professions",
                schema: "workspace");

            migrationBuilder.DropIndex(
                name: "IX_users_ProfessionId",
                schema: "workspace",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                schema: "workspace",
                table: "users");
        }
    }
}

