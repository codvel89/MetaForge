# MetaForge.Core - Refactorización en Progreso

## ✅ Completado

### Fase 1: Limpieza
- ✅ Removido soporte para MySQL y SQL Server
- ✅ Solo PostgreSQL como proveedor
- ✅ Actualizado `DatabaseProvider` enum
- ✅ Simplificado `DbContextFactory`

### Fase 2: Reorganización
- ✅ Creada nueva estructura de carpetas
- ✅ Movido `MetadataDbContext` a `Context/`
- ✅ Movido repositorios a `Repositories/`
- ✅ Movido `DbContextFactory` a `Factories/`
- ✅ Actualizado namespaces

---

## 🚧 Pendiente

### Fase 3: Entidades del Sistema
- ✅ `Migration.cs` - Historial de migraciones
- ✅ `Module.cs` - Módulos instalados
- ✅ `ModuleDependency.cs` - Dependencias entre módulos
- ✅ `SystemSetting.cs` - Configuración global

### Fase 4: Entidades de Seguridad
- ✅ `User.cs` - Usuarios del sistema
- ✅ `Role.cs` - Roles
- ✅ `Permission.cs` - Permisos granulares
- ✅ `UserRole.cs` - Relación usuarios-roles
- ✅ `RolePermission.cs` - Relación roles-permisos
- ✅ `ApiKey.cs` - Claves de API

### Fase 5: Entidades de Auditoría
- ✅ `AuditLog.cs` - Registro de cambios
- ❌ `EntityHistory.cs` - Historial de entidades (OPCIONAL)

### Fase 6: Entidades de Notificación
- ✅ `NotificationTemplate.cs` - Plantillas de notificaciones
- ✅ `EmailTemplate.cs` - Plantillas de email

### Fase 7: Entidades de Workflow
- ✅ `WorkflowDefinition.cs` - Definiciones de workflows
- ✅ `WorkflowInstance.cs` - Instancias en ejecución
- ✅ `WorkflowStep.cs` - Pasos de workflow

### Fase 8: RabbitMQ
- ❌ Agregar paquete `RabbitMQ.Client`
- ❌ Crear carpeta `Messaging/`
- ❌ Definir mensajes/eventos
  - `EmailQueuedMessage`
  - `NotificationQueuedMessage`
  - `WorkflowTriggeredMessage`
- ❌ `IMessagePublisher` interface
- ❌ `RabbitMQPublisher` implementation
- ❌ `MessageConsumerBase` clase base para workers

### Fase 9: Servicios Core
- ❌ `Services/Security/`
  - `IAuthenticationService`
  - `IAuthorizationService`
  - `IPasswordHasher`
  - `IJwtTokenService`
- ❌ `Services/Modules/`
  - `IModuleLoader`
  - `IModuleValidator`
- ❌ `Services/Notifications/`
  - `INotificationService` (publica a RabbitMQ)
- ❌ `Services/Email/`
  - `IEmailService` (publica a RabbitMQ)
- ❌ `Services/Audit/`
  - `IAuditService`
- ❌ `Services/Workflow/`
  - `IWorkflowEngine`

### Fase 10: Repositorios
- ❌ `IModuleRepository`
- ❌ `IUserRepository`
- ❌ `IRoleRepository`
- ❌ `IPermissionRepository`
- ❌ `IAuditLogRepository`
- ❌ `IWorkflowRepository`

### Fase 11: Actualizar MetadataDbContext
- ✅ Agregar DbSet para todas las nuevas entidades
- ✅ Configurar relaciones en `OnModelCreating`
- ✅ Índices y constraints

### Fase 12: Crear Migraciones
- ✅ Primera migración con todas las tablas
- ✅ MetadataDbContextFactory para design-time
- ✅ Configurar propiedades object como Ignore (se manejan en runtime)

### Fase 13: Workers (proyecto separado)
- ❌ Crear `MetaForge.Workers` project
- ❌ `EmailWorker` - Consume cola de emails
- ❌ `NotificationWorker` - Consume cola de notificaciones
- ❌ `WorkflowWorker` - Ejecuta workflows

---

## 📋 Notas

- **PostgreSQL Only**: Solo soportamos PostgreSQL para simplificar mantenimiento
- **RabbitMQ**: Todas las colas van por RabbitMQ, no en DB
- **Workers**: Procesos en background separados del Core
- **Esquema**: Todo en esquema `metaforge`

---

## 🎯 Próximo Paso

Fase 8: RabbitMQ - Agregar infraestructura de mensajería
Fase 9: Servicios Core - Implementar servicios básicos
Fase 10: Repositorios - Crear repositorios para las entidades
