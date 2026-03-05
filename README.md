# Api-ColombiaApi-Colombia 

Es una API RESTful desarrollada con .NET 8 diseñada para la gestión integral de las regiones de Colombia. El sistema permite realizar operaciones CRUD, sincronizar datos con servicios externos y garantiza la seguridad mediante autenticación JWT (JSON Web Token).

# Arquitectura del Proyecto

El proyecto sigue un patrón de arquitectura en capas, asegurando una separación de responsabilidades clara y facilitando el mantenimiento:

### API / Controllers: Exposición de endpoints REST y manejo de peticiones HTTP.

### BL (Business Layer): Orquestación de lógica de negocio y validaciones.

### DAL (Data Access Layer): Persistencia y comunicación directa con SQL Server.

### Services: Integración con servicios de terceros (sincronización de datos).

### Security / Auth: Gestión de identidad y protección de rutas con JWT.

### Middleware: Manejo global de excepciones para respuestas estandarizadas.

## Tecnologías UtilizadasFramework: 
1. .NET 8 (C#)
2. ORM: Entity Framework Core 8
3. Base de Datos: SQL Server
4. Seguridad: JWT & HttpClientFactory
5. Documentación: Swagger / OpenAPI
6. Patrones: Dependency Injection (DI) con ciclo de vida Scoped.

## Instalación y Configuración
1. Requisitos previos
    * Visual Studio 2022 o VS Code.
    * SQL Server (Local o Remoto). 
    * SDK de .NET 8.

2. Base de Datos
    * Ubica el archivo ScriptApiColombia.sql en la raíz del proyecto.
    * Ejecútalo en tu instancia de SQL Server.
    * Credenciales de prueba predefinidas:  Username: admin y Password: 123

3. Pasos para ejecutar
    * Clonar el repositorio
        git clone https://github.com/andreslc2803/Api-Colombia.git
        cd Api-Colombia

4. Restaurar paquetes NuGet
dotnet restore

5. Configurar ConnectionString
Edita el archivo appsettings.json con tus credenciales de SQL Server.

5. Iniciar la API
dotnet run

## Endpoints Principales

Autenticación
Método      Endpoint            Descripción 
POST        /api/Auth/login     Recibe credenciales y devuelve el JWT.Cuerpo de la petición (JSON):

{
  "username": "admin",
  "password": "123"
}

## Gestión de Regiones (Requiere JWT)

Todos los endpoints siguientes requieren el header: Authorization: Bearer {tu_token}.

GET     /api/Regions        Obtiene el listado completo de regiones.
GET     /api/Regions/{id}   Busca una región específica por su ID.
POST    /api/Regions        Crea una nueva región.
PUT     /api/Regions/{id}   Actualiza una región existente.
DELETE  /api/Regions/{id}   Elimina una región del sistema.
POST    /api/Regions/sync   Sincroniza datos con la API externa.

## Observaciones Técnicas

Ciclo de Vida: Los servicios y repositorios se inyectan como Scoped para mantener la coherencia con el DbContext.
Manejo de Errores: Se utiliza un Middleware personalizado que captura excepciones y devuelve un formato de error amigable al cliente.Validaciones: Cada operación CRUD incluye validaciones de integridad de datos antes de impactar la base de datos.

## Autor
Desarrollado por Andrés Londoño Carvajal.