using Microsoft.EntityFrameworkCore;
using MetaForge.Shared;
using MetaForge.Core.Entities.System;
using MetaForge.Core.Entities.Security;
using MetaForge.Core.Entities.Audit;
using MetaForge.Core.Entities.Notification;
using MetaForge.Core.Entities.Workflow;

namespace MetaForge.Core.Context;

/// <summary>
/// DbContext para almacenar los metadatos del sistema (TableDefinitions, ColumnDefinitions, etc.)
/// </summary>
public class MetadataDbContext : DbContext
{
    /// <summary>
    /// Constructor con opciones de configuración
    /// </summary>
    public MetadataDbContext(DbContextOptions<MetadataDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Definiciones de tablas
    /// </summary>
    public DbSet<TableDefinition> TableDefinitions { get; set; } = null!;

    /// <summary>
    /// Conexiones a bases de datos de aplicación
    /// </summary>
    public DbSet<DatabaseConnection> DatabaseConnections { get; set; } = null!;

    // System Entities
    /// <summary>
    /// Historial de migraciones
    /// </summary>
    public DbSet<Migration> Migrations { get; set; } = null!;

    /// <summary>
    /// Módulos instalados
    /// </summary>
    public DbSet<Module> Modules { get; set; } = null!;

    /// <summary>
    /// Configuraciones del sistema
    /// </summary>
    public DbSet<SystemSetting> SystemSettings { get; set; } = null!;

    // Security Entities
    /// <summary>
    /// Usuarios del sistema
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Roles del sistema
    /// </summary>
    public DbSet<Role> Roles { get; set; } = null!;

    /// <summary>
    /// Permisos del sistema
    /// </summary>
    public DbSet<Permission> Permissions { get; set; } = null!;

    /// <summary>
    /// Claves de API
    /// </summary>
    public DbSet<ApiKey> ApiKeys { get; set; } = null!;

    // Audit Entities
    /// <summary>
    /// Registro de auditoría
    /// </summary>
    public DbSet<AuditLog> AuditLogs { get; set; } = null!;

    // Notification Entities
    /// <summary>
    /// Plantillas de notificación
    /// </summary>
    public DbSet<NotificationTemplate> NotificationTemplates { get; set; } = null!;

    /// <summary>
    /// Plantillas de email
    /// </summary>
    public DbSet<EmailTemplate> EmailTemplates { get; set; } = null!;

    // Workflow Entities
    /// <summary>
    /// Definiciones de workflows
    /// </summary>
    public DbSet<WorkflowDefinition> WorkflowDefinitions { get; set; } = null!;

    /// <summary>
    /// Instancias de workflows en ejecución
    /// </summary>
    public DbSet<WorkflowInstance> WorkflowInstances { get; set; } = null!;

    /// <summary>
    /// Configuración del modelo
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureTableDefinition(modelBuilder);
        ConfigureDatabaseConnection(modelBuilder);
        ConfigureSystemEntities(modelBuilder);
        ConfigureSecurityEntities(modelBuilder);
        ConfigureAuditEntities(modelBuilder);
        ConfigureNotificationEntities(modelBuilder);
        ConfigureWorkflowEntities(modelBuilder);
    }

    /// <summary>
    /// Configura la entidad TableDefinition
    /// </summary>
    private void ConfigureTableDefinition(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TableDefinition>(entity =>
        {
            entity.ToTable("TableDefinitions", "metaforge");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Schema).IsRequired().HasMaxLength(50);
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);

            // Índice único por nombre de tabla y esquema
            entity.HasIndex(e => new { e.Name, e.Schema }).IsUnique();

            // Relación con columnas - owned entity
            entity.OwnsMany(e => e.Columns, columns =>
            {
                columns.ToTable("ColumnDefinitions", "metaforge");
                columns.WithOwner().HasForeignKey("TableDefinitionId");
                columns.Property<int>("Id").ValueGeneratedOnAdd();
                columns.HasKey("Id");

                columns.Property(c => c.Name).IsRequired().HasMaxLength(100);
                columns.Property(c => c.Type).IsRequired().HasMaxLength(50);
                columns.Property(c => c.DisplayName).HasMaxLength(200);

                // Owned collections dentro de columnas
                columns.OwnsMany(c => c.ValidationRules, rules =>
                {
                    rules.ToTable("ValidationRules", "metaforge");
                    rules.WithOwner();
                    rules.Property<int>("Id").ValueGeneratedOnAdd();
                    rules.HasKey("Id");
                    
                    // Ignorar propiedades object - se manejan como JSON en runtime
                    rules.Ignore(r => r.Min);
                    rules.Ignore(r => r.Max);
                });

                columns.OwnsOne(c => c.FormConfig, config =>
                {
                    config.ToTable("FieldFormConfigs", "metaforge");
                    config.WithOwner();
                    
                    // Ignorar propiedades object (Min, Max, Step) - se manejan como JSON en runtime
                    config.Ignore(f => f.Min);
                    config.Ignore(f => f.Max);
                    config.Ignore(f => f.Step);
                    
                    config.OwnsMany(f => f.Options, opts =>
                    {
                        opts.ToTable("SelectOptions", "metaforge");
                        opts.WithOwner();
                        opts.Property<int>("Id").ValueGeneratedOnAdd();
                        opts.HasKey("Id");
                    });

                    config.OwnsOne(f => f.DataSource);
                });

                columns.OwnsOne(c => c.SequenceConfig);
                columns.OwnsMany(c => c.StateTransitions);
            });

            // Owned collections de la tabla
            entity.OwnsMany(e => e.Indexes, indexes =>
            {
                indexes.ToTable("IndexDefinitions", "metaforge");
                indexes.WithOwner().HasForeignKey("TableDefinitionId");
                indexes.Property<int>("Id").ValueGeneratedOnAdd();
                indexes.HasKey("Id");
            });

            entity.OwnsMany(e => e.Relations, relations =>
            {
                relations.ToTable("RelationDefinitions", "metaforge");
                relations.WithOwner().HasForeignKey("TableDefinitionId");
                relations.Property<int>("Id").ValueGeneratedOnAdd();
                relations.HasKey("Id");

                relations.OwnsOne(r => r.FormConfig);
            });

            entity.OwnsMany(e => e.ComputedFields, computed =>
            {
                computed.ToTable("ComputedFieldDefinitions", "metaforge");
                computed.WithOwner().HasForeignKey("TableDefinitionId");
                computed.Property<int>("Id").ValueGeneratedOnAdd();
                computed.HasKey("Id");
            });

            entity.OwnsMany(e => e.Triggers, triggers =>
            {
                triggers.ToTable("TriggerDefinitions", "metaforge");
                triggers.WithOwner().HasForeignKey("TableDefinitionId");
                triggers.Property<int>("Id").ValueGeneratedOnAdd();
                triggers.HasKey("Id");

                triggers.OwnsMany(t => t.Actions);
            });

            entity.OwnsMany(e => e.BusinessRules, rules =>
            {
                rules.ToTable("BusinessRules", "metaforge");
                rules.WithOwner().HasForeignKey("TableDefinitionId");
                rules.Property<int>("Id").ValueGeneratedOnAdd();
                rules.HasKey("Id");
            });

            entity.OwnsOne(e => e.Permissions);
            entity.OwnsOne(e => e.ListViewConfig, config =>
            {
                config.OwnsMany(l => l.QuickFilters);
                config.OwnsMany(l => l.Aggregations);
            });
            entity.OwnsOne(e => e.FormViewConfig, config =>
            {
                config.OwnsMany(f => f.Sections);
            });
        });
    }

    /// <summary>
    /// Configura la entidad DatabaseConnection
    /// </summary>
    private void ConfigureDatabaseConnection(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DatabaseConnection>(entity =>
        {
            entity.ToTable("DatabaseConnections", "metaforge");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.ConnectionString).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.DefaultSchema).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasIndex(e => e.Name).IsUnique();
        });
    }

    /// <summary>
    /// Configura las entidades del sistema
    /// </summary>
    private void ConfigureSystemEntities(ModelBuilder modelBuilder)
    {
        // Migration
        modelBuilder.Entity<Migration>(entity =>
        {
            entity.ToTable("Migrations", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Version).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.AppliedBy).HasMaxLength(100);
            entity.HasIndex(e => e.Version).IsUnique();
            entity.HasIndex(e => e.AppliedAt);
        });

        // Module
        modelBuilder.Entity<Module>(entity =>
        {
            entity.ToTable("Modules", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DisplayName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Version).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Author).HasMaxLength(200);
            entity.Property(e => e.AssemblyPath).HasMaxLength(500);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.IsInstalled);
            entity.HasIndex(e => e.IsActive);
        });

        // ModuleDependency
        modelBuilder.Entity<ModuleDependency>(entity =>
        {
            entity.ToTable("ModuleDependencies", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RequiredModuleName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.MinVersion).HasMaxLength(50);
            
            entity.HasOne(e => e.Module)
                .WithMany(m => m.Dependencies)
                .HasForeignKey(e => e.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.ModuleId, e.RequiredModuleName }).IsUnique();
        });

        // SystemSetting
        modelBuilder.Entity<SystemSetting>(entity =>
        {
            entity.ToTable("SystemSettings", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Key).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Value).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.HasIndex(e => e.Key).IsUnique();
            entity.HasIndex(e => e.Category);
        });
    }

    /// <summary>
    /// Configura las entidades de seguridad
    /// </summary>
    private void ConfigureSecurityEntities(ModelBuilder modelBuilder)
    {
        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(500);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.IsActive);
        });

        // Role
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.IsSystemRole);
        });

        // Permission
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("Permissions", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Resource).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Action).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.HasIndex(e => new { e.Resource, e.Action }).IsUnique();
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // UserRole (Many-to-Many)
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRoles", "metaforge");
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.UserId, e.RoleId }).IsUnique();
        });

        // RolePermission (Many-to-Many)
        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.ToTable("RolePermissions", "metaforge");
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(e => e.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.RoleId, e.PermissionId }).IsUnique();
        });

        // ApiKey
        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.ToTable("ApiKeys", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Key).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(500);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.ApiKeys)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.Key).IsUnique();
            entity.HasIndex(e => e.IsActive);
            entity.HasIndex(e => e.ExpiresAt);
        });
    }

    /// <summary>
    /// Configura las entidades de auditoría
    /// </summary>
    private void ConfigureAuditEntities(ModelBuilder modelBuilder)
    {
        // AuditLog
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("AuditLogs", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EntityName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.EntityId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Action).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Changes).IsRequired();
            entity.Property(e => e.PerformedBy).HasMaxLength(100);
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.UserAgent).HasMaxLength(500);
            
            entity.HasIndex(e => new { e.EntityName, e.EntityId });
            entity.HasIndex(e => e.PerformedAt);
            entity.HasIndex(e => e.PerformedBy);
            entity.HasIndex(e => e.Action);
        });
    }

    /// <summary>
    /// Configura las entidades de notificación
    /// </summary>
    private void ConfigureNotificationEntities(ModelBuilder modelBuilder)
    {
        // NotificationTemplate
        modelBuilder.Entity<NotificationTemplate>(entity =>
        {
            entity.ToTable("NotificationTemplates", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Subject).HasMaxLength(500);
            entity.Property(e => e.Body).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Category).HasMaxLength(100);
            
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasIndex(e => e.IsActive);
            entity.HasIndex(e => e.Category);
        });

        // EmailTemplate
        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.ToTable("EmailTemplates", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Subject).IsRequired().HasMaxLength(500);
            entity.Property(e => e.HtmlBody).IsRequired();
            entity.Property(e => e.TextBody).IsRequired();
            entity.Property(e => e.FromEmail).HasMaxLength(255);
            entity.Property(e => e.FromName).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Category).HasMaxLength(100);
            
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasIndex(e => e.IsActive);
            entity.HasIndex(e => e.Category);
        });
    }

    /// <summary>
    /// Configura las entidades de workflow
    /// </summary>
    private void ConfigureWorkflowEntities(ModelBuilder modelBuilder)
    {
        // WorkflowDefinition
        modelBuilder.Entity<WorkflowDefinition>(entity =>
        {
            entity.ToTable("WorkflowDefinitions", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Version).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Definition).IsRequired();
            entity.Property(e => e.Category).HasMaxLength(100);
            
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasIndex(e => e.IsActive);
            entity.HasIndex(e => e.Category);
        });

        // WorkflowInstance
        modelBuilder.Entity<WorkflowInstance>(entity =>
        {
            entity.ToTable("WorkflowInstances", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Context).IsRequired();
            entity.Property(e => e.Result).HasMaxLength(1000);
            entity.Property(e => e.Error).HasMaxLength(2000);
            entity.Property(e => e.StartedBy).HasMaxLength(100);
            
            entity.HasOne(e => e.WorkflowDefinition)
                .WithMany(w => w.Instances)
                .HasForeignKey(e => e.WorkflowDefinitionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.WorkflowDefinitionId);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.StartedAt);
            entity.HasIndex(e => e.StartedBy);
        });

        // WorkflowStep
        modelBuilder.Entity<WorkflowStep>(entity =>
        {
            entity.ToTable("WorkflowSteps", "metaforge");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StepName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Input).HasMaxLength(2000);
            entity.Property(e => e.Output).HasMaxLength(2000);
            entity.Property(e => e.Error).HasMaxLength(2000);
            
            entity.HasOne(e => e.WorkflowInstance)
                .WithMany(w => w.Steps)
                .HasForeignKey(e => e.WorkflowInstanceId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.WorkflowInstanceId);
            entity.HasIndex(e => e.StepOrder);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.StartedAt);
        });
    }
}
