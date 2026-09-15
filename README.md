# TP WinForm - Equipo B

## Descripción

Aplicación de escritorio desarrollada en C# con Windows Forms para la gestión de artículos de un catálogo comercial. Este proyecto corresponde al Trabajo Práctico de la materia **Programación 3**. La aplicación es genérica y está diseñada para administrar el inventario de cualquier tipo de comercio.

## Funcionalidades

* **Listado de artículos:** Visualización completa del catálogo disponible.

* **Búsqueda y filtrado:** Búsqueda de artículos mediante distintos criterios.

* **ABM de Artículos:** Agregar, modificar y eliminar artículos del sistema.

* **Detalle de Artículo:** Vista ampliada con toda la información del producto.

* **Gestión de Marcas y Categorías:** Administración de las opciones disponibles en el sistema.

* **Galería de Imágenes:** Soporte para múltiples imágenes asociadas a cada artículo.

## Tecnologías Utilizadas

* **Lenguaje:** C#

* **Framework:** .NET (Windows Forms)

* **Base de Datos:** SQL Server

* **Acceso a Datos:** ADO.NET

## Instalación y Configuración Base de Datos

Para poder ejecutar el proyecto correctamente de forma local, sigue estos pasos:

1. Clonar el repositorio:

   ```
   git clone https://github.com/Santiago-schmidt/tp-winform-equipo-b.git
   
   ```

2. **Base de Datos:**

   * Abrir SQL Server Management Studio (SSMS).

   * Localizar el archivo `.sql` (ubicado en la carpeta del repositorio) que contiene el esquema y los datos.

   * Ejecutar el script para recrear la base de datos `CATALOGO_P3_DB` en el servidor local.

3. **Ejecución:**

   * Abrir la solución `TPWinForm_equipo-b.sln` con Visual Studio.

   * Si tu instancia de SQL Server no es la predeterminada (por ejemplo, si usas `.\SQLEXPRESS`), asegúrate de revisar la clase `Conexion.cs` o el archivo de configuración para que la cadena de conexión coincida con tu entorno local.

   * Compilar y ejecutar (F5).

## Integrantes

* Lewandowski, Marcos

* Schmidt, Santiago 

* Wachenschwan, Federico