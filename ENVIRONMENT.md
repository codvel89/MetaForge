# Environment Variables

MetaForge requires the following environment variables to be configured.

## Required Variables

### Database Configuration (System Database)

The system database stores all metadata, user management, security, and system configuration.

```bash
# PostgreSQL connection for system database
DB_SYSTEM_HOST=localhost
DB_SYSTEM_PORT=5432
DB_SYSTEM_NAME=metaforge_system
DB_SYSTEM_USER=postgres
DB_SYSTEM_PASSWORD=your_secure_password_here
```

**Important Notes:**
- `DB_SYSTEM_USER` is the root/admin user for the designer application
- This user must have CREATE DATABASE privileges to create application databases
- Designer authentication is done directly against PostgreSQL (no Users table for root)

### JWT Configuration

```bash
# JWT secret key for token signing (minimum 32 characters)
JWT_SECRET_KEY=your-super-secret-key-minimum-32-chars-long
```

**Security Notes:**
- This key should be kept secret and never committed to source control
- Generate a strong random key for production
- Changing this key will invalidate all existing tokens

## Optional Configuration (Stored in Database)

The following configurations are stored in the `SystemSettings` table and can be modified through the Designer UI:

### JWT Settings

```
Key: jwt.issuer
Value: MetaForge
Description: JWT token issuer

Key: jwt.audience  
Value: MetaForge.API
Description: JWT token audience

Key: jwt.expiration_minutes
Value: 60
Description: Token expiration time in minutes
```

### OAuth Providers

#### Google OAuth

```
Key: oauth.google.enabled
Value: true/false
Description: Enable Google OAuth login

Key: oauth.google.client_id
Value: your-client-id.apps.googleusercontent.com
Description: Google OAuth client ID

Key: oauth.google.client_secret
Value: GOCSPX-xxxxxxxx
Description: Google OAuth client secret (encrypted)
```

#### Microsoft OAuth

```
Key: oauth.microsoft.enabled
Value: true/false
Description: Enable Microsoft OAuth login

Key: oauth.microsoft.client_id
Value: your-app-id
Description: Microsoft OAuth application ID

Key: oauth.microsoft.client_secret
Value: your-secret
Description: Microsoft OAuth client secret (encrypted)
```

#### Facebook OAuth

```
Key: oauth.facebook.enabled
Value: true/false
Description: Enable Facebook OAuth login

Key: oauth.facebook.client_id
Value: your-app-id
Description: Facebook app ID

Key: oauth.facebook.client_secret
Value: your-app-secret
Description: Facebook app secret (encrypted)
```

## Application Databases

Application databases (for end users) are NOT configured via environment variables. They are created dynamically by the root user through the Designer application and stored in the `DatabaseConnections` table in the system database.

Each application database connection includes:
- Database name
- Host
- Port
- Schema
- Connection credentials

## Architecture Notes

### Two-Database Architecture

1. **System Database** (`metaforge_system`)
   - Stores: Metadata, Users, Roles, Permissions, Modules, etc.
   - Access: Designer/Admin users (root)
   - Authentication: Direct PostgreSQL authentication

2. **Application Database(s)** (Dynamic names)
   - Stores: Application data based on dynamic schema
   - Access: End users (with limited permissions)
   - Authentication: JWT tokens + application-level permissions
   - Created: Dynamically by root user through Designer

### User Permissions

- **Root User**: Full database access, creates schemas, manages everything
- **End Users**: Application-level permissions only (no direct database grants)
- End users connect with a shared PostgreSQL user; permissions are enforced by the API

## Example Configuration

### Development

```bash
export DB_SYSTEM_HOST=localhost
export DB_SYSTEM_PORT=5432
export DB_SYSTEM_NAME=metaforge_dev
export DB_SYSTEM_USER=postgres
export DB_SYSTEM_PASSWORD=postgres
export JWT_SECRET_KEY=dev-secret-key-at-least-32-characters-long
```

### Production

```bash
export DB_SYSTEM_HOST=db.production.com
export DB_SYSTEM_PORT=5432
export DB_SYSTEM_NAME=metaforge_system
export DB_SYSTEM_USER=metaforge_admin
export DB_SYSTEM_PASSWORD=$(cat /secrets/db_password)
export JWT_SECRET_KEY=$(cat /secrets/jwt_secret)
```

## Verification

To verify your configuration is working:

1. Ensure all required environment variables are set
2. Try connecting to the system database with the provided credentials
3. Start the application and check logs for any configuration errors
4. The Designer app should authenticate using the database credentials
5. JWT tokens should be generated successfully after authentication
