# Sales Management API

API REST desarrollada con **ASP.NET Core** para gestionar ventas, clientes, productos, cuentas corrientes, pagos y eventos de calendario.

Este proyecto fue creado como práctica para aplicar **arquitectura por capas, diseño de APIs REST y modelado de dominio**.

---

## Tecnologías

- ASP.NET Core
- Entity Framework Core
- SQLite
- JWT Authentication
- FluentValidation
- Docker
- Docker Compose
- Serilog

---

## Arquitectura

El proyecto está organizado en las siguientes capas:

- **Domain** → Entidades y reglas de negocio  
- **Application** → Casos de uso, DTOs y validaciones  
- **Infrastructure** → Repositorios y acceso a datos  
- **API** → Controllers y configuración de la aplicación  

---

## Funcionalidades

### Clientes

- Crear clientes  
- Consultar clientes  
- Ver cuenta corriente  
- Registrar movimientos manuales  

### Ventas

- Crear ventas  
- Registrar pagos  
- Cancelar ventas  
- Control de stock  

### Cuenta corriente

- Generación automática de deuda al crear una venta  
- Registro de pagos  
- Movimientos manuales  
- Balance calculado a partir del historial de movimientos  

### Calendario

- Crear eventos  
- Obtener eventos por rango de fechas  
- Actualizar eventos  
- Eliminar eventos  

---

## Ejemplos de endpoints

```
POST   /sales
POST   /sales/{id}/payments
PATCH  /sales/{id}/cancel

GET    /customers
GET    /customers/{id}/account
POST   /customers/{id}/account-movements

GET    /calendar-events?start=2026-03-01&end=2026-03-31
POST   /calendar-events
PUT    /calendar-events/{id}
DELETE /calendar-events/{id}
```

---

## Ejecutar el proyecto

### Con Docker

```
docker compose up --build
```

### Desarrollo local

```
docker compose up -d
dotnet run
```

---
## Variables de entorno

La aplicación requiere las siguientes variables de entorno.  
Puedes configurarlas en un archivo `.env`.

```
ADMIN_EMAIL=admin@example.com
ADMIN_PASSWORD=YourStrongPassword!
CONNECTION_STRING=Data Source=salesmanagement.db
APP_TOKEN=your_secret_token_here
```


## Autor

Federico Ramos
