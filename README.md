Proyecto Clon de Reddit con ASP.NET Core Web API

Este proyecto consiste en una API desarrollada con ASP.NET Core que emula las funcionalidades básicas de Reddit, incluyendo posteos, usuarios, comentarios, likes, autenticación de usuarios (login y registro), entre otras características.

Paquetes NuGet Utilizados
AutoMapper: AutoMapper facilita la proyección de objetos de un tipo a otro automáticamente, útil para transformar entidades de base de datos en modelos de vista y viceversa.

BCrypt.Net-Next: Librería utilizada para el hashing seguro de contraseñas utilizando el algoritmo BCrypt, asegurando el almacenamiento seguro de las contraseñas de los usuarios.

Microsoft.AspNet.WebApi.Core: Contiene componentes esenciales para la creación de aplicaciones web API en ASP.NET, proporcionando funcionalidad para crear servicios HTTP que pueden ser consumidos por diversos clientes.

Microsoft.AspNetCore.Authentication.JwtBearer: Permite la autenticación basada en tokens JWT (JSON Web Token) en aplicaciones ASP.NET Core, asegurando la autenticación y autorización de usuarios de manera eficiente y segura.

Microsoft.EntityFrameworkCore.SqlServer: Permite a Entity Framework Core trabajar con bases de datos SQL Server, simplificando las operaciones CRUD y la comunicación con la base de datos.

Microsoft.EntityFrameworkCore.Tools: Herramientas para el desarrollo de aplicaciones con Entity Framework Core, incluyendo la generación de migraciones de bases de datos y el mantenimiento del esquema de la base de datos.

Swashbuckle.AspNetCore: Integración de Swagger en aplicaciones ASP.NET Core para documentar y probar la API de manera interactiva, generando una interfaz web que describe todas las operaciones disponibles en la API.

System.IdentityModel.Tokens.Jwt: Funcionalidades para la creación, lectura y validación de tokens JWT en aplicaciones .NET, fundamental para la implementación de autenticación basada en tokens en aplicaciones web y servicios.

Configuración y Uso
Para utilizar este proyecto, asegúrate de tener instalado el SDK de .NET Core. Puedes clonar este repositorio y abrirlo en tu entorno de desarrollo favorito. Asegúrate de configurar correctamente las conexiones a la base de datos y las claves para la generación de tokens JWT en el archivo de configuración (appsettings.json).

A continuación, puedes compilar la solución y ejecutarla. La API estará disponible en la URL base especificada en la configuración. Utiliza herramientas como Postman o la interfaz Swagger generada para probar y explorar las diferentes funcionalidades implementadas.
