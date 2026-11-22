# Catálogo de videojuegos – Práctica 3: acceso a BBDD MySQL

Aplicación WPF (.NET 10) cuyo login valida a los usuarios contra una base de datos MySQL.

## Cómo ejecutarlo
1. Arrancar MySQL (por ejemplo desde el panel de XAMPP).
2. Ejecutar el script `database.sql` como root. Crea la base `catalogo_db`, la tabla `Usuarios`, los usuarios de prueba y el usuario de MySQL `catalogo_app` que usa la aplicación.
   ```
   C:\xampp\mysql\bin\mysql.exe -u root -p < database.sql
   ```
3. Abrir `Catalogo.slnx` en Visual Studio y ejecutar con F5. El paquete NuGet `MySql.Data` se restaura solo.

Usuarios de prueba: `admin / 1234`, `jugador1 / pass2024`, `profesor / dam123`.

## Estructura
- `Catalogo/DatabaseHelper.cs`: cadena de conexión, apertura y cierre de la conexión, y consultas parametrizadas.
- `Catalogo/Login.xaml(.cs)`: login contra la tabla `Usuarios`, botón «Nuevo usuario» y control de errores de conexión.
- `database.sql`: script de creación de la base de datos.
- `Evidencias/` y `Evidencias_Practica3.pdf`: capturas y documento de evidencias del Plan de Pruebas (TC 01 – TC 09).
