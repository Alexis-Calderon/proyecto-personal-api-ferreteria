# API Ferretería Juanito

Esta API está diseñada para ofrecer los servicios de una ferretería.

El administrador podrá gestionar los usuarios del sistema como también actualizar su información de usuario y contraseña, de igual forma podrá gestionar los productos que ofrece y acceder a todas las ventas realizadas.

El cliente podrá actualizar su información de usuario y contraseña, listar los productos con los stocks, gestionar los productos del carrito de compras y generar la compra de estos productos.

## Comenzando

El proyecto está almacenado en el siguiente repositorio
[https://github.com/Alexis-Calderon/ferreteriaJuanito](https://github.com/Alexis-Calderon/ferreteriaJuanito) desde allí se puede descargar una copia.

En el contenido existe una carpeta llamada **Complementos** la cual contiene lo siguientes archivos necesarios para realizar las pruebas:

* **Collection API Ferreteria Juanito.postman_collection.json**
* **Ferreteria Juanito.sql**
* **FerreteriaJuanito.db**


### Pre-requisitos

* **Visual Studio Code**
* **SDK .Net 7**

### Instalación

**Paso N° 1**: Extraer una copia del proyecto desde repositorio en [Github](https://github.com/Alexis-Calderon/ferreteriaJuanito) en el equipo local.

**Paso N° 2**: Crear archivo de base de datos **Sqlite** con el nombre **FerreteriaJuanito**, quedando la ruta de la siguiente manera: 

```
C:\Sqlite\FerreteriaJuanito.db
```

Para crear la base de datos **Sqlite** se pueden ejecutar la secuencia de scripts que están en el archivo **Ferreteria Juanito.sql**, o se puede hacer uso del archivo **FerreteriaJuanito.db** que ya cuenta con la estructura de la base de datos.

### Paquetes instalados

* **Microsoft.EntityFrameworkCore Versión 7.0.0**
* **Microsoft.EntityFrameworkCore.Design Versión 7.0.0**
* **Microsoft.EntityFrameworkCore.Sqlite Versión 7.0.0**
* **Microsoft.EntityFrameworkCore.Tools Versión 7.0.0**
* **Swashbuckle.AspNetCore.SwaggerGen Versión 6.5.0**
* **Swashbuckle.AspNetCore.SwaggerUI Versión 6.5.0**
* **Microsoft.AspNetCore.Authentication.JwtBearer Versión 7.0.0**

### Pruebas

En el archivo **Collection API Ferreteria Juanito.postman_collection.json**, se encuenta una serie de request predefinidos para la ejecución de los métodos creados para la gestión de los datos del sistema, estos métodos deben estar configurados con el tipo de autenticación **"Bearer Token"** utilizando el token generado a través de los request **"Generar Token Administrador JWT"** o **"Generar Token Cliente JWT"** los mismos se generan con un tiempo de expiración de 3 días.