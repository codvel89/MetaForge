# MetaForge.Core - Development Progress

## ✅ Completed

### Phase 1: Cleanup
- ✅ Removed MySQL and SQL Server support
- ✅ PostgreSQL as exclusive provider
- ✅ Updated `DatabaseProvider` enum
- ✅ Simplified `DbContextFactory`

### Phase 2: Reorganization
- ✅ Created new folder structure
- ✅ Moved `MetadataDbContext` to `Context/`
- ✅ Moved repositories to `Repositories/`
- ✅ Moved `DbContextFactory` to `Factories/`
- ✅ Updated namespaces

### Phase 3: System Entities
- ✅ `Migration.cs` - Migration history
- ✅ `Module.cs` - Installed modules
- ✅ `ModuleDependency.cs` - Module dependencies
- ✅ `SystemSetting.cs` - Global configuration

### Phase 4: Security Entities
- ✅ `User.cs` - System users
- ✅ `Role.cs` - Roles
- ✅ `Permission.cs` - Granular permissions
- ✅ `UserRole.cs` - User-role relationship
- ✅ `RolePermission.cs` - Role-permission relationship
- ✅ `ApiKey.cs` - API keys

### Phase 5: Audit Entities
- ✅ `AuditLog.cs` - Change tracking
- ❌ `EntityHistory.cs` - Entity history (OPTIONAL)

### Phase 6: Notification Entities
- ✅ `NotificationTemplate.cs` - Notification templates
- ✅ `EmailTemplate.cs` - Email templates

### Phase 7: Workflow Entities
- ✅ `WorkflowDefinition.cs` - Workflow definitions
- ✅ `WorkflowInstance.cs` - Running instances
- ✅ `WorkflowStep.cs` - Workflow steps

### Phase 9: Core Services
- ✅ `Services/Security/`
  - ✅ `IPasswordHasher` + `PasswordHasher` (PBKDF2)
  - ✅ `IJwtTokenService` + `JwtTokenService` (JWT tokens)
  - ✅ `IAuthenticationService` (login, refresh token, change password)
  - ✅ `IAuthorizationService` (permission verification)
  - ❌ `AuthenticationService` implementation
  - ❌ `AuthorizationService` implementation
- ✅ `Services/Audit/`
  - ✅ `IAuditService` + `AuditService` (change tracking)
- ✅ `Services/`
  - ✅ `ISettingsService` + `SettingsService` (dynamic configuration)

### Phase 11: Update MetadataDbContext
- ✅ Add DbSets for all new entities
- ✅ Configure relationships in `OnModelCreating`
- ✅ Indexes and constraints

### Phase 12: Create Migrations
- ✅ Initial migration with all tables
- ✅ MetadataDbContextFactory for design-time
- ✅ Configure object properties as Ignore (handled at runtime)

---

## 🚧 Pending

### Phase 8: RabbitMQ
- ❌ Add `RabbitMQ.Client` package
- ❌ Create `Messaging/` folder
- ❌ Define messages/events
  - `EmailQueuedMessage`
  - `NotificationQueuedMessage`
  - `WorkflowTriggeredMessage`
- ❌ `IMessagePublisher` interface
- ❌ `RabbitMQPublisher` implementation
- ❌ `MessageConsumerBase` base class for workers

### Phase 10: Repositories
- ✅ `IUserRepository` + `UserRepository`
- ✅ `IRoleRepository` + `RoleRepository`
- ✅ `IPermissionRepository` + `PermissionRepository`
- ✅ `IModuleRepository` + `ModuleRepository`
- ✅ `IWorkflowRepository` + `WorkflowRepository`
- ✅ `INotificationTemplateRepository` + `NotificationTemplateRepository`
- ✅ `IEmailTemplateRepository` + `EmailTemplateRepository`

### Services (Not Started)
- ❌ `Services/Modules/`
  - `IModuleLoader` - Load DLL modules
  - `IModuleValidator` - Validate dependencies
- ❌ `Services/Notifications/`
  - `INotificationService` (publishes to RabbitMQ)
- ❌ `Services/Email/`
  - `IEmailService` (publishes to RabbitMQ)
- ❌ `Services/Workflow/`
  - `IWorkflowEngine` - Execute workflows

### Phase 13: Workers (Separate Projects)
- ❌ Create `MetaForge.Workers` project
- ❌ `EmailWorker` - Consumes email queue
- ❌ `NotificationWorker` - Consumes notification queue
- ❌ `WorkflowWorker` - Executes workflows

### Projects (Not Started)
- ❌ `MetaForge.API` - REST WebAPI
- ❌ `MetaForge.Designer` - Blazor for admins/designers
- ❌ `MetaForge.App` - Blazor for end users

---

## 📋 Notes

- **PostgreSQL Only**: Only PostgreSQL supported to simplify maintenance
- **RabbitMQ**: All queues go through RabbitMQ, not database
- **Workers**: Background processes separate from Core
- **Schema**: Everything in `metaforge` schema
- **No BCrypt**: Using native PBKDF2 from System.Security.Cryptography
- **Environment Variables**: Only DB connection and JWT_SECRET_KEY required
- **OAuth Providers**: Configured in SystemSettings database table

---

## 🎯 Next Steps

**Current**: Phase 10 - Repositories (Data Access Layer)

**Upcoming**:
1. Implement AuthenticationService and AuthorizationService
2. Phase 8: RabbitMQ - Messaging infrastructure
3. MetaForge.API - REST WebAPI
4. MetaForge.Designer - Blazor admin application

## ⚙️ Refactoring Notes

- ✅ BCrypt removed, using native PBKDF2 (System.Security.Cryptography)
- ✅ Configuration via environment variables (Environment.GetEnvironmentVariable)
- ✅ OAuth providers configurable in SystemSettings (not environment variables)
- ✅ ISettingsService for dynamic configuration from database
- ✅ JWT_SECRET_KEY as only sensitive environment variable
- ✅ Two-database architecture documented (system + app)
- ✅ Root authentication: Direct PostgreSQL (no Users table for admin)
- ✅ End user authentication: JWT + application-level permissions
