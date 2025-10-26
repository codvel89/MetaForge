# MetaForge

MetaForge is a dynamic database management system built on .NET 9.0, designed to allow creation, modification, and management of database schemas at runtime without needing to recompile the application.

## 🎯 Objective

Create a flexible platform that allows users to define and manage complex data structures dynamically, with the following capabilities:

### Main Features

- **Dynamic Schema Management**: Create and modify tables, fields, and relationships without recompilation
- **PostgreSQL Database**: Focused support for PostgreSQL as the primary database provider
- **Multi-tenant Architecture**: Separate system and application databases for clean separation of concerns
- **Plugin System**: Hot-loadable modules (DLLs) with marketplace support for distribution
- **Module Marketplace**: Version-controlled module repository with creator attribution
- **Extensibility**: Easy to extend through modules without modifying the base code

### System Capabilities

#### Metadata Definition
- Tables with complete metadata (name, description, icons, permissions)
- Fields with flexible data types and custom validations
- Complex indexes and relationships (1:1, 1:N, N:M)
- Computed fields with dynamic expressions
- Customizable automatic sequences

#### Validation and Business Rules
- Type-based validations (Regex, Email, Range, Custom, ConditionalRegex)
- Business rules with triggers (BeforeInsert, AfterInsert, BeforeUpdate, AfterUpdate)
- Controlled state transitions
- Automated actions (notifications, workflows, field updates)

#### Dynamic User Interface
- Complete form configuration
- Multiple UI components (Input, Select, TextArea, DatePicker, FileUpload, etc.)
- Configurable list views with filters, search, and aggregations
- Dynamic grouping and sorting
- Data export in multiple formats

#### Permissions System
- Granular access control per table (Create, Read, Update, Delete)
- Role-based permissions
- Conditional read-only fields

#### Audit and Traceability
- Optional change auditing per table
- Soft delete support
- Workflow integration

## 🏗️ Architecture

### Technologies
- **.NET 9.0**: Base framework
- **Entity Framework Core 9.0.10**: ORM for dynamic data access
- **Central Package Management (CPM)**: Centralized dependency management
- **Nullable Reference Types**: Enabled for better type safety
- **Implicit Usings**: Simplified imports

### Database Provider
- **PostgreSQL** (Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4) - Primary and only supported provider

### Project Structure

```
MetaForge/
├── src/
│   ├── MetaForge.Shared/              # Shared system models
│   │   ├── DatabaseConnection.cs       # Connection management
│   │   ├── TableDefinition.cs          # Table definition
│   │   ├── ColumnDefinition.cs         # Column definition
│   │   ├── RelationDefinition.cs       # Relationships between tables
│   │   ├── ValidationRule.cs           # Validation rules
│   │   ├── BusinessRule.cs             # Business rules
│   │   ├── TriggerDefinition.cs        # Triggers
│   │   ├── FormViewConfig.cs           # Form configuration
│   │   ├── ListViewConfig.cs           # List configuration
│   │   └── ...                         # Other supporting models
│   │
│   └── MetaForge.Core/                 # Main system engine
│       ├── Context/
│       │   ├── DynamicDbContext.cs     # EF Core dynamic DbContext
│       │   ├── DynamicModelBuilder.cs  # EF Core model builder
│       │   ├── DbContextFactory.cs     # Context factory
│       │   └── MetadataDbContext.cs    # Metadata database context
│       │
│       ├── Entities/                   # Core system entities
│       │   ├── System/                 # System entities (Migration, SystemSetting)
│       │   ├── Security/               # Security entities (User, Role, Permission, ApiKey)
│       │   ├── Audit/                  # Audit entities (AuditLog)
│       │   ├── Notification/           # Notification entities (NotificationTemplate, EmailTemplate)
│       │   └── Workflow/               # Workflow entities (WorkflowDefinition, WorkflowInstance, WorkflowStep)
│       │
│       ├── Repositories/               # Data access repositories
│       │   ├── IMetadataRepository.cs  # Metadata repository interface
│       │   └── MetadataRepository.cs   # Metadata repository implementation
│       │
│       ├── Services/                   # Business logic services
│       │
│       └── Messaging/                  # RabbitMQ messaging infrastructure
│
├── Directory.Packages.props            # NuGet package versions
└── MetaForge.sln                       # Main solution
```

## 🚀 Use Cases

1. **Customizable ERP Systems**: Allows each client to customize tables and fields according to their specific needs
2. **Multi-tenant Platforms**: Each tenant can have their own data schema
3. **Low-Code Applications**: Generate complete CRUD applications without programming
4. **Complex Configuration Management**: Systems that require highly configurable data structures
5. **Rapid Prototyping**: Agile development of data management applications

## 🎨 Design Principles

- **One Object per File**: Each class, enum, or interface in its own file
- **Spanish Documentation**: All elements documented with XML comments in Spanish (code standard)
- **Multilanguage Support**: The frontend will support multiple languages (the core is language-agnostic)
- **Clean Architecture**: Clear separation between system and application databases
- **Extensibility**: Easy to extend without modifying the base code

## 📋 Project Status

### ✅ Completed
- Main metadata models (26+ classes)
- Validation and business rules system
- Form and list configuration
- Database connection definitions
- **Dynamic DbContext with Entity Framework Core 9.0.10**
- **PostgreSQL exclusive support**
- **EF Core model builder from metadata**
- **Core system entities**: System, Security, Audit, Notification, Workflow
- **Metadata repository** with EF Core implementation
- **Multi-database architecture**: Separate system and application databases

### 🚧 In Development
- Generic CRUD repository
- REST API for data management
- Automatic migrations from metadata

### 📅 Roadmap
- **Module System** (High Priority):
  - DLL-based plugin architecture with hot-loading
  - Module template/SDK for third-party development
  - Module installation: upload DLL, parse metadata, generate schema
  - Module marketplace with versioning and creator management
  - Dependency resolution between modules
  - Module activation/deactivation without system restart
- Expression engine for computed fields
- Workflow system execution
- Dynamic UI generator
- Web client (frontend)
- Migration tools
- Worker services for messaging (Email, Push Notifications, WebSocket, SMS)

## 🛠️ Development

### Requirements
- .NET 9.0 SDK
- PostgreSQL 12+
- Compatible editor (Visual Studio, VS Code, Rider)

### Commands

```bash
# Restore dependencies
dotnet restore MetaForge.sln

# Build
dotnet build MetaForge.sln

# Build in Release
dotnet build MetaForge.sln --configuration Release

# Clean
dotnet clean MetaForge.sln
```

### Supported Platforms
- Any CPU (default)
- x64
- x86

## 📄 License

To be defined

## 👥 Contributing

This is an early-stage project. Contributions will be welcome once contribution guidelines are established.

---

**MetaForge** - Forging dynamic data with precision
