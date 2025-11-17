TP PP III – Integración de Programación  
Profesor: Victor Cordero  
Integrantes: Ignacio Maciel, Gianluca Cejas, Santiago Tropea 

---

DESCRIPCIÓN DEL PROYECTO

Este proyecto es una plataforma de gestión académica que permite registrar usuarios, administrar materias, cursos, tareas y asistencias, con funcionalidades diferenciadas según el tipo de usuario: Administrador, Docente, Alumno y Preceptor.

---

TECNOLOGÍAS UTILIZADAS

Este proyecto fue desarrollado utilizando:
- .NET 8 – Framework principal para construir aplicaciones web con alto rendimiento y escalabilidad.
- Razor Pages – Modelo de desarrollo basado en páginas, que facilita la separación entre lógica y presentación.
- Entity Framework Core – ORM utilizado para mapear las entidades del sistema a la base de datos relacional.
- SQL Server Management Studio 2021 – Herramienta para administrar y consultar la base de datos del proyecto.
- Bootstrap 5 – Framework de diseño responsivo para mejorar la experiencia visual y la usabilidad.
- Somee – Servicio de hosting gratuito donde se aloja la base de datos en la nube.

---

PASOS DE INSTALACIÓN
Para ejecutar el proyecto correctamente en tu entorno local, seguí estos pasos:

1) Clonar el repositorio
- Usá Git para clonar el repositorio en tu máquina local.

2) Conectar con la base de datos
- Abrí SQL Server Management Studio 2021.
- Ingresá las credenciales de acceso a la base de datos alojada en Somee.
- Buscá la base de datos llamada Owledge y verificá que esté activa.

3) Ejecutar el proyecto
- Abrí Visual Studio 2022.
- Cargá la solución clonada (tpweb.sln).
- Ejecutá la solución para iniciar la aplicación.

---

ESTRUCTURA DEL PROYECTO

Solución "tpweb"
├── tpweb
│   ├── wwwroot
│   │   Archivos estáticos (CSS, JS, imágenes, íconos)
│
│   ├── Data
│   │   AppDbContext.cs – Configuración de la base de datos y relaciones entre entidades
│
│   ├── Migrations
│   │   Migraciones de Entity Framework para crear y actualizar el esquema de la base de datos
│
│   ├── Modelos
│   │   ├── Clase_Escuela – Entidades académicas: Escuela, Curso, Materia, Tarea, Asistencia
│   │   └── Clase_Persona – Entidades personales: Alumno, Usuario, Rol, relaciones con materias y tareas
│
│   ├── Pages
│   │   ├── Alumnos – Vistas para gestión de alumnos y sus tareas/asistencias
│   │   ├── Asistencias – Registro y edición de asistencias por materia
│   │   ├── Cursos – Listado y vinculación de cursos con alumnos y materias
│   │   ├── Materias – Gestión de materias, alumnos por materia y tareas
│   │   ├── Tareas – Creación, edición y corrección de tareas por parte del docente
│   │   ├── Usuarios – Gestión de usuarios (docentes, preceptores, administradores)
│   │   ├── Shared – Layout general y scripts comunes
│   │   ├── Formularios, Login, Logout, Registrarse – Páginas de autenticación y navegación general
│   │   └── Index, Error, Privacy – Páginas base del proyecto
│
│   ├── appsettings.json / appsettings.Development.json
│   │   Configuración de conexión a la base de datos y parámetros del entorno
│
│   └── Program.cs
│       Punto de entrada del proyecto, configuración inicial de la aplicación

---

CASOS DE PRUEBA

Página principal (Index)
<img src="img/capturas/Index.png" alt="" width="600"/>

# Registro

- Desde el botón "Registrarse" en el index, se accede a un formulario para crear una nueva cuenta.
<img src="img/capturas/registrarse.png" alt="" width="600"/>

- Todos los campos son obligatorios.
<img src="img/capturas/registrarse_campos_requeridos.png" alt="" width="600"/>

- Solo se permite registrar usuarios de tipo Docente, Alumno o Preceptor. No se puede registrar un Administrador.

---

# Administrador

## Credenciales
- **Usuario:** vcordero  
- **Contraseña:** 123

## Login
- Verifica las credenciales en la base de datos.
<img src="img/capturas/ventana_login.png" alt="" width="600"/>

- Si son válidas, redirige al index y muestra el usuario y rol en la barra de navegación personalizada.
<img src="img/capturas/login_ok_admin.png" alt="" width="600"/>

- Si son inválidas, muestra un mensaje de error y deniega el acceso.
<img src="img/capturas/login_error.png" alt="" width="600"/>

## Sección Alumnos
<img src="img/capturas/admin_seccion_alumnos.png" alt="" width="600"/>

- **Registrar alumno:** 

  Accede a un formulario para dar de alta un nuevo alumno.
  <img src="img/capturas/admin_alta_alumnos.png" alt="" width="600"/>

  Todos los campos son obligatorios.
  <img src="img/capturas/admin_alta_alumnos_campos_requeridos.png" alt="" width="600"/>

  Al guardar, se redirige al listado y se muestra el nuevo registro.
  <img src="img/capturas/admin_alta_alumnos_ok.png" alt="" width="600"/>

- **Buscar alumno:**  
  Permite buscar por nombre y filtrar por curso.
  <img src="img/capturas/admin_filtro_alumnos.png" alt="" width="600"/>

- **Acciones disponibles:**  
  
  ***Editar*** 
  Redirige al formulario con los datos precargados. 
  <img src="img/capturas/admin_editar_alumnos.png" alt="" width="600"/>

  Al guardar, vuelve al listado con los cambios aplicados.
  <img src="img/capturas/admin_editar_alumnos_ok.png" alt="" width="600"/>

  ***Detalles*** 
  Muestra la información del alumno con botones para editar o volver al listado.
  <img src="img/capturas/admin_detalles_alumnos.png" alt="" width="600"/>

  ***Eliminar*** 
  Muestra los detalles del alumno con opción de confirmar la eliminación o volver.
  <img src="img/capturas/admin_eliminar_alumnos.png" alt="" width="600"/>

  ***Asistencias*** 
  Muestra las asistencias del alumno. Permite modificar el estado (presente/ausente) mediante checkbox y guardar los cambios.
  <img src="img/capturas/admin_asistencias_alumnos.png" alt="" width="600"/>

## Sección Usuarios
<img src="img/capturas/admin_seccion_usuarios.png" alt="" width="600"/>

- Similar a la sección Alumnos, pero incluye:
  - Listado de todos los usuarios (excepto alumnos).
  - Alta de nuevos usuarios (docente, preceptor, administrador).
  - Filtro por tipo de rol.
  - Acciones: Editar, Detalles, Eliminar.

## Sección Materias
<img src="img/capturas/admin_seccion_materias.png" alt="" width="600"/>

- Listado de materias.
- Alta de nueva materia.
- Filtro por curso.
- Acciones: Editar, Detalles, Eliminar.

## Sección Cursos
<img src="img/capturas/admin_seccion_cursos.png" alt="" width="600"/>

- Listado de todos los cursos.
- **Ver alumnos:** Muestra los alumnos vinculados al curso.
- **Ver materias:** Muestra las materias del curso con su respectivo docente.
- **Vincular alumno:** Permite vincular alumnos no asignados a otro curso mediante un desplegable.

## Logout

- Al cerrar sesión, se redirige al index y se muestra la barra de navegación con los botones "Login" y "Registrarse".
<img src="img/capturas/Index.png" alt="" width="600"/>

---

# Docente

## Credenciales
- **Usuario:** gcejas  
- **Contraseña:** 123

## Login
- Igual que el administrador: redirige al index y personaliza la barra de navegación.
<img src="img/capturas/login_docente_ok.png" alt="" width="600"/>

## Sección Materias
<img src="img/capturas/docente_seccion_materias.png" alt="" width="600"/>

- Listado de materias asignadas al docente.

### Ver alumnos
<img src="img/capturas/docente_alumnos_materia.png" alt="" width="600"/>
- Muestra los alumnos de la materia.
- Permite buscar por nombre.
- Acciones disponibles:
  - **Tareas:** Muestra las tareas del alumno y el promedio general.
  - **Asistencias:** Muestra el historial y permite modificar el estado.

### Ver asistencias
<img src="img/capturas/docente_alumnos_asistencia.png" alt="" width="600"/>
- Al hacer clic en "Asistencia", se abre un menú modal con:
  - **Tomar asistencia:** Redirige a la página para registrar la asistencia del día.
  - **Historial:** Muestra todas las asistencias de la materia con opciones de editar y ver detalles.
  - **Cancelar:** Cierra el modal y vuelve al listado.

### Ver tareas
<img src="img/capturas/docente_alumnos_tareas_materia.png" alt="" width="600"/>
- Al hacer clic en "Tareas", se abre un menú modal con:
  - **Crear tarea:** Redirige al formulario de alta. Todos los campos son obligatorios.
  - **Ver tareas:** Muestra todas las tareas creadas por el docente con opciones:
    - **Editar:** Redirige al formulario con los datos precargados.
    - **Archivar/Desarchivar:** Muestra un alert de confirmación.
    - **Corregir:** Muestra el estado de entrega, nota y permite calificar o editar la nota.

  - **Cancelar:** Cierra el modal y vuelve al listado.

## Logout

- Igual que el administrador.

---

# Alumno

## Credenciales
- **Usuario:** gcejas  
- **Contraseña:** 123

## Login
- Igual que los demás roles.
<img src="img/capturas/login_alumno_ok.png" alt="" width="600"/>

## Sección Mis Asistencias
<img src="img/capturas/alumno_seccion_asistencias.png" alt="" width="600"/>

- Muestra todas las asistencias del alumno.
- Permite filtrar por materia mediante un menú desplegable.

## Sección Mis Materias
<img src="img/capturas/alumno_seccion_materias.png" alt="" width="600"/>

- Muestra todas las materias del alumno.
- Permite acceder a las asistencias y tareas de cada materia.

## Sección Mis Tareas
<img src="img/capturas/alumno_seccion_tareas.png" alt="" width="600"/>

- Muestra todas las tareas asignadas al alumno.
- Permite filtrar por materia.

## Logout

- Igual que los demás roles.

---

# Preceptor

## Credenciales
- **Usuario:** fromano  
- **Contraseña:** 123

## Login
- Igual que los demás roles.

## Funcionalidades

- El rol de Preceptor combina funciones de administrador y docente.
- **Implementado:** Secciones de Alumnos, Usuarios y Cursos.
- **No implementado:** Secciones de Asistencias, Materias y Tareas.

## Logout

- Igual que los demás roles.

---
