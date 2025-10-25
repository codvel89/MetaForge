# MetaForge

MetaForge es un sistema de gestión de bases de datos dinámico y modular construido en .NET 9.0, diseñado para permitir la creación, modificación y gestión de esquemas de base de datos en tiempo de ejecución sin necesidad de recompilar la aplicación.

## 🎯 Objetivo

Crear una plataforma flexible que permita a los usuarios definir y gestionar estructuras de datos complejas de forma dinámica, con las siguientes capacidades:

### Características Principales

- **Sistema Modular con Hot-Loading**: Carga y descarga de módulos en tiempo de ejecución sin reiniciar la aplicación
- **Gestión Dinámica de Esquemas**: Creación y modificación de tablas, campos y relaciones sin recompilación
- **Múltiples Proveedores de Base de Datos**: Soporte para PostgreSQL, MySQL, SQL Server, SQLite y Oracle
- **Sistema de Dependencias**: Los módulos pueden declarar dependencias de campos/tablas requeridos
- **Extensibilidad**: Los módulos pueden extender tablas existentes agregando sus propios campos y relaciones

### Capacidades del Sistema

#### Definición de Metadatos
- Tablas con metadatos completos (nombre, descripción, iconos, permisos)
- Campos con tipos de datos flexibles y validaciones personalizadas
- Índices y relaciones complejas (1:1, 1:N, N:M)
- Campos calculados con expresiones dinámicas
- Secuencias automáticas personalizables

#### Validación y Reglas de Negocio
- Validaciones por tipo (Regex, Email, Range, Custom, ConditionalRegex)
- Reglas de negocio con triggers (BeforeInsert, AfterInsert, BeforeUpdate, AfterUpdate)
- Transiciones de estado controladas
- Acciones automatizadas (notificaciones, workflows, actualizaciones de campos)

#### Interfaz de Usuario Dinámica
- Configuración completa de formularios
- Múltiples componentes de UI (Input, Select, TextArea, DatePicker, FileUpload, etc.)
- Vistas de lista configurables con filtros, búsqueda y agregaciones
- Agrupamiento y ordenamiento dinámico
- Exportación de datos en múltiples formatos

#### Sistema de Permisos
- Control de acceso granular por tabla (Create, Read, Update, Delete)
- Permisos basados en roles
- Campos de solo lectura condicionales

#### Auditoría y Trazabilidad
- Auditoría de cambios opcional por tabla
- Eliminación lógica (soft delete)
- Integración con workflows

## 🏗️ Arquitectura

### Tecnologías
- **.NET 9.0**: Framework base
- **Central Package Management (CPM)**: Gestión centralizada de dependencias
- **Nullable Reference Types**: Habilitado para mayor seguridad de tipos
- **Implicit Usings**: Simplificación de imports

### Estructura del Proyecto

```
MetaForge/
├── src/
│   └── MetaForge.Shared/          # Modelos compartidos del sistema
│       ├── DatabaseConnection.cs   # Gestión de conexiones
│       ├── TableDefinition.cs      # Definición de tablas
│       ├── ColumnDefinition.cs     # Definición de columnas
│       ├── RelationDefinition.cs   # Relaciones entre tablas
│       ├── ValidationRule.cs       # Reglas de validación
│       ├── BusinessRule.cs         # Reglas de negocio
│       ├── TriggerDefinition.cs    # Triggers
│       ├── FormViewConfig.cs       # Configuración de formularios
│       ├── ListViewConfig.cs       # Configuración de listas
│       └── ...                     # Otros modelos de soporte
├── Directory.Packages.props        # Versiones de paquetes NuGet
└── MetaForge.sln                   # Solución principal
```

## 🚀 Casos de Uso

1. **Sistemas ERP Personalizables**: Permite a cada cliente personalizar tablas y campos según sus necesidades específicas
2. **Plataformas Multi-tenant**: Cada tenant puede tener su propio esquema de datos
3. **Aplicaciones Low-Code**: Generación de aplicaciones CRUD completas sin programación
4. **Gestión de Configuraciones Complejas**: Sistemas que requieren estructuras de datos altamente configurables
5. **Prototipado Rápido**: Desarrollo ágil de aplicaciones de gestión de datos

## 🎨 Principios de Diseño

- **Un Objeto por Archivo**: Cada clase, enum o interface en su propio archivo
- **Documentación en Español**: Todos los elementos documentados con XML comments en español
- **Soporte Multilenguaje**: El frontend soportará múltiples idiomas (el core es agnóstico)
- **Modularidad**: Arquitectura completamente modular con carga dinámica
- **Extensibilidad**: Fácil de extender sin modificar el código base

## 📋 Estado del Proyecto

### ✅ Completado
- Modelos de metadatos principales
- Sistema de validaciones
- Configuración de formularios y listas
- Definición de conexiones a base de datos

### 🚧 En Desarrollo
- Motor de ejecución de metadatos
- Sistema de módulos y carga dinámica
- Generadores de esquemas DDL
- API REST para gestión de datos

### 📅 Roadmap
- Motor de expresiones para campos calculados
- Sistema de workflows
- Generador de UI dinámico
- Cliente web (frontend)
- Herramientas de migración

## 🛠️ Desarrollo

### Requisitos
- .NET 9.0 SDK
- Editor compatible (Visual Studio, VS Code, Rider)

### Comandos

```bash
# Restaurar dependencias
dotnet restore MetaForge.sln

# Compilar
dotnet build MetaForge.sln

# Compilar en Release
dotnet build MetaForge.sln --configuration Release

# Limpiar
dotnet clean MetaForge.sln
```

### Plataformas Soportadas
- Any CPU (predeterminado)
- x64
- x86

## 📄 Licencia

Por definir

## 👥 Contribución

Este es un proyecto en fase inicial. Las contribuciones serán bienvenidas una vez que se establezcan las guías de contribución.

---

**MetaForge** - Forjando datos dinámicos con precisión
