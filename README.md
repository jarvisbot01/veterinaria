# Veterinaria API en ASPNet 7.0

## Introducción al Proyecto

Bienvenido al emocionante mundo de PetShop API en ASP.Net 7.0. Este proyecto representa una solución completa para la gestión de una tienda de mascotas, abarcando desde la autenticación y autorización hasta la persistencia de datos y la interacción con una base de datos.

## Arquitectura del Proyecto

La arquitectura de PetShop API sigue los principios sólidos de diseño y buenas prácticas de desarrollo. Se estructura en capas para garantizar la modularidad, la flexibilidad y la facilidad de mantenimiento. Las principales capas son:

1. **Dominio (Domain):** Aquí se definen las entidades de dominio, que representan los conceptos clave del negocio, así como las interfaces y contratos necesarios para la implementación de la capa de infraestructura.

2. **Persistencia (Persistence):** Esta capa se encarga de la interacción con la base de datos. Aquí encontrarás el contexto de Entity Framework, las configuraciones de las entidades y las migraciones de base de datos.

3. **Aplicación (Application):** En esta capa se implementan las clases concretas de las interfaces definidas en el dominio. Incluye la implementación de repositorios, la unidad de trabajo y otros servicios de infraestructura.

4. **Presentación (Api):** Esta capa gestiona las solicitudes HTTP, maneja la autenticación y autorización mediante JWT, y orquesta las operaciones del dominio.

    - **DTOs (Data Transfer Objects):** Los DTOs facilitan la transferencia de datos entre capas y actúan como mensajes entre la capa de presentación y la capa de dominio.

    - **Controladores:** En esta subcapa se encuentran los controladores que manejan las solicitudes HTTP, interactúan con la capa de aplicación y devuelven las respuestas adecuadas.

## Contenido

1. [Implementación de JWT en ASP.NET Core](#implementación-de-jwt-en-aspnet-core)
   - [Generación de Clave y Credenciales](#generación-de-clave-y-credenciales)
2. [Configuración del Entity Framework y el Contexto](#configuración-del-entity-framework-y-el-contexto)
   - [Clase PetShopContext](#clase-petshopcontext)
3. [Migraciones y Actualización de la Base de Datos](#migraciones-y-actualización-de-la-base-de-datos)
4. [Patrones de diseño en PetShop API](#patrones-de-diseño-en-petshop-api)
   - [Unidad de Trabajo (UnitOfWork)](#unidad-de-trabajo-unitofwork)
   - [Repositorios](#repositorios)
   - [DTOs (Data Transfer Objects)](#dtos-data-transfer-objects)
5. [Controladores](#controladores)
6. [Servicios Adicionales](#servicios-adicionales)
   - [Rate Limit](#rate-limit)
   - [CORS (Cross-Origin Resource Sharing)](#cors-cross-origin-resource-sharing)
   - [Serilog](#serilog)
7. [Consulta de los Endpoints](#consulta-de-los-endpoints)
8. [License](#license)

## Implementación de JWT en ASP.NET Core

En este proyecto, se utiliza JSON Web Token (JWT) para autenticación y autorización en ASP.NET Core. A continuación, se presenta una descripción detallada del proceso.

### Generación de Clave y Credenciales

Para generar una clave secreta y credenciales para la firma JWT, se utiliza la clase `SymmetricSecurityKey` de ASP.NET Core. Esto asegura la integridad y seguridad del token.

```code
var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
var signingCredentials = new SigningCredentials(
    symmetricSecurityKey,
    SecurityAlgorithms.HmacSha256
);
```

**Configuración del appsettings.json para JWT:**

```json
{
  "JWT": {
    "Key": "njMCY^UbEskeAFL6eDzHuqY!s^x6Qrwe",
    "Issuer": "MyStoreApi",
    "Audience": "MyStoreApiUser",
    "DurationInMinutes": 1
  }
}
```

## Configuración del Entity Framework y el Contexto

A continuación, se presenta la configuración del Entity Framework y el contexto de la base de datos.

### Clase PetShopContext

```csharp
using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Entity> Entities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
```

**Configuración del appsettings.json para Entity Framework:**

```json
{
  "ConnectionStrings": {
    "ConexMysql": "server=beautykatherin.com;user=jose;password=jmKuHmQGyGwb;database=petshop"
  }
}
```

## Migraciones y Actualización de la Base de Datos

Para configurar tu base de datos, ejecuta los siguientes comandos en la terminal:

```bash
dotnet ef migrations add InitialCreate --project ./Persistence/ --startup-project ./Api/ --output-dir ./Data/Migrations

dotnet ef database update --project ./Persistence/ --startup-project ./Api/
```

Estos comandos generan una migración inicial y actualizan la base de datos según el contexto y las entidades definidas en tu proyecto.

## Patrones de diseño en PetShop API

En este documento se describen los patrones de diseño utilizados en la PetShop API. Estos patrones contribuyen a un diseño modular y mantenible al proporcionar una separación clara de responsabilidades, facilitar la implementación de operaciones de base de datos y optimizar la transferencia de datos entre las capas de la aplicación.

### Unidad de Trabajo (UnitOfWork)

La Unidad de Trabajo es un patrón de diseño que agrupa todas las operaciones de lectura y escritura en una única transacción. En el contexto de una base de datos, representa una sesión que mantiene el estado de las entidades y coordina su persistencia.

#### Función en PetShop API:

En el proyecto, la Unidad de Trabajo, implementada a través de la interfaz IUnitOfWork, se encarga de gestionar las transacciones en la base de datos. Permite agrupar varias operaciones en una única transacción, lo que asegura la coherencia y la integridad de la base de datos.

```csharp
using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PetShopContext _context;

        private IUser _users;

        public UnitOfWork(PetShopContext context)
        {
            _context = context;
        }

        public IUser Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_context);
                }
                return _users;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
```

## Repositorios

Los repositorios son patrones de diseño que encapsulan la lógica de acceso a los datos y proporcionan una interfaz para interactuar con la capa de persistencia. Cada entidad en la base de datos tiene su propio repositorio.

#### Función en PetShop API:

Los repositorios en PetShop API implementan la interfaz IRepository<T> definida en el dominio. Proporcionan métodos para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en la base de datos para entidades específicas, como Appointment, Breed, entre otras. Los repositorios abstraen la complejidad de las operaciones de base de datos y permiten interactuar con ellas de manera sencilla.

```csharp
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class GenericRepository<T> : IGeneric<T>
        where T : BaseEntity
    {
        private readonly PetShopContext _context;

        public GenericRepository(PetShopContext context)
        {
            _context = context;
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public virtual async Task<(int totalRecords, IEnumerable<T> records)> GetAllAsync(
            int pageIndex,
            int pageSize,
            string search
        )
        {
            var totalRecords = await _context.Set<T>().CountAsync();
            var records = await _context
                .Set<T>()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (totalRecords, records);
        }
    }
}
```

## DTOs (Data Transfer Objects)

Los DTOs son objetos utilizados para transferir datos entre capas de una aplicación. Se utilizan para encapsular la información y reducir la cantidad de datos transferidos entre la capa de presentación y la capa de dominio.

#### Función en PetShop API:

En el proyecto, los DTOs, como EntityDTO, se emplean para estructurar y transferir datos entre la capa de presentación (controladores en la API) y la capa de dominio (entidades y servicios de aplicación). Facilitan la serialización de objetos complejos y permiten una comunicación eficiente entre las diferentes capas de la aplicación.

```csharp
namespace Api.Dtos
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
```

Con estos patrones y capas, PetShop API en ASP.Net 7.0 está diseñado para brindar modularidad, mantenibilidad y escalabilidad. La arquitectura se organiza en capas bien definidas, cada una cumpliendo una función específica para garantizar un desarrollo eficiente y ordenado.

## Controladores

Controladores en ASP.NET Core
Los controladores en ASP.NET Core son clases que manejan las solicitudes HTTP y ejecutan la lógica de la aplicación correspondiente. Se utilizan para:

- **Gestión de Solicitudes HTTP:** Los controladores reciben solicitudes HTTP específicas (como GET, POST, PUT, DELETE) y ejecutan acciones correspondientes a esas solicitudes.

- **Interacción con Modelos y Vistas:** Los controladores trabajan con modelos y vistas para procesar y presentar datos. La lógica de la aplicación suele residir en los controladores, y estos interactúan con los modelos y las vistas para proporcionar una respuesta completa al cliente.

- **Enrutamiento:** ASP.NET Core utiliza enrutamiento para asociar solicitudes HTTP con controladores y acciones específicas. Los controladores juegan un papel importante en este proceso al definir acciones que responden a rutas específicas.

- **Inyección de Dependencias:** Los controladores admiten la inyección de dependencias, lo que facilita la prueba unitaria y la creación de aplicaciones más modulares y mantenibles.

- **Separación de Preocupaciones:** La arquitectura de ASP.NET Core fomenta la separación de preocupaciones. Los controladores se centran en la lógica de la aplicación, mientras que las vistas manejan la presentación y los modelos representan los datos.

- **Operaciones CRUD:** Los controladores son comúnmente utilizados para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en entidades de la aplicación.

## Servicios Adicionales

Además de las funcionalidades básicas proporcionadas por los controladores, PetShop API en ASP.Net 7.0 incluye servicios adicionales para mejorar la seguridad y la eficiencia. Uno de estos servicios es:

### Rate Limit

El servicio de Rate Limit se encarga de controlar la frecuencia con la que se pueden realizar solicitudes a la API. Implementa límites de velocidad para prevenir el uso excesivo y proteger la API contra posibles ataques de fuerza bruta o abuso.

#### Funcionalidades Principales:

- **Control de Frecuencia:** Limita la cantidad de solicitudes que un cliente puede realizar en un intervalo de tiempo específico.

- **Protección contra Ataques:** Ayuda a prevenir ataques de fuerza bruta y garantiza un uso equitativo de los recursos de la API.

- **Configuración Personalizada:** Permite ajustar los límites de velocidad según las necesidades específicas de la aplicación.

El servicio de Rate Limit se integra directamente en la capa de controladores, asegurando que todas las solicitudes se sometan a las restricciones establecidas, proporcionando así una capa adicional de seguridad y eficiencia a PetShop API.

```csharp
public static void ConfigureRateLimiting(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();
            services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = true;
                options.StackBlockedRequests = true;
                options.HttpStatusCode = 429;
                options.RealIpHeader = "X-real-ip";
                options.GeneralRules = new List<RateLimitRule>
                {
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Period = "10s",
                        Limit = 5
                    }
                };
            });
        }
```

### CORS (Cross-Origin Resource Sharing)

El servicio CORS permite o restringe las solicitudes HTTP que se pueden realizar a la API desde dominios distintos al dominio de origen. Esto es esencial para garantizar la seguridad del navegador y controlar el acceso a recursos de la API desde diferentes orígenes.

#### Funcionalidades Principales:

- **Control de Acceso:** Define políticas para permitir o restringir solicitudes desde dominios específicos.

- **Configuración Flexible:** Ofrece configuraciones flexibles para adaptarse a los requisitos específicos de la aplicación.

- **Prevención de Ataques:** Ayuda a prevenir solicitudes malintencionadas y protege la API contra vulnerabilidades de seguridad.

```csharp
public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );
            });
```

### Serilog

Serilog es un servicio de registro que proporciona una forma flexible y estructurada de registrar información sobre el funcionamiento de la aplicación. Facilita el seguimiento y la solución de problemas al tiempo que ofrece opciones de salida personalizables.

#### Funcionalidades Principales:

- **Registros Estructurados:** Permite crear registros estructurados para facilitar la búsqueda y el análisis.

- **Múltiples Destinos:** Admite la configuración de múltiples destinos para los registros, como archivos, bases de datos o servicios externos.

- **Niveles de Severidad:** Clasifica los registros en diferentes niveles de severidad para una gestión eficiente de la información.

Ambos servicios, CORS y Serilog, son componentes esenciales para mejorar la seguridad y la visibilidad de PetShop API.

## Consulta de los Endpoints

### 1. Listar todas las mascotas agrupadas por especie:

      Endpoint: `http://localhost:5000/api/pet/consulta1B`

### 2. Listar todos los movimientos de medicamentos y el valor total de cada movimiento:

    Endpoint: `http://localhost:5000/api/medicationmovement/consulta2B`

### 3. Listar las mascotas que fueron atendidas por un determinado veterinario:

      Endpoint: `http://localhost:5000/api/pet/consulta3B`

#### 4. Listar los proveedores que me venden un determinado medicamento:

      Endpoint: `http://localhost:5000/api/supplier/consulta4B`

### 5. Listar las mascotas y sus propietarios cuya raza sea Golden Retriver:

      Endpoint: `http://localhost:5000/api/owner/consulta5B`

### 6. Listar la cantidad de mascotas que pertenecen a una raza:

      Endpoint: `http://localhost:5000/api/pet/consulta6B`

## License

Este proyecto está licenciado bajo la [ GNU General Public License v3.0](https://www.gnu.org/licenses/gpl-3.0.html) - consulta el archivo [LICENSE](LICENSE) para obtener más detalles.
