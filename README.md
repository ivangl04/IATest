# IATestAPI

# Funciones

  * El usuario puede enviar un solicitud de ingreso.
  * El usuario puede actualizar una solicitud de ingreso existente.
  * EL usuario puede actualizar el estado de un solicitud existente.
  * El usuario puede consultar todas las solicitudes registradas en la base de datos con estatus de aprobadas y rechazadas.
  * El usuario puede consultar la asignacion de Grimario de la solicitud de ingreso.
  * EL usuario puede eliminar una solicitud de ingreso creada previamente.

# Requisitos 

  * .NET Core 6
  
  * Instancia local de MySQL

# Guia de Instalacion
  
   1 Configure la conexion de la base de datos, reemplace la informacion de user id, y password por los datos de su conexion local de MYSQL esto en el archivo de appsettings.json.

   2 Click derecho en  en el boton play o run IATestApi para compilar y ejecutar el proyecto, la migracion creará de forma automatica la base de datos en la primera compilacion del proyecto.

   3 Conectese a la API usando SWAGGER(Al ejecutarse el proyecto aparece de manera automatica) o POSTMAN en el puerto 
  
    ```
    https://localhost:7294.
    ```

# Endpoints

| HTTP Verbs | Endpoints | Acciones |
| --- | --- | --- |
| POST | /api/solicitud | Registra una nueva solicitud |
| PUT | /api/solicitud  | Actualizar una solicitud existente |
| GET | /api/solicitud | Consultar todas las solicitudes creadas |
| GET | /api/solicitud/grimorio/:Id | Consultar el grimorio asignado a una solicitud existente |
| PATCH | /api/solicitud/:Id | Actualizar el estado de una solicitud existente |
| DELETE | /api/solicitud/:Id | Eliminar una solicitud existente |

# Autor 
  
  IVAN ROGER GARRIDO LUGO
