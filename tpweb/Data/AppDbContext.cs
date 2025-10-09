using Microsoft.EntityFrameworkCore;
using tpweb.Modelos.Clase_Escuela;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Escuela> Escuelas => Set<Escuela>();
        public DbSet<Curso> Cursos => Set<Curso>();
        public DbSet<Materia> Materias => Set<Materia>();
        public DbSet<Alumno> Alumnos => Set<Alumno>();
        public DbSet<MateriaAlumno> MateriasAlumnos => Set<MateriaAlumno>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<Asistencia> Asistencias => Set<Asistencia>();
        public DbSet<AsistenciaAlumno> AsistenciasAlumnos => Set<AsistenciaAlumno>();
        public DbSet<Tarea> Tareas => Set<Tarea>();
        public DbSet<TareaAlumno> TareasAlumnos => Set<TareaAlumno>();



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("workstation id=OWDLEGE.mssql.somee.com;packet size=4096;user id=Corchitobj_SQLLogin_1;pwd=i68ptgehkw;data source=OWDLEGE.mssql.somee.com;persist security info=False;initial catalog=OWDLEGE;TrustServerCertificate=True");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación MateriaAlumno (muchos a muchos)
            modelBuilder.Entity<MateriaAlumno>()
                .HasKey(ma => new { ma.MateriaId, ma.AlumnoId });

            modelBuilder.Entity<MateriaAlumno>()
                .HasOne(ma => ma.Materia)
                .WithMany(m => m.MateriaAlumnos)
                .HasForeignKey(ma => ma.MateriaId);

            modelBuilder.Entity<MateriaAlumno>()
                .HasOne(ma => ma.Alumno)
                .WithMany(a => a.MateriaAlumnos)
                .HasForeignKey(ma => ma.AlumnoId);

            // Relación AsistenciaAlumno (muchos a muchos con campo adicional)
            modelBuilder.Entity<AsistenciaAlumno>()
                .HasKey(aa => new { aa.AsistenciaId, aa.AlumnoId });

            modelBuilder.Entity<AsistenciaAlumno>()
                .HasOne(aa => aa.Asistencia)
                .WithMany(a => a.AsistenciasAlumnos)
                .HasForeignKey(aa => aa.AsistenciaId);

            modelBuilder.Entity<AsistenciaAlumno>()
                .HasOne(aa => aa.Alumno)
                .WithMany(a => a.AsistenciasAlumnos)
                .HasForeignKey(aa => aa.AlumnoId);

            // Relación TareaAlumno (muchos a muchos con campo Nota)
            modelBuilder.Entity<TareaAlumno>()
                .HasKey(ta => new { ta.TareaId, ta.AlumnoId });

            modelBuilder.Entity<TareaAlumno>()
                .HasOne(ta => ta.Tarea)
                .WithMany(t => t.TareasAlumnos)
                .HasForeignKey(ta => ta.TareaId);

            modelBuilder.Entity<TareaAlumno>()
                .HasOne(ta => ta.Alumno)
                .WithMany(a => a.TareasAlumnos)
                .HasForeignKey(ta => ta.AlumnoId);

            // Relación Usuario -> Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId);

            // Relación Materia -> Usuario (Docente)
            modelBuilder.Entity<Materia>()
                .HasOne(m => m.Docente)
                .WithMany(d => d.MateriasDictadas)
                .HasForeignKey(m => m.DocenteId);

            // Relación Curso -> Escuela
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Escuela)
                .WithMany(e => e.Cursos)
                .HasForeignKey(c => c.EscuelaId);

            // Relación Materia -> Curso
            modelBuilder.Entity<Materia>()
                .HasOne(m => m.Curso)
                .WithMany(c => c.Materias)
                .HasForeignKey(m => m.CursoId);

            // Relación Asistencia -> Materia
            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Materia)
                .WithMany(m => m.Asistencias)
                .HasForeignKey(a => a.MateriaId);

            // Relación Tarea -> Materia
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Materia)
                .WithMany(m => m.Tareas)
                .HasForeignKey(t => t.MateriaId);


            //DATOS PRECARGADOS
            modelBuilder.Entity<Escuela>().HasData(
              new Escuela { IdEscuela = 1, Nombre = "Inmaculada Concepcion" }
             );

            modelBuilder.Entity<Curso>().HasData(
             new Curso { Id = 1, Nombre = "Primer año", EscuelaId = 1 },
             new Curso { Id = 2, Nombre = "Segundo año", EscuelaId = 1 },
             new Curso { Id = 3, Nombre = "Tercer año", EscuelaId = 1 },
             new Curso { Id = 4, Nombre = "Cuarto año", EscuelaId = 1 },
             new Curso { Id = 5, Nombre = "Quinto año", EscuelaId = 1 }
            );

            modelBuilder.Entity<Rol>().HasData(
              new Rol { IdRol = 1, Nombre = "Administrador" },
              new Rol { IdRol = 2, Nombre = "Docente" },
              new Rol { IdRol = 3, Nombre = "Preceptor" },
              new Rol { IdRol = 4, Nombre = "Alumno" }
              );
        }
    }
}
