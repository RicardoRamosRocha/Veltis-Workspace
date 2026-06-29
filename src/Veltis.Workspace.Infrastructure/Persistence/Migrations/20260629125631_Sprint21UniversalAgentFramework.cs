using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veltis.Workspace.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Sprint21UniversalAgentFramework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                schema: "workspace",
                table: "workspaces",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                schema: "workspace",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                schema: "workspace",
                table: "user_professions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                schema: "workspace",
                table: "professions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "agent_categories",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Slug = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Icon = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agent_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ai_providers",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Key = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ai_providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "audit_logs",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IpAddress = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Action = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    EntityName = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    EntityId = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    OldValue = table.Column<string>(type: "text", nullable: true),
                    NewValue = table.Column<string>(type: "text", nullable: true),
                    OccurredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit_logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "credits",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Reason = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    GrantedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "feature_flags",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FeatureKey = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feature_flags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "feature_settings",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FeatureKey = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Key = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feature_settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "features",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    EnabledByDefault = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "form_definitions",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    JsonSchema = table.Column<string>(type: "text", nullable: false),
                    UiSchema = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_form_definitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Read = table.Column<bool>(type: "boolean", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "permission_groups",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Key = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission_groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "plans",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Slug = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    MonthlyPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "prompt_templates",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SystemPrompt = table.Column<string>(type: "text", nullable: false),
                    Instructions = table.Column<string>(type: "text", nullable: true),
                    Variables = table.Column<string>(type: "text", nullable: true),
                    OutputFormat = table.Column<string>(type: "text", nullable: true),
                    Language = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Tone = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prompt_templates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "system_settings",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tenants",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Slug = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Region = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usages",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MetricKey = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_settings",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "workspace_settings",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workspace_settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ai_models",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AIProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Provider = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    ModelName = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    ContextWindow = table.Column<int>(type: "integer", nullable: false),
                    SupportsVision = table.Column<bool>(type: "boolean", nullable: false),
                    SupportsFunctions = table.Column<bool>(type: "boolean", nullable: false),
                    SupportsStreaming = table.Column<bool>(type: "boolean", nullable: false),
                    MaxTokens = table.Column<int>(type: "integer", nullable: false),
                    InputPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    OutputPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ai_models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ai_models_ai_providers_AIProviderId",
                        column: x => x.AIProviderId,
                        principalSchema: "workspace",
                        principalTable: "ai_providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_permissions_permission_groups_PermissionGroupId",
                        column: x => x.PermissionGroupId,
                        principalSchema: "workspace",
                        principalTable: "permission_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscriptions",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndsAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subscriptions_plans_PlanId",
                        column: x => x.PlanId,
                        principalSchema: "workspace",
                        principalTable: "plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "agents",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Slug = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Icon = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    CoverImage = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    PromptTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    FormDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefaultModelId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExecutionMode = table.Column<int>(type: "integer", nullable: false),
                    Temperature = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxTokens = table.Column<int>(type: "integer", nullable: false),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    RequiresSubscription = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_agents_agent_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "workspace",
                        principalTable: "agent_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_agents_ai_models_DefaultModelId",
                        column: x => x.DefaultModelId,
                        principalSchema: "workspace",
                        principalTable: "ai_models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_agents_form_definitions_FormDefinitionId",
                        column: x => x.FormDefinitionId,
                        principalSchema: "workspace",
                        principalTable: "form_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_agents_professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalSchema: "workspace",
                        principalTable: "professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_agents_prompt_templates_PromptTemplateId",
                        column: x => x.PromptTemplateId,
                        principalSchema: "workspace",
                        principalTable: "prompt_templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_permissions_permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "workspace",
                        principalTable: "permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    DueAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoices_subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "workspace",
                        principalTable: "subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agent_executions",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Prompt = table.Column<string>(type: "text", nullable: false),
                    Response = table.Column<string>(type: "text", nullable: true),
                    Provider = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Model = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    ExecutionTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    TokensInput = table.Column<int>(type: "integer", nullable: false),
                    TokensOutput = table.Column<int>(type: "integer", nullable: false),
                    EstimatedCost = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agent_executions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_agent_executions_agents_AgentId",
                        column: x => x.AgentId,
                        principalSchema: "workspace",
                        principalTable: "agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Provider = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payments_invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "workspace",
                        principalTable: "invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "generated_documents",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExecutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Format = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generated_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_generated_documents_agent_executions_ExecutionId",
                        column: x => x.ExecutionId,
                        principalSchema: "workspace",
                        principalTable: "agent_executions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "workspace",
                table: "ai_providers",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "Key", "Name", "UpdatedAt" },
                values: new object[] { new Guid("aaaaaaaa-0000-0000-0000-000000000001"), new DateTime(2026, 6, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "architecture-only", "Architecture Only", null });

            migrationBuilder.UpdateData(
                schema: "workspace",
                table: "professions",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "workspace",
                table: "professions",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "workspace",
                table: "professions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "TenantId",
                value: null);

            migrationBuilder.InsertData(
                schema: "workspace",
                table: "ai_models",
                columns: new[] { "Id", "AIProviderId", "ContextWindow", "CreatedAt", "DeletedAt", "InputPrice", "IsActive", "MaxTokens", "ModelName", "OutputPrice", "Provider", "SupportsFunctions", "SupportsStreaming", "SupportsVision", "UpdatedAt" },
                values: new object[] { new Guid("aaaaaaaa-0000-0000-0000-000000000002"), new Guid("aaaaaaaa-0000-0000-0000-000000000001"), 8192, new DateTime(2026, 6, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, 0m, true, 2048, "architecture-only-model", 0m, "architecture-only", false, false, false, null });

            migrationBuilder.CreateIndex(
                name: "IX_agent_categories_TenantId_Slug",
                schema: "workspace",
                table: "agent_categories",
                columns: new[] { "TenantId", "Slug" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_agent_executions_AgentId",
                schema: "workspace",
                table: "agent_executions",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_agents_CategoryId",
                schema: "workspace",
                table: "agents",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_agents_DefaultModelId",
                schema: "workspace",
                table: "agents",
                column: "DefaultModelId");

            migrationBuilder.CreateIndex(
                name: "IX_agents_FormDefinitionId",
                schema: "workspace",
                table: "agents",
                column: "FormDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_agents_ProfessionId",
                schema: "workspace",
                table: "agents",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_agents_PromptTemplateId",
                schema: "workspace",
                table: "agents",
                column: "PromptTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_agents_TenantId_ProfessionId_Slug",
                schema: "workspace",
                table: "agents",
                columns: new[] { "TenantId", "ProfessionId", "Slug" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ai_models_AIProviderId",
                schema: "workspace",
                table: "ai_models",
                column: "AIProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ai_providers_Key",
                schema: "workspace",
                table: "ai_providers",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_feature_flags_TenantId_FeatureKey",
                schema: "workspace",
                table: "feature_flags",
                columns: new[] { "TenantId", "FeatureKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_feature_settings_TenantId_FeatureKey_Key",
                schema: "workspace",
                table: "feature_settings",
                columns: new[] { "TenantId", "FeatureKey", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_features_Key",
                schema: "workspace",
                table: "features",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_generated_documents_ExecutionId",
                schema: "workspace",
                table: "generated_documents",
                column: "ExecutionId");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_SubscriptionId",
                schema: "workspace",
                table: "invoices",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_payments_InvoiceId",
                schema: "workspace",
                table: "payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_permission_groups_Key",
                schema: "workspace",
                table: "permission_groups",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_permissions_Key",
                schema: "workspace",
                table: "permissions",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_permissions_PermissionGroupId",
                schema: "workspace",
                table: "permissions",
                column: "PermissionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_plans_Slug",
                schema: "workspace",
                table: "plans",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_PermissionId",
                schema: "workspace",
                table: "role_permissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_RoleId_PermissionId",
                schema: "workspace",
                table: "role_permissions",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_PlanId",
                schema: "workspace",
                table: "subscriptions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_system_settings_Key",
                schema: "workspace",
                table: "system_settings",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tenants_Slug",
                schema: "workspace",
                table: "tenants",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_settings_UserId_Key",
                schema: "workspace",
                table: "user_settings",
                columns: new[] { "UserId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_workspace_settings_WorkspaceId_Key",
                schema: "workspace",
                table: "workspace_settings",
                columns: new[] { "WorkspaceId", "Key" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_logs",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "credits",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "feature_flags",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "feature_settings",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "features",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "generated_documents",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "notifications",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "payments",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "role_permissions",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "system_settings",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "tenants",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "usages",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "user_settings",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "workspace_settings",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "agent_executions",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "invoices",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "permissions",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "agents",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "subscriptions",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "permission_groups",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "agent_categories",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "ai_models",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "form_definitions",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "prompt_templates",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "plans",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "ai_providers",
                schema: "workspace");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "workspace",
                table: "workspaces");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "workspace",
                table: "users");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "workspace",
                table: "user_professions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "workspace",
                table: "professions");
        }
    }
}
