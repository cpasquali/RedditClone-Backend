# Proyecto Clon de Reddit con ASP.NET Core Web API

Este proyecto consiste en una API desarrollada con ASP.NET Core que emula las funcionalidades básicas de Reddit, incluyendo posteos, usuarios, comentarios, likes, autenticación de usuarios (login y registro), entre otras características.

## Paquetes NuGet Utilizados

### AutoMapper
AutoMapper facilita la proyección de objetos de un tipo a otro automáticamente, útil para transformar entidades de base de datos en modelos de vista y viceversa.

### BCrypt.Net-Next
Librería utilizada para el hashing seguro de contraseñas utilizando el algoritmo BCrypt, asegurando el almacenamiento seguro de las contraseñas de los usuarios.

### Microsoft.AspNet.WebApi.Core
Contiene componentes esenciales para la creación de aplicaciones web API en ASP.NET, proporcionando funcionalidad para crear servicios HTTP que pueden ser consumidos por diversos clientes.

### Microsoft.AspNetCore.Authentication.JwtBearer
Permite la autenticación basada en tokens JWT (JSON Web Token) en aplicaciones ASP.NET Core, asegurando la autenticación y autorización de usuarios de manera eficiente y segura.

### Microsoft.EntityFrameworkCore.SqlServer
Permite a Entity Framework Core trabajar con bases de datos SQL Server, simplificando las operaciones CRUD y la comunicación con la base de datos.

### Microsoft.EntityFrameworkCore.Tools
Herramientas para el desarrollo de aplicaciones con Entity Framework Core, incluyendo la generación de migraciones de bases de datos y el mantenimiento del esquema de la base de datos.

### Swashbuckle.AspNetCore
Integración de Swagger en aplicaciones ASP.NET Core para documentar y probar la API de manera interactiva, generando una interfaz web que describe todas las operaciones disponibles en la API.

### System.IdentityModel.Tokens.Jwt
Funcionalidades para la creación, lectura y validación de tokens JWT en aplicaciones .NET, fundamental para la implementación de autenticación basada en tokens en aplicaciones web y servicios.

## Diagrama de Entidad-Relación (ER) de la Base de Datos

![image](https://github.com/cpasquali/RedditClone-Backend/assets/163750560/846292a4-547c-40ba-9533-1e88e0a41110)

