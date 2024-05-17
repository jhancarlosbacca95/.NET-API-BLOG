
# API-BLOG

API-BLOG es una API construida con .NET 8 que permite la gestión de Posts. Incluye características como gestión de usuarios, roles y permisos, autenticación y autorización usando JWT, así como la generación y gestión de tokens y refresh tokens.

## Tabla de Contenidos

- [Características](#características)
- [Requisitos](#requisitos)
- [Instalación](#instalación)
- [Configuración](#configuración)
- [Uso](#uso)


## Características

- Gestión de usuarios con roles (Admin, SuperAdmin, Usuario)
- Autenticación y autorización usando JWT
- Generación y manejo de tokens y refresh tokens
- Endpoints protegidos por roles
- CRUD de blogs, etiquetas y comentarios

## Requisitos

- .NET 8 SDK
- SQL Server
- Visual Studio o Visual Studio Code

## Instalación

1. Clona el repositorio:
    ```bash
    git clone https://github.com/tu-usuario/api-blog.git
    ```

2. Navega al directorio del proyecto:
    ```bash
    cd api-blog
    ```

3. Restaura las dependencias de NuGet:
    ```bash
    dotnet restore
    ```

## Configuración

1. Configura la cadena de conexión a la base de datos en `appsettings.json`:
    ```json
    {
      "ConnectionStrings": {
        "conexion": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
      },
      "JwtSettings": {
        "key": "your_secret_key"
      }
    }
    ```

2. Asegúrate de haber configurado las migraciones y la base de datos correctamente:
    ```bash
    dotnet ef database update
    ```

## Uso

Para ejecutar el proyecto, usa el siguiente comando:
```bash
dotnet run
