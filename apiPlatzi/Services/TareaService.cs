
using apiPlatzi.Models;
using apiPlatzi;

namespace apiPlatzi.Services;

public class TareaService : ITareaService
{
    TareasContext context;
    public TareaService(TareasContext dbcontext)
    {
        context = dbcontext;
    }
    public IEnumerable<Tarea> Get()
    {
        return context.Tareas;
    }

    public async Task Save(Tarea tarea)
    {
        context.Add(tarea);
        await context.SaveChangesAsync();
    }

    public async Task Update(Guid id, Tarea tarea)
    {
        var tareaActual = context.Tareas.Find(id);
        if (tareaActual != null)
        {
            tareaActual.Titulo = tarea.Titulo;
            tareaActual.Desrcripcion = tarea.Desrcripcion;
            tareaActual.FechaCreacion = tarea.FechaCreacion;
            tareaActual.PrioridadTarea = tarea.PrioridadTarea;
            tareaActual.Fecha = tarea.Fecha;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id)
    {
        var tareaActual = context.Tareas.Find(id);
        if (tareaActual != null)
        {
            context.Remove(tareaActual);
            await context.SaveChangesAsync();
        }

    }
}

public interface ITareaService
{
    IEnumerable<Tarea> Get();
    Task Save(Tarea tarea);
    Task Update(Guid id, Tarea tarea);
    Task Delete(Guid id);

}