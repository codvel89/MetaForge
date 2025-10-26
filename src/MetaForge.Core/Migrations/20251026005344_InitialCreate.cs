using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MetaForge.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "metaforge");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntityName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Changes = table.Column<string>(type: "text", nullable: false),
                    OldValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: true),
                    PerformedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    PerformedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DatabaseConnections",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ConnectionString = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    DefaultSchema = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TimeoutSeconds = table.Column<int>(type: "integer", nullable: false),
                    PoolSize = table.Column<int>(type: "integer", nullable: false),
                    UseSsl = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseConnections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Subject = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    HtmlBody = table.Column<string>(type: "text", nullable: false),
                    TextBody = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FromEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FromName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Migrations",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Version = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AppliedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AppliedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExecutedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DatabaseConnectionId = table.Column<int>(type: "integer", nullable: true),
                    SqlScript = table.Column<string>(type: "text", nullable: true),
                    Success = table.Column<bool>(type: "boolean", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Migrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Version = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Author = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IsInstalled = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    InstalledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AssemblyPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Subject = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Body = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Metadata = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Resource = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystemRole = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemSettings",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DataType = table.Column<string>(type: "text", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableDefinitions",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Schema = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    PluralName = table.Column<string>(type: "text", nullable: true),
                    SingularName = table.Column<string>(type: "text", nullable: true),
                    IsSystemTable = table.Column<bool>(type: "boolean", nullable: false),
                    EnableAudit = table.Column<bool>(type: "boolean", nullable: false),
                    EnableSoftDelete = table.Column<bool>(type: "boolean", nullable: false),
                    EnableWorkflow = table.Column<bool>(type: "boolean", nullable: false),
                    WorkflowDefinitionId = table.Column<string>(type: "text", nullable: true),
                    Permissions_Create = table.Column<string[]>(type: "text[]", nullable: true),
                    Permissions_Read = table.Column<string[]>(type: "text[]", nullable: true),
                    Permissions_Update = table.Column<string[]>(type: "text[]", nullable: true),
                    Permissions_Delete = table.Column<string[]>(type: "text[]", nullable: true),
                    ListViewConfig_DefaultPageSize = table.Column<int>(type: "integer", nullable: true),
                    ListViewConfig_DefaultSortColumn = table.Column<string>(type: "text", nullable: true),
                    ListViewConfig_DefaultSortOrder = table.Column<string>(type: "text", nullable: true),
                    ListViewConfig_EnableSearch = table.Column<bool>(type: "boolean", nullable: true),
                    ListViewConfig_SearchColumns = table.Column<string[]>(type: "text[]", nullable: true),
                    ListViewConfig_EnableFilters = table.Column<bool>(type: "boolean", nullable: true),
                    ListViewConfig_GroupBy = table.Column<string[]>(type: "text[]", nullable: true),
                    ListViewConfig_EnableExport = table.Column<bool>(type: "boolean", nullable: true),
                    ListViewConfig_ExportFormats = table.Column<string[]>(type: "text[]", nullable: true),
                    FormViewConfig_Layout = table.Column<string>(type: "text", nullable: true),
                    FormViewConfig_UseTabs = table.Column<bool>(type: "boolean", nullable: true),
                    FormViewConfig_Width = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EmailVerified = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowDefinitions",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TriggerEvent = table.Column<string>(type: "text", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: true),
                    StepsDefinition = table.Column<string>(type: "text", nullable: false),
                    Definition = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleDependencies",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuleId = table.Column<int>(type: "integer", nullable: false),
                    RequiredModuleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MinVersion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleDependencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleDependencies_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalSchema: "metaforge",
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "metaforge",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "metaforge",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aggregation",
                schema: "metaforge",
                columns: table => new
                {
                    ListViewConfigTableDefinitionId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Field = table.Column<string>(type: "text", nullable: false),
                    Function = table.Column<string>(type: "text", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Format = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aggregation", x => new { x.ListViewConfigTableDefinitionId, x.Id });
                    table.ForeignKey(
                        name: "FK_Aggregation_TableDefinitions_ListViewConfigTableDefinitionId",
                        column: x => x.ListViewConfigTableDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "TableDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessRules",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Trigger = table.Column<string>(type: "text", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: true),
                    Expression = table.Column<string>(type: "text", nullable: true),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    Actions = table.Column<List<string>>(type: "text[]", nullable: true),
                    TableDefinitionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessRules_TableDefinitions_TableDefinitionId",
                        column: x => x.TableDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "TableDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColumnDefinitions",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MaxLength = table.Column<int>(type: "integer", nullable: true),
                    Precision = table.Column<int>(type: "integer", nullable: true),
                    Scale = table.Column<int>(type: "integer", nullable: true),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    IsPrimaryKey = table.Column<bool>(type: "boolean", nullable: false),
                    IsAutoIncrement = table.Column<bool>(type: "boolean", nullable: false),
                    IsForeignKey = table.Column<bool>(type: "boolean", nullable: false),
                    IsUnique = table.Column<bool>(type: "boolean", nullable: false),
                    IsIndexed = table.Column<bool>(type: "boolean", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "boolean", nullable: false),
                    DefaultValue = table.Column<string>(type: "text", nullable: true),
                    ShowInList = table.Column<bool>(type: "boolean", nullable: false),
                    ShowInForm = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    SequenceConfig_Prefix = table.Column<string>(type: "text", nullable: true),
                    SequenceConfig_YearFormat = table.Column<string>(type: "text", nullable: true),
                    SequenceConfig_NumberLength = table.Column<int>(type: "integer", nullable: true),
                    SequenceConfig_Pattern = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TableDefinitionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColumnDefinitions_TableDefinitions_TableDefinitionId",
                        column: x => x.TableDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "TableDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComputedFieldDefinitions",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Expression = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ShowInList = table.Column<bool>(type: "boolean", nullable: false),
                    Format = table.Column<string>(type: "text", nullable: true),
                    TableDefinitionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputedFieldDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputedFieldDefinitions_TableDefinitions_TableDefinitionId",
                        column: x => x.TableDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "TableDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormSection",
                schema: "metaforge",
                columns: table => new
                {
                    FormViewConfigTableDefinitionId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    FullWidth = table.Column<bool>(type: "boolean", nullable: false),
                    Columns = table.Column<string[]>(type: "text[]", nullable: false),
                    Collapsed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormSection", x => new { x.FormViewConfigTableDefinitionId, x.Id });
                    table.ForeignKey(
                        name: "FK_FormSection_TableDefinitions_FormViewConfigTableDefinitionId",
                        column: x => x.FormViewConfigTableDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "TableDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndexDefinitions",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Columns = table.Column<string[]>(type: "text[]", nullable: false),
                    IsUnique = table.Column<bool>(type: "boolean", nullable: false),
                    IndexType = table.Column<string>(type: "text", nullable: true),
                    TableDefinitionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndexDefinitions_TableDefinitions_TableDefinitionId",
                        column: x => x.TableDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "TableDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuickFilter",
                schema: "metaforge",
                columns: table => new
                {
                    ListViewConfigTableDefinitionId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Expression = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuickFilter", x => new { x.ListViewConfigTableDefinitionId, x.Id });
                    table.ForeignKey(
                        name: "FK_QuickFilter_TableDefinitions_ListViewConfigTableDefinitionId",
                        column: x => x.ListViewConfigTableDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "TableDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelationDefinitions",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ForeignKeyColumn = table.Column<string>(type: "text", nullable: true),
                    RelatedTable = table.Column<string>(type: "text", nullable: false),
                    RelatedColumn = table.Column<string>(type: "text", nullable: true),
                    RelatedForeignKey = table.Column<string>(type: "text", nullable: true),
                    OnDelete = table.Column<string>(type: "text", nullable: false),
                    DisplayInForm = table.Column<bool>(type: "boolean", nullable: false),
                    LoadByDefault = table.Column<bool>(type: "boolean", nullable: false),
                    CascadeDelete = table.Column<bool>(type: "boolean", nullable: false),
                    FormConfig_Component = table.Column<string>(type: "text", nullable: true),
                    FormConfig_AllowAdd = table.Column<bool>(type: "boolean", nullable: true),
                    FormConfig_AllowEdit = table.Column<bool>(type: "boolean", nullable: true),
                    FormConfig_AllowDelete = table.Column<bool>(type: "boolean", nullable: true),
                    FormConfig_MinRows = table.Column<int>(type: "integer", nullable: true),
                    FormConfig_MaxRows = table.Column<int>(type: "integer", nullable: true),
                    FormConfig_InlineEditing = table.Column<bool>(type: "boolean", nullable: true),
                    TableDefinitionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelationDefinitions_TableDefinitions_TableDefinitionId",
                        column: x => x.TableDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "TableDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TriggerDefinitions",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Event = table.Column<string>(type: "text", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: true),
                    TableDefinitionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TriggerDefinitions_TableDefinitions_TableDefinitionId",
                        column: x => x.TableDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "TableDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiKeys",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    KeyHash = table.Column<string>(type: "text", nullable: false),
                    Key = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    KeyPrefix = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastUsedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Permissions = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiKeys_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "metaforge",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "metaforge",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "metaforge",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowInstances",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkflowDefinitionId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Context = table.Column<string>(type: "text", nullable: false),
                    ContextData = table.Column<string>(type: "text", nullable: true),
                    Result = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Error = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CurrentStep = table.Column<int>(type: "integer", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    StartedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowInstances_WorkflowDefinitions_WorkflowDefinitionId",
                        column: x => x.WorkflowDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "WorkflowDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldFormConfigs",
                schema: "metaforge",
                columns: table => new
                {
                    ColumnDefinitionId = table.Column<int>(type: "integer", nullable: false),
                    Component = table.Column<string>(type: "text", nullable: false),
                    Placeholder = table.Column<string>(type: "text", nullable: true),
                    HelpText = table.Column<string>(type: "text", nullable: true),
                    InputType = table.Column<string>(type: "text", nullable: true),
                    FullWidth = table.Column<bool>(type: "boolean", nullable: false),
                    Rows = table.Column<int>(type: "integer", nullable: true),
                    DataSource_Type = table.Column<string>(type: "text", nullable: true),
                    DataSource_TableName = table.Column<string>(type: "text", nullable: true),
                    DataSource_ValueField = table.Column<string>(type: "text", nullable: true),
                    DataSource_LabelField = table.Column<string>(type: "text", nullable: true),
                    DataSource_SearchFields = table.Column<string[]>(type: "text[]", nullable: true),
                    DataSource_FilterExpression = table.Column<string>(type: "text", nullable: true),
                    DataSource_OrderBy = table.Column<string>(type: "text", nullable: true),
                    AllowSearch = table.Column<bool>(type: "boolean", nullable: false),
                    MinSearchLength = table.Column<int>(type: "integer", nullable: true),
                    AcceptedFormats = table.Column<string[]>(type: "text[]", nullable: true),
                    MaxSizeMB = table.Column<int>(type: "integer", nullable: true),
                    StoragePath = table.Column<string>(type: "text", nullable: true),
                    Prefix = table.Column<string>(type: "text", nullable: true),
                    DependsOn = table.Column<string[]>(type: "text[]", nullable: true),
                    ReadOnlyCondition = table.Column<string>(type: "text", nullable: true),
                    DisabledDate = table.Column<string>(type: "text", nullable: true),
                    Format = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldFormConfigs", x => x.ColumnDefinitionId);
                    table.ForeignKey(
                        name: "FK_FieldFormConfigs_ColumnDefinitions_ColumnDefinitionId",
                        column: x => x.ColumnDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "ColumnDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateTransition",
                schema: "metaforge",
                columns: table => new
                {
                    ColumnDefinitionId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    From = table.Column<string>(type: "text", nullable: false),
                    To = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateTransition", x => new { x.ColumnDefinitionId, x.Id });
                    table.ForeignKey(
                        name: "FK_StateTransition_ColumnDefinitions_ColumnDefinitionId",
                        column: x => x.ColumnDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "ColumnDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValidationRules",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Pattern = table.Column<string>(type: "text", nullable: true),
                    Expression = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Conditions = table.Column<Dictionary<string, string>>(type: "hstore", nullable: true),
                    ColumnDefinitionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValidationRules_ColumnDefinitions_ColumnDefinitionId",
                        column: x => x.ColumnDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "ColumnDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TriggerAction",
                schema: "metaforge",
                columns: table => new
                {
                    TriggerDefinitionId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Field = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    Template = table.Column<string>(type: "text", nullable: true),
                    Recipients = table.Column<string[]>(type: "text[]", nullable: true),
                    WorkflowId = table.Column<string>(type: "text", nullable: true),
                    Condition = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerAction", x => new { x.TriggerDefinitionId, x.Id });
                    table.ForeignKey(
                        name: "FK_TriggerAction_TriggerDefinitions_TriggerDefinitionId",
                        column: x => x.TriggerDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "TriggerDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowSteps",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkflowInstanceId = table.Column<int>(type: "integer", nullable: false),
                    StepNumber = table.Column<int>(type: "integer", nullable: false),
                    StepOrder = table.Column<int>(type: "integer", nullable: false),
                    StepName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Input = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Output = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Error = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowSteps_WorkflowInstances_WorkflowInstanceId",
                        column: x => x.WorkflowInstanceId,
                        principalSchema: "metaforge",
                        principalTable: "WorkflowInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SelectOptions",
                schema: "metaforge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: true),
                    FieldFormConfigColumnDefinitionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectOptions_FieldFormConfigs_FieldFormConfigColumnDefinit~",
                        column: x => x.FieldFormConfigColumnDefinitionId,
                        principalSchema: "metaforge",
                        principalTable: "FieldFormConfigs",
                        principalColumn: "ColumnDefinitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeys_ExpiresAt",
                schema: "metaforge",
                table: "ApiKeys",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeys_IsActive",
                schema: "metaforge",
                table: "ApiKeys",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeys_Key",
                schema: "metaforge",
                table: "ApiKeys",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeys_UserId",
                schema: "metaforge",
                table: "ApiKeys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Action",
                schema: "metaforge",
                table: "AuditLogs",
                column: "Action");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityName_EntityId",
                schema: "metaforge",
                table: "AuditLogs",
                columns: new[] { "EntityName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_PerformedAt",
                schema: "metaforge",
                table: "AuditLogs",
                column: "PerformedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_PerformedBy",
                schema: "metaforge",
                table: "AuditLogs",
                column: "PerformedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessRules_TableDefinitionId",
                schema: "metaforge",
                table: "BusinessRules",
                column: "TableDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnDefinitions_TableDefinitionId",
                schema: "metaforge",
                table: "ColumnDefinitions",
                column: "TableDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputedFieldDefinitions_TableDefinitionId",
                schema: "metaforge",
                table: "ComputedFieldDefinitions",
                column: "TableDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseConnections_Name",
                schema: "metaforge",
                table: "DatabaseConnections",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_Category",
                schema: "metaforge",
                table: "EmailTemplates",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_Code",
                schema: "metaforge",
                table: "EmailTemplates",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_IsActive",
                schema: "metaforge",
                table: "EmailTemplates",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_IndexDefinitions_TableDefinitionId",
                schema: "metaforge",
                table: "IndexDefinitions",
                column: "TableDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Migrations_AppliedAt",
                schema: "metaforge",
                table: "Migrations",
                column: "AppliedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Migrations_Version",
                schema: "metaforge",
                table: "Migrations",
                column: "Version",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleDependencies_ModuleId_RequiredModuleName",
                schema: "metaforge",
                table: "ModuleDependencies",
                columns: new[] { "ModuleId", "RequiredModuleName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_IsActive",
                schema: "metaforge",
                table: "Modules",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_IsInstalled",
                schema: "metaforge",
                table: "Modules",
                column: "IsInstalled");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Name",
                schema: "metaforge",
                table: "Modules",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_Category",
                schema: "metaforge",
                table: "NotificationTemplates",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_Code",
                schema: "metaforge",
                table: "NotificationTemplates",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_IsActive",
                schema: "metaforge",
                table: "NotificationTemplates",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                schema: "metaforge",
                table: "Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Resource_Action",
                schema: "metaforge",
                table: "Permissions",
                columns: new[] { "Resource", "Action" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelationDefinitions_TableDefinitionId",
                schema: "metaforge",
                table: "RelationDefinitions",
                column: "TableDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "metaforge",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId_PermissionId",
                schema: "metaforge",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IsSystemRole",
                schema: "metaforge",
                table: "Roles",
                column: "IsSystemRole");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                schema: "metaforge",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelectOptions_FieldFormConfigColumnDefinitionId",
                schema: "metaforge",
                table: "SelectOptions",
                column: "FieldFormConfigColumnDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemSettings_Category",
                schema: "metaforge",
                table: "SystemSettings",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_SystemSettings_Key",
                schema: "metaforge",
                table: "SystemSettings",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TableDefinitions_Name_Schema",
                schema: "metaforge",
                table: "TableDefinitions",
                columns: new[] { "Name", "Schema" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TriggerDefinitions_TableDefinitionId",
                schema: "metaforge",
                table: "TriggerDefinitions",
                column: "TableDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "metaforge",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId_RoleId",
                schema: "metaforge",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "metaforge",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsActive",
                schema: "metaforge",
                table: "Users",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                schema: "metaforge",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ValidationRules_ColumnDefinitionId",
                schema: "metaforge",
                table: "ValidationRules",
                column: "ColumnDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinitions_Category",
                schema: "metaforge",
                table: "WorkflowDefinitions",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinitions_Code",
                schema: "metaforge",
                table: "WorkflowDefinitions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinitions_IsActive",
                schema: "metaforge",
                table: "WorkflowDefinitions",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstances_StartedAt",
                schema: "metaforge",
                table: "WorkflowInstances",
                column: "StartedAt");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstances_StartedBy",
                schema: "metaforge",
                table: "WorkflowInstances",
                column: "StartedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstances_Status",
                schema: "metaforge",
                table: "WorkflowInstances",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstances_WorkflowDefinitionId",
                schema: "metaforge",
                table: "WorkflowInstances",
                column: "WorkflowDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowSteps_StartedAt",
                schema: "metaforge",
                table: "WorkflowSteps",
                column: "StartedAt");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowSteps_Status",
                schema: "metaforge",
                table: "WorkflowSteps",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowSteps_StepOrder",
                schema: "metaforge",
                table: "WorkflowSteps",
                column: "StepOrder");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowSteps_WorkflowInstanceId",
                schema: "metaforge",
                table: "WorkflowSteps",
                column: "WorkflowInstanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aggregation",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "ApiKeys",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "BusinessRules",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "ComputedFieldDefinitions",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "DatabaseConnections",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "EmailTemplates",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "FormSection",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "IndexDefinitions",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "Migrations",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "ModuleDependencies",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "NotificationTemplates",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "QuickFilter",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "RelationDefinitions",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "SelectOptions",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "StateTransition",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "SystemSettings",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "TriggerAction",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "ValidationRules",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "WorkflowSteps",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "Modules",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "FieldFormConfigs",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "TriggerDefinitions",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "WorkflowInstances",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "ColumnDefinitions",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "WorkflowDefinitions",
                schema: "metaforge");

            migrationBuilder.DropTable(
                name: "TableDefinitions",
                schema: "metaforge");
        }
    }
}
