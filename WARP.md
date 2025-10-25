# WARP.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

## Project Overview

MetaForge is a .NET 9.0 solution using Central Package Management (CPM) for dependency versioning. This is a new/early-stage project with a shared library structure.

## Build & Development Commands

### Build
```bash
dotnet build MetaForge.sln
```

Build specific configuration:
```bash
dotnet build MetaForge.sln --configuration Release
```

### Restore packages
```bash
dotnet restore MetaForge.sln
```

### Clean build artifacts
```bash
dotnet clean MetaForge.sln
```

### Build specific project
```bash
dotnet build src/MetaForge.Shared/MetaForge.Shared.csproj
```

## Architecture

### Solution Structure
- **MetaForge.sln**: Main solution file supporting Debug/Release builds for Any CPU, x64, and x86
- **Directory.Packages.props**: Central Package Management (CPM) configuration at solution root
- **src/**: Contains all project source code

### Projects
- **MetaForge.Shared**: Shared library project targeting .NET 9.0
  - Target Framework: net9.0
  - Nullable reference types: Enabled
  - Implicit usings: Enabled

### Modular System
The system is designed to be modular with hot-loading/unloading capabilities:
- **Module loading**: Modules can be loaded and unloaded at runtime without restarting the application
- **Module dependencies**: Each module must declare required fields/tables as dependencies. If dependencies are not met, the module cannot be installed
- **Database schema per module**: Each module defines its own tables, fields, and relationships
- **Schema extension**: Modules can extend existing tables by adding their own fields and relationships

## Development Guidelines

### Package Management
This solution uses Central Package Management (CPM). All NuGet package versions are managed centrally in `Directory.Packages.props`. When adding packages:
1. Add version in `Directory.Packages.props`
2. Reference without version in individual `.csproj` files

### C# Language Features
- Nullable reference types are enabled across all projects
- Implicit usings are enabled - common namespaces are automatically imported

### Code Organization
- **One object per file**: Each class, enum, interface, or record must be in its own file
- File names should match the type name (e.g., `Customer.cs` for `Customer` class)

### Documentation Standards
- All code elements (classes, interfaces, enums, properties, methods, etc.) must include XML documentation comments in Spanish
- Use `/// <summary>` tags to describe the purpose of each element

Example:
```csharp
/// <summary>
/// Representa un cliente en el sistema
/// </summary>
public class Customer
{
    /// <summary>
    /// Identificador único del cliente
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre completo del cliente
    /// </summary>
    public string FullName { get; set; }
}
```

### Multilanguage Support
- The frontend (UI) of the core will support multiple languages
- The core library itself remains language-agnostic
- Consider creating localization/resource classes for translatable content
- Keep business logic separate from localized strings

### Platform Support
The solution supports multiple platform configurations:
- Any CPU (default)
- x64
- x86

Build for specific platform:
```bash
dotnet build MetaForge.sln --configuration Debug -p:Platform=x64
```
