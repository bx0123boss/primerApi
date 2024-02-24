using Microsoft.EntityFrameworkCore;
using apiPlatzi.Models;

namespace apiPlatzi
{
    public class TareasContext:DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fluent api
            List<Categoria> categoriasInit = new List<Categoria>();
            categoriasInit.Add(new Categoria { CategoriaID = Guid.Parse("c57d0688-ee45-4013-ab26-01c93f60eb50"), Nombre = "Actidades Pendientes",  Peso = 20 });
            categoriasInit.Add(new Categoria { CategoriaID = Guid.Parse("60da01d0-5ca2-4be7-adac-758953ebcab2"), Nombre = "Actidades Personales",  Peso = 50 });
            modelBuilder.Entity<Categoria>(categoria =>
            {
                //Nombre tabla
                categoria.ToTable("Categoria");
                //Llave
                categoria.HasKey(p => p.CategoriaID);
                //campo y restrincción
                categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
                categoria.Property(p => p.Descripcion).IsRequired(false);
                categoria.Property(p => p.Peso);
                //Datos initial
                categoria.HasData(categoriasInit);
            });

            List<Tarea> tareasInit = new List<Tarea>();
            tareasInit.Add(new Tarea { TareaId = Guid.Parse("c57d0688-ee45-4013-ab26-01c93f60eb10"), CategoriaId = Guid.Parse("c57d0688-ee45-4013-ab26-01c93f60eb50"), PrioridadTarea = Prioridad.Media, Titulo = "Pago de servicios publicos", FechaCreacion = DateTime.Now, Fecha = DateTime.Parse("2021/10/12 12:00:00") });
            tareasInit.Add(new Tarea { TareaId = Guid.Parse("c57d0688-ee45-4013-ab26-01c93f60eb11"), CategoriaId = Guid.Parse("60da01d0-5ca2-4be7-adac-758953ebcab2"), PrioridadTarea = Prioridad.Baja, Titulo = "Estudiar C#", FechaCreacion = DateTime.Now, Fecha = DateTime.Parse("2021/10/12 12:00:00") });
            modelBuilder.Entity<Tarea>(tarea =>
            {
                tarea.ToTable("Tarea");
                tarea.HasKey(p => p.TareaId);
                
                //funciona para indicar una llave foranea y la relación entra ambos modelos
                tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
                
                tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
                tarea.Property(p => p.Desrcripcion).IsRequired(false);
                tarea.Property(p => p.PrioridadTarea);
                tarea.Property(p => p.FechaCreacion);
                tarea.Ignore(p => p.Resumen);
                tarea.Property(p => p.Fecha);
                tarea.HasData(tareasInit);
            });
        }
    }
}
